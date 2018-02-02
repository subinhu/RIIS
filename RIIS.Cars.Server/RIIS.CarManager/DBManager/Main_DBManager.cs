using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RIIS.CarManager
{
    public class Main_DBManager
    {
        public static DataSet DBSet;
        public const string QueueName = "数据处理队列"; 
        public Main_DBManager()
        {
            //加载数据库数据


            RIIS.MsgQueue.MsgReceiver rcv = RIIS.MsgQueue.MsgQueue.RegisterReceiver(QueueName);   
            rcv.MessageArrived += new RIIS.MsgQueue.MsgReceiver.MessageArrivedEvent(rcv_MessageArrived);
            rcv.Interval = 10000;
            rcv.Tick += new RIIS.MsgQueue.MsgReceiver.TimerEvent(rcv_Tick);
            rcv.Start();
        }

        /// <summary>
        /// 用于处理"数据处理队列"中的数据
        /// </summary>
        /// <param name="host">接收者</param>
        /// <param name="sender">发送者</param>
        /// <param name="msg">消息内容</param>
        /// <param name="bRequest">是否要求有返回值</param>
        /// <returns>返回值</returns>
        object rcv_MessageArrived(RIIS.MsgQueue.MsgReceiver host, RIIS.MsgQueue.MsgSender sender, object msg, bool bRequest)
        {


            return null;
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="host"></param>
        void rcv_Tick(RIIS.MsgQueue.MsgReceiver host)
        {
           
        }


        /// <summary>
        /// 从数据库加载数据到DBSet
        /// </summary>
        /// <param name="tables">加载的表的名称列表</param>
        public void LoadDataFromDB(List<string> tables)
        {

        }



        



        
    }
}
