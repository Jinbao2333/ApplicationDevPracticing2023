namespace WindowsFormsApp0703 {
    partial class frmUserDetail {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserDetail));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.lblMajor = new System.Windows.Forms.Label();
            this.lblQQ = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblIntro = new System.Windows.Forms.Label();
            this.lblProvince = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblAtTimes = new System.Windows.Forms.Label();
            this.lblSendTimes = new System.Windows.Forms.Label();
            this.btnRanking = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(472, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("思源黑体 CN Heavy", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(32, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(92, 52);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "姓名";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.BackColor = System.Drawing.Color.Transparent;
            this.lblGender.Font = new System.Drawing.Font("思源黑体 CN Regular", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGender.Location = new System.Drawing.Point(173, 29);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(75, 40);
            this.lblGender.TabIndex = 2;
            this.lblGender.Text = "性别";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.BackColor = System.Drawing.Color.Transparent;
            this.lblBirthday.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBirthday.Location = new System.Drawing.Point(37, 89);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(87, 33);
            this.lblBirthday.TabIndex = 3;
            this.lblBirthday.Text = "生日：";
            // 
            // lblMajor
            // 
            this.lblMajor.AutoSize = true;
            this.lblMajor.BackColor = System.Drawing.Color.Transparent;
            this.lblMajor.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMajor.Location = new System.Drawing.Point(37, 124);
            this.lblMajor.Name = "lblMajor";
            this.lblMajor.Size = new System.Drawing.Size(87, 33);
            this.lblMajor.TabIndex = 4;
            this.lblMajor.Text = "专业：";
            // 
            // lblQQ
            // 
            this.lblQQ.AutoSize = true;
            this.lblQQ.BackColor = System.Drawing.Color.Transparent;
            this.lblQQ.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblQQ.Location = new System.Drawing.Point(37, 159);
            this.lblQQ.Name = "lblQQ";
            this.lblQQ.Size = new System.Drawing.Size(75, 33);
            this.lblQQ.TabIndex = 5;
            this.lblQQ.Text = "QQ：";
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblPhoneNumber.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPhoneNumber.Location = new System.Drawing.Point(240, 159);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(135, 33);
            this.lblPhoneNumber.TabIndex = 6;
            this.lblPhoneNumber.Text = "电话号码：";
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.BackColor = System.Drawing.Color.Transparent;
            this.lblIntro.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIntro.Location = new System.Drawing.Point(37, 229);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(135, 33);
            this.lblIntro.TabIndex = 7;
            this.lblIntro.Text = "个性签名：";
            // 
            // lblProvince
            // 
            this.lblProvince.AutoSize = true;
            this.lblProvince.BackColor = System.Drawing.Color.Transparent;
            this.lblProvince.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProvince.Location = new System.Drawing.Point(37, 264);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(63, 33);
            this.lblProvince.TabIndex = 8;
            this.lblProvince.Text = "来自";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblEmail.Location = new System.Drawing.Point(37, 194);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(103, 33);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email：";
            // 
            // lblAtTimes
            // 
            this.lblAtTimes.AutoSize = true;
            this.lblAtTimes.BackColor = System.Drawing.Color.Transparent;
            this.lblAtTimes.Font = new System.Drawing.Font("思源黑体 CN ExtraLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAtTimes.ForeColor = System.Drawing.Color.IndianRed;
            this.lblAtTimes.Location = new System.Drawing.Point(505, 159);
            this.lblAtTimes.Name = "lblAtTimes";
            this.lblAtTimes.Size = new System.Drawing.Size(49, 26);
            this.lblAtTimes.TabIndex = 10;
            this.lblAtTimes.Text = "被@";
            this.lblAtTimes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSendTimes
            // 
            this.lblSendTimes.AutoSize = true;
            this.lblSendTimes.BackColor = System.Drawing.Color.Transparent;
            this.lblSendTimes.Font = new System.Drawing.Font("思源黑体 CN ExtraLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSendTimes.ForeColor = System.Drawing.Color.Olive;
            this.lblSendTimes.Location = new System.Drawing.Point(505, 185);
            this.lblSendTimes.Name = "lblSendTimes";
            this.lblSendTimes.Size = new System.Drawing.Size(52, 26);
            this.lblSendTimes.TabIndex = 11;
            this.lblSendTimes.Text = "发帖";
            this.lblSendTimes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRanking
            // 
            this.btnRanking.BackColor = System.Drawing.Color.Transparent;
            this.btnRanking.Font = new System.Drawing.Font("思源黑体 CN Regular", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRanking.Location = new System.Drawing.Point(472, 264);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(130, 33);
            this.btnRanking.TabIndex = 12;
            this.btnRanking.Text = "互动榜前三";
            this.btnRanking.UseVisualStyleBackColor = false;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // frmUserDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(621, 324);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.lblSendTimes);
            this.Controls.Add(this.lblAtTimes);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblProvince);
            this.Controls.Add(this.lblIntro);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.lblQQ);
            this.Controls.Add(this.lblMajor);
            this.Controls.Add(this.lblBirthday);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUserDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人资料";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.Label lblMajor;
        private System.Windows.Forms.Label lblQQ;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAtTimes;
        private System.Windows.Forms.Label lblSendTimes;
        private System.Windows.Forms.Button btnRanking;
    }
}