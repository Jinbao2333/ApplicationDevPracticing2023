namespace WindowsFormsApp0703
{
    partial class frmPicture
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picPrev = new System.Windows.Forms.Button();
            this.picNext = new System.Windows.Forms.Button();
            this.loadfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.jumpto = new System.Windows.Forms.Button();
            this.autoplay = new System.Windows.Forms.Button();
            this.spdp = new System.Windows.Forms.Button();
            this.spdm = new System.Windows.Forms.Button();
            this.timenorm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1600, 1200);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick_1);
            // 
            // picPrev
            // 
            this.picPrev.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.picPrev.Location = new System.Drawing.Point(10, 1220);
            this.picPrev.Name = "picPrev";
            this.picPrev.Size = new System.Drawing.Size(100, 100);
            this.picPrev.TabIndex = 1;
            this.picPrev.Text = "Prev";
            this.picPrev.UseVisualStyleBackColor = true;
            this.picPrev.Click += new System.EventHandler(this.picPrev_Click);
            // 
            // picNext
            // 
            this.picNext.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.picNext.Location = new System.Drawing.Point(120, 1220);
            this.picNext.Name = "picNext";
            this.picNext.Size = new System.Drawing.Size(100, 100);
            this.picNext.TabIndex = 2;
            this.picNext.Text = "Next";
            this.picNext.UseVisualStyleBackColor = true;
            this.picNext.Click += new System.EventHandler(this.picNext_Click);
            // 
            // loadfile
            // 
            this.loadfile.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.loadfile.Location = new System.Drawing.Point(1510, 1220);
            this.loadfile.Name = "loadfile";
            this.loadfile.Size = new System.Drawing.Size(100, 100);
            this.loadfile.TabIndex = 3;
            this.loadfile.Text = "Load";
            this.loadfile.UseVisualStyleBackColor = true;
            this.loadfile.Click += new System.EventHandler(this.loadfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(1070, 1230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 33);
            this.label1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1060, 1280);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(440, 40);
            this.progressBar1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(230, 1220);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 41);
            this.textBox1.TabIndex = 6;
            // 
            // jumpto
            // 
            this.jumpto.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.jumpto.Location = new System.Drawing.Point(230, 1275);
            this.jumpto.Name = "jumpto";
            this.jumpto.Size = new System.Drawing.Size(100, 50);
            this.jumpto.TabIndex = 7;
            this.jumpto.Text = "Go";
            this.jumpto.UseVisualStyleBackColor = true;
            this.jumpto.Click += new System.EventHandler(this.jumpto_Click);
            // 
            // autoplay
            // 
            this.autoplay.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.autoplay.Location = new System.Drawing.Point(336, 1220);
            this.autoplay.Name = "autoplay";
            this.autoplay.Size = new System.Drawing.Size(206, 100);
            this.autoplay.TabIndex = 8;
            this.autoplay.Text = "Play";
            this.autoplay.UseVisualStyleBackColor = true;
            this.autoplay.Click += new System.EventHandler(this.autoplay_Click);
            // 
            // spdp
            // 
            this.spdp.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.spdp.Location = new System.Drawing.Point(548, 1220);
            this.spdp.Name = "spdp";
            this.spdp.Size = new System.Drawing.Size(65, 45);
            this.spdp.TabIndex = 9;
            this.spdp.Text = "+";
            this.spdp.UseVisualStyleBackColor = true;
            this.spdp.Click += new System.EventHandler(this.spdp_Click);
            // 
            // spdm
            // 
            this.spdm.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.spdm.Location = new System.Drawing.Point(548, 1275);
            this.spdm.Name = "spdm";
            this.spdm.Size = new System.Drawing.Size(65, 45);
            this.spdm.TabIndex = 10;
            this.spdm.Text = "-";
            this.spdm.UseVisualStyleBackColor = true;
            this.spdm.Click += new System.EventHandler(this.spdm_Click);
            // 
            // timenorm
            // 
            this.timenorm.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.timenorm.Location = new System.Drawing.Point(619, 1220);
            this.timenorm.Name = "timenorm";
            this.timenorm.Size = new System.Drawing.Size(151, 100);
            this.timenorm.TabIndex = 11;
            this.timenorm.Text = "RST Speed";
            this.timenorm.UseVisualStyleBackColor = true;
            this.timenorm.Click += new System.EventHandler(this.timenorm_Click);
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1614, 1329);
            this.Controls.Add(this.timenorm);
            this.Controls.Add(this.spdm);
            this.Controls.Add(this.spdp);
            this.Controls.Add(this.autoplay);
            this.Controls.Add(this.jumpto);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadfile);
            this.Controls.Add(this.picNext);
            this.Controls.Add(this.picPrev);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Picture";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "My Gallery";
            this.SizeChanged += new System.EventHandler(this.Picture_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void PictureBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button picPrev;
        private System.Windows.Forms.Button picNext;
        private System.Windows.Forms.Button loadfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button jumpto;
        private System.Windows.Forms.Button autoplay;
        private System.Windows.Forms.Button spdp;
        private System.Windows.Forms.Button spdm;
        private System.Windows.Forms.Button timenorm;
    }
}