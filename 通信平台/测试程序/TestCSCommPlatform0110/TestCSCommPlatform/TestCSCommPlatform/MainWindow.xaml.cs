using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using TestCSCommPlatform.NetRouter;

namespace TestCSCommPlatform
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.Width = 800;
			this.Height = 350;

			m_NetClient = new NetRouterClientHelper("测试通信平台发送接收");
			m_NetClient.setStatusStrAction(SetStatusLabel0);
			m_NetClient.setConnectingAction(ConnectCallBack);
			m_NetClient.setConnection1Action(SetStatusLabel2);
			m_NetClient.setConnection2Action(SetStatusLabel3);
			m_NetClient.setReceiveAction(OnReceiveMessage);
		}

		private NetRouterClientHelper m_NetClient = null;

		private void ConnectCallBack(bool p_Result)
		{
			this.m_Connected = p_Result;
			if (p_Result)
			{
				Action del = delegate()
				{
					this.btConnect.Content = "断开";
				};
				this.Dispatcher.Invoke(del, null);
				
				SetStatusLabel1("连接成功");
			}
			else
			{
				Action del = delegate()
				{
					this.btConnect.Content = "连接";
				};
				this.Dispatcher.Invoke(del, null);
				
				SetStatusLabel1("连接断开");
			}
		}

		private void SetStatusLabel0(string text)
		{
			Action del = delegate()
			{
				this.lblStatus0.Text = text;
			};
			this.Dispatcher.Invoke(del, null);
			this.DelayClearStatusBar();
		}

		private void SetStatusLabel1(string text)
		{
			Action del = delegate()
			{
				this.lblStatus1.Text = text;
			};
			this.Dispatcher.Invoke(del, null);
			//delayClearStatusBar();
		}

		private void SetStatusLabel2(string text)
		{
			Action del = delegate()
			{
				this.lblStatus2.Text = text;
			};
			this.Dispatcher.Invoke(del, null);
		}

		private void SetStatusLabel3(string text)
		{
			Action del = delegate()
			{
				this.lblStatus3.Text = text;
			};
			this.Dispatcher.Invoke(del, null);
		}

		private bool m_Connected = false;

		private void btConnect_Click(object sender, RoutedEventArgs e)
		{
			if (!m_Connected)
			{
				string AddressStr = this.txtLocalAddr.Text;
				m_NetClient.SetLocalAddress(AddressStr, this.radioClient.IsChecked.Value, this.radioServer.IsChecked.Value);
				//m_NetClient.SetLocalAddress(AddressStr, false, false);
				m_NetClient.AddRemoteAddress(this.txtRemoteAddr.Text);
				m_NetClient.ConnectToServer("10.2.33.171", 9003, "10.2.33.171", 9005);
				//m_NetClient.ConnectToServer("172.16.8.8", 9003, "172.16.8.8", 9005); // 沈北会议室
				//m_NetClient.ConnectToServer("127.0.0.1", 9003, "127.0.0.1", 9005); // 沈北会议室
			}
			else
			{
				m_NetClient.Close();
			}
		}

		private void btSend_Click(object sender, RoutedEventArgs e)
		{
			string DstAddr = this.txtRemoteAddr.Text;
			string t_Message = this.txtSend.Text;
			byte[] t_Data = null;
			if (this.chkBinarySend.IsChecked == false)
				t_Data = Encoding.UTF8.GetBytes(t_Message);
			else
			{
				t_Message = t_Message.Trim();
				string[] t_strs = t_Message.Split(' ');
				t_Data = new byte[t_strs.Length];
				for (int i = 0; i < t_strs.Length; i++)
				{
					t_Data[i] = NetRouterClientHelper.HexStr2Byte(t_strs[i]);
				}
			}
			if (this.m_NetClient != null)
			{
				this.m_NetClient.Send(DstAddr, t_Data);
			}
		}

		private void OnReceiveMessage(string text)
		{
			Action del = delegate()
			{
				string str = this.txtReceive.Text;
				str = str + text;
				this.txtReceive.Text = str;
			};
			this.Dispatcher.Invoke(del, null);
		}

		private int t_ReceiveByteCount = 0;
		private void OnReceiveMessage(byte[] p_Data, int p_StartIndex, int p_Length)
		{
			this.SetStatusLabel0("接收到" + p_Length + "字节的数据");
			string t_Message = null;
			// 字符串表示
			string Message1 = Encoding.UTF8.GetString(p_Data, p_StartIndex, p_Length);
			// 十六进制表示
			StringBuilder t_SB = new StringBuilder();
			for (int i = 0; i < p_Length; i++)
			{
				if (t_ReceiveByteCount % 16 == 0) t_SB.Append("\r\n");
				//t_SB.Append(NetRouterClientHelper.Byte2HexStr(p_Data[p_StartIndex + i]));
				t_SB.Append(NetRouterClientHelper.Byte2HexStr(p_Data[p_StartIndex + i]));
				t_SB.Append(" ");
				t_ReceiveByteCount++;
			}
			string Message2 = t_SB.ToString();
							
			Action del = delegate()
			{
				if (this.chkHEXDisplay.IsChecked == true)
					t_Message = Message2;
				else
					t_Message = Message1 + "\r\n";
				string str = this.txtReceive.Text;
				str = str + t_Message;
				this.txtReceive.Text = str;
				
			};
			this.Dispatcher.Invoke(del, null);
		}


		private void btSwap_Click(object sender, RoutedEventArgs e)
		{
			string str = this.txtLocalAddr.Text;
			this.txtLocalAddr.Text = this.txtRemoteAddr.Text;
			this.txtRemoteAddr.Text = str;
			if (this.radioClient.IsChecked == true)
				this.radioServer.IsChecked = true;
			else
				this.radioClient.IsChecked = true;
		}

		private void ClearStatusBarThreadMethod()
		{
			while (true)
			{
				if (m_ClearValueTick > 0)
				{
					long tick = DateTime.Now.Ticks;
					if (tick > m_ClearValueTick)
					{
						m_ClearValueTick = 0;
						Action del = delegate()
						{
							this.lblStatus0.Text = "";
						};
						this.Dispatcher.Invoke(del, null);
					}
				}
				Thread.Sleep(1000);
			}
		}

		private Thread m_ClearStatusBarThread = null;
		private object m_lock = new object();

		private void DelayClearStatusBar()
		{
			if (m_ClearStatusBarThread == null)
			{
				lock (m_lock)
				{
					if (m_ClearStatusBarThread == null)
					{
						Thread th = new Thread(this.ClearStatusBarThreadMethod);
						th.Name = "延迟清除状态栏";
						th.IsBackground = true;
						th.Start();
						m_ClearStatusBarThread = th;
					}
				}
			}
			DateTime dt = DateTime.Now + new TimeSpan(0, 0, 5);
			m_ClearValueTick = dt.Ticks; 
		}

		private long m_ClearValueTick = 0;

		private void mnuClearReceive_Click(object sender, RoutedEventArgs e)
		{
			this.txtReceive.Text = "";
		}
	}
}
