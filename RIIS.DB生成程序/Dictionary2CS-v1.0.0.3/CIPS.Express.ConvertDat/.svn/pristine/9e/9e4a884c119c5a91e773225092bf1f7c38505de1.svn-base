using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// 调车位置集合
    /// </summary>
    public class LocomInfoList:List<LocomInfoList.Locom>
    {
        /// <summary>
        /// 调机
        /// </summary>
        public class Locom
        {
            internal string name;
            internal short speed;
            internal string sectionname;
            internal byte state;
            internal byte location;
            //ClRow cr;
            //internal Locom(ClRow _cr)
            //{
            //    cr = _cr;
            //}
            /// <summary>
            /// 调机名
            /// </summary>
            public string LocomName
            {
                get
                {
                    return name;
                }
                set
                {
                    if (value != null)
                        name = value;
                }
            }
            /// <summary>
            /// 所在区段名
            /// </summary>
            public string SectionName
            {
                get
                {
                    return sectionname;
                }
                set
                {
                    if (value != null)
                        sectionname = value;
                }
            }
            /// <summary>
            /// 速度
            /// </summary>
            public float Speed
            {
                get
                {
                    return speed / 10f;
                }
                set
                {
                    speed = (short)(value * 10);
                }
            }
            /// <summary>
            /// 位置
            /// </summary>
            public int Location
            {
                get
                {
                    return location;
                }
                set
                {
                    location = (byte)value;
                }
            }
            /// <summary>
            /// 是否为自动驾驶
            /// </summary>
            public bool Auto
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
                        state &= 0xfe;
                }
            }
            /// <summary>
            /// 故障
            /// </summary>
            public bool Error
            {
                get
                {
                    return (state & 2) != 0;
                }
                set
                {
                    if (value)
                        state |= 2;
                    else
                        state &= 0xfd;
                }
            }
            /// <summary>
            /// 方向
            /// </summary>
            public bool Direction
            {
                get
                {
                    return (state & 4) != 0;
                }
                set
                {
                    if (value)
                        state |= 4;
                    else
                        state &= 0xfb;
                }
            }
        }
        //private List<Locom> locoms = new List<Locom>();
        /// <summary>
        /// 
        /// </summary>
        public LocomInfoList()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs"></param>
        public LocomInfoList(ClSet cs)
        {
            Init(cs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        public LocomInfoList(byte[] buf)
        {
            Init(new ClSet(buf, 2));
        }
        private byte sysstate = 0;
        /// <summary>
        /// 调机自动化系统状态
        /// </summary>
        public byte SystemState
        {
            get
            {
                return sysstate;
            }
            set
            {
                sysstate = value;
            }
        }
        private void Init(ClSet cs)
        {
            ClTable ct = cs.Tables["Locoms"];
            if (ct == null)
                return;
            int idna = 0, idsna = 1, idsp = 2, idlo = 3, idst = 4;
            foreach (ClRow cr in ct.Rows)
            {
                Locom l = new Locom();
                l.LocomName = cr[idna].ToString();
                l.sectionname = cr[idsna].ToString();
                l.speed = Convert.ToInt16(cr[idsp]);
                l.location = Convert.ToByte(cr[idlo]);
                l.state = Convert.ToByte(cr[idst]);
                Add(l);
            }
            ct = cs.Tables["System"];
            ClRow _cr = ct.Rows[0];
            sysstate = (byte)Convert.ToInt32(_cr["INFO"]);
        }
        /// <summary>
        /// 机车元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Locom this[string name]
        {
            get
            {
                foreach (Locom l in this)
                {
                    if (l.LocomName == name)
                        return l;
                }
                return null;
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            ClSet cs = new ClSet();
            ClTable ct = new ClTable("System");
            ct.AddColumn("INFO", typeof(int));
            cs.Tables.Add(ct);
            ClRow _cr = ct.NewRow();
            _cr[0] = sysstate;
            ct.Rows.Add(_cr);
            int idna = 0, idsna = 1, idsp = 2, idlo = 3, idst = 4;
            ct = new ClTable("Locoms");
            ct.AddColumn("NAME", typeof(string));
            ct.AddColumn("SECTIONNA", typeof(string));
            ct.AddColumn("SPEED", typeof(short));
            ct.AddColumn("LOCATION", typeof(byte));
            ct.AddColumn("STATE", typeof(byte));
            cs.Tables.Add(ct);
            foreach (Locom l in this)
            {
                ClRow cr = ct.NewRow();
                cr[idna] = l.name;
                cr[idsna] = l.sectionname;
                cr[idsp] = l.speed;
                cr[idlo] = l.location;
                cr[idst] = l.state;
                ct.Rows.Add(cr);
            }
            return cs.Serialize(2);
        }
    }
}
