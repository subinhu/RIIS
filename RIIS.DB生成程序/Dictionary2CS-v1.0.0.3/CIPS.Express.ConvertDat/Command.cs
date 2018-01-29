using System;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// Command 的摘要说明。
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 命令
        /// </summary>
        public Command()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 数据类型
        /// 现车1
        /// </summary>
        public const byte DataType_Car1 = 1;
        /// <summary>
        /// 数据类型
        /// 现车2
        /// </summary>
        public const byte DataType_Car2 = 2;
        /// <summary>
        /// 数据类型
        /// 现车3
        /// </summary>
        public const byte DataType_Car3 = 3;

        /// <summary>
        /// 数据类型
        /// 总表
        /// </summary>
        public const byte DataType_MainTable = 5;//后备总表
        /// <summary>
        /// 数据类型
        /// 流程内容
        /// </summary>
        public const byte DataType_Plan = 10;
        /// <summary>
        /// 数据类型
        /// 流程索引
        /// </summary>
        public const byte DataType_PlanIndex = 11;
        /// <summary>
        /// 数据类型
        /// 编组计划
        /// </summary>
        public const byte DataType_Ass = 15;//集结车次
        /// <summary>
        /// 数据类型
        /// 到报目录
        /// </summary>
        public const byte DataType_ArrConsList = 20;
        /// <summary>
        /// 数据类型
        /// 发报目录
        /// </summary>
        public const byte DataType_DepConsList = 21;
        /// <summary>
        /// 数据类型
        /// 实时现车
        /// </summary>
        public const byte DataType_RealCar = 30;
        /// <summary>
        /// 数据类型
        /// 计划现车
        /// </summary>
        public const byte DataType_PlanCar = 31;
        /// <summary>
        /// 数据类型
        /// 计划现车及计划
        /// </summary>
        public const byte DataType_PlanCarAndPlan = 32;
        /// <summary>
        /// 数据类型
        /// 自动打印
        /// </summary>
        public const byte DataType_AutoPrint = 1;
        /// <summary>
        /// 数据类型
        /// 请求打印
        /// </summary>
        public const byte DataType_Ask4Print = 2;

        /// <summary>
        /// 数据类型
        /// 运货5-1
        /// </summary>
        public const byte DataType_YH5_1 = 41;
        /// <summary>
        /// 数据类型
        /// 运货5-2
        /// </summary>
        public const byte DataType_YH5_2 = 42;
        /// <summary>
        /// 数据类型
        /// 强制保存
        /// </summary>
        public const byte DataType_CipsForceSave = 50;
        /// <summary>
        /// 数据类型
        /// 修补计划
        /// </summary>
        public const byte DataType_CipsRepairPlan = 51;
        /// <summary>
        /// 数据类型
        /// 故障恢复
        /// </summary>
        public const byte DataType_CipsRestorePast = 52;
        /// <summary>
        /// 数据类型-货位
        /// 进货
        /// </summary>
        public const byte DataType_BoothCmd_GoodsIn = 1;
        /// <summary>
        /// 数据类型-货位
        /// 出货
        /// </summary>
        public const byte DataType_BoothCmd_GoodsOut = 2;
        /// <summary>
        /// 数据类型-货位
        /// 完结
        /// </summary>
        public const byte DataType_BoothCmd_Finish = 3;
        /// <summary>
        /// 数据类型-货位
        /// 开始进货
        /// </summary>
        public const byte DataType_BoothCmd_GoodsInStart = 4;
        /// <summary>
        /// 数据类型-货位
        /// 开始出货
        /// </summary>
        public const byte DataType_BoothCmd_GoodsOutStart = 5;
        /// <summary>
        /// 数据类型-货位
        /// 开始装车
        /// </summary>
        public const byte DataType_BoothCmd_LoadStart = 6;
        /// <summary>
        /// 数据类型-货位
        /// 开始卸车
        /// </summary>
        public const byte DataType_BoothCmd_UnloadStart = 7;
        /// <summary>
        /// 重新翻译组号
        /// </summary>
        public const byte DataType_TranslateCarGroup = 1;
        /// <summary>
        /// 重新翻译特征
        /// </summary>
        public const byte DataType_TranslateCarChara = 2;


        /// <summary>
        /// 命令类型
        /// 无
        /// </summary>
        public const byte Cmd_Null = 0;
        /// <summary>
        /// 命令类型
        /// 命令返回状态
        /// </summary>
        public const byte Cmd_CmdResponse = 1;      //
        /// <summary>
        /// 命令类型
        /// 设备停用管理
        /// </summary>
        public const byte Cmd_DeviceMan = 5;        //

        /// <summary>
        /// 命令类型
        /// 后备系统广播现车
        /// </summary>
        public const byte Cmd_SendCar = 50;         //
        /// <summary>
        /// 命令类型
        /// 后备系统广播计划头
        /// 过期
        /// </summary>
        public const byte Cmd_SendPlanIndex = 51;   //
        /// <summary>
        /// 命令类型
        /// 后备系统查找车辆命令
        /// （过期）
        /// </summary>
        public const byte Cmd_FindCar = 52;         //
        /// <summary>
        /// 命令类型
        /// CIPS系统广播实时现车
        /// 过期
        /// </summary>
        public const byte Cmd_SendRealCar = 53;     //
        /// <summary>
        /// CIPS系统广播计划现车
        /// 过期
        /// </summary>
        public const byte Cmd_SendPlanCar = 54;     //
        /// <summary>
        /// 命令类型
        /// 发送集结的车次信息
        /// </summary>
        public const byte Cmd_SendAss = 55;         //
        /// <summary>
        /// 命令类型
        /// 请求/发送后备系统总表
        /// </summary>
        public const byte Cmd_MainInfo = 56;        //

        /// <summary>
        /// 命令类型
        /// 作业车相关命令-配合Datatype使用
        /// </summary>
        public const byte Cmd_CarsWork_Update = 62;        //
        /// <summary>
        /// 命令类型
        /// 取消作业车相关命令
        /// </summary>
        public const byte Cmd_CancelCarsWork_Update = 63;  //

        /// <summary>
        /// 命令类型
        /// 申请现车（用于修改现车）
        /// </summary>
        public const byte Cmd_AskCars4Load = 64;
        /// <summary>
        /// 命令类型
        /// 发送现车（用于修改现车）
        /// </summary>
        public const byte Cmd_SendCars4Load = 65;
        /// <summary>
        /// 命令类型
        /// 装车
        /// </summary>
        public const byte Cmd_CarsLoaded = 66;      //
        /// <summary>
        /// 命令类型
        /// 移动车辆
        /// </summary>
        public const byte Cmd_MoveCars = 67;        //
        /// <summary>
        /// 申请一个完整计划（为取消操作用）
        /// </summary>
        public const byte Cmd_Ask1Plan = 68;        //
        /// <summary>
        /// 预装车
        /// </summary>
        public const byte Cmd_PreLoad = 69;
        /// <summary>
        /// 命令类型
        /// 车辆拦截命令
        /// </summary>
        public const byte Cmd_HoldupCars = 70;      //
        /// <summary>
        /// 命令类型
        /// 添加一辆车
        /// </summary>
        public const byte Cmd_AddCar = 71;          //
        /// <summary>
        /// 命令类型
        /// 删除一辆车
        /// </summary>
        public const byte Cmd_DelCar = 72;          //
        /// <summary>
        /// 命令类型
        /// 申请现车详细
        /// </summary>
        public const byte Cmd_AskCarsDetail = 73;
        /// <summary>
        /// 命令类型
        /// 重新装入字典数据
        /// </summary>
        public const byte Cmd_ReloadDictionary = 74;
        /// <summary>
        /// 命令类型
        /// 取实时分析详细
        /// 过期
        /// </summary>
        public const byte Cmd_GetAnalyzeDetail = 80;//
        /// <summary>
        /// 命令类型
        /// 新建计划命令
        /// </summary>
        public const byte Cmd_NewPlan = 100;        //
        /// <summary>
        /// 命令类型
        /// 发送计划正文
        /// </summary>
        public const byte Cmd_LoadPlan = 101;       //
        /// <summary>
        /// 命令类型
        /// 打印计划
        /// </summary>
        public const byte Cmd_PrintPlan = 102;      //
        /// <summary>
        /// 命令类型
        /// 申请计划正文
        /// </summary>
        public const byte Cmd_AskPlans = 103;       //
        /// <summary>
        /// 命令类型
        /// 申请计划正文
        /// </summary>
        public const byte Cmd_SendPlans = 104;      //
        /// <summary>
        /// 命令类型
        /// 申请计划执行前的现车切面
        /// </summary>
        public const byte Cmd_AskTracksBeforePlan = 105;    //
        /// <summary>
        /// 命令类型
        /// 申请流程ID
        /// </summary>
        public const byte Cmd_AskFlowID = 106;

        /// <summary>
        /// 命令类型
        /// 拆分勾计划
        /// </summary>
        public const byte Cmd_SplitPlan = 108;      //
        /// <summary>
        /// 命令类型
        /// 请求编辑计划
        /// </summary>
        public const byte Cmd_EditPlan = 109;       //
        /// <summary>
        /// 命令类型
        /// 报点
        /// </summary>
        public const byte Cmd_PlanEnter = 110;      //
        /// <summary>
        /// 命令类型
        /// 分步报点
        /// </summary>
        public const byte Cmd_PlanPartEnter = 111;  //
        /// <summary>
        /// 命令类型
        /// 取消报点
        /// </summary>
        public const byte Cmd_PlanExit = 112;       //
        /// <summary>
        /// 命令类型
        /// 推入现车
        /// </summary>
        public const byte Cmd_PushPlan = 113;       //
        /// <summary>
        /// 命令类型
        /// 取消推入
        /// </summary>
        public const byte Cmd_PopPlan = 114;        //
        /// <summary>
        /// 命令类型
        /// 申请计划
        /// </summary>
        public const byte Cmd_AskPlan = 115;        //
        /// <summary>
        /// 命令类型
        /// 移动计划
        /// DataType==0:intdata[0]=被移流程，intdata[1]=参考流程
        /// DataType==1:intdata=要调整的新的流程顺序
        /// </summary>
        public const byte Cmd_MovePlan = 116;       //
        /// <summary>
        /// 命令类型
        /// 删除计划
        /// </summary>
        public const byte Cmd_DeletePlan = 117;     //
        /// <summary>
        /// 命令类型
        /// 修改勾计划表头
        /// time1=计划开始,time2=计划结束,time3=实际开始,time4=实际结束;
        /// int1=FLOWID,int2=LOCOMID;string1=车次,string2=编制人
        /// </summary>
        public const byte Cmd_ModifyFlowHead = 118; //
        /// <summary>
        /// 命令类型
        /// 发送勾计划至驼峰
        /// </summary>
        public const byte Cmd_Send2Hump = 119;      //

        /// <summary>
        /// 命令类型
        /// 驼峰申请全部计划
        /// </summary>
        public const byte Cmd_AskAllPlan4Hump = 120;//
        /// <summary>
        /// 命令类型
        /// 溜放开始
        /// </summary>
        public const byte Cmd_SplitStart = 121;     //
        /// <summary>
        /// 命令类型
        /// 溜放结束
        /// </summary>
        public const byte Cmd_SplitOver = 122;      //
        /// <summary>
        /// 命令类型
        /// 驼峰连接报IP
        /// </summary>
        public const byte Cmd_HumpLink = 123;       //
        /// <summary>
        /// 命令类型
        /// 提交计划并推入现车
        /// </summary>
        public const byte Cmd_SubmitPlan = 200;     //
        /// <summary>
        /// 命令类型
        /// 保存计划草稿
        /// </summary>
        public const byte Cmd_SavePlan = 201;       //
        /// <summary>
        /// 命令类型
        /// 修改计划
        /// </summary>
        public const byte Cmd_ModifyPlan = 202;     //
        /// <summary>
        /// 命令类型
        /// 现车转确报
        /// </summary>
        public const byte Cmd_TrackToCons = 210;    //
        /// <summary>
        /// 命令类型
        /// 确报转现车
        /// </summary>
        public const byte Cmd_ConsToTrack = 211;    //
        /// <summary>
        /// 命令类型
        /// 出发报点
        /// </summary>
        public const byte Cmd_TrainDepart = 212;    //
        /// <summary>
        /// 命令类型
        /// 到达报点
        /// </summary>
        public const byte Cmd_TrainArrive = 213;    //
        /// <summary>
        /// 命令类型
        /// 发报
        /// </summary>
        public const byte Cmd_SendCons = 214;       //
        /// <summary>
        /// 命令类型
        /// 确报转现车成功回执
        /// </summary>
        public const byte Cmd_ConsToTrackSuccess = 215;//
        /// <summary>
        /// 命令类型
        /// 确报核准
        /// </summary>
        public const byte Cmd_ApproveCons = 216;    //
        /// <summary>
        /// 命令类型
        /// 删到报
        /// </summary>
        public const byte Cmd_DelConsArr = 220;     //
        /// <summary>
        /// 命令类型
        /// 删发报
        /// </summary>
        public const byte Cmd_DelConsDep = 221;     //
        /// <summary>
        /// 命令类型
        /// 新建到报
        /// </summary>
        public const byte Cmd_NewConsArr = 222;     //
        /// <summary>
        /// 命令类型
        /// 新建发报
        /// </summary>
        public const byte Cmd_NewConsDep = 223;     //
        /// <summary>
        /// 命令类型
        /// 修改现车
        /// </summary>
        public const byte Cmd_UpdateCars = 224;     //
        /// <summary>
        /// 命令类型
        /// 修改车号
        /// </summary>
        public const byte Cmd_ModifyCarNum = 225;   //
        /// <summary>
        /// 命令类型
        /// 申请/发送现车详细至毛玻璃
        /// </summary>
        public const byte Cmd_CarsDetail = 226;     //
        /// <summary>
        /// 命令类型
        /// 货位命令
        /// </summary>
        public const byte Cmd_BoothCommand = 227;
        /// <summary>
        /// 命令类型
        /// 交接班
        /// </summary>
        public const byte Cmd_HandOver = 230;       //
        /// <summary>
        /// 命令类型
        /// 转入在线
        /// </summary>
        public const byte Cmd_ToOnLine = 231;       //
        /// <summary>
        /// 命令类型
        /// 转入值班
        /// </summary>
        public const byte Cmd_ToBackup = 232;       //
        /// <summary>
        /// 命令类型
        /// 接管CIPS
        /// </summary>
        public const byte Cmd_ToOnLineFromCIPS = 233;//
        /// <summary>
        /// 命令类型
        /// 选择是否使用TDCS操作
        /// </summary>
        public const byte Cmd_TdcsMode = 234;       //
        /// <summary>
        /// 命令类型
        /// 从原备份中接管
        /// </summary>
        public const byte Cmd_ToOnLineFromPast = 235;//
        /// <summary>
        /// 命令类型
        /// CIPS系统修复
        /// </summary>
        public const byte Cmd_CipsRepair = 236;     //
        /// <summary>
        /// 命令类型
        /// 后备系统数据技术作业图表写实
        /// </summary>
        public const byte Cmd_ToChart = 240;        //
        /// <summary>
        /// 命令类型
        /// 阶段计划流程
        /// </summary>
        public const byte Cmd_PlanFromChart = 241;  //

      

        /// <summary>
        /// 后备系统操作命令
        /// </summary>
        public class ExpressCommand
        {
            /// <summary>
            /// 命令类型
            /// </summary>
            public byte Cmd = Cmd_Null;
            /// <summary>
            /// 数据类型
            /// </summary>
            public byte Datatype = 0;
            /// <summary>
            /// 工作区域
            /// </summary>
            public short Workstation = 0;
            /// <summary>
            /// 字节数据
            /// </summary>
            public byte[] byteData = new byte[0];
            /// <summary>
            /// 整数数据
            /// </summary>
            public uint[] intData = new uint[0];
            /// <summary>
            /// 字符串数据
            /// </summary>
            public string[] strData = new string[0];
            /// <summary>
            /// 时间数据
            /// </summary>
            public DateTime[] timeData = new DateTime[0];
            /// <summary>
            /// 岗位
            /// </summary>
            public short postid;
            /// <summary>
            /// 内容
            /// </summary>
            public string[] strs = { "", "", "", "", "" ,"", "",""};
            /// <summary>
            /// 操作人
            /// </summary>
            public string maker
            {
                get { return strs[0]; }
                set { strs[0] = value; }
            }
            /// <summary>
            /// IP
            /// </summary>
            public string ip
            {
                get { return strs[1]; }
                set { strs[1] = value; }
            }
            /// <summary>
            /// 线路
            /// </summary>
            public string trackname
            {
                get { return strs[2]; }
                set { strs[2] = value; }
            }
            /// <summary>
            /// 车号
            /// </summary>
            public string carnum
            {
                get { return strs[3]; }
                set { strs[3] = value; }
            }
            /// <summary>
            /// 车次
            /// </summary>
            public string trainnum
            {
                get { return strs[4]; }
                set { strs[4] = value; }
            }
            /// <summary>
            /// 操作类型
            /// </summary>
            public string flowtype
            {
                get { return strs[5]; }
                set { strs[5] = value; }
            }
            /// <summary>
            /// 岗位
            /// </summary>
            public string postname
            {
                get { return strs[6]; }
                set { strs[6] = value; }
            }
            /// <summary>
            /// 站码
            /// </summary>
            public string stationcode
            {
                get { return strs[7]; }
                set { strs[7] = value; }
            }
            /// <summary>
            /// 注释
            /// </summary>
            public string note = "";
            /// <summary>
            /// 序列化
            /// </summary>
            /// <returns></returns>
            public byte[] Serialize()
            {
                return Command.Serialize(this);
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Serialize(ExpressCommand data)
        {
            int n = 5 + 4 + data.byteData.Length + 4 + data.intData.Length * 4 + 4 + data.timeData.Length * 6 + 4;
            foreach (string s in data.strData)
                n += System.Text.Encoding.Default.GetByteCount(s) + 1;
            foreach (string s in data.strs)
                n += System.Text.Encoding.Default.GetByteCount(s) + 1;
            byte[] b = new byte[n];
            b[0] = data.Cmd;
            BitConverter.GetBytes(data.Workstation).CopyTo(b, 1);
           // b[1] = data.Workstation;
            b[3] = data.Datatype;
            BitConverter.GetBytes(data.postid).CopyTo(b, 4);
            n = 6;
            BitConverter.GetBytes((int)data.byteData.Length).CopyTo(b, n);
            n += 4;
            data.byteData.CopyTo(b, n);
            n += data.byteData.Length;
            BitConverter.GetBytes((int)data.intData.Length).CopyTo(b, n);
            n += 4;
            foreach (uint u in data.intData)
            {
                BitConverter.GetBytes(u).CopyTo(b, n);
                n += 4;
            }
            BitConverter.GetBytes((int)data.timeData.Length).CopyTo(b, n);
            n += 4;
            foreach (DateTime t in data.timeData)
            {
                Tools.Time2Byte(t).CopyTo(b, n);
                n += 6;
            }
            BitConverter.GetBytes((int)data.strData.Length).CopyTo(b, n);
            n += 4;
            foreach (string s in data.strData)
            {
                Tools.String2Byte(s, b, ref n);
            }
            foreach (string s in data.strs)
            {
                Tools.String2Byte(s, b, ref n);
            }
            return b;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExpressCommand Deserialize(byte[] b)
        {
            if (b.Length < 12)
                return null;
            int i, n;
            try
            {
                ExpressCommand cmd = new ExpressCommand();

                cmd.Cmd = b[0];
                cmd.Workstation = BitConverter.ToInt16(b, 1);
               // cmd.Workstation = b[1];
                cmd.Datatype = b[3];
                cmd.postid = BitConverter.ToInt16(b, 4);
                i = 6;
                n = BitConverter.ToInt32(b, i);
                i += 4;
                if (i + n + 3 > b.Length)
                    return null;
                cmd.byteData = new byte[n];
                Buffer.BlockCopy(b, i, cmd.byteData, 0, n);
                i += n;
                n = BitConverter.ToInt32(b, i);
                i += 4;
                if (i + n * 4 + 2 > b.Length)
                    return null;
                cmd.intData = new uint[n];
                int j;
                for (j = 0; j < n; j++)
                {
                    cmd.intData[j] = BitConverter.ToUInt32(b, i);
                    i += 4;
                }
                n = BitConverter.ToInt32(b, i);
                i += 4;
                cmd.timeData = new DateTime[n];
                if (i + n * 6 + 1 > b.Length)
                    return null;
                for (j = 0; j < n; j++)
                {
                    cmd.timeData[j] = Tools.Byte2Time(b, i);
                    i += 6;
                }
                n = BitConverter.ToInt32(b, i);
                i += 4;
                cmd.strData = new string[n];
                if (i + n > b.Length)
                    return null;
                try
                {
                    for (j = 0; j < n; j++)
                    {
                        cmd.strData[j] = Tools.Byte2String(b, ref i);
                    }
                }
                catch
                {
                    cmd = null;
                }
                try
                {
                    for (j = 0; j < cmd.strs.Length; j++)
                    {
                        try
                        {
                            cmd.strs[j] = Tools.Byte2String(b, ref i);
                        }
                        catch
                        {
                            cmd.strs[j] = "";
                        }
                    }
                }
                catch
                {
                    cmd = null;
                }

                return cmd;
            }
            catch
            {
                return null;
            }
        }
    }
}
