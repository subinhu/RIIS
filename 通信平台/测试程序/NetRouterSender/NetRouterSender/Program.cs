using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetRouterClient;
using System.Threading;

namespace NetRouterSender
{
    class Program
    {
        static void Main(string[] args)
        {
            NETADDR localaddr = new NETADDR();
            localaddr.bureauCode = 8;
            localaddr.unitType = 2;
            localaddr.unitId = 0;
            localaddr.devType = 1;
            localaddr.devId = 7;

             NETADDR remote1 = new NETADDR();
            remote1.bureauCode = 8;
            remote1.unitType = 2;
            remote1.unitId = 0;
            remote1.devType = 1;
            remote1.devId = 5;


            NETADDR remote2 = new NETADDR();
            remote2.bureauCode = 8;
            remote2.unitType = 2;
            remote2.unitId = 0;
            remote2.devType = 1;
            remote2.devId = 6;

            Console.WriteLine("Sender!!!!!!");
            INetRouterClient netRouterClient = NetRouterClientFactory.CreateNetRouterClient("Test", "172.168.0.1", 9003, "172.168.0.1", 9005, ref localaddr, "");
            while (!netRouterClient.start())
            {
                Console.Write("Start fails.\n");
                Thread.Sleep(10);
            }
            Console.Write("Start succeeds.\n");

            List<NETADDR> adrlist = new List<NETADDR>();
            adrlist.Add(remote1);
            adrlist.Add(remote2);
            string s = "Hello";

            byte[] data = new byte[65000];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;
            }
           
            SENDMSG sMessage = new SENDMSG(ref data,ref adrlist);

            while (true)
            {
                if (!netRouterClient.sendMessage(ref sMessage))
                {
                    Console.Write("Send Wrong!\n");
                }
                Thread.Sleep(2000);
            }
           
            Console.ReadKey();
        }
    }
}
