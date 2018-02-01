using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetRouterClient;
using TransMsgByPkgsHelper;

namespace RsvMsgDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            INetRouterClient client = NetRouterClientFactory.CreateNetRouterClient("Test");

            //NETADDR addr = new NETADDR(8, 1, 0, 1, 3);
            //INetRouterClient client = NetRouterClientFactory.CreateNetRouterClient("Test", "172.168.0.1", 9003, "172.168.0.1", 9005, ref addr, "");

            List<NETADDR> netAddrList = new List<NETADDR>();
            netAddrList.Add(new NETADDR(8, 1, 0, 1, 3));
            StringBuilder sssb = new StringBuilder();
            for (int i = 0; i < 80; i++)
            {
                sssb.Append("AAABB");
            }
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(sssb.ToString());

            client.start();


            #region 加上分包协议之后的接收
            TransMsgByPkgs transfer = new TransMsgByPkgs();

            //接收
            REVMSGBYPACKAGES rsvMsg = new REVMSGBYPACKAGES();
            while (true)
            {
                while (transfer.receiveMsgByPackages(ref rsvMsg, client))
                {
                    string revmsgTemp = rsvMsg.msg;

                    Console.WriteLine(revmsgTemp.Length); //测试数据较大时，这里只显示组包后解析的字符串长度
                    Console.WriteLine(revmsgTemp); //当测试信息数据比较短时，可以将信息显示出来
                }
            }
            #endregion


            Console.ReadLine();
        }

    }
}
