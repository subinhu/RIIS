﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RIIS.Cars.Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            RIIS.CarManager.Main_DBManager _main = new RIIS.CarManager.Main_DBManager();
            
            
        }
    }
}
