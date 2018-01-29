using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

using NetRouterClient;

namespace TestCSCommPlatform.NetRouter
{
	public class NetRouterClientHelper
	{
		private string m_ClientName = "";
		private string m_IP1 = "";
		private int m_Port1 = 0;
		private string m_IP2 = "";
		private int m_Port2 = 0;
		private NETADDR m_LocalAddr;
		private INetRouterClient m_NetRouterClient = null;

		public NetRouterClientHelper(string p_ClientName)
		{
			this.m_ClientName = p_ClientName;
		}

		//private bool m_ifSetIPAddress = false;
		private Thread m_ConnectThread = null;
		private Thread m_ReceiveThread = null;
		public void ConnectToServer(string IP1, int Port1, string IP2, int Port2)
		{

			this.m_IP1 = IP1;
			this.m_Port1 = Port1;
			this.m_IP2 = IP2;
			this.m_Port2 = Port2;

			if (this.m_NetRouterClient == null)
			{
				this.m_NetRouterClient = NetRouterClientFactory.CreateNetRouterClient
					(this.m_ClientName, this.m_IP1, this.m_Port1, this.m_IP2, this.m_Port2, ref this.m_LocalAddr, "");
			}

			if (this.m_StatusStrAction != null)
				this.m_StatusStrAction("正在连接路由服务器...");
			while (!m_NetRouterClient.start())
			{
				Thread.Sleep(10);
			}
			if (this.m_StatusStrAction != null)
				this.m_StatusStrAction("连接路由服务器成功！");

			m_ThreadClose = false;

			m_ReceiveThread = new Thread(this.ReceiveThreadMethod);
			m_ReceiveThread.Name = "接收线程";
			m_ReceiveThread.IsBackground = true;
			m_ReceiveThread.Start();

			if (m_ConnectThread == null)
			{
				m_ConnectThread = new Thread(this.ConnectionThreadMethod);
				m_ConnectThread.Name = "保持连接线程";
				m_ConnectThread.IsBackground = true;
				m_ConnectThread.Start();
			}
		}

		/// <summary>
		/// FA DA FA DA
		/// </summary>
		private bool m_ifSendHeartBeat = false;
		
		/// <summary>
		/// FA EA FA EA
		/// </summary>
		private bool m_ifReplyHeartBeat = false;

		public void SetLocalAddress(string LocalAddressString, bool p_ifSendHeartBeat, bool p_ifReplyHeartBeat)
		{
			this.m_ifSendHeartBeat = p_ifSendHeartBeat;
			this.m_ifReplyHeartBeat = p_ifReplyHeartBeat;

			NETADDR NetAddr = NetRouterClientHelper.ConvertAddress(LocalAddressString);
			this.m_LocalAddr = NetAddr;
		}

		public static NETADDR ConvertAddress(string AddressString)
		{
			NETADDR NetAddr = new NETADDR();
			byte b;
			ushort u;

			if (!string.IsNullOrEmpty(AddressString))
			{
				string[] strs = AddressString.Split(',');
				if (strs.Length == 5)
				{
					if (byte.TryParse(strs[0], out b))
						NetAddr.bureauCode = b;
					if (byte.TryParse(strs[1], out b))
						NetAddr.unitType = b;
					if (ushort.TryParse(strs[2], out u))
						NetAddr.unitId = u;
					if (byte.TryParse(strs[3], out b))	
						NetAddr.devType = b;
					if (ushort.TryParse(strs[4], out u))
						NetAddr.devId = u;
				}
			}
			return NetAddr;
		}

		public static string ConvertAddress(NETADDR p_NetAddr)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(p_NetAddr.bureauCode);
			sb.Append(',');
			sb.Append(p_NetAddr.unitType);
			sb.Append(',');
			sb.Append(p_NetAddr.unitId);
			sb.Append(',');
			sb.Append(p_NetAddr.devType);
			sb.Append(',');
			sb.Append(p_NetAddr.devId);
			return sb.ToString();
		}

		public void SetLocalAddress(byte BureauCode, byte UnitType, ushort UnitId, byte DevType, ushort DevId)
		{
			this.m_LocalAddr = new NETADDR();
			this.m_LocalAddr.bureauCode = BureauCode;
			this.m_LocalAddr.unitType = UnitType;
			this.m_LocalAddr.unitId = UnitId;
			this.m_LocalAddr.devType = DevType;
			this.m_LocalAddr.devId = DevId;
		}

		//private ConcurrentBag<NETADDR> m_RemoteAddressSet = new ConcurrentBag<NETADDR>();
		private ConcurrentDictionary<string, EndPointRecord> m_RemoteAddressMap = new ConcurrentDictionary<string, EndPointRecord>();
		/// <summary>
		/// 将需要保持连接的远程地址添加到地址表
		/// </summary>
		/// <param name="p_Address"></param>
		public void AddRemoteAddress(string p_Address)
		{
			NETADDR t_NetAddr = NetRouterClientHelper.ConvertAddress(p_Address);
			EndPointRecord t_EndPoint = new EndPointRecord();
			t_EndPoint.m_NetAddr = t_NetAddr;
			t_EndPoint.m_AddressStr = p_Address;
			this.m_RemoteAddressMap.TryAdd(p_Address, t_EndPoint);
		}

		private bool m_Connected = false;
		private bool m_ConnectStatus1 = false;
		private bool m_ConnectStatus2 = false;

		private void ConnectionThreadMethod()
		{
			int count = 0;
			while (true)
			{
				try
				{
					INetRouterClient t_Client = this.m_NetRouterClient;
					if (t_Client != null)
					{
						bool isNet1Connected = t_Client.isNet1Connected();
						if (this.m_ConnectStatus1 != isNet1Connected)
						{
							this.m_ConnectStatus1 = isNet1Connected;
							if (this.m_Connection1Action != null)
								this.m_Connection1Action("网络连接1状态: " + isNet1Connected);
						}

						bool isNet2Connected = t_Client.isNet2Connected();
						if (this.m_ConnectStatus2 != isNet2Connected)
						{
							this.m_ConnectStatus2 = isNet2Connected;
							if (this.m_Connection2Action != null)
								this.m_Connection2Action("网络连接2状态: " + isNet2Connected);
						}
						
						if (!isNet1Connected && !isNet2Connected)
						{
							if (m_Connected)
							{
								m_Connected = false;
								if (this.m_ConnectingAction != null)
									this.m_ConnectingAction(m_Connected);
							}
						}
						else
						{
							if (!m_Connected)
							{
								m_Connected = true;
								if (this.m_ConnectingAction != null)
									this.m_ConnectingAction(m_Connected);
							}
							if (m_ifSendHeartBeat && count % 2 == 0)
							{
								foreach (EndPointRecord t_EndPoint in this.m_RemoteAddressMap.Values)
								{
									NETADDR t_Addr = t_EndPoint.m_NetAddr;
									t_EndPoint.m_HeartBeatInterval++;
									SendHeartBeatData(t_Addr);
									if (t_EndPoint.m_HeartBeatInterval > 5)
									{
										if (this.m_StatusStrAction != null)
											this.m_StatusStrAction("[" + t_EndPoint.m_AddressStr + "]已断开");
									}
								}
							}
						}
					}
					count++;
					Thread.Sleep(1000);
				}
				catch (ThreadAbortException e)
				{
					System.Console.WriteLine(Thread.CurrentThread.Name + "已被要求退出");
					Thread.ResetAbort();
				}
				catch (Exception e)
				{
					if (this.m_StatusStrAction != null)
					{
						this.m_StatusStrAction(e.Message);
					}
				}
			}
		}

		private byte[] m_HeartBeatData = new byte[4] { 0xFA, 0xDA, 0xFA, 0xDA };
		private void SendHeartBeatData(NETADDR p_NetAddr)
		{
			this.Send(p_NetAddr, m_HeartBeatData);
		}

		private byte[] m_HeartBeatReply = new byte[4] { 0xFA, 0xEA, 0xFA, 0xEA };
		private void SendHeartBeatReply(NETADDR p_NetAddr)
		{
			this.Send(p_NetAddr, m_HeartBeatReply);
		}

		private Action<string> m_StatusStrAction = null;

		public void setStatusStrAction(Action<string> p_Action)
		{
			this.m_StatusStrAction = p_Action;
		}


		private Action<bool> m_ConnectingAction = null;
		
		public void setConnectingAction(Action<bool> p_Action)
		{
			this.m_ConnectingAction = p_Action;
		}

		private Action<string> m_Connection1Action = null;

		public void setConnection1Action(Action<string> p_Action)
		{
			this.m_Connection1Action = p_Action;
		}

		private Action<string> m_Connection2Action = null;

		public void setConnection2Action(Action<string> p_Action)
		{
			this.m_Connection2Action = p_Action;
		}

		//public void Send(string p_DstAddressStr, string p_Message)
		//{
		//    if (this.m_NetRouterClient == null)
		//    {
		//        this.m_StatusStrAction("尚未与网络路由服务器建立连接");
		//        return;
		//    }
		//    NETADDR NetAddr = NetRouterClientHelper.ConvertAddress(p_DstAddressStr);
		//    List<NETADDR> adrlist = new List<NETADDR>();
		//    adrlist.Add(NetAddr);
		//    //SENDMSG sMessage = new SENDMSG(ref p_Message, ref adrlist);
		//    //for (int i = 0; i < 10; i++)
		//    {
		//        byte[] t_Data = Encoding.UTF8.GetBytes(p_Message);
		//        int length = t_Data.Length;
		//        byte[] t_LengthBytes = Int32ToByte(length);
		//        byte[] t_SendData = new byte[4 + length];
		//        t_LengthBytes.CopyTo(t_SendData, 0);
		//        t_Data.CopyTo(t_SendData, 4);
		//        SENDMSG tMessage = new SENDMSG(ref t_SendData, ref adrlist);
		//        if (!m_NetRouterClient.sendMessage(ref tMessage))
		//        {
		//            if (this.m_StatusStrAction != null)
		//            {
		//                this.m_StatusStrAction("发送错误！");
		//            }
		//        }
		//        else
		//        {
		//            if (this.m_StatusStrAction != null)
		//            {
		//                this.m_StatusStrAction("发送成功！");
		//            }
		//        }
		//    }
		//}

		/// <summary>
		/// 添加头部，并发送
		/// </summary>
		/// <param name="p_NetAddr"></param>
		/// <param name="p_Data"></param>
		/// <param name="p_ifNeedHeader"></param>
		public void Send(NETADDR p_NetAddr, byte[] p_Data, bool p_ifNeedHeader = true)
		{
			if (!m_Connected)
			{
				if (this.m_StatusStrAction != null)
				{
					this.m_StatusStrAction("尚未与网络路由服务器建立连接");
					return;
				}
			}
			try
			{
				List<NETADDR> adrlist = new List<NETADDR>();
				adrlist.Add(p_NetAddr);

				byte[] t_SendData;
				int length = p_Data.Length;
				if (p_ifNeedHeader)
				{
					byte[] t_LengthBytes = Int32ToByte(length);
					t_SendData = new byte[4 + length];
					t_LengthBytes.CopyTo(t_SendData, 0);
					p_Data.CopyTo(t_SendData, 4);
				}
				else
				{
					t_SendData = p_Data;
				}
				SENDMSG tMessage = new SENDMSG(ref t_SendData, ref adrlist);

				if (!m_NetRouterClient.sendMessage(ref tMessage))
				{
					if (this.m_StatusStrAction != null)
					{
						this.m_StatusStrAction("发送错误！");
					}
				}
				else
				{
					if (p_Data != this.m_HeartBeatData && p_Data != this.m_HeartBeatReply)
					{
						if (this.m_StatusStrAction != null)
						{
							this.m_StatusStrAction("成功发送" + length + "字节的数据");
						}
					}
				}
			}
			catch (Exception e)
			{
				if (this.m_StatusStrAction != null)
				{
					this.m_StatusStrAction(e.Message);
					return;
				}
			}
		}

		public void Send(string p_DstAddressStr, byte[] p_Data)
		{
			try
			{
				NETADDR NetAddr = NetRouterClientHelper.ConvertAddress(p_DstAddressStr);
				Send(NetAddr, p_Data);
			}
			catch (Exception e)
			{
				if (this.m_StatusStrAction != null)
				{
					this.m_StatusStrAction(e.Message);
					return;
				}
			}
		}

		private bool m_ThreadClose = true;
 
		private void ReceiveThreadMethod()
		{
			while (!m_ThreadClose)
			{
				try
				{
					if (this.m_NetRouterClient != null)
					{
						if (this.m_ConnectStatus1 || this.m_ConnectStatus2)
						{
							REVMSG recvMsg = new REVMSG();
							while (m_NetRouterClient.receiveMessage(ref recvMsg))
							{
								byte[] t_ReceiveData = recvMsg.msg;
								if (t_ReceiveData != null)
								{
									int t_DataLength = ByteToInt32(t_ReceiveData);
									bool t_NeedOperate = true;
									if (t_DataLength == 4)
									{
										if (t_ReceiveData[4] == m_HeartBeatData[0]
											&& t_ReceiveData[5] == m_HeartBeatData[1]
											&& t_ReceiveData[6] == m_HeartBeatData[2]
											&& t_ReceiveData[7] == m_HeartBeatData[3])
										{
											t_NeedOperate = false;
											DateTime dt = DateTime.Now;
											if (this.m_StatusStrAction != null)
												this.m_StatusStrAction(dt.ToLongTimeString() + " 收到心跳信息包");
											if (m_ifReplyHeartBeat)
											{
												SendHeartBeatReply(recvMsg.srcAddr);
											}
										}
										else if (t_ReceiveData[4] == m_HeartBeatReply[0]
											&& t_ReceiveData[5] == m_HeartBeatReply[1]
											&& t_ReceiveData[6] == m_HeartBeatReply[2]
											&& t_ReceiveData[7] == m_HeartBeatReply[3])
										{
											t_NeedOperate = false;
											DateTime dt = DateTime.Now;
											if (this.m_StatusStrAction != null)
												this.m_StatusStrAction(dt.ToLongTimeString() + " 收到心跳信息应答包");
											NETADDR t_SrcAddr = recvMsg.srcAddr;
											string t_AddrStr = NetRouterClientHelper.ConvertAddress(t_SrcAddr);
											EndPointRecord t_EndPoint;
											if (this.m_RemoteAddressMap.TryGetValue(t_AddrStr, out t_EndPoint))
											{
												t_EndPoint.m_HeartBeatInterval = 0;
											}
										}
									}
									if (t_NeedOperate && this.m_ReceiveAction != null)
										this.m_ReceiveAction(t_ReceiveData, 4, t_DataLength);
								}
							}
						}
					}
					Thread.Sleep(10);
				}
				catch (ThreadAbortException e)
				{
					System.Console.WriteLine(Thread.CurrentThread.Name + "已被要求退出");
					Thread.ResetAbort();
				}
				catch (Exception e)
				{
					if (this.m_StatusStrAction != null)
					{
						this.m_StatusStrAction(e.Message);
					}
				}
			}
		}

		//private Action<string> m_ReceiveAction = null;
		private Action<byte[], int, int> m_ReceiveAction = null;
		public void setReceiveAction(Action<byte[], int, int> p_Action)
		{
			this.m_ReceiveAction = p_Action;
		}

		/// <summary>
		/// 将Int32转成四字节数组，高位在高地址字节
		/// </summary>
		/// <param name="a">待转换的32位整数</param>
		/// <returns>转换后的字节数组</returns>
		private static byte[] Int32ToByte(int a)
		{
			byte[] b = new byte[4];
			b[3] = (byte)((a >> 24) & 0xff);
			b[2] = (byte)((a >> 16) & 0xff);
			b[1] = (byte)((a >> 8) & 0xff);
			b[0] = (byte)((a) & 0xff);
			return b;
		}

		/// <summary>
		/// 将长度为4的字节数组转成Int32，高地址字节在高位
		/// </summary>
		/// <param name="b">长度为4的字节数组</param>
		/// <returns>转换后的Int32型整数</returns>
		private static Int32 ByteToInt32(byte[] b)
		{
			int a = 0;
			if (b.Length >= 4)
			{
				a = (((Int32)b[0])) + (((Int32)b[1]) << 8) +
						(((Int32)b[2]) << 16) + (((Int32)b[3]) << 24);
			}
			return a;
		}

		public void Close()
		{
			INetRouterClient t_Client = this.m_NetRouterClient;
			if (t_Client == null)
				return;
			try
			{
				if (t_Client.close())
				{
					if (this.m_StatusStrAction != null)
						this.m_StatusStrAction("连接已断开");
					this.m_ThreadClose = true;
					//Thread th = this.m_ConnectThread;
					//if (th != null)
					//    th.Abort();
					//Thread th2 = this.m_ReceiveThread;
					//if (th2 != null)
					//    th2.Abort();
					this.m_ReceiveThread = null;
				}
			}
			catch (Exception e)
			{
				if (this.m_StatusStrAction != null)
				{
					this.m_StatusStrAction(e.Message);
				}
			}
		}

		public static string Byte2HexStr(byte i)
		{
			int s = i / 16;
			int g = i % 16;
			return Int2HexChar(s) + Int2HexChar(g);
		}

		private static string Int2HexChar(int i)
		{
			if (i >= 0 && i <= 9)
				return i.ToString();
			if (i == 10)
				return "A";
			if (i == 11)
				return "B";
			if (i == 12)
				return "C";
			if (i == 13)
				return "D";
			if (i == 14)
				return "E";
			if (i == 15)
				return "F";
			return " ";
		}

		public static byte HexStr2Byte(string p_HexStr)
		{
			int s = 0;
			int len = p_HexStr.Length;
			int sz;
			for (int i = 0; i < len; i++)
			{
				char c = p_HexStr[i];
				if (c == '0') sz = 0;
				else if (c == '1') sz = 1;
				else if (c == '2') sz = 2;
				else if (c == '3') sz = 3;
				else if (c == '4') sz = 4;
				else if (c == '5') sz = 5;
				else if (c == '6') sz = 6;
				else if (c == '7') sz = 7;
				else if (c == '8') sz = 8;
				else if (c == '9') sz = 9;
				else if (c == 'a' || c == 'A') sz = 10;
				else if (c == 'b' || c == 'B') sz = 11;
				else if (c == 'c' || c == 'C') sz = 12;
				else if (c == 'd' || c == 'D') sz = 13;
				else if (c == 'e' || c == 'E') sz = 14;
				else if (c == 'f' || c == 'F') sz = 15;
				else sz = 0;
				s = s * 16 + sz;
			}
			return (byte)s;
		}
	}
}
