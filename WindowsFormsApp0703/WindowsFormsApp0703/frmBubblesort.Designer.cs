namespace WindowsFormsApp0703
{
    partial class frmBubblesort
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.spdplus = new System.Windows.Forms.Button();
            this.spdminus = new System.Windows.Forms.Button();
            this.nmspd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(16, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 510);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 600);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(440, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 600);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(440, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // spdplus
            // 
            this.spdplus.Location = new System.Drawing.Point(30, 554);
            this.spdplus.Name = "spdplus";
            this.spdplus.Size = new System.Drawing.Size(150, 40);
            this.spdplus.TabIndex = 3;
            this.spdplus.Text = "speed+";
            this.spdplus.UseVisualStyleBackColor = true;
            this.spdplus.Click += new System.EventHandler(this.spdplus_Click);
            // 
            // spdminus
            // 
            this.spdminus.Location = new System.Drawing.Point(186, 554);
            this.spdminus.Name = "spdminus";
            this.spdminus.Size = new System.Drawing.Size(150, 40);
            this.spdminus.TabIndex = 4;
            this.spdminus.Text = "speed-";
            this.spdminus.UseVisualStyleBackColor = true;
            this.spdminus.Click += new System.EventHandler(this.spdminus_Click);
            // 
            // nmspd
            // 
            this.nmspd.Location = new System.Drawing.Point(342, 554);
            this.nmspd.Name = "nmspd";
            this.nmspd.Size = new System.Drawing.Size(150, 40);
            this.nmspd.TabIndex = 5;
            this.nmspd.Text = "normspeed";
            this.nmspd.UseVisualStyleBackColor = true;
            this.nmspd.Click += new System.EventHandler(this.nmspd_Click);
            // 
            // bubblesort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(874, 1061);
            this.Controls.Add(this.nmspd);
            this.Controls.Add(this.spdminus);
            this.Controls.Add(this.spdplus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "bubblesort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Visualized Bubblesort";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button spdplus;
        private System.Windows.Forms.Button spdminus;
        private System.Windows.Forms.Button nmspd;
    }
}