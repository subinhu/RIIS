using System;
using System.Data;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// 车辆
    /// </summary>
    public class CAR
    {
        private int _oriid = -1;
        /// <summary>
        /// 原始ID
        /// </summary>
        public int oriid
        {
            get
            {
                if (_oriid == -1)
                    return _id;
                return _oriid;
            }
            set
            {
                _oriid = value;
            }
        }

        private int _id = 0;
        /// <summary>
        /// ID
        /// </summary>
        public int id
        {
            get
            {
                 return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 整数位表示的车号
        /// </summary>
        public uint CarNum = 0;
        /// <summary>
        /// 车号,
        /// </summary>
        public string CarCo_8
        {
            get
            {
                return CarCoFromCarNum(CarNum);
            }
            set
            {
                CarNum = CarNumFromCarCo(value);
            }
        }
        /// <summary>
        /// 7位车号
        /// </summary>
        public string CarCo
        {
            get
            {
               
                return CarCoFromCarNum(CarNum);
            }
            set
            {
               
                CarNum = CarNumFromCarCo(value);
            }
        }

        /// <summary>
        /// 根据车号编码获取7位车号
        /// </summary>
        /// <param name="_CarNum"></param>
        /// <returns></returns>
        public static string CarCoFromCarNum(uint _CarNum)
        {
            if (_CarNum == 0)
                return "";
            bool bHaveChar = (_CarNum & 0x80000000) != 0;//版本号
            bool bHaveZero = (_CarNum & 0x40000000) != 0;//是否有0
            string snum = "";
            if (bHaveChar)//大于8位车号的版本
            {
                if (bHaveZero)
                {
                    uint carcolen = ((_CarNum & 0x3f000000) >> 24) & 0xf;//车号总长度
                    snum = ((int)(_CarNum & 0xfffffff)).ToString();//数字部分
                    while (snum.Length < carcolen)
                        snum = "0" + snum;
                }
                else
                {
                    snum = ((int)(_CarNum & 0x3fffffff)).ToString();//数字部分
                }
            }
            else
            {
                if (bHaveZero)
                {
                    uint carcolen = ((_CarNum & 0xe00000) >> 21) & 7;//车号总长度
                    snum = ((int)(_CarNum & 0x1fffff)).ToString();//数字部分
                    if (bHaveChar)
                    {
                        int charcode = (int)((_CarNum >> 24) & 0x3f);//字符编码
                        string schar = GetDescFromCode(charcode);
                        while (snum.Length + schar.Length < carcolen)
                            snum = "0" + snum;
                    }
                    else
                    {
                        while (snum.Length < carcolen)
                            snum = "0" + snum;
                    }
                }
                else
                {
                    snum = ((int)(_CarNum & 0xfffffff)).ToString();//数字部分
                }
                if (bHaveChar)
                {
                    int charcode = (int)((_CarNum >> 24) & 0x3f);//字符编码
                    string schar = GetDescFromCode(charcode);
                    snum = schar + snum;
                }
            }
            return CarCoTranslate.Translator.GetCarCoFromNo(snum);
        }

        /// <summary>
        /// 根据车号获取7位编码
        /// 32位：版本号；30：数字部分是否0开头
        /// 版本号为0时
        /// 25-29位：字符部分的转义编码；
        /// 如果数字部分0开头：22-24位：车号长度，占3位，最多存储长度为7；1-21位：存储数字部分转化的int值，2097151，所以除掉首0后只能存6位车号
        /// 如果数字部分非0开头：1-24位：存储数字部分转化的int值，只能存储到16777215，所以只能存7位车号
        /// 版本号为1时
        /// 如果数字部分0开头：25-30位：车号长度；1-24位：存储数字部分转化的int值
        /// 如果数字部分非0开头：1-29位：存储数字部分转化的int值
        /// </summary>
        /// <param name="_CarCo"></param>
        /// <returns></returns>
        public static uint CarNumFromCarCo(string _CarCo)
        {
            uint _CarNum = 0;
            int i;
            _CarCo = CarCoTranslate.Translator.GetCarNoFromCo(_CarCo);
            if (_CarCo.Length >= 8)
            {
                _CarNum |= 0x80000000;
                i = Convert.ToInt32(_CarCo);
                bool bHaveZero = _CarCo.StartsWith("0");
                if (bHaveZero)
                {
                    _CarNum |= 0x40000000;
                    _CarNum |= (uint)((_CarCo.Length) << 24);
                }
                _CarNum |= (uint)(i & 0x3fffffff);
                return _CarNum;
            }
            else
            {
                string snum = "", schar = "";
                for (i = 0; i < _CarCo.Length; i++)
                {
                    if (_CarCo[i] >= '0' && _CarCo[i] <= '9')
                    {
                        schar = _CarCo.Substring(0, i);
                        snum = _CarCo.Substring(i, _CarCo.Length - i);
                        break;
                    }
                }
                if (snum.Length == 0)
                    i = 0;
                else
                    i = Convert.ToInt32(snum);
                _CarNum = 0;
                bool bHaveChar = false;// schar.Length != 0;//强行将最高位设置为0，作为版本号
                if (bHaveChar)
                {
                    _CarNum |= 0x80000000;
                    _CarNum |= (uint)((GetCodeFromDesc(schar) & 0x3f) << 24);
                }
                bool bHaveZero = snum.StartsWith("0");
                if (bHaveZero)
                {
                    _CarNum |= 0x40000000;
                    _CarNum |= (uint)((_CarCo.Length) << 21);
                }
                _CarNum |= (uint)(i & 0xfffffff);
            }
            return _CarNum;
        }

        private int _ver = 0;
        /// <summary>
        /// 版本号
        /// </summary>
        public int ver
        {
            get
            {
                 return _ver;
            }
            set
            {
                _ver = value;
            }
        }

        private short _dir = 0;
        /// <summary>
        /// 组号
        /// </summary>      
        public short dir
        {
            get
            {
                 return _dir;
            }
            set
            {
                _dir = value;
            }
        }

        private string _station = "";
        /// <summary>
        /// 到站
        /// </summary>
        public string station
        {
            get
            {
                return _station;
            }
            set
            {
                _station = value;
            }
        }

        private string _goods = "";
        /// <summary>
        /// 品名
        /// </summary>
        public string goods
        {
            get
            {
                 return _goods;
            }
            set
            {
                _goods = value;
            }
        }

        private float _length = 0.0f;
        /// <summary>
        /// 换长
        /// </summary>
        public float length
        {
            get
            {
                 return _length;
            }
            set
            {
                _length = value;
            }
        }

        private byte _feature_enum = 0;
        /// <summary>
        /// 特征枚举
        /// </summary>
        public byte feature_enum
        {
            get
            {
                 return _feature_enum;
            }
            set
            {
                _feature_enum = value;
            }
        }

        private long _feature_bit = 0;
        /// <summary>
        /// BIT特征
        /// </summary>
        public long feature_bit
        {
            get
            {
                 return _feature_bit;
            }
            set
            {
                _feature_bit = value;
            }
        }
        /// <summary>
        /// 存在本线
        /// </summary>
        public byte inhere = 0;
        /// <summary>
        /// 其它属性
        /// </summary>
        public byte car_attr = 0;

        private int _feature_state = 0;
        /// <summary>
        /// 特征状态
        /// </summary>
        public int feature_state
        {
            get
            {
                 return _feature_state;
            }
            set
            {
                _feature_state = value;
            }
        }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string cddnote = "";

        private float _weight = 0.0f;
        /// <summary>
        /// 总重
        /// </summary>
        public float weight
        {
            get
            {
                 return _weight;
            }
            set
            {
                _weight = value;
            }
        }

        private byte _oiltype = 0;
        /// <summary>
        /// 油种
        /// </summary>
        public byte oiltype
        {
            get
            {
                 return _oiltype;
            }
            set
            {
                _oiltype = value;
            }
        }

        private string _sender = "";
        /// <summary>
        /// 发货人
        /// </summary>
        public string sender
        {
            get
            {
                 return _sender;
            }
            set
            {
                _sender = value;
            }
        }

        private string _receiver = "";
        /// <summary>
        /// 收货人
        /// </summary>
        public string receiver
        {
            get
            {
                 return _receiver;
            }
            set
            {
                _receiver = value;
            }
        }

        private string _cartype = "";
        /// <summary>
        /// 车种
        /// </summary>
        public string cartype
        {
            get
            {
                 return _cartype;
            }
            set
            {
                _cartype = value;
            }
        }

        private byte _Ecartype;
        /// <summary>
        /// 杂类码定义的车种类别
        /// </summary>
        public byte Ecartype
        {
            get
            {
                return _Ecartype;
            }
            set
            {
                _Ecartype = value;
            }
        }

        private string _note = "";
        /// <summary>
        /// 记事栏
        /// </summary>
        public string note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
            }
        }

        private int _canvansnum = 0;
        /// <summary>
        /// 篷布数
        /// </summary>
        public int canvansnum
        {
            get
            {
                return _canvansnum;
            }
            set
            {
                _canvansnum = value;
            }
        }

        private string _cover1 = "";
        /// <summary>
        /// 棚布1
        /// </summary>
        public string cover1
        {
            get
            {
                 return _cover1;
            }
            set
            {
                _cover1 = value;
            }
        }

        private string _cover2 = "";
        /// <summary>
        /// 棚布2
        /// </summary>
        public string cover2
        {
            get
            {
                 return _cover2;
            }
            set
            {
                _cover2 = value;
            }
        }

        private string _cover3 = "";
        /// <summary>
        /// 棚布3
        /// </summary>
        public string cover3
        {
            get
            {
                 return _cover3;
            }
            set
            {
                _cover3 = value;
            }
        }

        private string _trainnumarr = "";
        /// <summary>
        /// 到达车次
        /// </summary>
        public string trainnumarr
        {
            get
            {
                 return _trainnumarr;
            }
            set
            {
                _trainnumarr = value;
            }
        }

        private string _stationdep = "";
        /// <summary>
        /// 发站
        /// </summary>
        public string stationdep
        {
            get
            {
                 return _stationdep;
            }
            set
            {
                _stationdep = value;
            }
        }

        private byte _limitspeed = 0;
        /// <summary>
        /// 限速
        /// </summary>
        public byte limitspeed
        {
            get
            {
                return _limitspeed;
            }
            set
            {
                _limitspeed = value;
            }
        }

        private char _arrbur = ' ';
        /// <summary>
        /// 到局
        /// </summary>
        public char arrbur
        {
            get
            {
                return _arrbur;
            }
            set
            {
                _arrbur = value;
            }
        }

        private DateTime _arrtime = new DateTime(1999, 1, 1);
        /// <summary>
        /// 到达时刻
        /// </summary>
        public DateTime arrtime
        {
            get
            {
                 return _arrtime;
            }
            set
            {
                _arrtime = value;
            }
        }

        private DateTime _lineintime = new DateTime(1999, 1, 1);
        /// <summary>
        /// 入线时刻
        /// </summary>
        public DateTime lineintime
        {
            get
            {
                return _lineintime;
            }
            set
            {
                _lineintime = value;
            }
        }
        /// <summary>
        /// 车辆表的行
        /// </summary>
        public DataRow carRow = null;
        /// <summary>
        /// 原有的
        /// </summary>
        public bool real
        {
            get
            {
                return inhere == 0 && (car_attr & 1) == 0;
            }
            set
            {
                if (value)
                {
                    inhere = 0;
                    car_attr &= 0xfe;
                }
                else
                {
                    inhere = 1;
                    car_attr |= 1;
                }
            }
        }
        /// <summary>
        /// 是否为空车
        /// </summary>
        public bool Empty
        {
            get
            {
                return (car_attr & 2) != 0;
            }
            set
            {
                if (value)
                {
                    car_attr |= 2;
                }
                else
                {
                    car_attr &= 0xfd;
                }
            }
        }
        /// <summary>
        /// 是否为自备车
        /// </summary>
        public bool Enterprise
        {
            get
            {
                return (car_attr & 4) != 0;
            }
            set
            {
                if (value)
                {
                    car_attr |= 4;
                }
                else
                {
                    car_attr &= 0xfb;
                }
            }
        }
        /// <summary>
        /// 是否为厂内车
        /// </summary>
        public bool Factory
        {
            get
            {
                return (car_attr & 8) != 0;
            }
            set
            {
                if (value)
                {
                    car_attr |= 8;
                }
                else
                {
                    car_attr &= 0xf7;
                }
            }
        }
        /// <summary>
        /// 已删除（过期）
        /// </summary>
        public bool removed
        {
            get
            {
                return false;
            }
            set
            {
                return;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="c"></param>
        public void CopyFrom(CAR c)
        {
            carRow = c.carRow;
            oriid = c.oriid;
            id = c.id;
            dir = c.dir;
            station = c.station;
            goods = c.goods;
            weight = c.weight;
            length = c.length;
            feature_enum = c.feature_enum;
            feature_bit = c.feature_bit;
            feature_state = c.feature_state;
            inhere = c.inhere;

            cddnote = c.cddnote;
            weight = c.weight;
            oiltype = c.oiltype;
            sender = c.sender;
            receiver = c.receiver;
            cartype = c.cartype;
            Ecartype = c.Ecartype;
            note = c.note;
            canvansnum = c.canvansnum;
            cover1 = c.cover1;
            cover2 = c.cover2;
            cover3 = c.cover3;
            trainnumarr = c.trainnumarr;
            stationdep = c.stationdep;
            arrtime = c.arrtime;
            car_attr = c.car_attr;
            CarCo = c.CarCo;
            ver = c.ver;
            limitspeed = c.limitspeed;
            arrbur = c.arrbur;
            lineintime = c.lineintime;
        }
        /// <summary>
        /// 根据编码获取车号字符表示
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        static string GetDescFromCode(int code)
        {
            return "";
        }
        /// <summary>
        /// 根据车号字符表示获取编码
        /// </summary>
        /// <param name="desc">字符表示</param>
        /// <returns></returns>
        static int GetCodeFromDesc(string desc)
        {
            return 0;
        }
    }
    /// <summary>
    /// 线路
    /// </summary>
    public class TRACK
    {
        /// <summary>
        /// 线路ID
        /// </summary>
        public short id = 0;
        /// <summary>
        /// 车次
        /// </summary>
        public string trainnum = "";
        /// <summary>
        /// 车辆集合
        /// </summary>
        public CAR[] cars;
        /// <summary>
        /// 停车器位置
        /// </summary>
        public byte zero = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public byte stat = 0;
        /// <summary>
        /// 
        /// </summary>
        public const byte Buildup_Sig = 0;
        /// <summary>
        /// 
        /// </summary>
        public const byte Buildup_Pri = 1;
        /// <summary>
        /// 
        /// </summary>
        public const byte Buildup_Ass = 2;
        /// <summary>
        /// 虚拟机车数
        /// </summary>
        public const int VirtualLocomCount = 0;
        /// <summary>
        /// 线路
        /// </summary>
        public TRACK()
        {
            Clear();
        }
        /// <summary>
        /// 是否为车流（过期）
        /// </summary>
        public bool IsStream
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 封锁
        /// </summary>
        public bool LockHump
        {
            get
            {
                return (stat & 8) != 0;
            }
            set
            {
                if (value)
                    stat |= 8;
                else
                    stat &= 0xf7;
            }
        }
        /// <summary>
        /// 锁定车次
        /// </summary>
        public bool LockTN
        {
            get
            {
                return (stat & 0x10) != 0;
            }
            set
            {
                if (value)
                    stat |= 0x10;
                else
                    stat &= 0xef;
            }
        }
        /// <summary>
        /// 车次无效（过期）
        /// </summary>
        public bool DisableTN
        {
            get
            {
                return (stat & 0x20) != 0;
            }
            set
            {
                if (value)
                    stat |= 0x20;
                else
                    stat &= 0xdf;
            }
        }
        /// <summary>
        /// 过期
        /// </summary>
        public bool canPush
        {
            get
            {
                return (stat & 0x80) != 0;
            }
            set
            {
                if (value)
                    stat |= 0x80;
                else
                    stat &= 0x7f;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="t"></param>
        public void CopyFrom(TRACK t)
        {
            id = t.id;
            trainnum = t.trainnum;
            zero = t.zero;
            stat = t.stat;
            cars = new CAR[t.cars.Length];
            int i;
            for (i = 0; i < cars.Length; i++)
            {
                cars[i] = new CAR();
                cars[i].CopyFrom(t.cars[i]);
            }
        }
        /// <summary>
        /// 清除车辆
        /// </summary>
        public void Clear()
        {
            cars = new CAR[0];
            trainnum = "";
            zero = 0;
            stat = 0;
        }
    }
    /// <summary>
    /// 序列化线路数据
    /// </summary>
    public class ConvertTrackDat
    {
        const int off_cmd = 0;
        const int off_datatype = off_cmd + 1;
        const int off_tracktype = off_datatype + 1;
        const int off_tracksum = off_tracktype + 1;
        const int off_packagehead = off_tracksum + 2;
        const int off_id = 0;				//线路ID
        const int off_tn = off_id + 2;		//车次
        const int off_sum = off_tn + 10;		//辆数
        const int off_zero = off_sum + 2;		//机车位置
        const int off_stat = off_zero + 1;
        const int off_trackhead = off_stat + 1;	//头总长
        const int off_cn = 0;				//车号
        const int off_ver = off_cn + 4;		//版本号
        const int off_dir = off_ver + 4;
        const int off_w = off_dir + 2;		//重
        const int off_l = off_w + 1;		//换长*10
        const int off_ec = off_l + 1;		//枚举特征
        const int off_bc = off_ec + 1;		//BIT特征
        const int off_real = off_bc + 4;		//BIT特征
        const int off_item = off_real + 1;	//车辆数据总长
        /// <summary>
        /// 反序列化（过期）
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static TRACK[] Buf2Struct(byte[] buf)
        {
            int type;
            type = buf[off_datatype] & 0xf;
            if (type != Command.DataType_Car1 && type != Command.DataType_Car2 && type != Command.DataType_Car3)
                return null;
            int l;
            l = BitConverter.ToInt16(buf, off_tracksum);
            TRACK[] t = new TRACK[l];
            l = off_packagehead;
            int i, j;
            byte[] _cn = new byte[4];
            for (i = 0; i < t.Length; i++)
            {
                t[i] = new TRACK();
                t[i].id = BitConverter.ToInt16(buf, l + off_id);
                t[i].trainnum = System.Text.Encoding.Default.GetString(buf, l + off_tn, 10).Trim('\0').Trim();
                t[i].cars = new CAR[BitConverter.ToInt16(buf, l + off_sum)];
                t[i].zero = buf[l + off_zero];
                t[i].stat = buf[l + off_stat];
                l += off_trackhead;
                for (j = 0; j < t[i].cars.Length; j++)
                {
                    t[i].cars[j] = new CAR();
                    t[i].cars[j].CarNum = BitConverter.ToUInt32(buf, l + off_cn);
                    t[i].cars[j].ver = BitConverter.ToInt32(buf, l + off_ver);
                    t[i].cars[j].dir = BitConverter.ToInt16(buf, l + off_dir);
                    short weight;
                    weight = buf[l + off_w];
                    t[i].cars[j].length = (buf[l + off_l] & 0x3f) / 10f;
                    weight |= (short)((buf[l + off_l] & 0xc0) << 2);
                    t[i].cars[j].weight = weight;
                    t[i].cars[j].feature_enum = buf[l + off_ec];
                    t[i].cars[j].feature_bit = BitConverter.ToUInt32(buf, l + off_bc);
                    t[i].cars[j].inhere = buf[l + off_real];
                    l += off_item;
                    if (type >= Command.DataType_Car2)
                    {
                        t[i].cars[j].station = Tools.Byte2String(buf, ref l);
                        t[i].cars[j].goods = Tools.Byte2String(buf, ref l);
                        t[i].cars[j].id = BitConverter.ToInt32(buf, l);
                        l += 4;
                        if (type >= Command.DataType_Car3)
                        {
                            t[i].cars[j].feature_state = buf[l];
                            l++;
                            t[i].cars[j].cddnote = Tools.Byte2String(buf, ref l);
                        }
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// 序列化（过期）
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte[] Struct2Buf(TRACK[] t, int type)
        {
            return Struct2Buf(t, type, false);
        }
        static byte[] Struct2Buf(TRACK[] t, int type, bool all)
        {
            int l = off_packagehead;
            short a = 0;
            int i, j;
            for (i = 0; i < t.Length; i++)
            {
                if (t[i] == null)
                    continue;
                if (t[i].cars == null)
                    continue;
                if (t[i].cars.Length == 0 && t[i].trainnum == "" && !all)
                    continue;
                a++;
                l += off_trackhead;
                for (j = 0; j < t[i].cars.Length; j++)
                {
                    l += off_item;
                    if (type >= Command.DataType_Car2)
                    {
                        l += 4;
                        l += ((byte[])System.Text.Encoding.Default.GetBytes(t[i].cars[j].station)).Length + 1;
                        l += ((byte[])System.Text.Encoding.Default.GetBytes(t[i].cars[j].goods)).Length + 1;
                        if (type >= Command.DataType_Car3)
                        {
                            l++;
                            l += System.Text.Encoding.Default.GetByteCount(t[i].cars[j].cddnote) + 1;
                        }
                    }
                }
            }
            byte[] buf = new byte[l];
            buf[off_cmd] = Command.Cmd_SendCar;
            buf[off_datatype] = (byte)type;
            BitConverter.GetBytes(a).CopyTo(buf, off_tracksum);
            l = off_packagehead;
            for (i = 0; i < t.Length; i++)
            {
                if (t[i] == null)
                    continue;
                if (t[i].cars == null)
                    continue;
                if (t[i].cars.Length == 0 && t[i].trainnum == "" && !all)
                    continue;
                a = (short)t[i].id;
                BitConverter.GetBytes(a).CopyTo(buf, l + off_id);
                byte[] btn = System.Text.Encoding.Default.GetBytes(t[i].trainnum);
                if (btn.Length > 10)
                    Buffer.BlockCopy(btn, 0, buf, l + off_tn, 10);
                else
                    btn.CopyTo(buf, l + off_tn);
                a = (short)t[i].cars.Length;
                BitConverter.GetBytes(a).CopyTo(buf, l + off_sum);
                buf[l + off_zero] = t[i].zero;
                buf[l + off_stat] = t[i].stat;
                l += off_trackhead;
                for (j = 0; j < t[i].cars.Length; j++)
                {
                    BitConverter.GetBytes(t[i].cars[j].CarNum).CopyTo(buf, l + off_cn);
                    BitConverter.GetBytes(t[i].cars[j].ver).CopyTo(buf, l + off_ver);
                    a = (short)t[i].cars[j].dir;
                    BitConverter.GetBytes(a).CopyTo(buf, l + off_dir);
                    short weight = (short)t[i].cars[j].weight;
                    buf[l + off_l] = (byte)((byte)(t[i].cars[j].length * 10 + .5) | (byte)((weight >> 2) & 0xc0));
                    buf[l + off_w] = (byte)(weight & 0xff);
                    buf[l + off_ec] = (byte)t[i].cars[j].feature_enum;
                    uint feature_bit = (uint)t[i].cars[j].feature_bit;
                    BitConverter.GetBytes(feature_bit).CopyTo(buf, l + off_bc);
                    buf[l + off_real] = t[i].cars[j].inhere;
                    l += off_item;
                    if (type >= Command.DataType_Car2)
                    {
                        Tools.String2Byte(t[i].cars[j].station, buf, ref l);
                        Tools.String2Byte(t[i].cars[j].goods, buf, ref l);
                        BitConverter.GetBytes(t[i].cars[j].id).CopyTo(buf, l);
                        l += 4;
                        if (type >= Command.DataType_Car3)
                        {
                            buf[l] = (byte)(t[i].cars[j].feature_state & 0xff);
                            l++;
                            Tools.String2Byte(t[i].cars[j].cddnote, buf, ref l);
                        }
                    }
                }
            }
            return buf;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="dtLine">DM_RA_LINE</param>
        /// <param name="dtCars">DM_CC_RCARS</param>
        /// <returns></returns>
        public static byte[] DataTable2Buf(DataTable dtLine, DataTable dtCars)
        {
            return DataTable2Buf(dtLine, dtCars, null);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="dtLine">DM_RA_LINE</param>
        /// <param name="dtCars">DM_CC_RCARS</param>
        /// <param name="tsorg">排序顺序</param>
        /// <returns></returns>
        public static byte[] DataTable2Buf(DataTable dtLine, DataTable dtCars, int[] tsorg)
        {
            return DataTable2Buf(dtLine, dtCars, tsorg, false);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="dtLine">DM_RA_LINE</param>
        /// <param name="dtCars">DM_CC_RCARS</param>
        /// <param name="tsorg">排序顺序</param>
        /// <param name="all">包括无车线</param>
        /// <returns></returns>
        public static byte[] DataTable2Buf(DataTable dtLine, DataTable dtCars, int[] tsorg, bool all)
        {
            TRACK[] ts;
            DataRow[] drs1 = dtLine.Select("I_CAR_NUM>0", "I_LINE_ID");
            DataRow[] drs2;
            int i, j, z;
            bool real;
            if (drs1 == null || drs1.Length == 0)
                ts = new TRACK[0];
            else
            {
                ts = new TRACK[drs1.Length];
                for (i = 0; i < drs1.Length; i++)
                {
                    ts[i] = new TRACK();
                    ts[i].id = Convert.ToInt16(drs1[i]["I_LINE_ID"]);
                    ts[i].trainnum = drs1[i]["C_TRA_NUM"].ToString().Trim();
                    drs2 = dtCars.Select("I_LINE_ID=" + ts[i].id.ToString(), "I_CAR_IN_NO");
                    ts[i].cars = new CAR[drs2.Length];
                    z = 0;
                    real = (Convert.ToInt32(drs1[i]["B_LINE_ST_U"]) & CIPS.DB.B_LINE_ST_U.WORK) == 0;
                    for (j = 0; j < ts[i].cars.Length; j++)
                    {
                        ts[i].cars[j] = new CAR();
                        ts[i].cars[j].CarCo = drs2[j]["C_CAR_CO"].ToString();
                        ts[i].cars[j].ver = Convert.ToInt32(drs2[j]["I_VERSION_NO"]);
                        ts[i].cars[j].dir = Convert.ToInt16(drs2[j]["I_GRO_ID"]);
                        ts[i].cars[j].feature_bit = Convert.ToInt64(drs2[j]["B_CAR_CHARA"]);
                        ts[i].cars[j].feature_enum = Convert.ToByte(drs2[j]["E_CAR_CHARA"]);
                        ts[i].cars[j].length = Convert.ToSingle(drs2[j]["F_LEN"]);
                        ts[i].cars[j].weight = Convert.ToSingle(drs2[j]["F_WEGH"]);
                        if (Convert.ToInt32(drs2[j]["I_CAR_IN_NO"]) < 0)
                            z = j + 1;
                        ts[i].cars[j].real = real;
                        if (dtCars.Columns["E_SYM_WEGH"] == null)
                            ts[i].cars[j].Empty = drs2[j]["C_GOODS_NA"].ToString().Trim() == "空";
                        else
                            ts[i].cars[j].Empty = Convert.ToInt32(drs2[j]["E_SYM_WEGH"]) == CIPS.DB.E_SYM_WEGH.WEGH_NO;// ;
                    }
                    ts[i].zero = (byte)z;
                }
            }
            if (tsorg != null)
                ts = AdjustTrackOrder(ts, tsorg);
            return Struct2Buf(ts, Command.DataType_Car1, all);
        }
        /// <summary>
        /// CIPS计划系统结构序列化
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static byte[] PlanLine2Buf(CIPS.Connect.CarProfile line)
        {
            return PlanLine2Buf(line, Command.DataType_Car2, null, null);
        }
        /// <summary>
        /// CIPS计划系统结构序列化
        /// </summary>
        /// <param name="line"></param>
        /// <param name="datatype"></param>
        /// <param name="mi"></param>
        /// <param name="tsorg"></param>
        /// <returns></returns>
        public static byte[] PlanLine2Buf(CIPS.Connect.CarProfile line, int datatype, CIPS.Connect.MassInfo mi, int[] tsorg)
        {
            TRACK[] ts;
            if (line == null || line.m_cur == 0)
                ts = new TRACK[0];
            else
            {
                ts = new TRACK[line.m_cur];
                int i, j;
                for (i = 0; i < ts.Length; i++)
                {
                    ts[i] = new TRACK();
                    ts[i].id = (short)line[i].lineID;
                    ts[i].trainnum = line[i].lineAim.assemCarNum;
                    ts[i].cars = new CAR[line[i].totalCarNum];
                    line[i].port = 0;
                    ts[i].zero = (byte)line[i].stopPos;
                    ts[i].stat = 0;
                    ts[i].canPush = true;
                    for (j = 0; j < ts[i].cars.Length; j++)
                    {
                        ts[i].cars[j] = new CAR();
                        ts[i].cars[j].CarCo = line[i][j].carCode;
                        ts[i].cars[j].ver = line[i][j].verNum;
                        ts[i].cars[j].id = (int)ts[i].cars[j].id;
                        ts[i].cars[j].dir = (short)line[i][j].groID;
                        ts[i].cars[j].feature_bit = line[i][j].carBitCode;
                        ts[i].cars[j].feature_enum = (byte)line[i][j].carEnumCode;
                        ts[i].cars[j].length = (float)line[i][j].len;
                        ts[i].cars[j].weight = (float)line[i][j].weight;
                        ts[i].cars[j].station = line[i][j].arrStation;
                        ts[i].cars[j].goods = line[i][j].productName;
                        if (line[i][j].symWeigh == -1)
                            ts[i].cars[j].Empty = line[i][j].productName == "空";
                        else
                            ts[i].cars[j].Empty = line[i][j].symWeigh == CIPS.DB.E_SYM_WEGH.WEGH_NO;
                        ts[i].cars[j].real = line[i][j].isReal;
                    }
                }
            }
            if (mi != null)
            {
                int i, j, k;
                mi.curWorkRange = 1;
                for (j = 0; j < 2; j++)
                {
                    for (i = 0; i < mi.nCurPriorNum; i++)
                    {
                        if (mi[i].lineID > 0 && mi[i].planLineOrder != -1)
                        {
                            for (k = 0; k < ts.Length; k++)
                            {
                                if (mi[i].lineID == ts[k].id)
                                {
                                    if (mi[i].assemType == CIPS.DB.E_FLOW_TYPE.ASSEM_AC || mi[i].assemType == CIPS.DB.E_FLOW_TYPE.ASSEM_RE && mi[i].classID < 0)
                                        ts[k].DisableTN = true;
                                    ts[k].LockTN = mi[i].isLock;
                                    ts[k].LockHump = mi[i].Finished;
                                }
                            }
                        }
                    }
                    mi.curWorkRange = 2;
                }
            }
            if (tsorg != null)
                ts = AdjustTrackOrder(ts, tsorg);
            return Struct2Buf(ts, datatype, true);
        }

        #region 新现车广播
        static ClSet CreateClSet(int mode)
        {
            ClSet cs = new ClSet();
            ClTable ctLine = new ClTable("DM_RA_LINE");
            ctLine.AddColumn("I_LINE_ID", typeof(short));
            ctLine.AddColumn("C_TRA_NUM", typeof(string));
            ctLine.AddColumn("I_ZERO", typeof(short));
            ClTable ctCars = new ClTable("DM_CC_RCARS");
            ctCars.AddColumn("I_CAR_CO", typeof(uint));
            ctCars.AddColumn("I_VER", typeof(int));
            ctCars.AddColumn("I_LINE_ID", typeof(short));
            ctCars.AddColumn("I_GRO_ID", typeof(short));
            ctCars.AddColumn("B_CAR_CHARA", typeof(long));
            ctCars.AddColumn("E_CAR_CHARA", typeof(byte));
            ctCars.AddColumn("F_LEN", typeof(byte));
            ctCars.AddColumn("F_WEGH", typeof(short));
            ctCars.AddColumn("B_CAR_ATTR", typeof(byte));
            ctCars.AddColumn("E_CAR_TYPE", typeof(byte));
            ctCars.AddColumn("I_LIMIT_SPEED", typeof(byte));
            ctCars.AddColumn("D_LINE_IN", typeof(DateTime));
            ctCars.AddColumn("I_CANVAS_NUM", typeof(int));
            ctCars.AddColumn("C_CAR_TYPE", typeof(string));
            ctCars.AddColumn("C_STA_NA_ARR", typeof(string));
            if (mode >= Command.DataType_Car2)
            {
                ctCars.AddColumn("C_GOODS_NA", typeof(string));
                //ctCars.AddColumn("C_STA_NA_ARR", typeof(string));
                ctCars.AddColumn("I_CAR_ID", typeof(long));
                //ctCars.AddColumn("C_CAR_TYPE", typeof(string));
                ctCars.AddColumn("C_BUR_CO_ARR", typeof(UInt16));
                ctCars.AddColumn("C_RECEIVER", typeof(string));
                ctCars.AddColumn("E_OIL_TYPE",typeof(byte));
                ctCars.AddColumn("C_NOTE", typeof(string));
                if (mode == Command.DataType_Car3)
                    ctCars.AddColumn("C_CAR_INFO", typeof(string));
            }
            ClTable ctinfo = new ClTable("CarsInfo");
            ctinfo.AddColumn("C_INFO_TYPE", typeof(string));
            ctinfo.AddColumn("C_INFO_VALUE", typeof(string));
            ctinfo.AddColumn("C_STA_CO", typeof(string));
            cs.Tables.Add(ctLine);
            cs.Tables.Add(ctCars);
            cs.Tables.Add(ctinfo);
            return cs;
        }
        /// <summary>
        /// CIPS计划系统计划场结构转为CL数据集
        /// </summary>
        /// <param name="line"></param>
        /// <param name="datatype"></param>
        /// <param name="mi"></param>
        /// <param name="tsorg"></param>
        /// <returns></returns>
        public static ClSet PlanLine2ClSet(CIPS.Connect.CarProfile line, int datatype, CIPS.Connect.MassInfo mi, int[] tsorg)
        {
            TRACK[] ts = PlanLine2Track(line, datatype, mi, tsorg);
            return Tracks2ClSet(ts, datatype);
        }
        /// <summary>
        /// CIPS计划系统实际场表转为CL数据集
        /// </summary>
        /// <param name="dtLine"></param>
        /// <param name="dtCars"></param>
        /// <param name="tsorg"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public static ClSet DataTable2ClSet(DataTable dtLine, DataTable dtCars, int[] tsorg, bool all)
        {
            TRACK[] ts = DataTable2Track(dtLine, dtCars, tsorg, all);
            return Tracks2ClSet(ts);
        }
        static TRACK[] DataTable2Track(DataTable dtLine, DataTable dtCars, int[] tsorg, bool all)
        {
            TRACK[] ts;
            DataRow[] drs1 = dtLine.Select("", "I_LINE_ID");
            DataRow[] drs2;
            int i, j, z;
            bool real;
            if (drs1 == null || drs1.Length == 0)
                ts = new TRACK[0];
            else
            {
                ts = new TRACK[drs1.Length];
                for (i = 0; i < drs1.Length; i++)
                {
                    ts[i] = new TRACK();
                    ts[i].id = Convert.ToInt16(drs1[i]["I_LINE_ID"]);
                    ts[i].trainnum = drs1[i]["C_TRA_NUM"].ToString().Trim();
                    drs2 = dtCars.Select("I_LINE_ID=" + ts[i].id.ToString(), "I_CAR_IN_NO");
                    ts[i].cars = new CAR[drs2.Length];
                    z = 0;
                    real = (Convert.ToInt32(drs1[i]["B_LINE_ST_U"]) & CIPS.DB.B_LINE_ST_U.WORK) == 0;
                    for (j = 0; j < ts[i].cars.Length; j++)
                    {
                        ts[i].cars[j] = new CAR();
                        ts[i].cars[j].CarCo = drs2[j]["C_CAR_CO"].ToString();
                        ts[i].cars[j].ver = Convert.ToInt32(drs2[j]["I_VERSION_NO"]);
                        ts[i].cars[j].dir = Convert.ToInt16(drs2[j]["I_GRO_ID"]);
                        ts[i].cars[j].feature_bit = Convert.ToInt64(drs2[j]["B_CAR_CHARA"]);
                        ts[i].cars[j].feature_enum = Convert.ToByte(drs2[j]["E_CAR_CHARA"]);
                        ts[i].cars[j].length = Convert.ToSingle(drs2[j]["F_LEN"]);
                        ts[i].cars[j].weight = Convert.ToSingle(drs2[j]["F_WEGH"]);
                        if (Convert.ToInt32(drs2[j]["I_CAR_IN_NO"]) < 0)
                            z = j + 1;
                        ts[i].cars[j].real = real;
                        if (dtCars.Columns["E_SYM_WEGH"] == null)
                            ts[i].cars[j].Empty = drs2[j]["C_GOODS_NA"].ToString().Trim() == "空";
                        else
                            ts[i].cars[j].Empty = Convert.ToInt32(drs2[j]["E_SYM_WEGH"]) == CIPS.DB.E_SYM_WEGH.WEGH_NO;//.ToString().Trim() == "空";
                        ts[i].cars[j].Ecartype = Convert.ToByte(drs2[j]["E_CAR_TYPE"]);
                        ts[i].cars[j].limitspeed = Convert.ToByte(drs2[j]["I_LIMIT_SPEED"]);
                    }
                    ts[i].zero = (byte)z;
                }
            }
            if (tsorg != null)
                ts = AdjustTrackOrder(ts, tsorg);
            return ts;
        }

        public static TRACK PlanLine2Track(CIPS.Connect.PlanLine line)
        {
            if (line == null)
                return null;
            TRACK t = new TRACK();
            int port = line.port;
            t.id = (short)line.lineID;
            t.trainnum = line.lineAim.assemCarNum;
            t.cars = new CAR[line.totalCarNum];
            line.port = 0;
            t.zero = (byte)line.stopPos;
            t.stat = 0;
            t.canPush = true;
            for (int j = 0; j < t.cars.Length; j++)
            {
                t.cars[j] = new CAR();
                t.cars[j].CarCo = line[j].carCode;
                t.cars[j].ver = line[j].verNum;
                t.cars[j].id = (int)t.cars[j].CarNum;
                t.cars[j].dir = (short)line[j].groID;
                t.cars[j].feature_bit = line[j].carBitCode;
                t.cars[j].feature_enum = (byte)line[j].carEnumCode;
                t.cars[j].length = (float)line[j].len;
                t.cars[j].weight = (float)line[j].weight;
                t.cars[j].Ecartype = (byte)line[j].eCarType;
                t.cars[j].limitspeed = (byte)line[j].limitSpeed;
                t.cars[j].station = line[j].arrStation;
                t.cars[j].goods = line[j].productName;
                t.cars[j].Empty = line[j].symWeigh == CIPS.DB.E_SYM_WEGH.WEGH_NO;
                t.cars[j].real = line[j].isReal;
                t.cars[j].cartype = line[j].carType;
                t.cars[j].oiltype = (byte)line[j].oiltype;
                t.cars[j].receiver = line[j].consignee;
                t.cars[j].note = line[j].cremark;
                if (line[j].arrBurCo.Trim().Length > 0)
                    t.cars[j].arrbur = line[j].arrBurCo.Trim()[0];
            }
            line.port = port;
            return t;
        }

        static TRACK[] PlanLine2Track(CIPS.Connect.CarProfile line, int datatype, CIPS.Connect.MassInfo mi, int[] tsorg)
        {
            TRACK[] ts;
            string strTrainNum = "";
            if (line == null || line.m_cur == 0)
                ts = new TRACK[0];
            else
            {
                ts = new TRACK[line.m_cur];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = PlanLine2Track(line[i]);
                    if(datatype == CIPS.Express.ConvertDat.Command.DataType_Car1)
                    {
                        strTrainNum = "";
                        for(int j = 0;j<line[i].traNumLockedInfo.Length;j++)
                        {
                            if (line[i].traNumLockedInfo[j].traNum == "")
                                continue;
                            strTrainNum += line[i].traNumLockedInfo[j].traNum;
                            if(line[i].traNumLockedInfo[j].locked)
                            {
                                strTrainNum += "\r";
                                strTrainNum += line[i].traNumLockedInfo[j].start.ToString() + "\r";
                                strTrainNum += line[i].traNumLockedInfo[j].end.ToString();
                            }
                            //if (j != line[i].traNumLockedInfo.Length - 1)
                                strTrainNum += "\n";
                        }
                        strTrainNum = strTrainNum.TrimEnd('\n');
                        if (strTrainNum != "")
                            ts[i].trainnum = strTrainNum;
                    }
                }
            }
            if (mi != null)
            {
                int i, j, k;
                mi.curWorkRange = 1;
                for (j = 0; j < 2; j++)
                {
                    for (i = 0; i < mi.nCurPriorNum; i++)
                    {
                        if (mi[i].lineID > 0 && mi[i].planLineOrder != -1)
                        {
                            for (k = 0; k < ts.Length; k++)
                            {
                                if (mi[i].lineID == ts[k].id)
                                {
                                    if (mi[i].assemType == CIPS.DB.E_FLOW_TYPE.ASSEM_AC || mi[i].assemType == CIPS.DB.E_FLOW_TYPE.ASSEM_RE && mi[i].classID < 0)
                                        ts[k].DisableTN = true;
                                    ts[k].LockTN = mi[i].isLock;
                                    ts[k].LockHump = mi[i].Finished;
                                }
                            }
                        }
                    }
                    mi.curWorkRange = 2;
                }
            }
            if (tsorg != null)
                ts = AdjustTrackOrder(ts, tsorg);
            return ts;
        }
        /// <summary>
        /// 结构转为CL数据集
        /// </summary>
        /// <param name="ts">线路集合</param>
        /// <returns></returns>
        public static ClSet Tracks2ClSet(TRACK[] ts)
        {
            return Tracks2ClSet(ts, Command.DataType_Car1);
        }
        /// <summary>
        /// 结构转为CL数据集
        /// </summary>
        /// <param name="ts">线路集合</param>
        /// <param name="mode">方式</param>
        /// <returns></returns>
        public static ClSet Tracks2ClSet(TRACK[] ts, int mode)
        {
            ClSet cs = CreateClSet(mode);
            ClTable ctLine = cs.Tables["DM_RA_LINE"];
            ClTable ctCars = cs.Tables["DM_CC_RCARS"];
            ClRow crline, crcar;
            foreach (TRACK t in ts)
            {
                if (t == null)
                    continue;
                crline = ctLine.NewRow();
                crline["I_LINE_ID"] = t.id;
                crline["C_TRA_NUM"] = t.trainnum;
                crline["E_STATE"] = t.stat;
                crline["I_ZERO"] = t.zero;
                ctLine.Rows.Add(crline);
                foreach (CAR car in t.cars)
                {
                    crcar = ctCars.NewRow();
                    crcar["I_CAR_CO"] = car.CarNum;
                    crcar["I_VER"] = car.ver;
                    crcar["I_LINE_ID"] = t.id;
                    crcar["I_GRO_ID"] = car.dir;
                    crcar["B_CAR_CHARA"] = car.feature_bit;
                    crcar["E_CAR_CHARA"] = car.feature_enum;
                    crcar["F_LEN"] = (short)(car.length * 10 + .5f);
                    //crcar["F_WEGH"] = car.weight;
                    crcar["F_WEGH"] = (short)(car.weight * 10 + .5f);
                    crcar["E_CAR_TYPE"] = car.Ecartype;
                    crcar["C_STA_NA_ARR"] = car.station;
                    crcar["C_GOODS_NA"] = car.goods;
                    crcar["I_CAR_ID"] = car.id;
                    crcar["C_CAR_INFO"] = car.cddnote;
                    byte attr = (byte)(car.car_attr & 0xfe);
                    if (!car.real)
                        attr |= 1;
                    crcar["B_CAR_ATTR"] = attr;
                    crcar["C_CAR_TYPE"] = car.cartype;
                    crcar["I_LIMIT_SPEED"] = car.limitspeed;
                    crcar["C_BUR_CO_ARR"] = (UInt16)car.arrbur;
                    crcar["C_RECEIVER"] = car.receiver;
                    crcar["E_OIL_TYPE"] = car.oiltype;
                    crcar["C_NOTE"] = car.note;
                    crcar["D_LINE_IN"] = car.lineintime;
                    crcar["I_CANVAS_NUM"] = car.canvansnum;
                    ctCars.Rows.Add(crcar);
                }
            }
            return cs;
        }
        static int GetCarsCountInTrack(ClTable ct, int offset, int lineid, int colomlid)
        {
            int cnt = 0;
            for (int i = offset; i < ct.Rows.Count; i++)
            {
                if (Convert.ToInt32(ct.Rows[i][colomlid]) == lineid)
                    cnt++;
                else
                    break;
            }
            return cnt;
        }
        /// <summary>
        /// CL数据集转为结构
        /// </summary>
        /// <param name="cs">CL数据集</param>
        /// <returns></returns>
        public static TRACK[] ClSet2Track(ClSet cs)
        {
            ClTable ctLine = cs.Tables["DM_RA_LINE"];
            ClTable ctCars = cs.Tables["DM_CC_RCARS"];
            TRACK[] ts;
            int col_carco = ctCars.GetColumnIndex("I_CAR_CO");
            int col_ver = ctCars.GetColumnIndex("I_VER");
            int col_gro = ctCars.GetColumnIndex("I_GRO_ID");
            int col_bchar = ctCars.GetColumnIndex("B_CAR_CHARA");
            int col_echar = ctCars.GetColumnIndex("E_CAR_CHARA");
            int col_len = ctCars.GetColumnIndex("F_LEN");
            int col_wegh = ctCars.GetColumnIndex("F_WEGH");
            int col_attr = ctCars.GetColumnIndex("B_CAR_ATTR");
            int col_ecartype = ctCars.GetColumnIndex("E_CAR_TYPE");
            int col_sta = ctCars.GetColumnIndex("C_STA_NA_ARR");
            int col_goods = ctCars.GetColumnIndex("C_GOODS_NA");
            int col_id = ctCars.GetColumnIndex("I_CAR_ID");
            int col_carinfo = ctCars.GetColumnIndex("C_CAR_INFO");
            int col_cartype = ctCars.GetColumnIndex("C_CAR_TYPE");
            int col_limitspeed = ctCars.GetColumnIndex("I_LIMIT_SPEED");
            int col_arrbur = ctCars.GetColumnIndex("C_BUR_CO_ARR");
            int col_receiver = ctCars.GetColumnIndex("C_RECEIVER");
            int col_oil_type = ctCars.GetColumnIndex("E_OIL_TYPE");
            int col_note = ctCars.GetColumnIndex("C_NOTE");
            int col_line = ctCars.GetColumnIndex("I_LINE_ID");
            int col_lineintime = ctCars.GetColumnIndex("D_LINE_IN");
            int col_canvas_num = ctCars.GetColumnIndex("I_CANVAS_NUM");
            int col_line_line = ctLine.GetColumnIndex("I_LINE_ID");
            int col_line_tname = ctLine.GetColumnIndex("C_TRA_NUM");
            int col_line_zero = ctLine.GetColumnIndex("I_ZERO");
            int col_line_state = ctLine.GetColumnIndex("E_STATE");
            int i, j;
            ts = new TRACK[ctLine.Rows.Count];
            int caroffset = 0;
            ClRow cr;
            for (i = 0; i < ts.Length; i++)
            {
                ClRow crline = ctLine.Rows[i];
                ts[i] = new TRACK();
                ts[i].id = Convert.ToInt16(crline[col_line_line]);
                ts[i].trainnum = crline[col_line_tname].ToString().Trim();
                ts[i].zero = Convert.ToByte(crline[col_line_zero]);
                ts[i].stat = Convert.ToByte(crline[col_line_state]);
                int cnt = GetCarsCountInTrack(ctCars, caroffset, ts[i].id, col_line);
                ts[i].cars = new CAR[cnt];
                for (j = 0; j < ts[i].cars.Length; j++)
                {
                    cr = ctCars.Rows[j + caroffset];
                    ts[i].cars[j] = new CAR();
                    ts[i].cars[j].CarNum = Convert.ToUInt32(cr[col_carco].ToString());
                    ts[i].cars[j].ver = Convert.ToInt32(cr[col_ver]);
                    ts[i].cars[j].dir = Convert.ToInt16(cr[col_gro]);
                    ts[i].cars[j].feature_bit = Convert.ToInt64(cr[col_bchar]);
                    ts[i].cars[j].feature_enum = Convert.ToByte(cr[col_echar]);
                    ts[i].cars[j].length = Convert.ToSingle(cr[col_len]) / 10f;
                    ts[i].cars[j].weight = Convert.ToInt16(cr[col_wegh]) / 10f;
                    ts[i].cars[j].car_attr = Convert.ToByte(cr[col_attr]);
                    ts[i].cars[j].inhere = (byte)(ts[i].cars[j].car_attr & 1);
                    ts[i].cars[j].Ecartype = Convert.ToByte(cr[col_ecartype]);
                    ts[i].cars[j].limitspeed = Convert.ToByte(cr[col_limitspeed]);
                    ts[i].cars[j].lineintime = Convert.ToDateTime(cr[col_lineintime]);
                    if (col_sta >= 0)
                    {
                        ts[i].cars[j].station = Convert.ToString(cr[col_sta]);
                        ts[i].cars[j].goods = Convert.ToString(cr[col_goods]);
                        ts[i].cars[j].id = Convert.ToInt32(cr[col_id]);
                        if(col_cartype>=0)
                            ts[i].cars[j].cartype = cr[col_cartype].ToString();
                        if(col_arrbur>=0)
                            ts[i].cars[j].arrbur = Convert.ToChar(cr[col_arrbur]);
                        if(col_receiver>=0)
                            ts[i].cars[j].receiver = cr[col_receiver].ToString();
                        if(col_oil_type>=0)
                            ts[i].cars[j].oiltype = Convert.ToByte(cr[col_oil_type]);
                        if(col_note>=0)
                            ts[i].cars[j].note = cr[col_note].ToString();
                    }
                    if (col_carinfo >= 0)
                        ts[i].cars[j].cddnote = Convert.ToString(cr[col_carinfo]);
                    if (col_canvas_num >= 0)
                        ts[i].cars[j].canvansnum = Convert.ToInt32(cr[col_canvas_num]);
                }
                caroffset += cnt;
            }
            return ts;
        }
        #endregion

        #region 调整线路顺序
        int[] lineid;
        /// <summary>
        /// 线路结构转换
        /// </summary>
        /// <param name="ts">线路顺序</param>
        public ConvertTrackDat(TRACK[] ts)
        {
            lineid = new int[ts.Length];
            int i;
            for (i = 0; i < lineid.Length; i++)
                lineid[i] = ts[i].id;
        }
        /// <summary>
        /// 调整顺序
        /// </summary>
        /// <param name="ts0">原线路集合</param>
        /// <returns></returns>
        public TRACK[] AdjustTrackOrder(TRACK[] ts0)
        {
            return AdjustTrackOrder(ts0, lineid);
        }
        /// <summary>
        /// 调整顺序
        /// </summary>
        /// <param name="ts0">原线路集合</param>
        /// <param name="lineid">新顺序</param>
        /// <returns></returns>
        public static TRACK[] AdjustTrackOrder(TRACK[] ts0, int[] lineid)
        {
            int i, j, n;
            if (ts0.Length > lineid.Length)
                i = lineid.Length;
            else
                i = ts0.Length;
            TRACK[] ts = new TRACK[i];
            n = 0;
            for (i = 0; i < lineid.Length; i++)
            {
                for (j = 0; j < ts0.Length; j++)
                {
                    if (lineid[i] == ts0[j].id)
                    {
                        ts[n] = ts0[j];
                        n++;
                        break;
                    }
                }
                if (n == ts.Length)
                    break;
            }
            if (n < ts.Length)
            {
                TRACK[] ts1 = new TRACK[n];
                for (i = 0; i < ts1.Length; i++)
                    ts1[i] = ts[i];
                ts = ts1;
            }
            return ts;
        }
        /// <summary>
        /// 调整顺序
        /// </summary>
        /// <param name="ts0">原线路集合</param>
        /// <param name="tsorg">新顺序</param>
        /// <returns></returns>
        public static TRACK[] AdjustTrackOrder(TRACK[] ts0, TRACK[] tsorg)
        {
            int[] lineid = new int[tsorg.Length];
            int i;
            for (i = 0; i < lineid.Length; i++)
                lineid[i] = tsorg[i].id;
            return AdjustTrackOrder(ts0, lineid);
        }
        #endregion
    }

    #region 非运用车
    /// <summary>
    /// 备用车
    /// </summary>
    public class BackupCar
    {
        /// <summary>
        /// 内容
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string name = "";
            /// <summary>
            /// 是否为BIT
            /// </summary>
            public bool bit;
            /// <summary>
            /// 值
            /// </summary>
            public uint value;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="na"></param>
            /// <param name="b"></param>
            /// <param name="v"></param>
            public Item(string na, bool b, uint v)
            {
                name = na;
                bit = b;
                value = v;
            }
        }
        static Item[] item = new Item[] { 
            new Item("备用",false,CIPS.DB.E_CAR_CHARA.CAR_SPARE),
            new Item("站修",true,CIPS.DB.B_CAR_CHARA.CAR_STA),
            new Item("段修",true,CIPS.DB.B_CAR_CHARA.DEPO_BIT),
            new Item("修装",true,CIPS.DB.B_CAR_CHARA.REPAIRLOAD_BIT),
            new Item("倒装",true,CIPS.DB.B_CAR_CHARA.SPILLLOAD_BIT),
            new Item("厂修",true,CIPS.DB.B_CAR_CHARA.TRIANGLE_BIT)};
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string Name(int i)
        {
            if (i < item.Length)
                return item[i].name;
            else
                return null;
        }
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Item Content(int i)
        {
            if (i < item.Length)
                return item[i];
            else
                return null;
        }
        /// <summary>
        /// 数量
        /// </summary>
        public static int Count
        {
            get
            {
                return item.Length;
            }
        }
    }
    #endregion

    #region 集结方向
    /// <summary>
    /// 编组计划
    /// </summary>
    [Serializable]
    public class ClassiTrack
    {
        /// <summary>
        /// 线路
        /// </summary>
        public short track = 0;
        /// <summary>
        /// 编组计划ID
        /// </summary>
        public int classiid = 0;
        /// <summary>
        /// 车次
        /// </summary>
        public string trainnum = "";
        /// <summary>
        /// 编组计划名
        /// </summary>
        public string classiname = "";
        /// <summary>
        /// 状态
        /// </summary>
        public uint state = 0;
        /// <summary>
        /// 站码
        /// </summary>
        public string stationCode = "";
        /// <summary>
        /// 类型
        /// </summary>
        public byte[] type = new byte[0];
        /// <summary>
        /// 辆数上下限
        /// </summary>
        public short[] carcount = new short[2];
        /// <summary>
        /// 总重上下限
        /// </summary>
        public short[] weight = new short[2];
        /// <summary>
        /// 换长上下限
        /// </summary>
        public float[] length = new float[2];
        /// <summary>
        /// 组号
        /// </summary>
        public System.Collections.ArrayList groups = new System.Collections.ArrayList();
        /// <summary>
        /// 说明
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return classiname;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool Empty
        {
            get
            {
                return groups.Count == 0;
            }
        }
        /// <summary>
        /// 是否为部分
        /// </summary>
        public bool Part
        {
            get
            {
                foreach (short[] gs in groups)
                {
                    foreach (short g in gs)
                    {
                        if (g < 0)
                            return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public byte Type
        {
            get
            {
                if (type.Length == 1)
                    return type[0];
                else
                    return CIPS.DB.E_CLASSI_REQ.CLA_NULL;
            }
        }
        /// <summary>
        /// 人工标置
        /// </summary>
        public bool Manual
        {
            get
            {
                return (state & 1) != 0;
            }
            set
            {
                if (value)
                    state |= 1;
                else
                    state &= ~(uint)1;
            }
        }
        /// <summary>
        /// 交流车
        /// </summary>
        public bool Exchange
        {
            get
            {
                return classiname.IndexOf("交流") >= 0;
            }
        }
        /// <summary>
        /// 乱车
        /// </summary>
        public bool Confusion
        {
            get
            {
                return classiname.IndexOf("乱车") >= 0;
            }
        }
    }
    #endregion
}
