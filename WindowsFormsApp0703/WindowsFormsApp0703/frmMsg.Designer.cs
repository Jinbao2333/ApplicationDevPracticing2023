namespace WindowsFormsApp0703 {
    partial class frmMsg {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMsg));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showUserDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.privateChatBox = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnMoreMsgs = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(181, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(691, 403);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            this.richTextBox1.Enter += new System.EventHandler(this.richTextBox1_Enter);
            this.richTextBox1.Leave += new System.EventHandler(this.richTextBox1_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(181, 411);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(633, 83);
            this.textBox1.TabIndex = 2;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(878, 411);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(61, 83);
            this.btnSendMsg.TabIndex = 3;
            this.btnSendMsg.Text = "发送";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(581, 50);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(304, 29);
            this.comboBox1.TabIndex = 5;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.Font = new System.Drawing.Font("思源黑体 CN Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(175, 494);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showUserDetails,
            this.privateChatBox});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 48);
            // 
            // showUserDetails
            // 
            this.showUserDetails.Name = "showUserDetails";
            this.showUserDetails.Size = new System.Drawing.Size(123, 22);
            this.showUserDetails.Text = "查看个人信息";
            this.showUserDetails.Click += new System.EventHandler(this.showUserDetails_Click);
            // 
            // privateChatBox
            // 
            this.privateChatBox.Name = "privateChatBox";
            this.privateChatBox.Size = new System.Drawing.Size(123, 22);
            this.privateChatBox.Text = "私聊";
            this.privateChatBox.Click += new System.EventHandler(this.privateChatBox_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("思源黑体 CN Regular", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(878, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "在线状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.listView2.Location = new System.Drawing.Point(829, 66);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(34, 339);
            this.listView2.SmallImageList = this.imageList1;
            this.listView2.TabIndex = 8;
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
            this.button2.Location = new System.Drawing.Point(820, 411);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 83);
            this.button2.TabIndex = 9;
            this.button2.Text = "😊";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("思源黑体 CN Normal", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(878, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 58);
            this.button3.TabIndex = 10;
            this.button3.Text = "聊天记录";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnMoreMsgs
            // 
            this.btnMoreMsgs.Font = new System.Drawing.Font("思源黑体 CN Heavy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoreMsgs.Location = new System.Drawing.Point(842, 0);
            this.btnMoreMsgs.Name = "btnMoreMsgs";
            this.btnMoreMsgs.Size = new System.Drawing.Size(30, 29);
            this.btnMoreMsgs.TabIndex = 11;
            this.btnMoreMsgs.Text = "↑";
            this.btnMoreMsgs.UseVisualStyleBackColor = true;
            this.btnMoreMsgs.Click += new System.EventHandler(this.btnMoreMsgs_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(246, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 361);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("思源黑体 CN Normal", 12F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "默认白",
            "藤蔓",
            "信封",
            "气泡"});
            this.comboBox2.Location = new System.Drawing.Point(879, 106);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(60, 30);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.Text = "主题";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // frmMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.leavesreversed;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(946, 494);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.btnMoreMsgs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TopStu聊天室";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showUserDetails;
        private System.Windows.Forms.ToolStripMenuItem privateChatBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnMoreMsgs;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}