using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RIIS.MsgQueue
{
    public class MsgQueue
    {
        static Mutex sMutex = new Mutex();
        static List<MsgReceiver> Receivers = new List<MsgReceiver>();
        static List<MsgSender> Senders = new List<MsgSender>();
        internal const int msgtype_Send = 0;
        internal const int msgtype_Request = 1;
        
        
        
        /// <summary>
        /// 跨线程消息处理
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate object MessageProc(object msg);
        /// <summary>
        /// 远程消息队列自定义反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public delegate object DeserializeProc(byte[] data);
        /// <summary>
        /// 远程消息队列自定义序列化
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate byte[] SerializeProc(object msg);

        /// <summary>
        /// 获取接收端状态
        /// </summary>
        /// <param name="queuename">队列名称</param>
        /// <returns>-1:接收端不存在, 0:初始化, 1:等待消息, 2:正在处理消息</returns>
        public static int GetReceiverStatus(string queuename)
        {
            int status = -1;
            queuename = queuename.Trim().ToUpper();
            sMutex.WaitOne();
            foreach (MsgReceiver m in Receivers)
            {
                if (m.Name == queuename)
                {
                    status = (int)m.State;
                    break;
                }
            }
            sMutex.ReleaseMutex();
            return status;
        }


        bool CheckThreadConflict()
        {
            return false;
        }

        internal static void GetAllItems(List<MsgReceiver> rcvs, List<MsgSender> snds)
        {
            rcvs.Clear();
            snds.Clear();
            sMutex.WaitOne();
            foreach (MsgReceiver m in Receivers)
                rcvs.Add(m);
            foreach (MsgSender m in Senders)
                snds.Add(m);
            sMutex.ReleaseMutex();
        }
        /// <summary>
        /// 注册本地消息接收端
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MsgReceiver RegisterReceiver(string name)
        {
            if (name == null)
                return null;
            name = name.ToUpper().Trim();
            if (name == "")
                return null;
            sMutex.WaitOne();
            MsgReceiver r = _RegisterReceiver(name, 2);
            if (r != null)
                r.bRegistered = true;
            sMutex.ReleaseMutex();
            return r;
        }

        static MsgReceiver _RegisterReceiver(string name, int remotemode)
        {
            MsgReceiver r = null;
            foreach (MsgReceiver mr in Receivers)
            {
                if (mr.Name == name)
                {
                    if (mr.bRegistered)
                    {
                        return null;
                    }
                    r = mr;
                    break;
                }
            }
            if (r == null)
            {
                r = new MsgReceiver(name);
                Receivers.Add(r);
            }
            return r;
        }

        /// <summary>
        /// 获取临时本地消息发送端
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MsgSender GetTmpSender(string name)
        {
            return GetSender(name, false);
        }
        /// <summary>
        /// 获取本地消息发送端
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MsgSender GetSender(string name)
        {
            return GetSender(name, true);
        }
        static MsgSender GetSender(string name, bool save)
        {
            if (name == null)
                return null;
            name = name.ToUpper().Trim();
            sMutex.WaitOne();
            MsgSender sender = null;
            foreach (MsgReceiver ms in Receivers)
            {
                if (ms.Name == name)
                {
                    sender = new MsgSender(ms);
                    break;
                }
            }
            if (sender == null)
                sender = new MsgSender(_RegisterReceiver(name, 0));
            if (save)
                Senders.Add(sender);
            sMutex.ReleaseMutex();
            return sender;
        }
    }
}
