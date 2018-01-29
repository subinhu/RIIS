using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetRouterClient;
using System.Threading;

namespace NetRouterTest
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
            localaddr.devId = 6;


            INetRouterClient netRouterClient = NetRouterClientFactory.CreateNetRouterClient("Test", "172.168.0.1", 9003, "172.168.0.1", 9005, ref localaddr, "");

            while (!netRouterClient.start())
            {
                Console.Write("Start fails.\n");
                Thread.Sleep(10);
            }

            Console.Write("Start succeeds.\n");
            REVMSG recvMsg = new REVMSG();

            while (true)
            {

                while (netRouterClient.receiveMessage(ref recvMsg))
                {

                    string revmsgTemp = System.Text.Encoding.Default.GetString(recvMsg.msg, 0,5);
                    

                    Console.WriteLine(revmsgTemp);
                }
            }

            

          
            Console.ReadKey();

        }
    }
}
