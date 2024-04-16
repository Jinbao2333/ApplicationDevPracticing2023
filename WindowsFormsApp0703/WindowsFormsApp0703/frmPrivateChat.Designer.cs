namespace WindowsFormsApp0703 {
    partial class frmPrivateChat {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("", 11);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("", 12);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("", 10);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("", 1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("", 2);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("", 3);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("", 4);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("", 5);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("", 7);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrivateChat));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSendPrivateMsg = new System.Windows.Forms.Button();
            this.lblIntro = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Silver;
            this.lblUser.Font = new System.Drawing.Font("思源黑体 CN Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.Location = new System.Drawing.Point(80, 13);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(87, 33);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "用户名";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 80);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(572, 302);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged_1);
            this.richTextBox1.Enter += new System.EventHandler(this.richTextBox1_Enter);
            this.richTextBox1.Leave += new System.EventHandler(this.richTextBox1_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("思源宋体 CN Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(13, 389);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(466, 65);
            this.textBox1.TabIndex = 3;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop_1);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // btnSendPrivateMsg
            // 
            this.btnSendPrivateMsg.Font = new System.Drawing.Font("思源宋体 CN", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendPrivateMsg.Location = new System.Drawing.Point(525, 389);
            this.btnSendPrivateMsg.Name = "btnSendPrivateMsg";
            this.btnSendPrivateMsg.Size = new System.Drawing.Size(60, 65);
            this.btnSendPrivateMsg.TabIndex = 4;
            this.btnSendPrivateMsg.Text = "发送";
            this.btnSendPrivateMsg.UseVisualStyleBackColor = true;
            this.btnSendPrivateMsg.Click += new System.EventHandler(this.btnSendPrivateMsg_Click);
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.BackColor = System.Drawing.Color.Transparent;
            this.lblIntro.Font = new System.Drawing.Font("思源黑体 CN Normal", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIntro.ForeColor = System.Drawing.Color.Gray;
            this.lblIntro.Location = new System.Drawing.Point(81, 47);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(88, 26);
            this.lblIntro.TabIndex = 5;
            this.lblIntro.Text = "个性签名";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.BackColor = System.Drawing.Color.LightGray;
            this.lblGender.Font = new System.Drawing.Font("思源黑体 CN Heavy", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGender.Location = new System.Drawing.Point(173, 17);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(81, 28);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "gender";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView2.HideSelection = false;
            listViewItem11.Tag = "10";
            listViewItem11.ToolTipText = "10";
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20});
            this.listView2.Location = new System.Drawing.Point(485, 47);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(34, 339);
            this.listView2.SmallImageList = this.imageList1;
            this.listView2.TabIndex = 9;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.SmallIcon;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "angry_emoticon6.png");
            this.imageList1.Images.SetKeyName(1, "cry_emoticon9.png");
            this.imageList1.Images.SetKeyName(2, "expecting_emoticon28.png");
            this.imageList1.Images.SetKeyName(3, "happy_emoticon22.png");
            this.imageList1.Images.SetKeyName(4, "happywithhand_emoticon24.png");
            this.imageList1.Images.SetKeyName(5, "huaji_emoticon25.png");
            this.imageList1.Images.SetKeyName(6, "love_emoticon20.png");
            this.imageList1.Images.SetKeyName(7, "nowords---_emoticon8.png");
            this.imageList1.Images.SetKeyName(8, "scary_emoticon4.png");
            this.imageList1.Images.SetKeyName(9, "shy_emoticon66.png");
            this.imageList1.Images.SetKeyName(10, "smile_emoticon1.png");
            this.imageList1.Images.SetKeyName(11, "thumber_emoticon13.png");
            this.imageList1.Images.SetKeyName(12, "tongue_emoticon3.png");
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.button2.Location = new System.Drawing.Point(485, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 65);
            this.button2.TabIndex = 10;
            this.button2.Text = "😊";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.White;
            this.lblStatus.Font = new System.Drawing.Font("思源黑体 CN ExtraLight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblStatus.Location = new System.Drawing.Point(529, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 28);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "在线";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(110, 102);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(384, 260);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("思源黑体 CN Normal", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "默认白",
            "藤蔓",
            "信封",
            "气泡"});
            this.comboBox1.Location = new System.Drawing.Point(525, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(60, 27);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.Text = "主题";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // frmPrivateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(597, 466);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblIntro);
            this.Controls.Add(this.btnSendPrivateMsg);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "frmPrivateChat";
            this.Text = "frmPrivateChat";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSendPrivateMsg;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}