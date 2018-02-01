using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetRouterClient;
using TransMsgByPkgsHelper;

namespace SendMsgDemo
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


            #region 加上分包协议之后的发送
            TransMsgByPkgs transfer = new TransMsgByPkgs();

            //发送
            if (transfer.sendMsgByPackages(byteArray, netAddrList, client))
                Console.WriteLine("发送成功！");

            #endregion


            Console.ReadLine();
        }

    }
}
