using SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace WindowsFormsApp0703 {
    
    public partial class frmMsg : Form {

        private SQLHelper sqlHelper = new SQLHelper();
        
        string stuNo;
        string stuNo1;
        string getdate = null;
        bool isOnline = true;
        bool emoicon = false;
        bool isOpenFilter = true;
        int rangeHour = 1;
        int lineNumber = 100;
        //private DateTime lastMessageTime = DateTime.MinValue;
        private DateTime lastMessageTime = DateTime.Now;
        private Timer updateTimer;
        private DateTime lastLoadedMessageTimestamp = DateTime.MinValue;
        DataSet dataSet = new DataSet();
        bool scrollToEnd = true;

        private static List<string> sensitiveWords = new List<string> { "他妈", "tmd", "cnm","sb","傻逼","敏感词" }; // 添加敏感词列表

        // 增加一个属性用来标识窗体是否已关闭
        private bool isClosed;

        DateTime lastUpdated = DateTime.MinValue;

        public frmMsg() {
            InitializeComponent();
            InitializeComponents();

            //BindListBoxWithSelectedColumn();
            BindComboBoxWithData();
            LoadMessages();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            // 初始化计时器
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // 以毫秒为单位设置更新时间间隔（此处为1秒）
            updateTimer.Tick += UpdateTimer_Tick;

            // 开始计时器
            updateTimer.Start();

            button2.Text = "😊";
            listView2.Visible = false;
            // 在窗体关闭时注册 FormClosed 事件
            this.FormClosed += FrmMsg_FormClosed;
            isClosed = false;

            textBox1.KeyPress += TextBox1_KeyPress;
            listView1.KeyPress += ListView1_KeyPress;

            Properties.Settings.Default.lastUpdateTime = GetDatabaseCurrentTime();

            sqlHelper.RunSQL("UPDATE tblTopStudents " +
                "SET LoginTimes = LoginTimes + 1 " +
                "WHERE studentNo = '" +
                Properties.Settings.Default.Username +
                "';");

        }

        private void ListView1_KeyPress(object sender, KeyPressEventArgs e) {
            // 判断按下的键是否是 Enter 键
            if (e.KeyChar == (char)Keys.Enter) {
                e.Handled = true;
                btnSendMsg_Click(sender, e);
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            // 判断按下的键是否是 Enter 键
            if (e.KeyChar == (char)Keys.Enter) {
                e.Handled = true;
                btnSendMsg_Click(sender, e);
            }
        }

        private void InitializeComponents() {
            changeBackground();
            textBox1.AllowDrop = true;

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("", 30);
            listView1.Columns.Add("用户列表", -1);

            // Create an ImageList to store student avatars
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(30, 30); // Set the size of avatar images (adjust as needed)
            listView1.SmallImageList = imageList;

            // Get student data from the database and bind it to the ListView
            BindStudentDataToListView(imageList);
            if (!isOnline) {
                button1.Text = "隐身";
                button1.BackColor = Color.FromArgb(150, 150, 150);
                button1.ForeColor = Color.White;
                isOnline = false;
                sqlHelper.RunSQL("UPDATE tblTopStudents SET status = 0 WHERE studentNo = '" + Properties.Settings.Default.Username + "'");
            }
            else {
                button1.Text = "在线";
                button1.BackColor = Color.LightGreen;
                button1.ForeColor = Color.Black;
                isOnline = true;
                sqlHelper.RunSQL("UPDATE tblTopStudents SET status = 1 WHERE studentNo = '" + Properties.Settings.Default.Username + "'");
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e) {
            // 获取消息内容
            string message = textBox1.Text.Trim();

            if (isOpenFilter) {
                message = FilterSensitiveWords(message);
            }

            // 检查消息内容是否为空
            if (string.IsNullOrEmpty(message)) {
                MessageBox.Show("消息内容不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // 检查 stuNo 是否为空
            if (string.IsNullOrEmpty(stuNo)) {
                MessageBox.Show("发送人不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 合并消息内容与 stuNo，并按照一定的格式拼接
            string formattedMessage = $"[from_user]: {stuNo}, [msg]: {message}, [dtedate]: {DateTime.Now}";

            try {
                // 使用 SQL 命令将消息插入到表中
                string insertQuery = "INSERT INTO tblMsgs ([msg], [from_user], [dtedate], [content_type], [status], [to_user]) VALUES (@msg, @from_user, GETDATE(), @content_type, @status, @to_user)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@msg", SqlDbType.NVarChar) { Value = message },
                    new SqlParameter("@from_user", SqlDbType.NVarChar) { Value = Properties.Settings.Default.Username },
                    new SqlParameter("@dtedate", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@content_type", SqlDbType.Int) { Value = 0 }, // 假设内容类型为纯文字消息，您可以根据实际情况修改
                    new SqlParameter("@status", SqlDbType.Int) { Value = 0 },
                    new SqlParameter("@to_user", SqlDbType.NVarChar) { Value = stuNo }
                };

                int rowsAffected = sqlHelper.RunSQL(insertQuery, parameters);

                // 检查插入是否成功
                if (rowsAffected > 0) {
                    // 插入成功，刷新消息列表
                    textBox1.Clear();
                    lastMessageTime = DateTime.Now;
                }
                else {
                    MessageBox.Show("消息发送失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindComboBoxWithData() {
            // 获取选定列的内容并绑定到 ComboBox
            string selectQuery = "SELECT studentName, studentNo FROM tbltopstudents";

            try {
                // 执行SQL语句并获取数据

                sqlHelper.RunSQL(selectQuery, ref dataSet);

                // 清空 ComboBox 中的项
                comboBox1.Items.Clear();
                comboBox1.Visible = false;

                // 将选定列的内容添加到 ComboBox 中
                foreach (DataRow row in dataSet.Tables[0].Rows) {
                    string itemName = row["studentName"].ToString();
                    comboBox1.Items.Add(itemName);
                }
            }
            catch (Exception ex) {
                // 处理异常
                MessageBox.Show("An error occurred while binding data to ComboBox: " + ex.Message);
            }
        }

        private void BindStudentDataToListView(ImageList imageList) {
            try {
                string selectSql = "SELECT studentNo, studentName, face, status FROM tbltopstudents";
                SqlDataReader dataReader = sqlHelper.RunSQLWithDataReader(selectSql);

                while (dataReader.Read()) {
                    string studentNo = dataReader.GetString(0);
                    string studentName = dataReader.GetString(1);
                    object faceData = dataReader.GetValue(2);
                    int studentStatus = dataReader.GetInt32(3);

                    // Check if faceData is DBNull.Value or null
                    if (faceData != DBNull.Value && faceData != null) {
                        byte[] imageData = (byte[])faceData;

                        // Try loading the image data into an Image object
                        Image image = null;
                        try {
                            using (MemoryStream memoryStream = new MemoryStream(imageData)) {
                                image = Image.FromStream(memoryStream);
                            }

                            // Add the image to the ImageList and set the studentNo as the ImageKey
                            imageList.Images.Add(studentNo, image);

                            // Create a new ListViewItem and add it to the ListView
                            ListViewItem item = new ListViewItem();
                            item.Text = ""; // Set an empty string for the first column (avatar column)
                            item.ImageKey = studentNo; // Set the studentNo as the ImageKey for the first column (avatar column)

                            // Add the studentName to the second column (name column)
                            item.SubItems.Add(studentName);
                            // 设置 ListViewItem 的文本颜色
                            if (studentStatus == 0) {
                                item.ForeColor = Color.LightGray; // 设置为灰色文本
                            }
                            else {
                                item.ForeColor = Color.Black; // 设置为黑色文本
                            }

                            listView1.Items.Add(item);
                        }
                        catch (Exception ex) {
                            Console.WriteLine($"Error loading image for {studentName} ({studentNo}): {ex.Message}");
                            // If image loading fails, set a placeholder image as default
                            image = Image.FromFile(@"C:\Users\Administrator.JINBAOSLAPTOP\Downloads\normIcon.jpg");

                            // Add the default image to the ImageList and set the studentNo as the ImageKey
                            imageList.Images.Add(studentNo, image);

                            // Create a new ListViewItem and add it to the ListView
                            ListViewItem item = new ListViewItem();
                            item.Text = ""; // Set an empty string for the first column (avatar column)
                            item.ImageKey = studentNo; // Set the studentNo as the ImageKey for the first column (avatar column)

                            // Add the studentName to the second column (name column)
                            item.SubItems.Add(studentName);
                            // 设置 ListViewItem 的文本颜色
                            if (studentStatus == 0) {
                                item.ForeColor = Color.LightGray; // 设置为灰色文本
                            }
                            else {
                                item.ForeColor = Color.Black; // 设置为黑色文本
                            }

                            listView1.Items.Add(item);
                        }

                    }
                    else {
                        // If faceData is DBNull.Value or null, set a placeholder image as default
                        Image image = Image.FromFile(@"C:\Users\Administrator.JINBAOSLAPTOP\Downloads\normIcon.jpg");

                        // Add the default image to the ImageList and set the studentNo as the ImageKey
                        imageList.Images.Add(studentNo, image);
                        imageList.ColorDepth = ColorDepth.Depth16Bit;

                        // Create a new ListViewItem and add it to the ListView
                        ListViewItem item = new ListViewItem();
                        item.Text = ""; // Set an empty string for the first column (avatar column)
                        item.ImageKey = studentNo; // Set the studentNo as the ImageKey for the first column (avatar column)

                        // Add the studentName to the second column (name column)
                        item.SubItems.Add(studentName);

                        // 设置 ListViewItem 的文本颜色
                        if (studentStatus == 0) {
                            item.ForeColor = Color.LightGray; // 设置为灰色文本
                        }
                        else {
                            item.ForeColor = Color.Black; // 设置为黑色文本
                        }

                        listView1.Items.Add(item);
                    }
                }

                dataReader.Close();
            }
            catch (Exception ex) {
                Console.WriteLine($"Error binding student data to ListView: {ex.Message}");
            }
        }

        // 添加一个成员变量，用于标识是否有新的私聊消息
        private bool hasNewPrivateMessage = false;

        // 在类的成员变量中添加存储新消息数量的字典
        private Dictionary<string, int> newMessageCounts = new Dictionary<string, int>();

        // 刷新用户列表登录状态的函数
        private void RefreshStudentStatus() {
            try {
                // 清空新消息数量的字典
                newMessageCounts.Clear();

                // 查询数据库，获取最新的学生状态信息
                foreach (ListViewItem item in listView1.Items) {
                    string studentNo = item.ImageKey; // 获取学号

                    string selectStatusSql = "SELECT status FROM tbltopstudents WHERE studentNo = '" + studentNo + "'";
                    int studentStatus = Convert.ToInt32(sqlHelper.RunSelectSQLToScalar(selectStatusSql));

                    // 设置 ListViewItem 的文本颜色
                    if (studentStatus == 0) {
                        item.ForeColor = Color.LightGray; // 设置为灰色文本
                    }
                    else if (studentStatus == 1) {
                        item.ForeColor = Color.Black; // 设置为黑色文本
                    }

                    // 将新消息数量初始化为0，并存储到字典中
                    newMessageCounts[studentNo] = 0;
                }

                string tmpUsername = Properties.Settings.Default.Username;

                // 查询数据库，检查是否有新的私聊消息
                string selectPrivateMessageSql = "SELECT DISTINCT [from_user] FROM tblMsgs WHERE [status] = 4 AND [to_user] = @to_user AND [dtedate] > @dtedate;";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@dtedate", SqlDbType.DateTime) { Value = Properties.Settings.Default.lastUpdateTime },
            new SqlParameter("@to_user", SqlDbType.NVarChar) { Value = tmpUsername }
                };

                DataTable dataTable = sqlHelper.RunSQLWithDataTable(selectPrivateMessageSql, parameters);

                if (dataTable.Rows.Count > 0) {
                    foreach (DataRow row in dataTable.Rows) {
                        string fromUserNo = row["from_user"].ToString();

                        // 更新用户的新消息数量
                        if (newMessageCounts.ContainsKey(fromUserNo)) {
                            newMessageCounts[fromUserNo]++;
                        }
                    }
                }

                // 判断是否有新的私聊消息，如果有，则进行名字闪动，并显示新消息数量
                foreach (ListViewItem item in listView1.Items) {
                    string studentNo = item.ImageKey; // 获取学号

                    if (newMessageCounts.ContainsKey(studentNo)) {
                        int newMessageCount = newMessageCounts[studentNo];

                        // 如果该用户有新消息，则进行名字闪动和文本颜色设置
                        if (newMessageCount > 0) {
                            // 保存标签的原始文本
                            string originalText = item.Text;

                            // 进行名字闪动和文本颜色设置
                            item.ForeColor = Color.Red;

                            // 将新消息数量显示在用户名字后面
                            item.Text = originalText + " (" + newMessageCount + ")";
                        }
                    }
                }


                // 更新标记，表示名字已经闪动过了，下次刷新时会重新检查
                hasNewPrivateMessage = true;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error refreshing student status: {ex.Message}");
            }
        }


        // 打开私聊窗口时，重置该用户的新消息数量为0
        private void OpenPrivateChat(string studentNo) {
            // 将用户的新消息数量重置为0
            if (newMessageCounts.ContainsKey(studentNo)) {
                newMessageCounts[studentNo] = 0;
            }

            // 在列表中找到该用户的 ListViewItem，将其文本修改为用户的名字（不显示新消息数量）
            foreach (ListViewItem item in listView1.Items) {
                if (item.ImageKey == studentNo) {
                    item.Text = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + studentNo + "'");
                    ; // 你的获取用户名字的方法
                    break;
                }
            }
        }

        // 收到新的私聊消息时，在相应的用户的新消息数量上加1，并更新列表中对应的 ListViewItem 的文本为用户的名字加上新消息数量
        private void ReceiveNewPrivateMessage(string fromUserNo) {
            if (newMessageCounts.ContainsKey(fromUserNo)) {
                newMessageCounts[fromUserNo]++;

                // 在列表中找到该用户的 ListViewItem，将其文本修改为用户的名字加上新消息数量
                foreach (ListViewItem item in listView1.Items) {
                    if (item.ImageKey == fromUserNo) {
                        item.Text = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + fromUserNo + "'");
                        item.ForeColor = Color.Red; // 进行名字闪动
                        break;
                    }
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            // 获取选中项的姓名
            string selectedName = comboBox1.SelectedItem.ToString();

            // 查找对应的学号
            foreach (DataRow row in dataSet.Tables[0].Rows) {
                if (row["studentName"].ToString() == selectedName) {
                    stuNo = row["studentNo"].ToString();
                    break;
                }
            }
        }

        // 获取消息列表
        private void LoadMessages() {
            DataSet dataSet2 = new DataSet();
            // 查询数据库中的消息
            string query = "SELECT * FROM ( SELECT TOP " + lineNumber + " [msg], [from_user], [dtedate], [to_user], [content_type], [picture], [status]  FROM tblMsgs  WHERE [status] != 4  ORDER BY [dtedate] DESC ) AS subquery ORDER BY [dtedate] ASC;";
            //DataSet dataSet = new DataSet();
            sqlHelper.RunSQL(query, ref dataSet2);

            // 清空 RichTextBox 中的文本
            richTextBox1.Clear();

            // 遍历查询结果并格式化添加到 RichTextBox 中
            foreach (DataRow row in dataSet2.Tables[0].Rows) {
                string message = row["msg"].ToString();
                string fromUserNo = row["from_user"].ToString();
                string fromUser;
                string toUser;
                // 如果能查询到对应的姓名，就替换为姓名
                try {
                    fromUser = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + fromUserNo + "'");
                }
                catch {
                    fromUser = fromUserNo;
                }

                string date = ((DateTime)row["dtedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                string toUserNo = row["to_user"].ToString();
                try {
                    toUser = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + toUserNo + "'");
                }
                catch {
                    toUser = toUserNo;
                }

                // 格式化消息文本
                string formattedMessage = $"{date}  {fromUser} TO {toUser}: ";

                //int contentType = Convert.ToInt32(row["content_type"]);

                if (row.IsNull("picture")) {
                    formattedMessage += message; // 纯文本消息
                }
                else if (!row.IsNull("picture")) {
                    formattedMessage += "[图片]"; // 图片消息
                }

                richTextBox1.AppendText("\n");

                richTextBox1.SelectionFont = new Font("Bahnschrift SemiBold", 12);
                richTextBox1.SelectionColor = Color.Purple;
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.AppendText(date);

                formattedMessage += "\n";
                richTextBox1.AppendText("\n");

                // 添加格式化后的消息到 RichTextBox
                //richTextBox1.AppendText(formattedMessage);

                if (fromUserNo == Properties.Settings.Default.Username) {
                    richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    richTextBox1.AppendText("我");

                    richTextBox1.SelectionFont = new Font("Microsoft YaHei", 10);
                    richTextBox1.SelectionColor = Color.FromArgb(200, 200, 200);
                    richTextBox1.SelectionBackColor = Color.Transparent;
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    richTextBox1.AppendText(" 发送给 ");

                    if (toUserNo == Properties.Settings.Default.Username) {
                        richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.AppendText("自己");
                    }
                    else {
                        richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.AppendText(toUser);
                    }


                    richTextBox1.AppendText("\n");

                    if (row.IsNull("picture")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.AppendText(message); // 纯文本消息
                    }
                    else if (!row.IsNull("picture")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.SelectionColor = Color.Green;
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.AppendText("[图片]"); // 图片消息
                        if (!row.IsNull("msg")) {
                            richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                            richTextBox1.AppendText(message); // 纯文本消息
                        }
                    }
                }
                else {
                    richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                    richTextBox1.SelectionColor = Color.Black;
                    richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox1.AppendText(fromUser);

                    richTextBox1.SelectionFont = new Font("Microsoft YaHei", 10);
                    richTextBox1.SelectionColor = Color.FromArgb(200, 200, 200);
                    richTextBox1.SelectionBackColor = Color.Transparent;
                    richTextBox1.AppendText(" 发送给 ");

                    if (toUserNo == Properties.Settings.Default.Username) {
                        richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionBackColor = Color.LightGoldenrodYellow;
                        richTextBox1.AppendText("我");
                        ShawForm();
                    }
                    else {
                        richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                        richTextBox1.SelectionColor = Color.Black;
                        richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                        richTextBox1.AppendText(toUser);
                    }

                    richTextBox1.AppendText("\n");

                    if (row.IsNull("picture")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.AppendText(message); // 纯文本消息
                    }
                    else if (!row.IsNull("picture") & (row.IsNull("msg"))|message == "") {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.SelectionColor = Color.Green;
                        richTextBox1.AppendText("[图片]"); // 图片消息
                        richTextBox1.SelectionColor = Color.Black;
                    }
                    else if (!row.IsNull("msg")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.AppendText(message); // 纯文本消息
                    }
                }
            }

            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            if (lineNumber == 100)
                richTextBox1.ScrollToCaret();
        }

        private void LoadNewMessages() {

            DataSet dataSet1 = new DataSet();
            //if (getdate != null) {
            // 查询数据库中的消息

            string query = "SELECT [msg], [from_user], [dtedate], [to_user], [content_type], [picture], [status] FROM tblMsgs WHERE [status] != 4 AND [dtedate] > DATEADD(SECOND, -1, GETDATE()) ORDER BY [dtedate]";
            // Query for new messages with timestamps greater than the last loaded message
            //string query = $"SELECT [msg], [from_user], [dtedate], [to_user], [content_type], [picture], [status] FROM tblMsgs WHERE [dtedate] > @lastLoadedTime ORDER BY [dtedate]";

            dataSet1.Clear();
            sqlHelper.RunSQL(query, ref dataSet1);
            try {
                string date = null;
                bool haveNewMSg = false;
                // 遍历查询结果并格式化添加到 RichTextBox 中
                foreach (DataRow row in dataSet1.Tables[0].Rows) {
                    string message = row["msg"].ToString();
                    string fromUserNo = row["from_user"].ToString();
                    string fromUser;
                    string toUser;

                    // 如果能查询到对应的姓名，就替换为姓名
                    try {
                        fromUser = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + fromUserNo + "'");
                    }
                    catch {
                        fromUser = fromUserNo;
                    }

                    date = ((DateTime)row["dtedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    if (!richTextBox1.Text.Contains(date)) {
                        haveNewMSg = true;
                        string toUserNo = row["to_user"].ToString();
                        try {
                            toUser = sqlHelper.RunSelectSQLToScalar("SELECT studentName FROM tbltopstudents WHERE studentNo = '" + toUserNo + "'");
                        }
                        catch {
                            toUser = toUserNo;
                        }

                        // 格式化消息文本
                        string formattedMessage = $"{date}  {fromUser} TO {toUser}: ";

                        //int contentType = Convert.ToInt32(row["content_type"]);

                        if (row.IsNull("picture")) {
                            formattedMessage += message; // 纯文本消息
                        }
                        else if (!row.IsNull("picture")) {
                            formattedMessage += "[图片]"; // 图片消息
                        }

                        richTextBox1.AppendText("\n");

                        richTextBox1.SelectionFont = new Font("Bahnschrift SemiBold", 12);
                        richTextBox1.SelectionColor = Color.Purple;
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                        richTextBox1.AppendText(date);

                        formattedMessage += "\n";
                        richTextBox1.AppendText("\n");

                        // 添加格式化后的消息到 RichTextBox
                        //richTextBox1.AppendText(formattedMessage);

                        if (fromUserNo == Properties.Settings.Default.Username) {
                            richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                            richTextBox1.SelectionColor = Color.DarkBlue;
                            richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                            richTextBox1.AppendText("我");

                            richTextBox1.SelectionFont = new Font("Microsoft YaHei", 10);
                            richTextBox1.SelectionColor = Color.FromArgb(200, 200, 200);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                            richTextBox1.AppendText(" 发送给 ");

                            if (toUserNo == Properties.Settings.Default.Username) {
                                richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                                richTextBox1.AppendText("自己");
                            }
                            else {
                                richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                                richTextBox1.AppendText(toUser);
                            }


                            richTextBox1.AppendText("\n");

                            if (row.IsNull("picture")) {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                                richTextBox1.AppendText(message); // 纯文本消息
                            }
                            else if (!row.IsNull("picture")) {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                                richTextBox1.AppendText("[图片]"); // 图片消息
                                if (!row.IsNull("msg")) {
                                    richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                    richTextBox1.SelectionBackColor = Color.Transparent;
                                    richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                                    richTextBox1.AppendText(message); // 纯文本消息
                                }
                            }
                        }
                        else {
                            richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                            richTextBox1.SelectionColor = Color.Black;
                            richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBox1.AppendText(fromUser);

                            richTextBox1.SelectionFont = new Font("Microsoft YaHei", 10);
                            richTextBox1.SelectionColor = Color.FromArgb(200, 200, 200);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.AppendText(" 发送给 ");

                            if (toUserNo == Properties.Settings.Default.Username) {
                                richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionBackColor = Color.LightGoldenrodYellow;
                                richTextBox1.AppendText("我");
                                ShawForm();
                            }
                            else {
                                richTextBox1.SelectionFont = new Font("Microsoft YaHei", 12);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionBackColor = Color.FromArgb(220, 220, 220);
                                richTextBox1.AppendText(toUser);
                            }

                            richTextBox1.AppendText("\n");

                            if (row.IsNull("picture")) {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.AppendText(message); // 纯文本消息
                            }
                            else if (!row.IsNull("picture") & (row.IsNull("msg")) | message == "") {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.AppendText("[图片]"); // 图片消息
                                richTextBox1.SelectionColor = Color.Black;
                            }
                            else if (!row.IsNull("msg")) {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.AppendText(message); // 纯文本消息
                            }
                        }
                    }
                }
                if (date != null & scrollToEnd & haveNewMSg) {
                    richTextBox1.ScrollToCaret();
                }
            }
            catch { }
            // dataSet1.Clear();
            //}
            //getdate = sqlHelper.RunSelectSQLToScalar("SELECT GETDATE();");
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;

        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            // 加载新消息

            LoadNewMessages();
            RefreshStudentStatus();
            richTextBox1.Refresh();
            //lastMessageTime = DateTime.Now;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            // Check if any item is selected
            if (listView1.SelectedItems.Count > 0) {
                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Get the index of the selected column
                int columnIndex = selectedItem.SubItems.IndexOf(selectedItem.GetSubItemAt(selectedItem.Position.X, selectedItem.Position.Y));

                if (columnIndex == 0) {
                    // The first column (头像列) is selected
                    // Get the student number from the image key or image index (assuming you have set the ImageKey or ImageIndex property of the ListViewItem)
                    stuNo = selectedItem.ImageKey; // Or stuNo1 = selectedItem.ImageIndex.ToString();
                    Properties.Settings.Default.SelectedNo = stuNo;
                }
                else if (columnIndex == 1) {
                    // The second column (姓名列) is selected
                    // Get the student name from the first subitem (index 0) of the selected item
                    string selectedName = selectedItem.SubItems[0].Text;

                    // Find the corresponding student number
                    stuNo = string.Empty;

                    foreach (DataRow row in dataSet.Tables[0].Rows) {
                        if (row["studentName"].ToString() == selectedName) {
                            stuNo = row["studentNo"].ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length >= 1) {
                    string imagePath = files[0];

                    try {
                        // 读取拖放的图片文件
                        byte[] imageData = File.ReadAllBytes(imagePath);

                        // 将图片数据保存到数据库中
                        SaveImageToDatabase(imageData);

                        // 获取消息内容
                        string message = $"[Image]: {imagePath}";

                        // 检查消息内容是否为空
                        if (string.IsNullOrEmpty(message)) {
                            MessageBox.Show("消息内容不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 检查 stuNo 是否为空
                        if (string.IsNullOrEmpty(stuNo)) {
                            MessageBox.Show("发送人不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 合并消息内容与 stuNo，并按照一定的格式拼接
                        string formattedMessage = $"[from_user]: {stuNo}, [msg]: {message}, [dtedate]: {DateTime.Now}";

                        // ... (接下来的发送消息逻辑和处理纯文本消息的代码)

                    }
                    catch (Exception ex) {
                        Console.WriteLine($"Error sending image: {ex.Message}");
                        // 发送失败时，可以在文本框中显示发送失败提示等
                        textBox1.AppendText("图片发送失败，请重试！");
                    }
                }
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        // 保存图片数据到 tblMsgs 表的方法
        private void SaveImageToDatabase(byte[] imageData) {

            try {
                // 使用 SQLHelper 或其他方式执行数据库插入操作
                string insertSql = "INSERT INTO tblMsgs ([msg], [picture], [from_user], [to_user], [status], [content_type], [dtedate]) " +
                                   "VALUES (@msg, @picture, @from_user, @to_user, @status, @content_type, " + "GETDATE())";

                // 假设message变量中存放的是消息内容
                string message = textBox1.Text.Trim();

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@msg", SqlDbType.NVarChar) { Value = message },
                    new SqlParameter("@picture", SqlDbType.VarBinary) { Value = imageData },
                    new SqlParameter("@from_user", SqlDbType.NVarChar) { Value = Properties.Settings.Default.Username },
                    new SqlParameter("@to_user", SqlDbType.NVarChar) { Value = stuNo },
                    new SqlParameter("@status", SqlDbType.Int) { Value = 0 },
                    new SqlParameter("@content_type", SqlDbType.Int) { Value = 1 },
                    // new SqlParameter("@dtedate", SqlDbType.NVarChar) { Value = "GETDATE()" }
                };

                sqlHelper.RunSQL(insertSql, parameters);
            }

            catch (Exception ex) {
                Console.WriteLine($"Error saving image to tblMsgs table: {ex.Message}");
                // 发生保存失败时，可以处理异常或者显示相应提示
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e) {
            string selectedText = richTextBox1.SelectedText.Trim(); // 获取选中的文本并去除首尾空格

            // 检查选中文本是否为时间格式 "2023-07-19 14:18:16"
            if (DateTime.TryParseExact(selectedText, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDateTime)) {
                // 使用 SQL 命令查询对应时间点的消息类型
                string query = "SELECT [content_type], [picture] FROM tblMsgs WHERE CONVERT(VARCHAR, [dtedate], 108) LIKE @selectedTime + '%'";
                SqlParameter parameter = new SqlParameter("@selectedTime", SqlDbType.VarChar) { Value = selectedDateTime.ToString("HH:mm:ss") };

                try {
                    // 执行 SQL 查询并获取结果
                    DataTable resultTable = sqlHelper.RunSQLWithDataTable(query, new SqlParameter[] { parameter });

                    // 确保查询结果非空且包含了所需的列
                    if (resultTable != null && resultTable.Rows.Count > 0 && resultTable.Columns.Contains("content_type") && resultTable.Columns.Contains("picture")) {
                        int contentType = Convert.ToInt32(resultTable.Rows[0]["content_type"]);

                        // 检查消息类型是否为图片类型
                        try {
                            // 获取图片数据，并加载到 pictureBox1 中
                            byte[] imageData = (byte[])resultTable.Rows[0]["picture"];
                            using (MemoryStream memoryStream = new MemoryStream(imageData)) {
                                pictureBox1.Image = Image.FromStream(memoryStream);
                                pictureBox1.Visible = true;
                            }
                        }
                        catch {
                            // 如果消息类型不是图片，则清空 pictureBox1
                            pictureBox1.Image = null;
                            pictureBox1.Visible = false;
                        }
                    }
                    else {
                        // 如果查询结果为空或者缺少所需的列，则清空 pictureBox1
                        pictureBox1.Image = null;
                        pictureBox1.Visible = false;
                    }
                }
                catch (Exception ex) {
                    // 处理异常
                    Console.WriteLine("Error executing SQL query: " + ex.Message);
                    pictureBox1.Image = null;
                    pictureBox1.Visible = false;
                }
            }
            else {
                // 如果选中文本不是时间格式，则清空 pictureBox1
                pictureBox1.Image = null;
                pictureBox1.Visible = false;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                // Get the item that was right-clicked
                ListViewItem clickedItem = listView1.GetItemAt(e.X, e.Y);

                // If an item was right-clicked, show the context menu at the clicked position
                if (clickedItem != null) {
                    contextMenuStrip1.Show(listView1, e.Location);
                }
            }
        }

        private void showUserDetails_Click(object sender, EventArgs e) {
            // 创建 frmUserDetail 窗体实例
            frmUserDetail userDetailForm = new frmUserDetail();

            // 显示 frmUserDetail 窗体
            userDetailForm.ShowDialog();
        }


        private void privateChatBox_Click(object sender, EventArgs e) {

            frmPrivateChat PricateChatFrm = new frmPrivateChat();
            OpenPrivateChat(Properties.Settings.Default.SelectedNo);
            PricateChatFrm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (isOnline) {
                sqlHelper.RunSQL("UPDATE tblTopStudents SET status = 0 WHERE studentNo = '" + Properties.Settings.Default.Username + "';");
                button1.Text = "隐身";
                button1.BackColor = Color.FromArgb(150, 150, 150);
                button1.ForeColor = Color.White;
                isOnline = false;
            }
            else {
                sqlHelper.RunSQL("UPDATE tblTopStudents SET status = 1 WHERE studentNo = '" + Properties.Settings.Default.Username + "';");
                button1.Text = "在线";
                button1.BackColor = Color.LightGreen;
                button1.ForeColor = Color.Black;
                isOnline = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e) {
            // 创建 frmUserDetail 窗体实例
            frmUserDetail userDetailForm = new frmUserDetail();

            // 显示 frmUserDetail 窗体
            userDetailForm.ShowDialog();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView2.SelectedItems.Count > 0) {
                ListViewItem selectedIcon = listView2.SelectedItems[0];
                int iconNo = selectedIcon.Index;

                // 根据图标 index 获取对应的图片路径
                if (iconImagePathMap.TryGetValue(iconNo, out string imagePath)) {
                    try {
                        // 读取图片文件
                        byte[] imageData = File.ReadAllBytes(imagePath);

                        // 将图片数据保存到数据库中
                        // 假设你已经有了保存文件到数据库的方法，这里需要根据你的数据库操作方式进行调用
                        SaveImageToDatabase(imageData);

                        // 获取消息内容
                        string message = $"[Image]: {imagePath}";

                        // ... (接下来的发送消息逻辑和处理纯文本消息的代码)
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"Error sending image: {ex.Message}");
                        // 发送失败时，可以在文本框中显示发送失败提示等
                        textBox1.AppendText("图片发送失败，请重试！");
                    }
                }
            }
            button2.Text = "😊";
            listView2.Visible = false;
            emoicon = false;
        }


        // 定义一个字典，将图标 index 映射到图片路径
        private Dictionary<int, string> iconImagePathMap = new Dictionary<int, string>
        {
            { 0, "D:\\学习文档\\ECNU\\CA\\emoicons\\angry_emoticon6.png" }, // 图标0对应的图片路径
            { 1, "D:\\学习文档\\ECNU\\CA\\emoicons\\thumber_emoticon13.png" }, // 图标1对应的图片路径
            { 2, "D:\\学习文档\\ECNU\\CA\\emoicons\\tongue_emoticon3.png" },
            { 3, "D:\\学习文档\\ECNU\\CA\\emoicons\\smile_emoticon1.png" },
            { 4, "D:\\学习文档\\ECNU\\CA\\emoicons\\cry_emoticon9.png" },
            { 5, "D:\\学习文档\\ECNU\\CA\\emoicons\\expecting_emoticon28.png" },
            { 6, "D:\\学习文档\\ECNU\\CA\\emoicons\\happy_emoticon22.png" },
            { 7, "D:\\学习文档\\ECNU\\CA\\emoicons\\happywithhand_emoticon24.png" },
            { 8, "D:\\学习文档\\ECNU\\CA\\emoicons\\huaji_emoticon25.png" },
            { 9, "D:\\学习文档\\ECNU\\CA\\emoicons\\nowords---_emoticon8.png" },
            };

        private void button2_Click(object sender, EventArgs e) {
            if (!emoicon) {
                button2.Text = "😄";
                listView2.Visible = true;
                emoicon = true;
            }
            else {
                button2.Text = "😊";
                listView2.Visible = false;
                emoicon = false;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            frmHistory HistoryFrm = new frmHistory();
            HistoryFrm.ShowDialog();
        }

        private void btnMoreMsgs_Click(object sender, EventArgs e) {
            lineNumber += 50;
            LoadMessages();
        }

        // FormClosed 事件处理程序
        private void FrmMsg_FormClosed(object sender, FormClosedEventArgs e) {
            isClosed = true;

            // 更新数据库表 tblTopStudents 中 status 字段
            if (Properties.Settings.Default.Username != null) {
                int status = isClosed ? 0 : 1;
                UpdateStatusInDatabase(Properties.Settings.Default.Username, status);
            }
        }

        // 更新数据库表 tblTopStudents 中 status 字段的方法
        private void UpdateStatusInDatabase(string accountNo, int status) {
            try {
                string updateQuery = "UPDATE tblTopStudents SET status = " + status + " WHERE studentNo = '" + Properties.Settings.Default.Username + "'";
                sqlHelper.RunSQL(updateQuery);
            }
            catch (Exception ex) {
                // 处理更新数据库过程中可能发生的异常
                MessageBox.Show("更新数据库时发生错误：" + ex.Message);
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e) {
            scrollToEnd = false;
        }

        private void richTextBox1_Leave(object sender, EventArgs e) {
            scrollToEnd = true;
        }

        public DateTime GetDatabaseCurrentTime() {
            string cmdText = "SELECT GETDATE();";
            SqlDataReader dataReader = null;
            DateTime currentTime = DateTime.MinValue;

            try {
                // 执行查询，并获取 SqlDataReader 对象
                dataReader = sqlHelper.RunSQLWithDataReader(cmdText);

                // 检查是否有数据行
                if (dataReader.HasRows) {
                    // 读取数据行
                    while (dataReader.Read()) {
                        // 获取数据库时间，并赋值给 currentTime 变量
                        currentTime = dataReader.GetDateTime(0);
                    }
                }
            }
            catch (Exception ex) {
                // 记录错误日志
                throw new Exception(ex.Message, ex);
            }
            finally {
                // 关闭 dataReader 和数据库连接
                if (dataReader != null && !dataReader.IsClosed) {
                    dataReader.Close();
                }

                // 关闭数据库连接
                sqlHelper.Close();
            }
            return currentTime;
        }

        public static string FilterSensitiveWords(string text) {
            StringBuilder result = new StringBuilder(text);
            foreach (string word in sensitiveWords) {
                int startIndex = 0;
                while (startIndex < result.Length) {
                    int index = result.ToString().IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase);
                    if (index == -1)
                        break; // 找不到敏感词，结束查找
                    for (int i = index; i < index + word.Length; i++) {
                        result[i] = '*'; // 替换敏感词为星号
                    }
                    startIndex = index + word.Length; // 继续下一次查找
                }
            }
            return result.ToString();
        }

        private Dictionary<string, string> backgroundMap = new Dictionary<string, string>
{
    {"默认白", "Default"},
    {"藤蔓", "leaves"},
    {"信封", "envelope"},
    {"气泡", "bubble"}
};

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {

            string selectedOption = comboBox2.SelectedItem.ToString();
            // 根据选项的文本从字典中获取对应的背景名称
            string selectedBackground = backgroundMap[selectedOption];

            // 更新配置文件中的配置项
            Properties.Settings.Default.BackImage = selectedBackground;
            Properties.Settings.Default.Save();

            // 根据用户的选择设置窗体的背景图像
            switch (selectedBackground) {
                case "Default":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
                case "leaves":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.leavesreversed; // 藤蔓背景
                    break;
                case "envelope":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.envelope; // 信封背景
                    break;
                case "bubble":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.bubble; // 气泡背景
                    break;
                default:
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
            }
        }

        private void changeBackground() {
            // 根据用户的选择设置窗体的背景图像
            switch (Properties.Settings.Default.BackImage) {
                case "Default":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
                case "leaves":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.leavesreversed; // 藤蔓背景
                    break;
                case "envelope":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.envelope; // 信封背景
                    break;
                case "bubble":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.bubble; // 气泡背景
                    break;
                default:
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            // 调用 AnimateWindow 函数触发窗体抖动效果
            ShawForm();
        }

        public void ShawForm() {
            Random ran = new Random((int)DateTime.Now.Ticks);

            Point point = this.Location;

            for (int i = 0; i < 40; i++) {
                this.Location = new Point(point.X + ran.Next(8) - 4, point.Y + ran.Next(8) - 4);

                System.Threading.Thread.Sleep(15);

                this.Location = point;

                System.Threading.Thread.Sleep(15);
            }
        }
    }

    public static class Win32Interop {
        // 导入 user32.dll
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        // 定义 AnimateWindow 的 flags 枚举
        [Flags]
        public enum AnimateWindowFlags : int {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }
    }
}
