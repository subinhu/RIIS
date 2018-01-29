package NetRouterClient;

import java.util.LinkedList;
import java.util.List;

import NetRouterClient.NetRouterClient;
import NetRouterClient.Address;



public class TestNetRouterClient{
	
	private static Integer SENDNUM = new Integer(0);
	public static String split(String s, String delim)  
	{  
	    int index=s.indexOf(delim); 
	    return s.substring(0,index);
	}  

	public static int dataNum(String s){
	    String splits = " ";
	    String numstring = split(s,splits);
	    return Integer.parseInt(numstring);
	}
	
	public static void main(String[] args) throws InterruptedException{
		System.loadLibrary("NetRouterCppClient");
		Address localaddr = new Address((byte)8,(byte)2,(short)0,(byte)1,(short)6);
		NetRouterClient netRouterClient = new NetRouterClient("Test","192.168.99.98", 9003, "10.2.48.123",9005,localaddr,"");
		
		while(!netRouterClient.start()){
			System.out.println("Start fails.");
			Thread.sleep(10);
		}
		System.out.println("Start succeeds.");
		
		while(true){
			if(netRouterClient.isNet1Connected() || netRouterClient.isNet2Connected()){
				if(SENDNUM + 1 < SENDNUM){
					SENDNUM = 0;
				}
				
				RecvMessage recvMsg = new RecvMessage();
				while(netRouterClient.receiveMessage(recvMsg)){
					System.out.println("Rev ("+recvMsg.getSrcAddr().getUnitId()+":"
										+recvMsg.getSrcAddr().getDevId()+")---"
										+dataNum(recvMsg.getMessage()));
					
					List<Address> detAddrs = new LinkedList<Address>();
					detAddrs.add(recvMsg.getSrcAddr());
					

					String ack = (++SENDNUM).toString();
					byte a = 0x00;
					StringBuilder sb = new StringBuilder();
					sb.append(ack+" ");
					sb.append(a);
					sb.append("Hello!");
					
					SendMessage sMessage = new SendMessage(detAddrs, sb.toString());
					if(!netRouterClient.sendMessage(sMessage)){
						System.out.println("Send Wrong!" );
					}else{
						System.out.println("Send -------- " + ack+" : "+sb.toString().length());
					}
				}
			}
			try {
				Thread.sleep(10);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}
}