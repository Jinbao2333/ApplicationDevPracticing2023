namespace WindowsFormsApp0703
{
    partial class frmHello
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtbox = new System.Windows.Forms.TextBox();
            this.btn = new System.Windows.Forms.Button();
            this.wordbox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtbox
            // 
            this.txtbox.Location = new System.Drawing.Point(59, 65);
            this.txtbox.Name = "txtbox";
            this.txtbox.Size = new System.Drawing.Size(578, 35);
            this.txtbox.TabIndex = 0;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(59, 112);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(578, 50);
            this.btn.TabIndex = 1;
            this.btn.Text = "return";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // wordbox
            // 
            this.wordbox.AutoSize = true;
            this.wordbox.Location = new System.Drawing.Point(55, 186);
            this.wordbox.Name = "wordbox";
            this.wordbox.Size = new System.Drawing.Size(22, 24);
            this.wordbox.TabIndex = 2;
            this.wordbox.Text = "n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wordbox);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.txtbox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbox;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label wordbox;
    }
}

