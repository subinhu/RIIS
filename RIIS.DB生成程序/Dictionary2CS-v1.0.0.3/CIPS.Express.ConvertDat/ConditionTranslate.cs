using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// 条件分析
    /// </summary>
    public class ConditionTranslate
    {
        ConditionGroup myGroup;
        /// <summary>
        /// 条件分析
        /// </summary>
        /// <param name="txt">条件字符串</param>
        public ConditionTranslate(string txt)
        {
            myGroup = SplitConditionString(txt);
        }
        /// <summary>
        /// 逻辑值
        /// </summary>
        public bool Value
        {
            get
            {
                if (myGroup != null)
                    return myGroup.Value;
                else
                    return false;
            }
        }
        /// <summary>
        /// 表达式错误
        /// </summary>
        public bool Error
        {
            get
            {
                return myGroup == null;
            }
        }
        /// <summary>
        /// 运算表达式集合
        /// </summary>
        public List<Condition> AllConditions
        {
            get
            {
                List<Condition> cs = new List<Condition>();
                if (myGroup != null)
                    myGroup.GetAllCondition(cs);
                return cs;
            }
        }
        Condition GetCondition(string txt)
        {
            txt = txt.Trim();
            if (txt == "")
                return null;
            string[] sp ={ "<!>", ">!<" ,"!=", ">=", "<=", "<>", "><", ">", "<", "="};
            string s = txt;
            foreach (string c in sp)
                s = s.Replace(c, "\0");
            //string s = txt.Replace(">=", ")").Replace("<=", "(").Replace("<>", "&").Replace("><", "|");
            //string[] ss = s.Split("><=()&|".ToCharArray());
            string[] ss = s.Split('\0');
            if (ss.Length != 2)
                return null;
            foreach (string c in sp)
            {
                if (txt.Contains(c))
                    return new Condition(ss[0].Trim(), c, ss[1].Trim());
            }
            return null;
        }
        ConditionGroup SplitConditionString(string txt)
        {
            char c;
            Condition cd;
            string s = "";
            ConditionGroup group = new ConditionGroup();
            for (int i = 0; i < txt.Length; i++)
            {
                cd = GetCondition(s);
                c = txt[i];
                if (c == '(')
                {
                    s = "";
                    if (cd != null)
                        return null;
                    ConditionGroup cg = new ConditionGroup();
                    group.AddCondition(cg);
                    group = cg;
                }
                else if (c == ')')
                {
                    s = "";
                    if (cd != null)
                        group.AddCondition(cd);
                    if (group.Parent == null)
                        return null;
                    group = group.Parent;
                }
                else if (c == '&' || c == '|')
                {
                    s = "";
                    if (cd != null)
                        group.AddCondition(cd);
                    if (group.Method == ConditionGroup.Method_Or)
                    {
                        if (c == '&')
                        {
                            ConditionGroup g = new ConditionGroup();
                            g.Method = ConditionGroup.Method_And;
                            ConditionValue cc = group.RemoveLast();
                            if (cc == null)
                                return null;
                            g.AddCondition(cc);
                            group.AddCondition(g);
                            group = g;
                        }
                    }
                    else if (group.Method == ConditionGroup.Method_And)
                    {
                        if (c == '|')
                        {
                            ConditionGroup g;
                            if (group.Parent != null && group.Parent.Method == ConditionGroup.Method_Or)
                                g = group.Parent;
                            else
                            {
                                g = new ConditionGroup();
                                g.Method = ConditionGroup.Method_Or;
                                group.InsertCondition(g);
                            }
                            group = g;
                        }
                    }
                    else
                    {
                        if (c == '&')
                            group.Method = ConditionGroup.Method_And;
                        else
                            group.Method = ConditionGroup.Method_Or;
                    }
                }
                else
                    s += c;
            }
            cd = GetCondition(s);
            if (cd != null)
                group.AddCondition(cd);
            while (group.Parent != null)
                group = group.Parent;
            if (!group.Error)
                return group;
            return null;
        }
        /// <summary>
        /// 条件描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return myGroup.ToString();
        }
        interface ConditionValue
        {
            bool GetValue();
        }
        /// <summary>
        /// 运算表达式
        /// </summary>
        public class Condition : ConditionValue
        {
            internal Condition(string field, string mothod, string value)
            {
                _ConditionField = field;
                _ConditionValue = value;
                _ConditionMethod = mothod;
            }
            /// <summary>
            /// 条件变量
            /// </summary>
            public string ConditionField
            {
                get
                {
                    return _ConditionField;
                }
            }
            /// <summary>
            /// 运算方法
            /// </summary>
            public string ConditionMethod
            {
                get
                {
                    return _ConditionMethod;
                }
            }
            /// <summary>
            /// 期望结果
            /// </summary>
            public string ConditionValue
            {
                get
                {
                    return _ConditionValue;
                }
            }
            string _ConditionField = "";
            string _ConditionMethod = "";
            string _ConditionValue = "";
            bool ConditionValue.GetValue()
            {
                return Value;
            }
            /// <summary>
            /// 表达式结果
            /// </summary>
            public bool Value;
            /// <summary>
            /// 名称
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return _ConditionField + _ConditionMethod + _ConditionValue;
            }
        }
        class ConditionGroup : ConditionValue
        {
            public ConditionGroup()
            {
            }
            ConditionGroup myParent = null;
            public ConditionGroup Parent
            {
                get
                {
                    return myParent;
                }
            }
            public List<ConditionValue> mConditions = new List<ConditionValue>();
            public void AddCondition(ConditionValue c)
            {
                mConditions.Add(c);
                if (c is ConditionGroup)
                    ((ConditionGroup)c).myParent = this;
            }
            public void InsertCondition(ConditionGroup c)
            {
                ConditionGroup p = myParent;
                c.AddCondition(this);
                if (p != null)
                {
                    p.mConditions.Remove(this);
                    p.AddCondition(c);
                }
            }
            public ConditionValue RemoveLast()
            {
                if (mConditions.Count == 0)
                    return null;
                ConditionValue c = mConditions[mConditions.Count - 1];
                mConditions.Remove(c);
                return c;
            }
            public void GetAllCondition(List<Condition> cs)
            {
                foreach (ConditionValue c in mConditions)
                {
                    if (c is Condition)
                        cs.Add((Condition)c);
                    else
                        ((ConditionGroup)c).GetAllCondition(cs);
                }
            }
            public const byte Method_And = 1;
            public const byte Method_Or = 2;
            public byte Method = 0;
            bool ConditionValue.GetValue()
            {
                return Value;
            }
            public bool Error
            {
                get
                {
                    if (mConditions.Count == 0)
                        return true;
                    if (Method == 0 && mConditions.Count > 1)
                        return true;
                    else
                    {
                        foreach (ConditionValue c in mConditions)
                        {
                            if (!(c is ConditionGroup))
                                continue;
                            if (((ConditionGroup)c).Error)
                                return true;
                        }
                        return false;
                    }
                }
            }
            public bool Value
            {
                get
                {
                    if (Method == Method_And)
                    {
                        foreach (ConditionValue c in mConditions)
                        {
                            if (!c.GetValue())
                                return false;
                        }
                        return true;
                    }
                    else if (Method == Method_Or)
                    {
                        foreach (ConditionValue c in mConditions)
                        {
                            if (c.GetValue())
                                return true;
                        }
                        return false;
                    }
                    else
                    {
                        if (mConditions.Count == 1)
                            return mConditions[0].GetValue();
                        else
                            return false;
                    }
                }
            }
            public override string ToString()
            {
                string m = "?";
                if (Method == Method_And)
                    m = " and ";
                else if (Method == Method_Or)
                    m = " or ";
                else
                    m = "?";
                string s = "";
                foreach (ConditionValue c in mConditions)
                    s += c.ToString() + m;
                return "(" + s.Remove(s.Length - m.Length) + ")";
            }
        }
    }
}
