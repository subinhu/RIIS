using System;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// Plan 的摘要说明。
    /// </summary>
    /// 
    public class PLANINDEX
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        public int id;
        /// <summary>
        /// 岗位ID
        /// </summary>
        public ushort subid;
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime startTime_plan = new DateTime(1999, 1, 1);
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime endTime_plan = new DateTime(1999, 1, 1);
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime startTime_real = new DateTime(1999, 1, 1);
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime endTime_real = new DateTime(1999, 1, 1);
        /// <summary>
        /// 流程状态
        /// </summary>
        public ushort state;
        /// <summary>
        /// 计划号
        /// </summary>
        public ushort plannum;	//计划号
        /// <summary>
        /// 车次
        /// </summary>
        public string trainnum = "";	//
        /// <summary>
        /// 调机
        /// </summary>
        public short locom;
        /// <summary>
        /// 调车组
        /// </summary>
        public string locomworker = "";
        /// <summary>
        /// 注意事项
        /// </summary>
        public string remark = "";
        /// <summary>
        /// 场间线路
        /// </summary>
        public int unyard = 0;
        /// <summary>
        /// 编制人
        /// </summary>
        public string maker = "";
        /// <summary>
        /// 第一勾序号
        /// </summary>
        public byte StartNum = 0;
        /// <summary>
        /// 对应确报
        /// </summary>
        public int consid = 0;
        /// <summary>
        /// 流程类型
        /// </summary>
        public short flowtype = 0;
        /// <summary>
        /// 自定义流程类型描述(车站定义的流程类型)
        /// </summary>
        public string cflowtype = "";
        /// <summary>
        /// 相关对象
        /// </summary>
        public object Tag = null;
        /// <summary>
        /// 
        /// </summary>
        public PLANINDEX()
        {
            startTime_plan = new DateTime(1999, 1, 1);
            endTime_plan = new DateTime(1999, 1, 1);
            startTime_real = new DateTime(1999, 1, 1);
            endTime_real = new DateTime(1999, 1, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool dir
        {
            get
            {
                return (subid & 0x4000) != 0;
            }
            set
            {
                if (value)
                    subid |= 0x4000;
                else
                    subid &= 0x3fff;
            }
        }

        //public bool dir
        //{
        //    get
        //    {
        //        return (subid & 0x8000) != 0;
        //    }
        //    set
        //    {
        //        if (value)
        //            subid |= 0x8000;
        //        else
        //            subid &= 0x7fff;
        //    }
        //}
        /// <summary>
        /// 是否为普通勾计划
        /// </summary>
        public bool isPlan
        {

            get
            {
               
                if((Type > 700 && Type < 800)  ||  Type == Type_GetPut || Type == Type_NormalPlan)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 岗位
        /// </summary>
        public int WorkStation
        {
            get
            {
                return subid & 0x3fff;
            }
            set
            {
                subid &= 0x4000;
                subid |= ((ushort)(value & 0x3fff));
            }
        }

        //public int WorkStation
        //{
        //    get
        //    {
        //        return subid & 0x7fff;
        //    }
        //    set
        //    {
        //        subid &= 0x8000;
        //        subid |= ((byte)(value & 0x7fff));
        //    }
        //}


        /// <summary>
        /// 第一勾源股道
        /// </summary>
        public short StartTrack = 0;
        /// <summary>
        /// 攻取流程类型集合
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Generic.List<Tools.ID_NAME> GetFlowTypeList()
        {
            System.Collections.Generic.List<Tools.ID_NAME> l = new System.Collections.Generic.List<Tools.ID_NAME>();
            l.Add(new Tools.ID_NAME(Type_Split, "解体"));
            l.Add(new Tools.ID_NAME(Type_Classi, "编组"));
            l.Add(new Tools.ID_NAME(Type_GetPut, "取送"));
            l.Add(new Tools.ID_NAME(Type_ToCons, "发车"));
            l.Add(new Tools.ID_NAME(Type_FromCons, "接车"));
            l.Add(new Tools.ID_NAME(Type_ModifyCars, "修改现车"));
            l.Add(new Tools.ID_NAME(Type_ExistCars, "站存"));
            l.Add(new Tools.ID_NAME(Type_装车, "装车"));
            l.Add(new Tools.ID_NAME(Type_卸车, "卸车"));
            l.Add(new Tools.ID_NAME(Type_加冰, "加冰"));
            l.Add(new Tools.ID_NAME(Type_洗刷, "洗刷"));
            l.Add(new Tools.ID_NAME(Type_过磅, "过磅"));
            l.Add(new Tools.ID_NAME(Type_消毒, "消毒"));
            l.Add(new Tools.ID_NAME(Type_扣修装, "扣修装"));
            l.Add(new Tools.ID_NAME(Type_扣待处, "扣待处"));
            l.Add(new Tools.ID_NAME(Type_扣倒装, "扣倒装"));
            l.Add(new Tools.ID_NAME(Type_置关门, "置关门"));
            l.Add(new Tools.ID_NAME(Type_加冰毕, "加冰毕"));
            l.Add(new Tools.ID_NAME(Type_洗刷毕, "洗刷毕"));
            l.Add(new Tools.ID_NAME(Type_过磅毕, "过磅毕"));
            l.Add(new Tools.ID_NAME(Type_消毒毕, "消毒毕"));
            l.Add(new Tools.ID_NAME(Type_修装毕, "修装毕"));
            l.Add(new Tools.ID_NAME(Type_解除待处, "解除待处"));
            l.Add(new Tools.ID_NAME(Type_扣车, "扣车"));
            l.Add(new Tools.ID_NAME(Type_解除扣车, "解除扣车"));
            l.Add(new Tools.ID_NAME(Type_清关门, "清关门"));
            l.Add(new Tools.ID_NAME(Type_运非_转备用, "转备用"));
            l.Add(new Tools.ID_NAME(Type_运非_转军用, "转军用"));
            l.Add(new Tools.ID_NAME(Type_运非_扣站修, "扣站修"));
            l.Add(new Tools.ID_NAME(Type_运非_扣段修, "扣段修"));
            l.Add(new Tools.ID_NAME(Type_运非_扣厂修, "扣厂修"));
            l.Add(new Tools.ID_NAME(Type_运非_扣集结, "扣集结"));
            l.Add(new Tools.ID_NAME(Type_非运_解除备用, "解除备用"));
            l.Add(new Tools.ID_NAME(Type_非运_解除军用, "解除军用"));
            l.Add(new Tools.ID_NAME(Type_非运_修峻, "修峻"));
            l.Add(new Tools.ID_NAME(Type_装交, "装交"));
            l.Add(new Tools.ID_NAME(Type_装交取消, "取消装交"));
            l.Add(new Tools.ID_NAME(Type_卸交, "卸交"));
            l.Add(new Tools.ID_NAME(Type_卸交取消, "取消卸交"));
            l.Add(new Tools.ID_NAME(Type_非运_取消集结, "取消集结"));
            l.Add(new Tools.ID_NAME(Type_修改现车, "修改现车"));
            l.Add(new Tools.ID_NAME(Type_移动现车, "移动现车"));
            l.Add(new Tools.ID_NAME(Type_添加现车, "添加现车"));
            l.Add(new Tools.ID_NAME(Type_删除现车, "删除现车"));
            l.Add(new Tools.ID_NAME(Type_修改车号, "修改车号"));
            l.Add(new Tools.ID_NAME(Type_修改装车, "修改装车"));
            l.Add(new Tools.ID_NAME(Type_取消装车, "取消装车"));
            l.Add(new Tools.ID_NAME(Type_取消卸车, "取消卸车"));
            l.Add(new Tools.ID_NAME(Type_运非_过轨, "过轨转非"));
            l.Add(new Tools.ID_NAME(Type_非运_过轨, "过轨转运"));
            l.Add(new Tools.ID_NAME(Type_报废, "报废"));
            l.Add(new Tools.ID_NAME(Type_取消报废, "取消报废"));
            l.Add(new Tools.ID_NAME(Type_扣蘑, "扣缺球头"));
            l.Add(new Tools.ID_NAME(Type_扣封存, "扣封存"));
            l.Add(new Tools.ID_NAME(Type_取消封存, "取消封存"));
            l.Add(new Tools.ID_NAME(Type_验配, "验配"));
            l.Add(new Tools.ID_NAME(Type_批卸车线, "批卸车线"));
            l.Add(new Tools.ID_NAME(Type_批装车线, "批装车线"));
            l.Add(new Tools.ID_NAME(Type_设置限速, "设置限速"));
            l.Add(new Tools.ID_NAME(Type_取消限速, "取消限速"));
            l.Add(new Tools.ID_NAME(Type_自定义车辆特征, "修改车辆特征"));
            return l;
        }
        /// <summary>
        /// 流程类型!
        /// 普通勾计划!
        /// </summary>
        public const int Type_NormalPlan = -1;
        /// <summary>
        /// 流程类型
        /// 解体
        /// </summary>
        public const int Type_Split = CIPS.DB.E_FLOW_TYPE.SWIT_SPLIT;
        /// <summary>
        /// 流程类型
        /// 编组
        /// </summary>
        public const int Type_Classi = CIPS.DB.E_FLOW_TYPE.SWIT_CLASSI;
        /// <summary>
        /// 流程类型
        /// 取送
        /// </summary>
        public const int Type_GetPut = CIPS.DB.E_FLOW_TYPE.SWIT_GETPUT;
        /// <summary>
        /// 流程类型!
        /// 现车转确报!
        /// </summary>
        public const int Type_ToCons = CIPS.DB.E_FLOW_TYPE.TRA_DEPARTURE;
        /// <summary>
        /// 流程类型!
        /// 确报转现车!
        /// </summary>
        public const int Type_FromCons = CIPS.DB.E_FLOW_TYPE.TRA_RECEPTION;
        /// <summary>
        /// 流程类型!
        /// 修改现车!
        /// </summary>
        public const int Type_ModifyCars = CIPS.DB.E_FLOW_TYPE.CARS_UPDATE;
        /// <summary>
        /// 流程类型!
        /// 存在!
        /// </summary>
        public const int Type_ExistCars = CIPS.DB.E_FLOW_TYPE.HAVE;
        /// <summary>
        /// 流程类型!
        /// 加冰!
        /// </summary>
        public const int Type_加冰 = -11;
        /// <summary>
        /// 流程类型!
        /// 洗刷!
        /// </summary>
        public const int Type_洗刷 = -12;
        /// <summary>
        /// 流程类型!
        /// 过磅!
        /// </summary>
        public const int Type_过磅 = -13;
        /// <summary>
        /// 流程类型!
        /// 消毒!
        /// </summary>
        public const int Type_消毒 = -14;
        /// <summary>
        /// 流程类型!
        /// 扣修装!
        /// </summary>
        public const int Type_扣修装 = -15;
        /// <summary>
        /// 流程类型!
        /// 扣待处!
        /// </summary>
        public const int Type_扣待处 = -16;
        /// <summary>
        /// 流程类型!
        /// 扣倒装!
        /// </summary>
        public const int Type_扣倒装 = -17;
        /// <summary>
        /// 流程类型!
        /// 扣关门!
        /// </summary>
        public const int Type_置关门 = -18;


        /// <summary>
        /// 流程类型!
        /// 加冰毕!
        /// </summary>
        public const int Type_加冰毕 = -21;
        /// <summary>
        /// 流程类型!
        /// 洗刷毕!
        /// </summary>
        public const int Type_洗刷毕 = -22;
        /// <summary>
        /// 流程类型!
        /// 过磅毕!
        /// </summary>
        public const int Type_过磅毕 = -23;
        /// <summary>
        /// 流程类型!
        /// 消毒毕!
        /// </summary>
        public const int Type_消毒毕 = -24;
        /// <summary>
        /// 流程类型!
        /// 修峻!
        /// </summary>
        public const int Type_修装毕 = -25;
        /// <summary>
        /// 流程类型!
        /// 解除待处!
        /// </summary>
        public const int Type_解除待处 = -26;
        /// <summary>
        /// 流程类型!
        /// 解除待处!
        /// </summary>
        public const int Type_扣车 = -27;
        /// <summary>
        /// 流程类型!
        /// 解除待处!
        /// </summary>
        public const int Type_解除扣车 = -28;
        /// <summary>
        /// 流程类型!
        /// 解除待处!
        /// </summary>
        public const int Type_清关门 = -29;

        /// <summary>
        /// 流程类型!
        /// 运转非-转备用!
        /// </summary>
        public const int Type_运非_转备用 = -51;
        /// <summary>
        /// 流程类型!
        /// 运转非-转军用!
        /// </summary>
        public const int Type_运非_转军用 = -52;
        /// <summary>
        /// 流程类型!
        /// 运转非-扣站修!
        /// </summary>
        public const int Type_运非_扣站修 = -53;
        /// <summary>
        /// 流程类型!
        /// 运转非-扣段修!
        /// </summary>
        public const int Type_运非_扣段修 = -54;
        /// <summary>
        /// 流程类型!
        /// 运转非-扣厂修!
        /// </summary>
        public const int Type_运非_扣厂修 = -55;
        /// <summary>
        /// 流程类型!
        /// 运转非-扣集结!
        /// </summary>
        public const int Type_运非_扣集结 = -56;
        /// <summary>
        /// 流程类型!
        /// 非转运-解除备用!
        /// </summary>
        public const int Type_非运_解除备用 = -61;
        /// <summary>
        /// 流程类型!
        /// 非转运-解除军用!
        /// </summary>
        public const int Type_非运_解除军用 = -62;
        /// <summary>
        /// 流程类型!
        /// 非转运-修峻!
        /// </summary>
        public const int Type_非运_修峻 = -63;
        /// <summary>
        /// 流程类型!
        /// 装车交班!
        /// </summary>
        public const int Type_装交 = -64;
        /// <summary>
        /// 流程类型!
        /// 取消装车交班!
        /// </summary>
        public const int Type_装交取消 = -65;
        /// <summary>
        /// 流程类型!
        /// 卸车交班!
        /// </summary>
        public const int Type_卸交 = -66;
        /// <summary>
        /// 流程类型!
        /// 取消卸车交班!
        /// </summary>
        public const int Type_卸交取消 = -67;
        /// <summary>
        /// 流程类型!
        /// 非转运-取消集结!
        /// </summary>
        public const int Type_非运_取消集结 = -68;

        /// <summary>
        /// 流程类型!
        /// 装车!
        /// </summary>
        public const int Type_装车 = -71;
        /// <summary>
        /// 流程类型!
        /// 卸车!
        /// </summary>
        public const int Type_卸车 = -72;

        /// <summary>
        /// 流程类型!
        /// 修改现车!
        /// </summary>
        public const int Type_修改现车 = -100;
        /// <summary>
        /// 流程类型!
        /// 移动现车!
        /// </summary>
        public const int Type_移动现车 = -101;
        /// <summary>
        /// 流程类型!
        /// 添加现车!
        /// </summary>
        public const int Type_添加现车 = -102;
        /// <summary>
        /// 流程类型!
        /// 删除现车!
        /// </summary>
        public const int Type_删除现车 = -103;
        /// <summary>
        /// 流程类型!
        /// 修改车号!
        /// </summary>
        public const int Type_修改车号 = -104;
        /// <summary>
        /// 流程类型!
        /// 修改装车!
        /// </summary>
        public const int Type_修改装车 = -105;
        /// <summary>
        /// 流程类型!
        /// 取消装车!
        /// </summary>
        public const int Type_取消装车 = -106;
        /// <summary>
        /// 流程类型!
        /// 取消卸车!
        /// </summary>
        public const int Type_取消卸车 = -107;
        /// <summary>
        /// 流程类型!
        /// 过轨运转非!
        /// </summary>
        public const int Type_运非_过轨 = -108;
        /// <summary>
        /// 流程类型!
        /// 过轨非转运!
        /// </summary>
        public const int Type_非运_过轨 = -109;
        /// <summary>
        /// 流程类型!
        /// 报废运转非!
        /// </summary>
        public const int Type_报废 = -110;
        /// <summary>
        /// 流程类型!
        /// 报废运转非!
        /// </summary>
        public const int Type_取消报废 = -111;
        /// <summary>
        /// 流程类型!
        /// 报废运转非!
        /// </summary>
        public const int Type_扣蘑 = -112;
        /// <summary>
        /// 流程类型!
        /// 扣封存!
        /// </summary>
        public const int Type_扣封存 = -113;
        /// <summary>
        /// 流程类型!
        /// 取消封存!
        /// </summary>
        public const int Type_取消封存 = -114;

        /// <summary>
        /// 流程类型!
        /// 自定义车辆特征!
        /// </summary>
        public const int Type_自定义车辆特征 = -115;
        /// <summary>
        /// 验配
        /// </summary>
        public const int Type_验配 = -116;
        /// <summary>
        /// 卸车开始
        /// </summary>
        public const int Type_卸车开始 = -117;
        /// <summary>
        /// 装车开始
        /// </summary>
        public const int Type_装车开始 = -118;
        /// <summary>
        /// 批卸车线
        /// </summary>
        public const int Type_批卸车线 = -123;
        /// <summary>
        /// 批装车线
        /// </summary>
        public const int Type_批装车线 = -124;
        /// <summary>
        /// 设置限速
        /// </summary>
        public const int Type_设置限速 = -125;
        /// <summary>
        /// 取消限速
        /// </summary>
        public const int Type_取消限速 = -126;

        /// <summary>
        /// 流程类型!
        /// 货位!
        /// </summary>
        public const int Type_货位 = -200;


        /// <summary>
        /// 流程类型!
        /// 未知!
        /// </summary>
        public const int Type_Unknow = 0;

      

        /// <summary>
        /// 流程类型
        /// </summary>
        public int Type
        {
            get
            {
                return flowtype;
            }
            set
            {
                flowtype = (short)value;
            }
        }
        

        /// <summary>
        /// 已报点
        /// </summary>
        public bool isOver
        {
            get
            {
                if ((state & 1) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 1;
                else
                    state &= 0xfffe;
            }
        }
        /// <summary>
        /// 已推入现车
        /// </summary>
        public bool Pushed
        {
            get
            {
                if ((state & 2) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    state |= 2;
                    //Pushing=false;
                }
                else
                    state &= 0xfffd;
            }
        }
        /// <summary>
        /// AEI检查通过
        /// </summary>
        public bool AEI_Right
        {
            get
            {
                if ((state & 4) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 4;
                else
                    state &= 0xfffb;
            }
        }
        //public bool Pushing
        //{
        //    get
        //    {
        //        if((state&4)!=0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    set
        //    {
        //        if(value)
        //        {
        //            state|=4;
        //            isPush=false;
        //        }
        //        else
        //            state&=0xfb;
        //    }
        //}
        /// <summary>
        /// 有错
        /// </summary>
        public bool Error
        {
            get
            {
                if ((state & 8) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 8;
                else
                    state &= 0xfff7;
            }
        }
        /// <summary>
        /// 已交班
        /// </summary>
        public bool HandOver//已交班
        {
            get
            {
                return (state & 0x10) != 0;
            }
            set
            {
                if (value)
                    state |= 0x10;
                else
                    state &= 0xffef;
            }
        }
        /// <summary>
        /// 已交班
        /// </summary>
        public bool LastClass//已交班
        {
            get
            {
                return (state & 0x20) != 0;
            }
            set
            {
                if (value)
                    state |= 0x20;
                else
                    state &= 0xffdf;
            }
        }
        /// <summary>
        /// 由工步链接执行
        /// </summary>
        public bool FromLink
        {
            get
            {
                return (state & 0x40) != 0;
            }
            set
            {
                if (value)
                    state |= 0x40;
                else
                    state &= 0xffbf;
            }
        }
        /// <summary>
        /// AEI检查未通过
        /// </summary>
        public bool AEI_Err
        {
            get
            {
                if ((state & 0x80) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x80;
                else
                    state &= 0xff7f;
            }
        }
        /// <summary>
        /// 是否已核对
        /// </summary>
        public bool Checked
        {
            get
            {
                if ((state & 0x100) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x100;
                else
                    state &= 0xfeff;
            }
        }
        /// <summary>
        /// 属于上一班但交班时还未报点的计划
        /// </summary>
        public bool LastClassWithUnOver
        {
            get
            {
                if ((state & 0x200) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x200;
                else
                    state &= 0xfdff;
            }
        }
        /// <summary>
        /// 推原始现车时发生错误
        /// </summary>
        public bool OriHaveError
        {
            get
            {
                if ((state & 0x400) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x400;
                else
                    state &= 0xfbff;
            }
        }
        /// <summary>
        /// 是否带现车,true表示不带现车
        /// </summary>
        public bool WithNoCars
        {
            get
            {
                if ((state & 0x800) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x800;
                else
                    state &= 0xf7ff;
            }
        }
        /// <summary>
        /// 是否已经发送至驼峰
        /// </summary>
        public bool Send2Hump
        {
            get
            {
                if ((state & 0x1000) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x1000;
                else
                    state &= 0xefff;
            }
        }
        /// <summary>
        /// 是否为技术作业图表添加的流程
        /// </summary>
        public bool ChartPlan
        {
            get
            {
                return PlanNum >= 3000;
            }
            set
            {
                PlanNum = 3000;
            }
        }
        /*public bool Upword
        {
            get
            {
                if ((state & 0x10) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x10;
                else
                    state &= 0xffef;
            }
        }
        public bool Downword
        {
            get
            {
                if ((state & 0x20) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    state |= 0x20;
                else
                    state &= 0xffdf;
            }
        }*/
        /// <summary>
        /// 头部计划
        /// </summary>
        public bool HeadMode
        {
            get
            {
                if (!isPlan)
                    return false;
                return flowtype == CIPS.DB.E_FLOW_TYPE.SWIT_SPLIT || flowtype == CIPS.DB.E_FLOW_TYPE.SWIT_NSPLIT;
            }
            set
            {
                if (!isPlan)
                    return;
                flowtype = CIPS.DB.E_FLOW_TYPE.SWIT_SPLIT;
            }
        }
        /// <summary>
        /// 编组计划
        /// </summary>
        public bool TailMode
        {
            get
            {
                if (!isPlan)
                    return false;
                return flowtype == CIPS.DB.E_FLOW_TYPE.SWIT_CLASSI || flowtype == CIPS.DB.E_FLOW_TYPE.SWIT_PRECLASSSI;
            }
            set
            {
                if (!isPlan)
                    return;
                flowtype = CIPS.DB.E_FLOW_TYPE.SWIT_CLASSI;
            }
        }
        /// <summary>
        /// 计划号
        /// </summary>
        public int PlanNum
        {
            get
            {
                return (plannum & 0xfff0) >> 4;
            }
            set
            {
                plannum &= 0xf;
                plannum |= (ushort)((value << 4) & 0xfff0);
            }
        }
        /// <summary>
        /// 拆分后计划子号
        /// </summary>
        public int SubNum//报点拆分的子号,从startnum的高2位来
        {
            get
            {
                return plannum & 0xf;
            }
            set
            {
                plannum &= 0xfff0;
                plannum |= (ushort)(value & 0xf);
            }
        }
        /// <summary>
        /// 操作报点时间
        /// </summary>
        public DateTime reportTime = new DateTime(1900, 1, 1, 12, 0, 0);
        /// <summary>
        /// 复制
        /// </summary>
        public void CopyFrom(PLANINDEX p)
        {
            id = p.id;
            //planorder=p.planorder;
            locom = p.locom;
            locomworker = p.locomworker;
            subid = p.subid;
            plannum = p.plannum;
            maker = p.maker;
            state = p.state;
            trainnum = p.trainnum;
            startTime_plan = p.startTime_plan;
            endTime_plan = p.endTime_plan;
            startTime_real = p.startTime_real;
            endTime_real = p.endTime_real;
            StartNum = p.StartNum;
            consid = p.consid;
            flowtype = p.flowtype;
            reportTime = p.reportTime;
            remark = p.remark;
            unyard = p.unyard;
            cflowtype = p.cflowtype;
            Tag = p.Tag;
        }
    }
    /// <summary>
    /// 流程
    /// </summary>
    public class PLAN : PLANINDEX
    {
        /// <summary>
        /// 工步
        /// </summary>
        public CUT[] cuts;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public System.Collections.ArrayList Classi = null;
        /// <summary>
        /// 调机等待列表
        /// </summary>
        public LocomWaitList locomWaiList = null;
        /// <summary>
        /// 流程
        /// </summary>
        public PLAN()
        {
            cuts = new CUT[0];
        }
        /// <summary>
        /// 是否已报点
        /// </summary>
        new public bool isOver
        {
            get
            {
                if (cuts.Length == 0)
                    return base.isOver;

                bool b = true;
                bool v = false;
                foreach (CUT c in cuts)
                {
                    if (!c.Visible)
                        continue;
                    v = true;
                    if (!c.Finished)
                    {
                        b = false;
                        break;
                    }
                }
                if (!v)
                    return base.isOver;
                else
                    base.isOver = b;
                return base.isOver;
            }
            set
            {
                bool b = true;
                bool v = false;
                foreach (CUT c in cuts)
                {
                    if (!c.Visible)
                        continue;
                    v = true;
                    if (!c.Finished)
                    {
                        b = false;
                        break;
                    }
                }
                if (!v)
                {
                    if (isPlan)
                        value = false;
                }
                else
                {
                    if (value)
                    {
                        foreach (CUT c in cuts)
                        {
                            if (!c.Finished)
                                c.endTime = endTime_real;
                        }
                    }
                    else
                        value = b;
                }
                base.isOver = value;
            }
        }
        /// <summary>
        /// 是否已推入
        /// </summary>
        new public bool Pushed
        {
            get
            {
                return base.Pushed;
            }
            set
            {
                if (!value)
                {
                    foreach (CUT c in cuts)
                        c.endTime = new DateTime(1999, 1, 1);
                }
                base.Pushed = value;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        public void CopyFrom(PLAN p)
        {
            base.CopyFrom((PLANINDEX)p);
            cuts = new CUT[p.cuts.Length];
            Classi = p.Classi;
            locomWaiList = null;
            if (p.locomWaiList != null)
            {
                locomWaiList = new LocomWaitList();
                locomWaiList.CopyFrom(p.locomWaiList);
            }
            int i;
            for (i = 0; i < cuts.Length; i++)
            {
                cuts[i] = new CUT();
                cuts[i].CopyFrom(p.cuts[i]);
            }
        }
        /// <summary>
        /// 比较两个流程是否相似
        /// </summary>
        /// <param name="p">对比流程</param>
        /// <returns></returns>
        public bool Like(PLAN p)
        {
            int i, j;
            if (id == p.id && locom == p.locom && locomworker == p.locomworker && subid == p.subid
                && state == p.state && trainnum == p.trainnum && consid == p.consid && flowtype == p.flowtype
                && remark == p.remark && unyard == p.unyard && cflowtype == p.cflowtype)
            {
                if(locomWaiList == null && p.locomWaiList != null)
                    return false;
                if(locomWaiList != null && p.locomWaiList == null)
                    return false;
                if(locomWaiList != null && p.locomWaiList != null && locomWaiList.GetLocomWaitStr() != p.locomWaiList.GetLocomWaitStr())
                    return false;
                if (cuts.Length == p.cuts.Length)
                {
                    for (i = 0; i < cuts.Length; i++)
                    {
                        if (cuts[i].track == p.cuts[i].track && cuts[i].coupFlag == p.cuts[i].coupFlag && cuts[i].count == p.cuts[i].count && cuts[i].trackzero == p.cuts[i].trackzero &&
                            cuts[i].note == p.cuts[i].note && cuts[i].carnumlist.Length == p.cuts[i].carnumlist.Length && cuts[i].endTime == p.cuts[i].endTime)
                        {
                            for (j = 0; j < cuts[i].carnumlist.Length; j++)
                            {
                                if (cuts[i].carnumlist[j] != p.cuts[i].carnumlist[j])
                                    return false;
                            }
                        }
                        else
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除工步
        /// </summary>
        /// <param name="c">工步</param>
        /// <returns></returns>
        public bool RemoveCut(CUT c)
        {
            System.Collections.Generic.List<CUT> cs = new System.Collections.Generic.List<CUT>();
            foreach (CUT _c in cuts)
            {
                if (_c != c)
                    cs.Add(_c);
            }
            if (cs.Count == cuts.Length)
                return false;
            cuts = new CUT[cs.Count];
            for (int i = 0; i < cuts.Length; i++)
                cuts[i] = cs[i];
            return true;
        }
    }
    /// <summary>
    /// 工步
    /// </summary>
    public class CUT
    {
        /// <summary>
        /// 股道
        /// </summary>
        public short track;
        /// <summary>
        /// 在途换长
        /// </summary>
        public short F_LEN_LOCOM = 0;
        /// <summary>
        /// 在途重量
        /// </summary>
        public short F_WEGH_LOCOM = 0;
        /// <summary>
        /// 车辆属性
        /// </summary>
        public byte CAR_PROPERTY = 0;
        /// <summary>
        /// 是否有空车
        /// </summary>
        public bool HaveEmptyCar
        {
            get
            {
                if ((CAR_PROPERTY & 0x80) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    CAR_PROPERTY |= 0x80;
                else
                    CAR_PROPERTY &= 0x7f;
            }
        }
        /// <summary>
        /// 是否有重车
        /// </summary>
        public bool HaveLoadCar
        {
            get
            {
                if ((CAR_PROPERTY & 0x40) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    CAR_PROPERTY |= 0x40;
                else
                    CAR_PROPERTY &= 0xbf;
            }
        }
        /// <summary>
        /// 平均重量
        /// </summary>
        public int Wegh
        {
            get
            {
                int wegh = CAR_PROPERTY & 0x3f;
                return wegh * 2;
            }
            set
            {
                byte wegh = (byte)(value / 2);
                if (wegh >= 63)
                    wegh = 63;
                CAR_PROPERTY |= wegh;
            }
        }

        /// <summary>
        /// 对应列车子表ID
        /// </summary>
        public Int64 subtraid = 0;

        /// <summary>
        /// 货位工步子类型
        /// 进货
        /// </summary>
        public const byte SUBCT_BOOTH_IN = 1;       //货位子类型-进货
        /// <summary>
        /// 货位工步子类型
        /// 出货
        /// </summary>
        public const byte SUBCT_BOOTH_OUT = 2;      //货位子类型-出货
        /// <summary>
        /// 货位工步子类型
        /// 交付
        /// </summary>
        public const byte SUBCT_BOOTH_FINISH = 3;   //货位子类型-交付

        /// <summary>
        /// 工步类型
        /// 无
        /// </summary>
        public const byte CT_NULL = 0;
        /// <summary>
        /// 换车次
        /// note:原车次，新车次
        /// </summary>
        public const byte CT_TNAME = 1;         //换线路上的车次
        /// <summary>
        /// 封锁股道
        /// </summary>
        public const byte CT_LOCKHUMP = 2;      //集结结束/封锁股道
        /// <summary>
        /// 集结牵出
        /// 集结主/辅/单独
        /// </summary>
        public const byte CT_DRAGOUT = 3;       //
        //public const byte CT_SHIFT = 4;         //线路与机车交换现车(用于到发确报及改现车)
        /// <summary>
        /// 对位
        /// </summary>
        public const byte CT_FIX = 4;           //对位
        /// <summary>
        /// 经由
        /// track:经由线
        /// </summary>
        public const byte CT_THROUGH = 5;       //经由,
        /// <summary>
        /// 移动车辆
        /// track=源线,trackzero=源停车器位置
        /// carlist0=被移车号,carlist1=源线参考车号,carlist2=目的参考车号
        /// note:目的线,目的线新停车器位置,目的线原停车器位置
        /// </summary>
        public const byte CT_MOVECAR = 6;       //移动车辆,,
        //
        /// <summary>
        /// 修改车辆,版本升级
        /// track=源线,trackzero=源停车器位置
        /// carlist[2n]=源车号,carlist[2n+1]=目的车号
        /// </summary>
        public const byte CT_UPDATECAR = 7;     //,,
        /// <summary>
        /// 货位
        /// trackzero=子类型,track=boothid,note:booth内容
        /// </summary>
        public const byte CT_BOOTH = 8;     //
        /// <summary>
        /// 调整顺序
        /// carlist=移到最前方的车辆
        /// </summary>
        public const byte CT_REORDER = 13;      //
        /// <summary>
        /// 移动一组车辆至目的线路的最后
        /// track=源线,trackzero=源停车器位置
        /// note:目的线,目的线新停车器位置,目的线旧停车器位置
        /// </summary>
        public const byte CT_MOVECARSARRAY = 14;//,,

        /// <summary>
        /// 新集结
        /// </summary>
        public const byte CT_NEWCLASSI = 15;    //
        /// <summary>
        /// 链接到其它流程
        /// note:flowID
        /// </summary>
        public const byte CT_LINK2FLOW = 16;    //


        /// <summary>
        /// 挂车
        /// </summary>
        public const byte CT_GET = 20;          //
        /// <summary>
        /// 迂回线取车
        /// </summary>
        public const byte CT_GET_YH = 21;       //
        /// <summary>
        /// 禁溜线取车
        /// </summary>
        public const byte CT_GET_JL = 22;       //
        /// <summary>
        /// 无序线取车
        /// </summary>
        public const byte CT_GET_DISORDER = 23; //


        /// <summary>
        /// 送车
        /// </summary>
        public const byte CT_PUT = 25;           //送车
        /// <summary>
        /// 迂回线送车
        /// </summary>
        public const byte CT_PUT_YH = 26;        //
        /// <summary>
        /// 禁溜线送车
        /// </summary>
        public const byte CT_PUT_JL = 27;        //
        /// <summary>
        /// 单溜
        /// </summary>
        public const byte CT_PUT_DL = 28;        //
        /// <summary>
        /// 连溜
        /// </summary>
        public const byte CT_PUT_LL = 29;        //
        /// <summary>
        /// 无序线送车
        /// </summary>
        public const byte CT_PUT_DISORDER = 30;

        /// <summary>
        /// 工步类型
        /// 0x40:port,0x80:stopcut,0x20:enforce
        /// </summary>
        public byte coupFlag = 0;                 //
        /// <summary>
        /// 辆数
        /// </summary>
        public byte count = 0;                    //辆数
        /// <summary>
        /// 注释
        /// </summary>
        public string note = "";                  //
        /// <summary>
        /// 用户注释
        /// </summary>
        public string usernote = "";            //
        /// <summary>
        /// 车号LIST
        /// </summary>
        public int[] carnumlist = new int[0];   //
        /// <summary>
        /// 该勾执行前的停车器位置
        /// </summary>
        public ushort trackzero = 0;             //
        /// <summary>
        /// 相关勾的信息,如推峰,停车器制动,缓解,股道封锁等
        /// </summary>
        public ushort correlate = 0;            //
        /// <summary>
        /// 报点时间
        /// </summary>
        public DateTime endTime = new DateTime(1999, 1, 1);
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime = new DateTime(1999, 1, 1);
        /// <summary>
        /// 相关对象
        /// </summary>
        public object Tag = null;
        /// <summary>
        /// 注释
        /// </summary>
        public string Note
        {
            get
            {
                return note + usernote;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        public void CopyFrom(CUT ct)
        {
            track = ct.track;
            coupFlag = ct.coupFlag;
            count = ct.count;
            note = ct.note;
            usernote = ct.usernote;
            trackzero = ct.trackzero;
            correlate = ct.correlate;
            subtraid = ct.subtraid;
            carnumlist = new int[ct.carnumlist.Length];
            endTime = ct.endTime;
            startTime = ct.startTime;
            F_WEGH_LOCOM = ct.F_WEGH_LOCOM;
            F_LEN_LOCOM = ct.F_LEN_LOCOM;
            CAR_PROPERTY = ct.CAR_PROPERTY;
            Tag = ct.Tag;
            int i;
            for (i = 0; i < carnumlist.Length; i++)
                carnumlist[i] = ct.carnumlist[i];
        }
        /// <summary>
        /// 已结束
        /// </summary>
        public bool Finished
        {
            get
            {
                return endTime.Year > 2000;
            }
        }

        #region 利用trackzero字段
        #region 换车次相关
        /// <summary>
        /// 锁定车次（过期）
        /// </summary>
        public bool LockTN_Before
        {
            get
            {
                return (trackzero & 0x8000) != 0;
            }
            set
            {
                if (value)
                    trackzero |= 0x8000;
                else
                    trackzero &= 0x7fff;
            }
        }
        /// <summary>
        /// 锁定车次（过期）
        /// </summary>
        public bool LockTN_After
        {
            get
            {
                return (trackzero & 0x4000) != 0;
            }
            set
            {
                if (value)
                    trackzero |= 0x4000;
                else
                    trackzero &= 0xbfff;
            }
        }
        /// <summary>
        /// 是否为自动添加的车次（过期）
        /// </summary>
        public bool AutoTN
        {
            get
            {
                return (trackzero & 0x2000) != 0;
            }
            set
            {
                if (value)
                    trackzero |= 0x2000;
                else
                    trackzero &= 0xdfff;
            }
        }
        /// <summary>
        /// 原车次
        /// </summary>
        public string OldTN
        {
            get
            {
                if (type == CT_TNAME)
                {
                    string[] tns;
                    char[] chsplit = new char[1];
                    chsplit[0] = ',';
                    tns = note.Split(chsplit);
                    if (tns.Length == 2)
                    {
                        return tns[0];
                    }
                }
                else if (type == CT_LOCKHUMP || type == CT_DRAGOUT)
                    return note;
                return "";
            }
        }
        /// <summary>
        /// 新车次
        /// </summary>
        public string NewTN
        {
            get
            {
                if (type == CT_TNAME)
                {
                    string[] tns;
                    char[] chsplit = new char[1];
                    chsplit[0] = ',';
                    tns = note.Split(chsplit);
                    if (tns.Length == 2)
                    {
                        return tns[1];
                    }
                }
                return "";
            }
        }
        #endregion

        #region 多出口线路的选择
        /// <summary>
        /// 线路出口
        /// </summary>
        public short doorway
        {
            get
            {
                return (short)(trackzero & 0x7fff);
            }
            set
            {
                trackzero = (ushort)((ushort)value | 0x8000);
            }
        }
        /// <summary>
        /// 是否为多出口调车工步
        /// </summary>
        public bool MultiWay
        {
            get
            {
                if (add_del == ' ')
                    return false;
                return (trackzero & 0x8000) != 0;
            }
        }
        #endregion

        #region 封锁股道
        /// <summary>
        /// 封锁股道（过期）
        /// </summary>
        public bool LockHump_Before
        {
            get
            {
                return LockTN_Before;
            }
            set
            {
                LockTN_Before = value;
            }
        }
        /// <summary>
        /// 封锁股道（过期）
        /// </summary>
        public bool LockHump_After
        {
            get
            {
                return LockTN_After;
            }
            set
            {
                LockTN_After = value;
            }
        }
        #endregion
        #endregion

        /*#region 牵空线路
        public bool DragOut_Before
        {
            get
            {
                return LockTN_Before;
            }
            set
            {
                LockTN_Before = value;
            }
        }
        public bool DragOut_After
        {
            get
            {
                return LockTN_After;
            }
            set
            {
                LockTN_After = value;
            }
        }
        #endregion*/

        #region 与工步类型/摘挂有关
        /// <summary>
        /// 工步类型
        /// </summary>
        public byte type
        {
            get
            {
                return (byte)(coupFlag & 0x1f);
            }
            set
            {
                coupFlag &= 0xe0;
                coupFlag |= (byte)(value & 0x1f);
            }
        }
        /// <summary>
        /// 端口
        /// </summary>
        public bool port
        {
            get
            {
                if ((coupFlag & 0x040) == 0)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    coupFlag |= 0x40;
                else
                    coupFlag &= 0xbf;
            }
        }
        /// <summary>
        /// 是否越过停车器
        /// </summary>
        public bool throughTCQ
        {
            get
            {
                if ((coupFlag & 0x080) == 0)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    coupFlag |= 0x80;
                else
                    coupFlag &= 0x7f;
            }
        }
        public bool Enforce
        {
            get
            {
                return (coupFlag & 0x20) != 0;
            }
            set
            {
                if (value)
                    coupFlag |= 0x20;
                else
                    coupFlag &= 0xdf;
            }
        }
        /// <summary>
        /// 是否为可见工步
        /// </summary>
        public bool Visible
        {
            get
            {
                return add_del != ' ' || type == CT_THROUGH || type == CT_FIX;
            }
        }
        /// <summary>
        /// +/-
        /// </summary>
        public char add_del
        {
            get
            {
                byte v = type;
                if (v >= CT_GET && v < CT_PUT)
                    return '+';
                else if (v >= CT_PUT)
                    return '-';
                else
                    return ' ';
            }
            set
            {
                byte v = 0;
                if (value == '+')
                    v = CT_GET;
                else if (value == '-')
                    v = CT_PUT;
                else
                    v = CT_NULL;
                coupFlag = (byte)((coupFlag & 0xe0) | v);
            }
        }
        /// <summary>
        /// 迂回取车
        /// </summary>
        static string coupSymbol_GET_YH = "≦";
        /// <summary>
        /// 迂回送车
        /// </summary>
        static string coupSymbol_PUT_YH = "≧";
        /// <summary>
        /// 取车
        /// </summary>
        static string coupSymbol_GET = "+";
        /// <summary>
        /// 送车
        /// </summary>
        static string coupSymbol_PUT = "-";
        /// <summary>
        /// 连溜
        /// </summary>
        static string coupSymbol_PUT_LL = ">>";
        /// <summary>
        /// 单溜
        /// </summary>
        static string coupSymbol_PUT_DL = ">";
        /// <summary>
        /// 通过
        /// </summary>
        static string coupSymbol_THROUGH = "过";
        /// <summary>
        /// 对位
        /// </summary>
        static string coupSymbol_FIX = "对";
        /// <summary>
        /// 工步类型的文字表达方式
        /// </summary>
        public string coupSymbol
        {
            get
            {
                byte ct = type;
                if (ct == CT_GET_YH)
                    return coupSymbol_GET_YH;
                if (ct == CT_PUT_LL)
                    return coupSymbol_PUT_LL;
                else if (ct == CT_PUT_DL)
                    return coupSymbol_PUT_DL;
                else if (ct == CT_PUT_YH)
                    return coupSymbol_PUT_YH;
                //else if (ct == CT_SHIFT)
                //    return "<>";
                else if (ct == CT_THROUGH)
                    return coupSymbol_THROUGH;
                else if (ct == CT_FIX)
                    return coupSymbol_FIX;
                else
                {
                    char c = add_del;
                    if (c == '+')
                        return coupSymbol_GET;
                    else if (c == '-')
                        return coupSymbol_PUT;
                    else
                        return " ";
                }
            }
        }
        /// <summary>
        /// 加载勾计划调车符号
        /// </summary>
        /// <param name="dt">DC_RA_MIXCO表</param>
        public static void SetSymbol(DataTable dt)
        {
            if (dt == null)
                return;
            DataRow[] drs;
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'PUT'");
            if (drs.Length > 0)
                coupSymbol_PUT = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'GET'");
            if (drs.Length > 0)
                coupSymbol_GET = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'GET_YH'");
            if (drs.Length > 0)
                coupSymbol_GET_YH = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'PUT_DL'");
            if (drs.Length > 0)
                coupSymbol_PUT_DL = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'PUT_LL'");
            if (drs.Length > 0)
                coupSymbol_PUT_LL = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'PUT_YH'");
            if (drs.Length > 0)
                coupSymbol_PUT_YH = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'THROUGH'");
            if (drs.Length > 0)
                coupSymbol_THROUGH = drs[0]["C_CO_DESC"].ToString();
            drs = dt.Select("C_SEG_NA = 'E_CUT_SYMBOL' and C_FORSH_SPELL = 'FIX'");
            if (drs.Length > 0)
                coupSymbol_FIX = drs[0]["C_CO_DESC"].ToString();

        }
        #endregion
        #region 与相关工步有关
        /*
        public bool 停车器缓解           //停车器缓解
        {
            get
            {
                if ((correlate & 1) == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 1;
                else
                    correlate &= 0xfe;
            }
        }
        public bool 停车器制动          //停车器制动
        {
            get
            {
                if ((correlate & 2) == 2)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 2;
                else
                    correlate &= 0xfd;
            }
        }
        public bool 封锁股道           //封锁股道
        {
            get
            {
                if ((correlate & 4) == 4)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 4;
                else
                    correlate &= 0xfb;
            }
        }
        public bool 解锁股道         //解锁股道
        {
            get
            {
                if ((correlate & 8) == 8)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 8;
                else
                    correlate &= 0xf7;
            }
        }
        public bool 推峰            //推峰
        {
             get
            {
                if ((correlate & 0x10) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 0x10;
                else
                    correlate &= 0xef;
            }
       }
        public bool 预编牵出        //预编牵出
        {
             get
            {
                if ((correlate & 0x20) != 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    correlate |= 0x20;
                else
                    correlate &= 0xdf;
            }
       }*/

        #endregion
    }
    /// <summary>
    /// 序列化计划数据
    /// </summary>
    public class ConvertPlanDat
    {
        //public const int locomDoorwayBase = 300;

        const int off_cmd = 0;
        const int off_datatype = off_cmd + 1;
        const int off_plantype = off_datatype + 1;
        const int off_plansum = off_plantype + 1;
        const int off_packagehead = off_plansum + 2;//头总长
        const int off_planid = 0;
        const int off_consid = off_planid + 4;
        const int off_flowtype = off_consid + 4;
        const int off_subid = off_flowtype + 2;
        const int off_maker = off_subid + 2;
        const int off_plannum = off_maker + 10;
        const int off_startnum = off_plannum + 2;
        const int off_tn = off_startnum + 1;
        const int off_locom = off_tn + 15;
        const int off_state = off_locom + 2;
        const int off_starttime_p = off_state + 2;
        const int off_endtime_p = off_starttime_p + 6;
        const int off_starttime_r = off_endtime_p + 6;
        const int off_endtime_r = off_starttime_r + 6;
        const int off_sum = off_endtime_r + 6;	//勾数
        const int off_planhead = off_sum + 1;	//头长
        const int off_track = 0;				//线路
        const int off_coup = off_track + 2;		//+/-
        const int off_cnt = off_coup + 1;		//辆数
        const int off_zero = off_cnt + 1;
        const int off_cuttime1 = off_zero + 2;
        const int off_cuttime2 = off_cuttime1 + 6;
        // public const int off_cuttime = off_cutstarttime + 6;
        const int off_note = off_cuttime2 + 6;		//备注


        //public ConvertPlanDat()
        //{
        //    //
        //    // TODO: 在此处添加构造函数逻辑
        //    //
        //}
        /// <summary>
        /// 计划转换为CL数据集
        /// </summary>
        /// <param name="ps">流程数组</param>
        /// <returns></returns>
        public static ClSet Plans2ClSet(PLAN[] ps)
        {
            ClSet cs = PlanIndex2ClSet(ps);
            ClTable ct = new ClTable("DM_EXPRESS_CUT");
            cs.Tables.Add(ct);
            ct.AddColumn("I_CUT_ID", typeof(long));
            int col_cutid = 0;
            ct.AddColumn("I_LINE_ID", typeof(short));
            int col_lineid = 1;
            ct.AddColumn("I_CAR_CNT", typeof(short));
            int col_carcnt = 2;
            ct.AddColumn("E_COUP_FLAG", typeof(byte));
            int col_coupflag = 3;
            ct.AddColumn("I_TRACK_ZERO", typeof(ushort));
            int col_zero = 4;
            ct.AddColumn("C_REMARK", typeof(string));
            int col_remark = 5;
            ct.AddColumn("C_CAR_ID", typeof(string));
            int col_carid = 6;
            ct.AddColumn("D_START", typeof(DateTime));
            int col_starttime = 7;
            ct.AddColumn("D_END", typeof(DateTime));
            int col_endtime = 8;
            ct.AddColumn("F_LEN_LOCOM", typeof(Int16));
            int col_F_LEN_LOCOM = 9;
            ct.AddColumn("F_WEGH_LOCOM", typeof(Int16));
            int col_F_WEGH_LOCOM = 10;
            ct.AddColumn("CAR_PROPERTY", typeof(byte));
            int col_CAR_PROPERTY = 11;
            ct.AddColumn("I_SUB_TRA_ID", typeof(Int64));
            int col_SUB_TRA_ID = 12;
            CIPS.ClRow cr;
            foreach (PLAN p in ps)
            {
                if (p == null)
                    continue;
                for (int i = 0; i < p.cuts.Length; i++)
                {
                    cr = ct.NewRow();
                    cr[col_cutid] = p.id * 1000L + i;
                    cr[col_lineid] = p.cuts[i].track;
                    cr[col_remark] = p.cuts[i].note + "\n" + p.cuts[i].usernote;
                    cr[col_zero] = p.cuts[i].trackzero;
                    cr[col_coupflag] = p.cuts[i].coupFlag;
                    cr[col_carcnt] = p.cuts[i].count;
                    cr[col_starttime] = p.cuts[i].startTime;
                    cr[col_endtime] = p.cuts[i].endTime;
                    cr[col_F_LEN_LOCOM] = p.cuts[i].F_LEN_LOCOM;
                    cr[col_F_WEGH_LOCOM] = p.cuts[i].F_WEGH_LOCOM;
                    cr[col_CAR_PROPERTY] = p.cuts[i].CAR_PROPERTY;
                    cr[col_SUB_TRA_ID] = p.cuts[i].subtraid;
                    string s = "";
                    foreach (long cid in p.cuts[i].carnumlist)
                        s += cid.ToString() + ",";
                    s = s.TrimEnd(',');
                    cr[col_carid] = s;
                    ct.Rows.Add(cr);
                }
            }
            ct = new ClTable("LocomWait");
            cs.Tables.Add(ct);
            ct.AddColumn("I_PL_ID", typeof(long));
            ct.AddColumn("C_WAIT_INFO", typeof(string));
            foreach (PLAN p in ps)
            {
                if (p == null)
                    continue;
                if (p.locomWaiList == null)
                    continue;
                cr = ct.NewRow();
                cr["I_PL_ID"] = p.id;
                cr["C_WAIT_INFO"] = p.locomWaiList.GetLocomWaitStr();
                ct.Rows.Add(cr);
            }
            return cs;
        }
        static ClRow[] GetCutRows(long fid, ClTable ct)
        {
            int col = ct.GetColumnIndex("I_CUT_ID");
            long fid1 = fid * 1000;
            long fid2 = fid1 + 999;
            System.Collections.Generic.List<ClRow> rows = new System.Collections.Generic.List<ClRow>();
            foreach (ClRow cr in ct.Rows)
            {
                long cid = Convert.ToInt64(cr[col]);
                if (cid < fid1)
                    continue;
                if (cid > fid2)
                    break;
                rows.Add(cr);
            }
            ClRow[] crs = new ClRow[rows.Count];
            for (int i = 0; i < crs.Length; i++)
                crs[i] = rows[i];
            return crs;
        }
        /// <summary>
        /// CL数据集转为计划
        /// </summary>
        /// <param name="cs">CL数据集</param>
        /// <returns></returns>
        public static PLAN[] ClSet2Plans(ClSet cs)
        {
            PLAN[] ps = ClSet2PlanIndex(cs);
            if (cs == null)
                return ps;
            ClTable ct = cs.Tables["DM_EXPRESS_CUT"];
            if (ct == null)
                return ps;
            ClRow[] crs;
            cs.Tables.Add(ct);
            ct.SortBy("I_CUT_ID");
            int I_LINE_ID = ct.GetColumnIndex("I_LINE_ID");
            int I_CAR_CNT = ct.GetColumnIndex("I_CAR_CNT");
            int E_COUP_FLAG = ct.GetColumnIndex("E_COUP_FLAG");
            int I_TRACK_ZERO = ct.GetColumnIndex("I_TRACK_ZERO");
            int C_REMARK = ct.GetColumnIndex("C_REMARK");
            int D_START = ct.GetColumnIndex("D_START");
            int D_END = ct.GetColumnIndex("D_END");
            int C_CAR_ID = ct.GetColumnIndex("C_CAR_ID");
            int F_LEN_LOCOM = ct.GetColumnIndex("F_LEN_LOCOM");
            int F_WEGH_LOCOM = ct.GetColumnIndex("F_WEGH_LOCOM");
            int CAR_PROPERTY = ct.GetColumnIndex("CAR_PROPERTY");
            int SUB_TRA_ID = ct.GetColumnIndex("I_SUB_TRA_ID");

            foreach (PLAN p in ps)
            {
                crs = GetCutRows(p.id, ct);
                p.cuts = new CUT[crs.Length];
                for (int i = 0; i < p.cuts.Length; i++)
                {
                    p.cuts[i] = new CUT();
                    p.cuts[i].track = Convert.ToInt16(crs[i][I_LINE_ID]);
                    p.cuts[i].count = (byte)Convert.ToInt16(crs[i][I_CAR_CNT]);
                    p.cuts[i].coupFlag = Convert.ToByte(crs[i][E_COUP_FLAG]);
                    p.cuts[i].endTime = Convert.ToDateTime(crs[i][D_END]);
                    p.cuts[i].trackzero = Convert.ToUInt16(crs[i][I_TRACK_ZERO]);
                    if (F_LEN_LOCOM >= 0)
                        p.cuts[i].F_LEN_LOCOM = Convert.ToInt16(crs[i][F_LEN_LOCOM]);
                    if (F_WEGH_LOCOM >= 0)
                        p.cuts[i].F_WEGH_LOCOM = Convert.ToInt16(crs[i][F_WEGH_LOCOM]);
                    if (CAR_PROPERTY >= 0)
                        p.cuts[i].CAR_PROPERTY = Convert.ToByte(crs[i][CAR_PROPERTY]);
                    string[] notes = Convert.ToString(crs[i][C_REMARK]).Split('\n');
                    if (notes.Length > 0)
                        p.cuts[i].note = notes[0];
                    if (notes.Length > 1)
                        p.cuts[i].usernote = notes[1];
                    p.cuts[i].subtraid = Convert.ToInt64(crs[i][SUB_TRA_ID]);
                    p.cuts[i].carnumlist = new int[p.cuts[i].count];
                    notes = Convert.ToString(crs[i][C_CAR_ID]).Split(',');
                    for (int j = 0; j < p.cuts[i].carnumlist.Length; j++)
                    {
                        if (notes.Length > j && notes[j].Trim() != "")
                        {
                            try
                            {
                                Int32.TryParse(notes[j], out p.cuts[i].carnumlist[j]);
                            }
                            catch { }
                        }
                    }
                }
            }
            ct = cs.Tables["LocomWait"];
            if (ct != null)
            {
                foreach (PLAN p in ps)
                {
                    crs = ct.Select("I_PL_ID=" + p.id);
                    if (crs.Length > 0)
                    {
                        p.locomWaiList = new LocomWaitList();
                        p.locomWaiList.SetLocomWaitList(crs[0]["C_WAIT_INFO"].ToString());
                    }
                }
            }
            return ps;
        }
        /// <summary>
        /// 流程索引转为数据集
        /// </summary>
        /// <param name="ps">流程索引数组</param>
        /// <returns></returns>
        public static ClSet PlanIndex2ClSet(PLAN[] ps)
        {
            ClSet cs = new ClSet();
            ClTable ct = new ClTable("DM_EXPRESS_FLOW");
            ct.AddColumn("I_FLOW_ID", typeof(int));
            int I_FLOW_ID = 0;
            ct.AddColumn("I_CONS_ID", typeof(int));
            int I_CONS_ID = 1;
            ct.AddColumn("E_FLOW_TYPE", typeof(short));
            int E_FLOW_TYPE = 2;
            ct.AddColumn("E_WORK_RANGE", typeof(short));
            int E_WORK_RANGE = 3;
            ct.AddColumn("B_FLOW_STATE", typeof(ushort));
            int B_FLOW_STATE = 4;
            ct.AddColumn("I_LOCOM_ID", typeof(short));
            int I_LOCOM_ID = 5;
            ct.AddColumn("I_FLOW_NO", typeof(ushort));
            int I_FLOW_NO = 6;
            ct.AddColumn("I_FLOW_NO_START", typeof(ushort));
            int I_FLOW_NO_START = 7;
            ct.AddColumn("C_TRAIN_NUM", typeof(string));
            int C_TRAIN_NUM = 8;
            ct.AddColumn("C_OP_MAKER", typeof(string));
            int C_OP_MAKER = 9;
            ct.AddColumn("D_FA_START", typeof(DateTime));
            int D_FA_START = 10;
            ct.AddColumn("D_FA_END", typeof(DateTime));
            int D_FA_END = 11;
            ct.AddColumn("D_PL_START", typeof(DateTime));
            int D_PL_START = 12;
            ct.AddColumn("D_PL_END", typeof(DateTime));
            int D_PL_END = 13;
            ct.AddColumn("I_START_TRACK_ID", typeof(short));
            int I_START_TRACK_ID = 14;
            ct.AddColumn("C_LOCOM_WORKER", typeof(string));
            int C_LOCOM_WORKER = 15;
            ct.AddColumn("C_REMARK", typeof(string));
            int C_REMARK = 16;
            ct.AddColumn("C_LOCOMWAIT_INFO", typeof(string));
            int C_LOCOMWAIT_INFO = 17;
            ct.AddColumn("I_LINE_ID_UNYARD", typeof(int));
            int I_LINE_ID_UNYARD = 18;
            ct.AddColumn("C_FLOW_TYPE", typeof(string));
            int C_FLOW_TYPE = 19;
            cs.Tables.Add(ct);
            foreach (PLAN p in ps)
            {
                if (p == null)
                    continue;
                ClRow cr = ct.NewRow();
                cr[I_FLOW_ID] = p.id;
                cr[I_CONS_ID] = p.consid;
                cr[E_FLOW_TYPE] = p.flowtype;
                cr[E_WORK_RANGE] = (short)p.subid;
                cr[B_FLOW_STATE] = p.state;
                cr[I_LOCOM_ID] = p.locom;
                cr[I_FLOW_NO] = p.plannum;
                cr[I_FLOW_NO_START] = p.StartNum;
                cr[C_TRAIN_NUM] = p.trainnum;
                cr[C_OP_MAKER] = p.maker;
                cr[D_FA_START] = p.startTime_real;
                cr[D_PL_START] = p.startTime_plan;
                cr[D_FA_END] = p.endTime_real;
                cr[D_PL_END] = p.endTime_plan;
                cr[I_START_TRACK_ID] = p.StartTrack;
                cr[C_LOCOM_WORKER] = p.locomworker;
                cr[C_REMARK] = p.remark;
                cr[I_LINE_ID_UNYARD] = p.unyard;
                cr[C_FLOW_TYPE] = p.cflowtype;
                if (p.locomWaiList == null)
                    cr[C_LOCOMWAIT_INFO] = "";
                else
                    cr[C_LOCOMWAIT_INFO] = p.locomWaiList.GetLocomWaitStr();
                ct.Rows.Add(cr);
            }
            return cs;
        }
        /// <summary>
        /// 数据集转为流程索引
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static PLAN[] ClSet2PlanIndex(ClSet cs)
        {
            if (cs == null)
                return new PLAN[0];
            ClTable ct;
            ct = cs.Tables["DM_EXPRESS_FLOW"];
            if (ct == null)
                ct = cs.Tables["PlanIndex"];
            if (ct == null)
                return new PLAN[0];
            //ct.SortBy("I_FLOW_ORDER");
            int col_fid = ct.GetColumnIndex("I_FLOW_ID");
            int col_consid = ct.GetColumnIndex("I_CONS_ID");
            int col_ftype = ct.GetColumnIndex("E_FLOW_TYPE");
            int col_subid = ct.GetColumnIndex("E_WORK_RANGE");
            int col_state = ct.GetColumnIndex("B_FLOW_STATE");
            int col_locom = ct.GetColumnIndex("I_LOCOM_ID");
            int col_plannum = ct.GetColumnIndex("I_FLOW_NO");
            int col_start = ct.GetColumnIndex("I_FLOW_NO_START");
            int col_trainnum = ct.GetColumnIndex("C_TRAIN_NUM");
            int col_maker = ct.GetColumnIndex("C_OP_MAKER");
            int col_starttime_r = ct.GetColumnIndex("D_FA_START");
            int col_starttime_p = ct.GetColumnIndex("D_PL_START");
            int col_endtime_r = ct.GetColumnIndex("D_FA_END");
            int col_endtime_p = ct.GetColumnIndex("D_PL_END");
            int col_starttrack = ct.GetColumnIndex("I_START_TRACK_ID");
            int col_locomworker = ct.GetColumnIndex("C_LOCOM_WORKER");
            int col_remark = ct.GetColumnIndex("C_REMARK");
            int col_locomwait = ct.GetColumnIndex("C_LOCOMWAIT_INFO");
            int col_unyard = ct.GetColumnIndex("I_LINE_ID_UNYARD");
            int col_cflowtype = ct.GetColumnIndex("C_FLOW_TYPE");
            PLAN[] ps = new PLAN[ct.Rows.Count];
            for (int i = 0; i < ps.Length; i++)
            {
                ClRow cr = ct.Rows[i];
                PLAN p = new PLAN();
                p.id = Convert.ToInt32(cr[col_fid]);
                p.consid = Convert.ToInt32(cr[col_consid]);
                p.flowtype = Convert.ToInt16(cr[col_ftype]);
                p.subid = Convert.ToUInt16(cr[col_subid]);
                p.state = Convert.ToUInt16(cr[col_state]);
                p.locom = Convert.ToInt16(cr[col_locom]);
                p.plannum = Convert.ToUInt16(cr[col_plannum]);
                p.StartNum = Convert.ToByte(cr[col_start]);
                p.trainnum = Convert.ToString(cr[col_trainnum]);
                p.maker = Convert.ToString(cr[col_maker]);
                p.startTime_real = Convert.ToDateTime(cr[col_starttime_r]);
                p.startTime_plan = Convert.ToDateTime(cr[col_starttime_p]);
                p.endTime_real = Convert.ToDateTime(cr[col_endtime_r]);
                p.endTime_plan = Convert.ToDateTime(cr[col_endtime_p]);
                p.StartTrack = Convert.ToInt16(cr[col_starttrack]);
                if (col_locomworker != -1)
                    p.locomworker = cr[col_locomworker].ToString();
                if (col_remark != -1)
                    p.remark = cr[col_remark].ToString();
                if (col_unyard != -1)
                    p.unyard = Convert.ToInt32(cr[col_unyard]);
                if (col_locomwait != -1)
                {
                    p.locomWaiList = new LocomWaitList();
                    p.locomWaiList.SetLocomWaitList(cr[col_locomwait].ToString());
                }
                if (col_cflowtype != -1)
                    p.cflowtype = cr[col_cflowtype].ToString();
                ps[i] = p;
            }
            return ps;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <returns></returns>
        public static PLAN[] Buf2Plan(byte[] buf)
        {
            if (ClSet.isClSet(buf, 0))
                return ClSet2Plans(new ClSet(buf));
            else
                return _Buf2Plan(buf);
        }
        static PLAN[] _Buf2Plan(byte[] buf)
        {
            int mode = 0, sum;
            if (buf[off_datatype] == Command.DataType_Plan)
                mode = 1;
            else if (buf[off_datatype] == Command.DataType_PlanIndex)
                mode = 0;
            else
                return null;
            int l;
            l = BitConverter.ToInt16(buf, off_plansum);
            //			if(l<=0)
            //				return null;
            PLAN[] t;
            t = new PLAN[l];
            l = off_packagehead;
            int i, j, k;
            for (i = 0; i < t.Length; i++)
            {
                t[i] = new PLAN();
                t[i].id = BitConverter.ToInt32(buf, l + off_planid);
                t[i].consid = BitConverter.ToInt32(buf, l + off_consid);
                t[i].flowtype = BitConverter.ToInt16(buf, l + off_flowtype);
                t[i].locom = BitConverter.ToInt16(buf, l + off_locom);
                t[i].subid = buf[l + off_subid];
               
                t[i].plannum = BitConverter.ToUInt16(buf, l + off_plannum);
                t[i].StartNum = buf[l + off_startnum];
                t[i].state = BitConverter.ToUInt16(buf, l + off_state);
                t[i].trainnum = System.Text.Encoding.Default.GetString(buf, l + off_tn, 15).Trim('\0').Trim();
                t[i].maker = System.Text.Encoding.Default.GetString(buf, l + off_maker, 10).Trim('\0').Trim();
                t[i].startTime_plan = Tools.Byte2Time(buf, l + off_starttime_p);
                t[i].endTime_plan = Tools.Byte2Time(buf, l + off_endtime_p);
                t[i].startTime_real = Tools.Byte2Time(buf, l + off_starttime_r);
                t[i].endTime_real = Tools.Byte2Time(buf, l + off_endtime_r);
                sum = buf[l + off_sum];
                l += off_planhead;
                if (mode == 1)
                {
                    t[i].cuts = new CUT[sum];
                    for (j = 0; j < t[i].cuts.Length; j++)
                    {
                        t[i].cuts[j] = new CUT();
                        t[i].cuts[j].track = BitConverter.ToInt16(buf, l + off_track);
                        t[i].cuts[j].coupFlag = buf[l + off_coup];
                        t[i].cuts[j].count = buf[l + off_cnt];
                        t[i].cuts[j].trackzero = BitConverter.ToUInt16(buf, l + off_zero);//t[i].cuts[j].trackzero=buf[l+off_zero];
                        t[i].cuts[j].startTime = Tools.Byte2Time(buf, l + off_cuttime1);
                        t[i].cuts[j].endTime = Tools.Byte2Time(buf, l + off_cuttime2);
                        l += off_note;
                        t[i].cuts[j].note = Tools.Byte2String(buf, ref l);
                        t[i].cuts[j].usernote = Tools.Byte2String(buf, ref l);

                        t[i].cuts[j].carnumlist = new int[t[i].cuts[j].count];
                        for (k = 0; k < t[i].cuts[j].count; k++)
                        {
                            t[i].cuts[j].carnumlist[k] = BitConverter.ToInt32(buf, l);
                            l += 4;
                        }
                    }
                    //short a = BitConverter.ToInt16(buf, l);
                    //l += 2;
                    //t[i].tnlist1 = new byte[a][];
                    //for (j = 0; j < a; j++)
                    //{
                    //    t[i].tnlist1[j] = new byte[12];
                    //    Buffer.BlockCopy(buf, l, t[i].tnlist1[j], 0, 12);
                    //    l += 12;
                    //}
                    //a = BitConverter.ToInt16(buf, l);
                    //l += 2;
                    //t[i].tnlist2 = new byte[a][];
                    //for (j = 0; j < a; j++)
                    //{
                    //    t[i].tnlist2[j] = new byte[12];
                    //    Buffer.BlockCopy(buf, l, t[i].tnlist2[j], 0, 12);
                    //    l += 12;
                    //}
                }
            }
            return t;
        }
        static byte[] Struct2Buf(PLAN[] t, int mode)
        {
            int l = off_packagehead;
            short a = 0;
            int i, j, k;
            int carnum;
            for (i = 0; i < t.Length; i++)
            {
                if (t[i] == null)
                    continue;
                if (t[i].cuts == null)
                    t[i].cuts = new CUT[0];
                //				if(t[i].cuts.Length==0)
                //					continue;
                a++;
                l += off_planhead;
                if (mode == 1)
                {
                    for (j = 0; j < t[i].cuts.Length; j++)
                    {
                        l += off_note;
                        l += ((byte[])System.Text.Encoding.Default.GetBytes(t[i].cuts[j].note)).Length + 1;
                        l += ((byte[])System.Text.Encoding.Default.GetBytes(t[i].cuts[j].usernote)).Length + 1;
                        l += t[i].cuts[j].count * 4;
                    }
                    //l += 2;
                    //l += t[i].tnlist1.Length * 12;
                    //l += 2;
                    //l += t[i].tnlist2.Length * 12;
                }
            }
            //if(a==0)
            //	return null;
            byte[] buf = new byte[l];
            if (mode == 1)
                buf[off_datatype] = Command.DataType_Plan;
            else
                buf[off_datatype] = Command.DataType_PlanIndex;
            BitConverter.GetBytes(a).CopyTo(buf, off_plansum);
            l = off_packagehead;
            for (i = 0; i < t.Length; i++)
            {
                if (t[i] == null)
                    continue;
               
                a = (short)t[i].id;
                BitConverter.GetBytes(t[i].id).CopyTo(buf, l + off_planid);
                BitConverter.GetBytes(t[i].consid).CopyTo(buf, l + off_consid);
                BitConverter.GetBytes(t[i].flowtype).CopyTo(buf, l + off_flowtype);     
                BitConverter.GetBytes(t[i].locom).CopyTo(buf, l + off_locom);
                BitConverter.GetBytes(t[i].subid).CopyTo(buf, l + off_subid);
               // buf[l + off_subid] = t[i].subid;               
                BitConverter.GetBytes(t[i].plannum).CopyTo(buf, l + off_plannum);
                buf[l + off_startnum] = t[i].StartNum;
                BitConverter.GetBytes(t[i].state).CopyTo(buf, l + off_state);               
                byte[] btn = System.Text.Encoding.Default.GetBytes(t[i].trainnum);
                if (btn.Length > 15)
                    Buffer.BlockCopy(btn, 0, buf, l + off_tn, 15);
                else
                    btn.CopyTo(buf, l + off_tn);
                if (t[i].maker == null)
                    t[i].maker = "　";
                btn = System.Text.Encoding.Default.GetBytes(t[i].maker);
                if (btn.Length > 10)
                    Buffer.BlockCopy(btn, 0, buf, l + off_maker, 10);
                else
                    btn.CopyTo(buf, l + off_maker);
                Tools.Time2Byte(t[i].startTime_plan).CopyTo(buf, l + off_starttime_p);
                Tools.Time2Byte(t[i].endTime_plan).CopyTo(buf, l + off_endtime_p);
                Tools.Time2Byte(t[i].startTime_real).CopyTo(buf, l + off_starttime_r);
                Tools.Time2Byte(t[i].endTime_real).CopyTo(buf, l + off_endtime_r);
                buf[l + off_sum] = (byte)t[i].cuts.Length;
                l += off_planhead;
                if (mode == 1)
                {
                    for (j = 0; j < t[i].cuts.Length; j++)
                    {
                        BitConverter.GetBytes(t[i].cuts[j].track).CopyTo(buf, l + off_track);
                        buf[l + off_coup] = t[i].cuts[j].coupFlag;
                        buf[l + off_cnt] = t[i].cuts[j].count;
                        BitConverter.GetBytes(t[i].cuts[j].trackzero).CopyTo(buf, l + off_zero);//buf[l+off_zero]=t[i].cuts[j].trackzero;
                        Tools.Time2Byte(t[i].cuts[j].startTime).CopyTo(buf, l + off_cuttime1);
                        Tools.Time2Byte(t[i].cuts[j].endTime).CopyTo(buf, l + off_cuttime2);
                        l += off_note;
                        Tools.String2Byte(t[i].cuts[j].note, buf, ref l);
                        Tools.String2Byte(t[i].cuts[j].usernote, buf, ref l);
                        for (k = 0; k < t[i].cuts[j].count; k++)
                        {
                            if (k < t[i].cuts[j].carnumlist.Length)
                                carnum = t[i].cuts[j].carnumlist[k];
                            else
                                carnum = 0;
                            BitConverter.GetBytes(carnum).CopyTo(buf, l);
                            l += 4;
                        }
                    }
                  
                }
            }
            return buf;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] PlanIndex2Buf(PLAN[] t)
        {
            return Struct2Buf(t, 0);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] Plan2Buf(PLAN[] t)
        {
            return Struct2Buf(t, 1);
        }
        /// <summary>
        /// 工步表行转化为cut结构
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static CUT DataRow2CutStruct(DataRow dr)
        {
            CUT ct = new CUT();
            ct.track = Convert.ToInt16(dr["I_LINE_ID_DEST"]);
            int symgetput = Convert.ToInt32(dr["E_SYM_GETPUT"]);
            if (symgetput == CIPS.DB.E_SYM_GETPUT.GET)
                ct.add_del = '+';
            else if (symgetput == CIPS.DB.E_SYM_GETPUT.PUT)
                ct.add_del = '-';
            else
                ct.add_del = ' ';
            ct.note = dr["C_REMARK"].ToString();
            ct.count = Convert.ToByte(dr["I_CAR_NUM"]);
            int cuttype = Convert.ToInt32(dr["E_CUT_TYPE"]);
            switch (cuttype)
            {
                case CIPS.DB.E_CUT_TYPE.CONNECT:
                    ct.type = CUT.CT_GET;
                    break;
                case CIPS.DB.E_CUT_TYPE.SPLIT_NOTHROWING:
                    if (ct.add_del == '+')
                        ct.type = CUT.CT_GET_JL;
                    else
                        ct.type = CUT.CT_PUT_JL;
                    break;
                case CIPS.DB.E_CUT_TYPE.SPLIT_WEAVE:
                    if (ct.add_del == '+')
                        ct.type = CUT.CT_GET_YH;
                    else
                        ct.type = CUT.CT_PUT_YH;
                    break;
                case CIPS.DB.E_CUT_TYPE.PRECLASSI_SINGL:
                    ct.type = CUT.CT_PUT_DL;
                    break;
                case CIPS.DB.E_CUT_TYPE.PRECLASSI_CONTINUE:
                    ct.type = CUT.CT_PUT_LL;
                    break;
                case CIPS.DB.E_CUT_TYPE.SWITCHING:
                    if (ct.add_del == '+')
                        ct.type = CUT.CT_GET;
                    else
                        ct.type = CUT.CT_PUT;
                    break;
                default:
                    break;
            }
            if (Convert.ToDateTime(dr["D_FA_START"]).Year > 2000)
                ct.startTime = Convert.ToDateTime(dr["D_FA_START"]);
            if (Convert.ToDateTime(dr["D_FA_END"]).Year > 2000)
                ct.endTime = Convert.ToDateTime(dr["D_FA_END"]);
            return ct;
        }

        #region 实例化过程
       
        static short GetCutType(CUT cut)
        {
            byte ct = cut.type;

            if (ct == CUT.CT_GET_JL || ct == CUT.CT_PUT_JL)
                return CIPS.DB.E_CUT_TYPE.SPLIT_NOTHROWING;//CT_禁溜调车;
            else if (ct == CUT.CT_GET_YH || ct == CUT.CT_PUT_YH)
                return CIPS.DB.E_CUT_TYPE.SPLIT_WEAVE;//CT_迂回调车;
            else if (ct == CUT.CT_PUT_DL)
                return CIPS.DB.E_CUT_TYPE.PRECLASSI_SINGL;//CT_单溜;
            else if (ct == CUT.CT_PUT_LL)
                return CIPS.DB.E_CUT_TYPE.PRECLASSI_CONTINUE;//CT_连溜;
            else if (ct == CUT.CT_THROUGH)
                return CIPS.DB.E_CUT_TYPE.PASS;//过
            return CIPS.DB.E_CUT_TYPE.SWITCHING;//CT_调车;
           
        }
        static void AdjustCutType(CIPS.Connect.CutsInfo ci)
        {
            
        }
        static CIPS.Connect.AssemAlterInfo Cut2AssemAlter(CUT c, string oldtn)
        {
            string[] tns;
            char[] chsplit = new char[1];
            chsplit[0] = ',';
            tns = c.note.Split(chsplit);
            if (tns.Length == 2)
            {
                if (oldtn == null)
                    oldtn = tns[0];
                CIPS.Connect.AssemAlterInfo aai;
                aai = new CIPS.Connect.AssemAlterInfo();
                aai.newTraNum = tns[1];
                aai.oldTraNum = oldtn;
                aai.lineID = c.track;
                aai.finished = false;
             
                return aai;
            }
            return null;
        }
        static CIPS.Connect.AssemAlterInfo GetAssemAlter_TN(CUT[] cts, int pos)
        {
            int i;
            short tid = cts[pos].track;
            if (cts[pos].AutoTN)
                return null;
            for (i = pos + 1; i < cts.Length; i++)
            {
                if (cts[i].track == tid && cts[i].type == CUT.CT_TNAME)
                {
                    return null;
                }
            }
            string oldtn = null;
            string[] tns;
            char[] chsplit = new char[1];
            chsplit[0] = ',';
            for (i = 0; i < pos; i++)
            {
                if (cts[i].type == CUT.CT_TNAME && cts[i].track == tid)
                {
                    tns = cts[i].note.Split(chsplit);
                    if (tns.Length == 2)
                    {
                        oldtn = tns[0];
                    }
                    break;
                }
            }
            return Cut2AssemAlter(cts[pos], oldtn);
        }
        static CIPS.Connect.AssemAlterInfo GetAssemAlter_LOCK(CUT[] cts, int pos)
        {
            if (!cts[pos].LockHump_After)
                return null;
            int i;
            short tid = cts[pos].track;
            for (i = pos + 1; i < cts.Length; i++)
            {
                if (cts[i].track == tid && cts[i].type == CUT.CT_LOCKHUMP && cts[i].LockHump_After)
                {
                    return null;
                }
            }
            return GetAssemAlter_DRAGOUT(cts[pos]);
        }
        static CIPS.Connect.AssemAlterInfo GetAssemAlter_DRAGOUT(CUT c)
        {
            CIPS.Connect.AssemAlterInfo aai;
            aai = new CIPS.Connect.AssemAlterInfo();
            aai.newTraNum = "";
            aai.oldTraNum = c.note;
            aai.lineID = c.track;
            aai.finished = true;
            //aai.mainSub = c.count;
            return aai;
        }
        /// <summary>
        /// 转换为CIPS计划系统的流程结构
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static CIPS.Connect.CutsInfo Plan2CutsInfo(PLAN p)
        {
            CIPS.Connect.CutsInfo ci = new CIPS.Connect.CutsInfo();
            ci.synthetize.flowID = p.id;
            ci.synthetize.trainNum = p.trainnum;
            ci.synthetize.splitTime = p.startTime_plan;
            ci.synthetize.locoID = p.locom;
            ci.synthetize.printNo = p.PlanNum;
            ci.synthetize.flowType = p.Type;
            ci.synthetize.controllerName = p.maker;
            ci.synthetize.workRange = p.WorkStation;
            int i, j, n;
            int m = 0;
            n = 0;
            ci.assemAlterList = new CIPS.Connect.AssemAlterInfo[p.cuts.Length];
            byte type;
            for (i = 0; i < p.cuts.Length; i++)
            {
                type = p.cuts[i].type;
                if (type == CUT.CT_TNAME)
                {
                    ci.assemAlterList[m] = GetAssemAlter_TN(p.cuts, i);
                    if (ci.assemAlterList[m] != null)
                    {
                        ci.assemAlterList[m].opOrder = n + p.StartNum;
                        m++;
                    }
                    continue;
                }
                else if (type == CUT.CT_LOCKHUMP)
                {
                    ci.assemAlterList[m] = GetAssemAlter_LOCK(p.cuts, i);
                    if (ci.assemAlterList[m] != null)
                    {
                        ci.assemAlterList[m].opOrder = n + p.StartNum;
                        m++;
                    }
                    continue;
                }
                else if (type == CUT.CT_DRAGOUT)
                {
                    ci.assemAlterList[m] = GetAssemAlter_DRAGOUT(p.cuts[i]);
                    m++;
                    continue;
                }
                if (p.cuts[i].add_del != ' ' || p.cuts[i].type == CUT.CT_THROUGH)
                {
                    ci[n].lineID = p.cuts[i].track;
                    ci[n].cutOrder = n + p.StartNum + 1;
                    ci[n].carCount = p.cuts[i].count;
                    ci[n].narrate = p.cuts[i].note + "\n" + p.cuts[i].usernote;
                    if (p.cuts[i].add_del == '+')
                        ci[n].coupFlag = 1;
                    else if (p.cuts[i].add_del == '-')
                        ci[n].coupFlag = 2;
                    else
                        ci[n].coupFlag = 0;
                    ci[n].cutType = GetCutType(p.cuts[i]);
                    if (p.cuts[i].port)
                        ci[n].port = 0;
                    else
                        ci[n].port = 1;
                    ci[n].passStop = p.cuts[i].throughTCQ;
                    if (p.cuts[i].MultiWay)
                        ci[n].attatchLineID = p.cuts[i].doorway;
                    else
                        ci[n].attatchLineID = p.cuts[i].track;
                    if (p.cuts[i].type == CUT.CT_GET_DISORDER)
                    {
                        ci.attachCars = new CIPS.Connect.PlanCar[p.cuts[i].carnumlist.Length];
                        for (j = 0; j < p.cuts[i].carnumlist.Length; j++)
                        {
                            ci.attachCars[j] = new CIPS.Connect.PlanCar();
                            ci.attachCars[j].carCode = CAR.CarCoFromCarNum((uint)p.cuts[i].carnumlist[j]);
                          
                        }
                    }
                    ci[n].avWeight = p.cuts[i].Wegh * 4;
                    if (p.cuts[i].HaveLoadCar)
                        ci[n].avWeight |= 0x10;
                    if (p.cuts[i].HaveEmptyCar)
                        ci[n].avWeight |= 0x01;
                    n++;
                }
            }
            CIPS.Connect.AssemAlterInfo[] aai = new CIPS.Connect.AssemAlterInfo[m];
            for (i = 0; i < aai.Length; i++)
                aai[i] = ci.assemAlterList[i];
            ci.assemAlterList = aai;
            ci.count = n;
            AdjustCutType(ci);
            return ci;
        }
        /// <summary>
        /// 由CIPS计划系统方式工步类型转换
        /// </summary>
        /// <param name="coupflag">+/-</param>
        /// <param name="cartype">CIPS工步类型</param>
        /// <returns></returns>
        public static byte ToCutType(int coupflag, int cartype)
        {
            if (cartype == CIPS.DB.E_CUT_TYPE.PASS)
                return CUT.CT_THROUGH;
            else if (cartype == CIPS.DB.E_CUT_TYPE.PRECLASSI_SINGL)//单溜
                return CUT.CT_PUT_DL;
            else if (cartype == CIPS.DB.E_CUT_TYPE.PRECLASSI_CONTINUE)//CT_连溜)
                return CUT.CT_PUT_LL;
            else if (cartype == CIPS.DB.E_CUT_TYPE.SPLIT_NOTHROWING)//CT_禁溜调车)
            {
                if (coupflag == CIPS.DB.E_SYM_GETPUT.GET)
                    return CUT.CT_GET_JL;
                else
                    return CUT.CT_PUT_JL;
            }
            else if (cartype == CIPS.DB.E_CUT_TYPE.SPLIT_WEAVE)//CT_迂回调车)
            {
                if (coupflag == CIPS.DB.E_SYM_GETPUT.GET)
                    return CUT.CT_GET_YH;
                else
                    return CUT.CT_PUT_YH;
            }
            else if (cartype == CIPS.DB.E_CUT_TYPE.SWITCHING)
            {
                if (coupflag == CIPS.DB.E_SYM_GETPUT.GET)
                    return CUT.CT_GET;
                else if (coupflag == CIPS.DB.E_SYM_GETPUT.PUT)
                    return CUT.CT_PUT;
                else
                    return CUT.CT_NULL;
            }
            else
                return CUT.CT_NULL;
        }
        /// <summary>
        /// 由CIPS计划系统流程结构转化
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static PLAN CutsInfo2Plan(CIPS.Connect.CutsInfo ci)
        {
            PLAN p = new PLAN();
            int i, n;
            p.id = (int)ci.synthetize.flowID;
            p.trainnum = ci.synthetize.trainNum;
            p.startTime_plan = ci.synthetize.splitTime;
            p.endTime_plan = ci.synthetize.splitEndTime;
            p.Pushed = ci.synthetize.momState >= CIPS.DB.E_MOM.SWIT_PLAN;
            p.locom = (short)ci.synthetize.locoID;
            p.PlanNum = (byte)ci.synthetize.printNo;
            p.Type = (short)ci.synthetize.flowType;
            p.WorkStation = ci.synthetize.workRange;
            p.StartNum = 0;
            p.maker = ci.synthetize.controllerName;
            p.cuts = new CUT[ci.count];
            n = 0;          
            CUT cut;
            for (i = 0; i < ci.count; i++)
            {
                p.cuts[n] = new CUT();
                cut = p.cuts[n];
                if ((ci[i].coupFlag == 0 || ci[i].carCount == 0) && ci[i].cutType != CIPS.DB.E_CUT_TYPE.PASS)
                {
                    continue;           
                }
                else
                {
                    cut.track = (short)ci[i].lineID;
                    cut.count = (byte)ci[i].carCount;
                    string[] notes = ci[i].narrate.Split('\n');
                    if (notes.Length > 0)
                        cut.note = notes[0];
                    if (notes.Length > 1)
                        cut.usernote = notes[1];
                    cut.coupFlag = ToCutType(ci[i].coupFlag, ci[i].cutType);
                    cut.carnumlist = new int[cut.count];
                    if (ci[i].attatchLineID != ci[i].lineID && ci[i].attatchLineID != 0)
                        cut.doorway = (short)(ci[i].attatchLineID);
                    if (ci[i].port == 0)
                        cut.port = true;
                    else
                        cut.port = false;
                    if (i == 0)
                        p.dir = cut.port;
                    cut.throughTCQ = ci[i].passStop;
                    if (ci[i].cutState >= CIPS.DB.E_CUT_STATE.USE)
                    {
                        cut.startTime = DateTime.Now;
                        cut.endTime = DateTime.Now;
                    }
                    cut.HaveEmptyCar = (ci[i].avWeight & 0x01) != 0;//主系统低一位表示是否有空车
                    cut.HaveLoadCar = (ci[i].avWeight & 0x10) != 0;//主系统低二位表示是否有重车
                    cut.Wegh = ci[i].avWeight / 4;//主系统高30位表示重量
                    n++;
                }
            }
            if (p.cuts.Length != n)
            {
                CUT[] cts = new CUT[n];
                for (i = 0; i < cts.Length; i++)
                    cts[i] = p.cuts[i];
                p.cuts = cts;
            }
            return p;
        }
        #endregion
       
        /// <summary>
        /// CIPS系统编组计划序列化
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static byte[] Ass2Buf(CIPS.Connect.MassInfo mi)
        {
           
            ClassiTrack ct;
            System.Collections.ArrayList a = new System.Collections.ArrayList();
         
            int i, k;
            for (k = 0; k < 2; k++)
            {
                for (i = 0; i < mi.nCurPriorNum; i++)
                {
                    ct = new ClassiTrack();
                    ct.trainnum = mi[i].traNum;
                  
                    ct.classiid = mi[i].classID;
                    ct.carcount[0] = (short)mi[i].amountLow;
                    ct.carcount[1] = (short)mi[i].amountUp;
                    ct.length[0] = (float)mi[i].lenLow;
                    ct.length[1] = (float)mi[i].lenUp;
                    ct.weight[0] = (short)(mi[i].weightLow);
                    ct.weight[1] = (short)(mi[i].weightUp);
                    a.Add(ct);
                }
                mi.curWorkRange = 2;
            }
            Command.ExpressCommand cmd = new Command.ExpressCommand();
            cmd.Cmd = Command.Cmd_SendAss;
            cmd.Datatype = Command.DataType_Ass;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            BinaryReader reader = new BinaryReader(stream);
            formatter.Serialize(stream, a);
            int n = (int)stream.Length;
            cmd.byteData = new byte[n];
            stream.Seek(0, SeekOrigin.Begin);
            reader.Read(cmd.byteData, 0, n);
            stream.Close();
            byte[] b = cmd.Serialize();
            return b;
        }
       
    }
    /// <summary>
    /// 调机等待作业结构
    /// </summary>
    public class LocomWaitInfo
    {
        /// <summary>
        /// 等待内容
        /// </summary>
        public string WaitDisp = "";
        /// <summary>
        /// 等待勾
        /// </summary>
        public byte CutIndex = 0;
        private short _WaitMinute = 0;
        /// <summary>
        /// 等待时间
        /// </summary>
        public short WaitMinute
        {
            get
            {
                if (D_START == DateTime.MinValue || D_END == DateTime.MinValue)
                    return _WaitMinute;
                else
                {
                    TimeSpan ts = D_END - D_START;
                    return (short)ts.TotalMinutes;
                }
            }
            set
            {
                _WaitMinute = value;
            }
        }
        /// <summary>
        /// 等待开始时间
        /// </summary>
        public DateTime D_START = DateTime.MinValue;
        /// <summary>
        /// 等待结束时间
        /// </summary>
        public DateTime D_END = DateTime.MinValue;
    }
    /// <summary>
    /// 调机等待作业列表
    /// </summary>
    public class LocomWaitList
    {
        /// <summary>
        /// 作业列表
        /// </summary>
        public List<LocomWaitInfo> locomWaitInfos = new List<LocomWaitInfo>();
        /// <summary>
        /// 合计等待时间
        /// </summary>
        public int LocomWaitMinute
        {
            get
            {
                int tep = 0;
                foreach (LocomWaitInfo l in locomWaitInfos)
                {
                    tep += l.WaitMinute;
                }
                return tep;
            }
        }
        /// <summary>
        /// 等待作业最终结束时间
        /// </summary>
        public DateTime D_Last
        {
            get
            {
                DateTime d = DateTime.MinValue;
                foreach (LocomWaitInfo l in locomWaitInfos)
                {
                    if (l.D_END >= d)
                        d = l.D_END;
                }
                return d;
            }
        }
        /// <summary>
        /// 等待作业最终开始时间
        /// </summary>
        public DateTime D_First
        {
            get
            {
                DateTime d = DateTime.MaxValue;
                foreach (LocomWaitInfo l in locomWaitInfos)
                {
                    if (l.D_START <= d)
                        d = l.D_START;
                }
                return d;
            }
        }
        /// <summary>
        /// 获取调机等待字符串
        /// </summary>
        /// <returns></returns>
        public string GetLocomWaitStr()
        {
            string str = "";
            foreach (CIPS.Express.ConvertDat.LocomWaitInfo WaitInfo in locomWaitInfos)
            {
                str += WaitInfo.WaitDisp + ",";
                str += WaitInfo.CutIndex.ToString() + ",";
                str += WaitInfo.WaitMinute.ToString() + ",";
                str += WaitInfo.D_START.ToString() + ",";
                str += WaitInfo.D_END.ToString() + ";";
            }
            return str.TrimEnd(';');
        }

        /// <summary>
        /// 设置调机等待列表
        /// </summary>
        /// <param name="LocomWaitStr"></param>
        /// <returns></returns>
        public void SetLocomWaitList(string LocomWaitStr)
        {
            locomWaitInfos = new List<CIPS.Express.ConvertDat.LocomWaitInfo>();
            LocomWaitInfo WaitInfo;
            LocomWaitStr = LocomWaitStr.Trim();
            if (LocomWaitStr == "")
                return;
            string[] sss, ss = LocomWaitStr.Split(';');
            try
            {
                foreach (string s in ss)
                {
                    sss = s.Split(',');
                    if (sss.Length >= 5)
                    {
                        WaitInfo = new LocomWaitInfo();
                        WaitInfo.WaitDisp = "";
                        for (int i = 0; i < sss.Length - 4; i++)
                            WaitInfo.WaitDisp += sss[i];
                        WaitInfo.CutIndex = Convert.ToByte(sss[sss.Length - 4]);
                        WaitInfo.WaitMinute = Convert.ToInt16(sss[sss.Length - 3]);
                        WaitInfo.D_START = Convert.ToDateTime(sss[sss.Length - 2]);
                        WaitInfo.D_END = Convert.ToDateTime(sss[sss.Length - 1]);
                        locomWaitInfos.Add(WaitInfo);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 获取相应表
        /// </summary>
        /// <returns></returns>
        public ClTable GetCLTable()
        {
            CIPS.ClTable ct = null;
            ct = new ClTable("LocomWait");
            ct.AddColumn("WaitDisp", typeof(string));
            ct.AddColumn("CutIndex", typeof(byte));
            ct.AddColumn("WaitMinute", typeof(short));
            ct.AddColumn("StartTime", typeof(DateTime));
            ct.AddColumn("EndTime", typeof(DateTime));
            if (locomWaitInfos != null && locomWaitInfos.Count > 0)
            {
                CIPS.ClRow cr;
                for (int i = 0; i < locomWaitInfos.Count; i++)
                {
                    cr = ct.NewRow();
                    cr["WaitDisp"] = locomWaitInfos[i].WaitDisp;
                    cr["CutIndex"] = locomWaitInfos[i].CutIndex;
                    cr["WaitMinute"] = locomWaitInfos[i].WaitMinute;
                    cr["StartTime"] = locomWaitInfos[i].D_START;
                    cr["EndTime"] = locomWaitInfos[i].D_END;
                    ct.Rows.Add(cr);
                }
            }
            return ct;
        }

        /// <summary>
        /// 设置调机等待列表
        /// </summary>
        /// <param name="ct"></param>
        public void SetLocomWaitList(ClTable ct)
        {
            locomWaitInfos = new List<CIPS.Express.ConvertDat.LocomWaitInfo>();
            if (ct == null)
                return;
            LocomWaitInfo WaitInfo;
            foreach (CIPS.ClRow cr in ct.Rows)
            {
                WaitInfo = new LocomWaitInfo();
                WaitInfo.WaitDisp = cr["WaitDisp"].ToString();
                WaitInfo.CutIndex = Convert.ToByte(cr["CutIndex"]);
                WaitInfo.WaitMinute = Convert.ToInt16(cr["WaitMinute"]);
                WaitInfo.D_START = Convert.ToDateTime(cr["StartTime"]);
                WaitInfo.D_END = Convert.ToDateTime(cr["EndTime"]);
                locomWaitInfos.Add(WaitInfo);
            }
        }

        public void CopyFrom(LocomWaitList lwl)
        {
            if (lwl == null)
                return;
            this.SetLocomWaitList(lwl.GetLocomWaitStr());
        }
    }
}