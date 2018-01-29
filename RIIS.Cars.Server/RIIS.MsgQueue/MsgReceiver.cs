using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RIIS.MsgQueue
{
    public class MsgReceiver
    {
        /// <summary>
        /// 消息到达事件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="bRequest"></param>
        /// <returns></returns>
        public delegate object MessageArrivedEvent(MsgReceiver host, MsgSender sender, object msg, bool bRequest);//声明委托
        /// <summary>
        /// 消息到达事件
        /// </summary>
        public event MessageArrivedEvent MessageArrived;//定义委托事件
        /// <summary>
        /// 定时任务事件
        /// </summary>
        /// <param name="host"></param>
        public delegate void TimerEvent(MsgReceiver host);
        /// <summary>
        /// 定时任务事件
        /// </summary>
        public event TimerEvent Tick;
        internal string Name = null;
        /// <summary>
        /// 定时任务时间跨度(ms)
        /// </summary>
        public int Interval = -1;
        /// <summary>
        /// 定时任务优先级
        /// -1为最低优先级，0为最高优先级
        /// </summary>
        public int PriorityOfTick = -1;
        internal DateTime CreateTime;
        internal List<OneQueue> Queues = new List<OneQueue>();
        ManualResetEvent[] evts;
        internal bool bRegistered = false;
        internal enum QueueState
        {
            Init,
            Waiting,
            Running
        }
        internal QueueState State = QueueState.Init;
        internal int CurrentQueue = 0;

        internal MsgReceiver(string name)
        {
            Name = name;
            QueueCount = 1;
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 一个单独的线程用于不停地遍历，接收信号
        /// </summary>
        void WaitThread()
        {
            DateTime lasttime = DateTime.Now;
            while (true)
            {
                State = QueueState.Waiting;
                int index = ManualResetEvent.WaitAny(evts, Interval, false);
                MessageArrivedEvent tmp = MessageArrived;
                State = QueueState.Running;
                bool senttick = false;
                for (int i = 0; i < Queues.Count; i++)
                {
                    CurrentQueue = i;
                    if (PriorityOfTick == i)
                    {
                        senttick = true;
                        SendTickEvent(ref lasttime);
                    }
                    OneQueue q = Queues[i];
                    while (true)
                    {
                        OneQueue.Message m = q.PopData();
                        if (m == null)
                            break;
                        object ret = null;
                        if (tmp != null)
                        {
                            bool req = m.type == MsgQueue.msgtype_Request;
                            ret = tmp(this, m.sender, m.Data, req);

                        }
                        m.sender.MessageFinished(m, ret);
                    }
                }
                if (!senttick)
                    SendTickEvent(ref lasttime);
            }
        }
        /// <summary>
        /// 按照一定时间间隔触发定时事件
        /// </summary>
        /// <param name="lasttime"></param>
        void SendTickEvent(ref DateTime lasttime)
        {
            if (Interval > 0 && lasttime.AddMilliseconds(Interval) <= DateTime.Now)
            {
                lasttime = DateTime.Now;
                TimerEvent te = Tick;
                if (te != null)
                    te(this);
            }
        }
        Thread runningThread = null;
        /// <summary>
        /// 启动接收
        /// </summary>
        public void Start()
        {
            if (runningThread != null)
                return;
            Thread thread = new Thread(new ThreadStart(WaitThread));
            thread.IsBackground = true;
            thread.Name = "MsgReceiver_" + Name;
            thread.Start();
            runningThread = thread;
        }

        /// <summary>
        /// 设置队列深度
        /// </summary>
        /// <param name="priority">优先级</param>
        /// <param name="size">深度</param>
        /// <returns>成功</returns>
        public bool SetBufferSize(int priority, int size)
        {
            if (Running)
                return false;
            if (priority >= 0 && priority < Queues.Count)
                Queues[priority].BufferSize = size;
            return true;
        }
        /// <summary>
        /// 获取及设置队列数
        /// </summary>
        public int QueueCount
        {
            get
            {
                return Queues.Count;
            }
            set
            {
                if (Running || value < 1)
                    return;
                List<OneQueue> qs = new List<OneQueue>();
                ManualResetEvent[] es = new ManualResetEvent[value];
                for (int i = 0; i < value; i++)
                {
                    OneQueue q;
                    if (Queues.Count > i)
                        q = Queues[i];
                    else
                        q = new OneQueue();
                    es[i] = q.MessageArrivedEvent;
                    qs.Add(q);
                }
                evts = es;
                Queues = qs;
            }
        }
        /// <summary>
        /// 队列中消息数
        /// </summary>
        public int MsgCount
        {
            get
            {
                int n = 0;
                foreach (OneQueue o in Queues)
                    n += o.Count;
                return n;
            }
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        internal bool Running
        {
            get
            {
                return runningThread != null;// || CreateTime.AddMinutes(1) > DateTime.Now;
            }
        }
    }
}
