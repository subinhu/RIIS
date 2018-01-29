using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetRouterClient;

namespace TestCSCommPlatform.NetRouter
{
	public class EndPointRecord
	{
		/// <summary>
		/// 远端的类型
		/// </summary>
		public string m_EndType;

		/// <summary>
		/// 订阅关键字
		/// </summary>
		public string m_SubscribeKey;

		public NETADDR m_NetAddr;

		/// <summary>
		/// 地址字符串，5位数字组成
		/// </summary>
		public string m_AddressStr;

		public int m_HeartBeatInterval = 0;
	}
}
