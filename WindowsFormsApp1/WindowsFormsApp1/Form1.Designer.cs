using System;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 在文本框的文本变化时的处理逻辑
            string txt = textBox1.Text;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // 窗体加载时的处理逻辑
        }

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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(37, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(400, 35);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(37, 147);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(400, 40);
            this.btn.TabIndex = 2;
            this.btn.Text = "button1";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(33, 96);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(0, 24);
            this.lbl.TabIndex = 3;
            this.lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(537, 254);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label lbl;
    }
}

