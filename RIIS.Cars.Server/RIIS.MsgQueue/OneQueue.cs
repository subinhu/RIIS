using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RIIS.MsgQueue
{
    class OneQueue
    {
        public class Message
        {
            public int type = 0;
            public object Data = null;
            public MsgSender sender = null;
        }
        /// <summary>
        /// 队列
        /// </summary>
        protected System.Collections.Queue cmdq = new System.Collections.Queue();
        /// <summary>
        /// 安全信号
        /// </summary>
        protected System.Threading.Mutex mutex = new System.Threading.Mutex();
        /// <summary>
        /// 队列事件
        /// </summary>
        protected System.Threading.ManualResetEvent evt = new System.Threading.ManualResetEvent(false);
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        internal int BufferSize = -1;
        internal int FinishedCount_Push = 0, FinishedCount_Request = 0, FinishedCount_Proc = 0, LostCount = 0;//崔恩著：不理解缓冲区的作用
        /// <summary>
        /// 队列中存在消息时此事件开放
        /// </summary>
        public System.Threading.ManualResetEvent MessageArrivedEvent
        {
            get
            {
                return evt;
            }
        }
        /// <summary>
        /// 添加一个消息
        /// </summary>
        public void PushData(Message ci)
        {
            mutex.WaitOne();
            if (BufferSize > 0 && BufferSize <= cmdq.Count)
            {
                Message c = cmdq.Peek() as Message;
                if (c != null && c.type == MsgQueue.msgtype_Send)
                {
                    LostCount++;
                    cmdq.Dequeue();
                }
            }
            cmdq.Enqueue(ci);
            evt.Set();
            mutex.ReleaseMutex();
        }
        /// <summary>
        /// 取出一个消息
        /// </summary>
        public Message PopData()
        {
            Message ci;
            mutex.WaitOne();
            if (cmdq.Count > 0)
                ci = cmdq.Dequeue() as Message;
            else
                ci = null;
            if (cmdq.Count == 0)
                evt.Reset();
            mutex.ReleaseMutex();
            return ci;
        }
        /// <summary>
        /// 清除队列
        /// </summary>
        public void Clear()
        {
            mutex.WaitOne();
            cmdq.Clear();
            mutex.ReleaseMutex();
        }
        /// <summary>
        /// 队列中对象数量
        /// </summary>
        public int Count
        {
            get
            {
                mutex.WaitOne();
                int n = cmdq.Count;
                mutex.ReleaseMutex();
                return n;
            }
        }
    }
}
