using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RIIS.MsgQueue
{
    public class MsgSender
    {
        class DataSent
        {
            public OneQueue.Message msg;
            public object retData = null;
            public AutoResetEvent evt = null;
            public DataSent(OneQueue.Message _msg)
            {
                msg = _msg;
                evt = new AutoResetEvent(false);
            }
            public void DataArrived(object data)
            {
                retData = data;
                if (evt != null)
                    evt.Set();
            }
        }
        Mutex mutex = new Mutex();
        List<DataSent> sentMsgs = new List<DataSent>();
        internal MsgReceiver Receiver = null;
        internal Thread requestThread = null;
        internal enum SenderState
        {
            Idle,
            Waiting,
            Busy
        }
        internal SenderState State = SenderState.Idle;
        string myName = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                if (myName == "")
                    return AskClassName;
                else
                    return myName;
            }
            set
            {
                myName = value;
                if (myName == null)
                    myName = "";
            }
        }
        /// <summary>
        /// 接收端名称
        /// </summary>
        public string receiverName
        {
            get
            {
                return Receiver.Name;
            }
        }
        string AskClassName = "";
        internal MsgSender(MsgReceiver receiver)
        {
            Receiver = receiver;
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] fs = st.GetFrames();
            foreach (System.Diagnostics.StackFrame f in fs)
            {
                Type tp = f.GetMethod().DeclaringType;
                if (tp == typeof(MsgSender) || tp == typeof(MsgQueue))
                    continue;
                AskClassName = "{" + tp.FullName + "}";
                break;
                //if (st.FrameCount > 1)
                //    dllname = st.GetFrame(1).GetMethod().DeclaringType.Namespace.ToUpper();
            }

        }
        /// <summary>
        /// 添加一个消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="priority">0:最高优先级，-1:最低优先级</param>
        public void PushData(object data, int priority)
        {
            PushData(MsgQueue.msgtype_Send, data, priority, null);
        }
        /// <summary>
        /// 发送消息并请求响应
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="priority">0:最高优先级，-1:最低优先级</param>
        /// <returns></returns>
        public object RequestData(object data, int priority)
        {
            State = SenderState.Waiting;
            DataSent d = PushData(MsgQueue.msgtype_Request, data, priority, null);
            requestThread = Thread.CurrentThread;
            d.evt.WaitOne();
            requestThread = null;
            State = SenderState.Idle;
            return d.retData;
        }
        /// <summary>
        /// 发送消息并在本地处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="priority">0:最高优先级，-1:最低优先级</param>
        /// <param name="proc">调用方法</param>
        /// <returns></returns>
        public object ProcessData(object data, int priority, MsgQueue.MessageProc proc)
        {
            State = SenderState.Busy;
            DataSent d = PushData(MsgQueue.msgtype_Request, data, priority, proc);
            requestThread = Thread.CurrentThread;
            d.evt.WaitOne();
            requestThread = null;
            State = SenderState.Idle;
            return d.retData;
        }
        DataSent PushData(int type, object data, int priority, MsgQueue.MessageProc proc)
        {
            List<OneQueue> qs = Receiver.Queues;
            if (priority < 0 || priority >= qs.Count)
                priority = qs.Count - 1;
            object o = data;


            OneQueue.Message msg = new OneQueue.Message();
            msg.sender = this;
            msg.Data = o;
            msg.type = type;
            mutex.WaitOne();
            qs[priority].PushData(msg);
            DataSent d = null;
            if (type == MsgQueue.msgtype_Request)
            {
                d = new DataSent(msg);
                sentMsgs.Add(d);
            }
            mutex.ReleaseMutex();
            return d;
        }
        internal void MessageFinished(OneQueue.Message msg, object ret)
        {
            mutex.WaitOne();
            foreach (DataSent d in sentMsgs)
            {
                if (d.msg == msg)
                {
                    sentMsgs.Remove(d);
                    d.DataArrived(ret);
                    break;
                }
            }
            mutex.ReleaseMutex();
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool Running
        {
            get
            {
                return Receiver.Running;
            }
        }
    }
}
