namespace WindowsFormsApp0703 {
    partial class frmChoice {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fillBlank = new System.Windows.Forms.TextBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.indx = new System.Windows.Forms.Label();
            this.handInButton = new System.Windows.Forms.Button();
            this.countdownLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("宋体", 10F);
            this.title.Location = new System.Drawing.Point(269, 35);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(96, 27);
            this.title.TabIndex = 1;
            this.title.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fillBlank);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(205, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1259, 875);
            this.panel1.TabIndex = 2;
            // 
            // fillBlank
            // 
            this.fillBlank.Location = new System.Drawing.Point(26, 482);
            this.fillBlank.Name = "fillBlank";
            this.fillBlank.Size = new System.Drawing.Size(914, 35);
            this.fillBlank.TabIndex = 4;
            this.fillBlank.TextChanged += new System.EventHandler(this.fillBlank_TextChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(96, 296);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(185, 28);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(96, 236);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(185, 28);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(96, 169);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(185, 28);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(96, 108);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(185, 28);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // indx
            // 
            this.indx.AutoSize = true;
            this.indx.Font = new System.Drawing.Font("宋体", 10F);
            this.indx.Location = new System.Drawing.Point(205, 35);
            this.indx.Name = "indx";
            this.indx.Size = new System.Drawing.Size(96, 27);
            this.indx.TabIndex = 4;
            this.indx.Text = "label2";
            // 
            // handInButton
            // 
            this.handInButton.Location = new System.Drawing.Point(1306, 722);
            this.handInButton.Name = "handInButton";
            this.handInButton.Size = new System.Drawing.Size(160, 100);
            this.handInButton.TabIndex = 5;
            this.handInButton.Text = "提交";
            this.handInButton.UseVisualStyleBackColor = true;
            this.handInButton.Click += new System.EventHandler(this.handInButton_Click);
            // 
            // countdownLabel
            // 
            this.countdownLabel.AutoSize = true;
            this.countdownLabel.Font = new System.Drawing.Font("宋体", 12F);
            this.countdownLabel.Location = new System.Drawing.Point(947, 46);
            this.countdownLabel.Name = "countdownLabel";
            this.countdownLabel.Size = new System.Drawing.Size(0, 33);
            this.countdownLabel.TabIndex = 6;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("宋体", 12F);
            this.scoreLabel.Location = new System.Drawing.Point(633, 48);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(111, 33);
            this.scoreLabel.TabIndex = 7;
            this.scoreLabel.Text = "label1";
            // 
            // frmChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1174, 829);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.countdownLabel);
            this.Controls.Add(this.handInButton);
            this.Controls.Add(this.indx);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.title);
            this.Name = "frmChoice";
            this.Text = "Choice";
            this.SizeChanged += new System.EventHandler(this.frmChoice_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label indx;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button handInButton;
        private System.Windows.Forms.Label countdownLabel;
        private System.Windows.Forms.TextBox fillBlank;
        private System.Windows.Forms.Label scoreLabel;
    }
}