using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NetRouterClient;
namespace CLTSendCsharp
{
    class Program
    {
        private static int SENDNUM = 0;
        public static String split(String s, String delim)
        {
            int index = s.IndexOf(delim);
            return s.Substring(0, index);
        }

        public static int dataNum(String s)
        {
            String splits = " ";
            String numstring = split(s, splits);
            return int.Parse(numstring);
        }
        static void Main(string[] args)
        {
		    NETADDR localaddr = new NETADDR();
            localaddr.bureauCode = 8;
            localaddr.unitType = 2;
            localaddr.unitId = 0;
            localaddr.devType = 1;
            localaddr.devId = 6;

		    INetRouterClient netRouterClient = NetRouterClientFactory.CreateNetRouterClient("Test","191.168.99.98", 9003, "10.2.48.123",9005,ref localaddr,"");
		
		    while(!netRouterClient.start()){
			    Console.Write("Start fails.\n");
			    Thread.Sleep(10);
		    }
		    Console.Write("Start succeeds.\n");
		    while(true){
			    if(netRouterClient.isNet1Connected() || netRouterClient.isNet2Connected()){
				    if(SENDNUM + 1 < SENDNUM){
					    SENDNUM = 0;
				    }

				    REVMSG recvMsg = new REVMSG();
                    List<SENDMSG> sendlist = new List<SENDMSG>();
				    while(netRouterClient.receiveMessage(ref recvMsg)){
                        string revmsgTemp = System.Text.Encoding.Default.GetString(recvMsg.msg, 0, recvMsg.msg.Length);

                        Console.WriteLine("Rev len : " + recvMsg.msg.Length);
                        Console.WriteLine("Rev (" + recvMsg.srcAddr.unitId + ":"
                                            + recvMsg.srcAddr.devId + ")---"
                                            + dataNum(revmsgTemp));
                        
                        //Console.WriteLine("Rev (" + recvMsg.srcAddr.unitId + ":"
                        //                    + recvMsg.srcAddr.devId + ")---"
                        //                    + recvMsg.msg);

					    String ack = (++SENDNUM).ToString();
                        ack += " Hello!";
                        //byte[] tempByte = Encoding.Default.GetBytes(ack);

                        List<NETADDR> adrlist = new List<NETADDR>();
                        adrlist.Add(recvMsg.srcAddr);
                        SENDMSG sMessage = new SENDMSG(ref ack, ref adrlist);

                        sendlist.Add(sMessage);
					    if(!netRouterClient.sendMessage(ref sMessage)){
						    Console.Write("Send Wrong!\n" );
					    }else{
						    Console.WriteLine("Send -------- " + ack+" : "+ack.ToString().Length+"\n");
					    }
				    }
			    }
			    try {
				    Thread.Sleep(10);
			    } catch (Exception e) {
				    // TODO Auto-generated catch block
                    Console.Write(e.Message+"\n");
			    }
		    }
	    }    
    }
}
