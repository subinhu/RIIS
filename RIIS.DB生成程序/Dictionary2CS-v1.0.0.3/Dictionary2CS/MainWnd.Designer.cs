namespace Dictionary2CS
{
    partial class MainWnd
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btMixco = new System.Windows.Forms.Button();
            this.brFunction = new System.Windows.Forms.Button();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.btCarChara = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btMixco
            // 
            this.btMixco.Location = new System.Drawing.Point(25, 12);
            this.btMixco.Name = "btMixco";
            this.btMixco.Size = new System.Drawing.Size(75, 23);
            this.btMixco.TabIndex = 0;
            this.btMixco.Text = "转杂类";
            this.btMixco.UseVisualStyleBackColor = true;
            this.btMixco.Click += new System.EventHandler(this.btMixco_Click);
            // 
            // brFunction
            // 
            this.brFunction.Location = new System.Drawing.Point(25, 41);
            this.brFunction.Name = "brFunction";
            this.brFunction.Size = new System.Drawing.Size(75, 23);
            this.brFunction.TabIndex = 1;
            this.brFunction.Text = "转功能表";
            this.brFunction.UseVisualStyleBackColor = true;
            this.brFunction.Click += new System.EventHandler(this.brFunction_Click);
            // 
            // txtWeb
            // 
            this.txtWeb.Location = new System.Drawing.Point(25, 99);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(442, 21);
            this.txtWeb.TabIndex = 2;
            // 
            // btCarChara
            // 
            this.btCarChara.Location = new System.Drawing.Point(25, 70);
            this.btCarChara.Name = "btCarChara";
            this.btCarChara.Size = new System.Drawing.Size(104, 23);
            this.btCarChara.TabIndex = 1;
            this.btCarChara.Text = "转自定义特征";
            this.btCarChara.UseVisualStyleBackColor = true;
            this.btCarChara.Click += new System.EventHandler(this.btCarChara_Click);
            // 
            // MainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 171);
            this.Controls.Add(this.txtWeb);
            this.Controls.Add(this.btCarChara);
            this.Controls.Add(this.brFunction);
            this.Controls.Add(this.btMixco);
            this.Name = "MainWnd";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btMixco;
        private System.Windows.Forms.Button brFunction;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.Button btCarChara;
    }
}

