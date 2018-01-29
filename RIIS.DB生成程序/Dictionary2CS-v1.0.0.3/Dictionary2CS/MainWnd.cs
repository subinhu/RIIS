using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Dictionary2CS
{
    public partial class MainWnd : Form
    {
        //DataSet ds;
        public MainWnd()
        {
            InitializeComponent();
            txtWeb.Text = System.Configuration.ConfigurationManager.AppSettings["WebService"];
            //CIPS.ClTable ct;
            //ds = new DataSet();
            //string[] tables = new string[] { "DC_RA_MIXCO", "DC_SA_FUNCTION", "DC_SA_EXE" };
            //foreach (string t in tables)
            //{
            //    ct = CIPS.Dictionary.DictTables.LoadClTable(System.Configuration.ConfigurationManager.AppSettings["WebService"], t, "", 1);
            //    ds.Tables.Add(ct.GetDataTable());
            //}
        }

        private void btMixco_Click(object sender, EventArgs e)
        {
            
            //CIPS.ClTable ct = CIPS.Dictionary.DictTables.LoadClTable(txtWeb.Text, "DC_MIXCO", "", 1);
            DbWebSvc.DbWebSvc webSvc = new Dictionary2CS.DbWebSvc.DbWebSvc();
            List<string> tableNames = new List<string>();
            tableNames.Add("DC_MIXCO");
            DataSet ds = webSvc.GetDataTables(tableNames.ToArray());
            string s;
            if (ds != null && ds.Tables.Count>0 && ds.Tables[0]!=null)
                s = MakeMixco.BuildCS("..\\..\\..\\RIIS.DB\\RIIS.DB\\EnumSet.cs", ds.Tables[0]);
            else
                s = "无法打开数据";
            MessageBox.Show(this, s, "Build DC_RA_MIXCO");
        }

        private void brFunction_Click(object sender, EventArgs e)
        {
            CIPS.ClTable ct;
            DataSet ds = new DataSet();
            string[] tables = new string[] { "DC_SA_FUNCTION", "DC_SA_EXE" };
            foreach (string t in tables)
            {
                ct = CIPS.Dictionary.DictTables.LoadClTable(txtWeb.Text, t, "", 1);
                if (ct == null)
                {
                    MessageBox.Show(this, "无法打开数据-" + t, "Build DC_RA_MIXCO");
                    return;
                }
                ds.Tables.Add(ct.GetDataTable());
            }
            string s = MakeFunc.BuildCS("..\\..\\..\\CIPS.DB\\CIPS.DB\\Function.cs", ds);
            s += "\n\n" + MakeFuncStr.BuildCS("..\\..\\..\\CIPS.DB\\CIPS.DB\\FunctionStr.cs", ds);
            MessageBox.Show(this, s, "Build DC_SA_FUNCTION");
        }

        private void btCarChara_Click(object sender, EventArgs e)
        {
            CIPS.ClTable ct = CIPS.Dictionary.DictTables.LoadClTable(txtWeb.Text, "DC_RA_CARCHARA", "", 1);
            string s;
            if (ct != null)
                s = MakeCarChara.BuildCS("..\\..\\..\\CIPS.DB\\CIPS.DB\\CarChara.cs", ct.GetDataTable());
            else
                s = "无法打开数据";
            MessageBox.Show(this, s, "Build DC_RA_CARCHARA");
        }
    }
}