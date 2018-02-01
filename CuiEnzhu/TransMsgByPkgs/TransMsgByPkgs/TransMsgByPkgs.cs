using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetRouterClient;

namespace TransMsgByPkgsHelper
{
    /// <summary>
    /// 传输协议类，分包发送，组包接收
    /// </summary>
    public class TransMsgByPkgs
    {
        const int PACKLEN = 30;//分包的包长,为了测试方便，这里包长设置很小，经测试设为30000也没有问题

        //分包发送
        public bool sendMsgByPackages(byte[] sb, List<NETADDR> das, INetRouterClient client)
        {
            List<byte[]> listsb = new List<byte[]>();
            int i = 0, j = 0;
            List<byte> bytArray = new List<byte>();

            for (i = 0; i < sb.Length;)
            {
                if (j >= PACKLEN)
                {
                    listsb.Add(bytArray.ToArray());
                    j = 0;
                    bytArray.Clear();
                }
                bytArray.Add(sb[i]);
                j++; i++;

                if (i == sb.Length)
                {
                    listsb.Add(bytArray.ToArray());
                }
            }

            int flag = 0;
            for (int len = 0; len < listsb.Count(); len++)
            {
                byte[] des = new byte[4];
                des[0] = (byte)(listsb.Count());  // 总包数
                des[1] = (byte)(len);       //第几包,从第0包开始
                des[2] = (byte)((listsb[len].Length >> 8) & 0xff);
                des[3] = (byte)((listsb[len].Length) & 0xff); //有效长度





                //将分包信息加入到每个分包的头
                List<byte> bytlist = new List<byte>();
                bytlist.AddRange(des);
                bytlist.AddRange(listsb[len]);
                byte[] sbtemp = bytlist.ToArray();


                SENDMSG sendMsg = new SENDMSG(ref sbtemp, ref das);
                if (!client.sendMessage(ref sendMsg))
                {
                    flag++;
                }

            }
            if (flag == 0)
                return true;
            else
                return false;
        }

        //组包接收
        public bool receiveMsgByPackages(ref REVMSGBYPACKAGES resMsg, INetRouterClient client)
        {
            int flag = 0;
            int packNum = 1; //包数
            List<string> rsvPckgs = new List<string>();
            List<int> packOrderList = new List<int>();

            REVMSG recvMsg = new REVMSG();
            while (flag < packNum)
            {
                while (client.receiveMessage(ref recvMsg))
                {
                    byte[] sbrsv = recvMsg.msg;
                    packNum = (int)sbrsv[0];
                    int order = (int)sbrsv[1];
                    packOrderList.Add(order);
                    int len = (int)(((sbrsv[2] & 0xff) << 8) | (sbrsv[3] & 0xff));
                    byte[] sbtemp = new byte[len];
                    for (int j = 4; j < len + 4; j++)
                    {
                        sbtemp[j - 4] = sbrsv[j];
                    }
                    rsvPckgs.Add(System.Text.Encoding.Default.GetString(sbtemp));
                    ++flag;
                }
            }

            if (flag == packNum)
            {
                int i = 0;
                StringBuilder sbres = new StringBuilder();
                while (i < flag)
                {
                    for (int j = 0; j < flag; j++)
                    {
                        if (packOrderList[j] == i)
                        {
                            sbres.Append(rsvPckgs[j]);
                        }
                    }
                    ++i;
                }
                resMsg.msg = sbres.ToString();//接收字符串长度不超过65535？

                return true;
            }
            else
                return false;
        }
    }

    /// <summary>
    /// 接收信息存储类
    /// </summary>
    public class REVMSGBYPACKAGES
    {
        public string msg;
        public NETADDR srcAddr;
    }
}
