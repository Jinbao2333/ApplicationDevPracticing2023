using SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp0703 {
    public partial class frmPrivateChat : Form {

        string selectedNo = Properties.Settings.Default.SelectedNo;
        string myStuNo = Properties.Settings.Default.Username;
        string stuName;
        int rangeHour = 1;
        int status;
        bool emoicon = false;
        bool scrollToEnd = true;
        bool haveNewMessage = false;
        private bool isOfflineMessageShown = false;
        SQLHelper sQLHelper = new SQLHelper();
        private DateTime lastMessageTime = DateTime.Now;
        private Timer updateTimer;

        public frmPrivateChat() {
            InitializeComponent();
            InitCompnts();
            LoadDetails();
            LoadPrivateMessages();

            updateTimer = new Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();

            this.FormClosed += FrmPrivateChat_FormClosed;
        }

        private void FrmPrivateChat_FormClosed(object sender, FormClosedEventArgs e) {
            Properties.Settings.Default.lastUpdateTime = GetDatabaseCurrentTime();
        }

        public DateTime GetDatabaseCurrentTime() {
            string cmdText = "SELECT GETDATE();";
            SqlDataReader dataReader = null;
            DateTime currentTime = DateTime.MinValue;

            try {
                // 执行查询，并获取 SqlDataReader 对象
                dataReader = sQLHelper.RunSQLWithDataReader(cmdText);

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
                sQLHelper.Close();
            }

            // 返回数据库当前时间
            return currentTime;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            RefreshStudentStatus();
            LoadNewMessages();
        }

        private void InitCompnts() {
            listView2.Visible = false;
        }

        private void LoadDetails() {
            string selectedNo = Properties.Settings.Default.SelectedNo;
            string query = "SELECT [studentName],[Gender],[Birthday],[Major],[QQ],[Email],[Phone],[Intro],[Province],[LoginTimes],[face],[status] FROM tblTopStudents WHERE studentNo = '" + selectedNo + "'";
            DataSet ds = new DataSet();
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.RunSQL(query, ref ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                DataRow row = ds.Tables[0].Rows[0];
                string studentName = row["studentName"].ToString();
                stuName = studentName;
                changeBackground();
                string gender = row["Gender"].ToString();
                DateTime birthdayd = Convert.ToDateTime(row["Birthday"]);
                string birthday = birthdayd.ToString("yyyy-MM-dd");
                string major = row["Major"].ToString();
                string qq = row["QQ"].ToString();
                string email = row["Email"].ToString();
                string phone = row["Phone"].ToString();
                string intro = row["Intro"].ToString();
                string province = row["Province"].ToString();
                int loginTimes = Convert.ToInt32(row["LoginTimes"]);
                // 检查头像字段是否为空，避免在加载时出现异常
                byte[] face = null;
                if (!row.IsNull("face")) {
                    face = (byte[])row["face"];
                }
                status = Convert.ToInt32(row["status"]);

                lblUser.Text = studentName;
                if (gender == "True") {
                    lblGender.Text = "♂";
                }
                else {
                    lblGender.Text = "♀";
                }
                lblIntro.Text = "个性签名: " + intro;
                //lblProvince.Text = "来自" + province;

                // 加载头像到 PictureBox
                if (face != null && face.Length > 0) {
                    using (MemoryStream memoryStream = new MemoryStream(face)) {
                        pictureBox1.Image = Image.FromStream(memoryStream);
                    }
                }
                else {
                    // 如果头像字段为空，则设置默认头像
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Administrator.JINBAOSLAPTOP\Downloads\normIcon.jpg");
                }

                if (status == 1) {
                    lblStatus.Text = "在线";
                    lblStatus.ForeColor = Color.ForestGreen;
                }
                else {
                    lblStatus.Text = "离线";
                    lblStatus.ForeColor = Color.Gray;
                }
            }
        }

        private void LoadPrivateMessages() {
            string selectedNo = Properties.Settings.Default.SelectedNo;
            string myStuNo = Properties.Settings.Default.Username;
            int rangeHour = 480;


            string query = $@"
        SELECT [msg], [from_user], [dtedate], [to_user], [content_type], [picture]
        FROM tblMsgs
        WHERE [status] = 4
        AND (([from_user] = '{myStuNo}' AND [to_user] = '{selectedNo}') OR ([from_user] = '{selectedNo}' AND [to_user] = '{myStuNo}'))
        AND [dtedate] > DATEADD(HOUR, -{rangeHour}, GETDATE())
        ORDER BY [dtedate];
    ";


            DataSet ds = new DataSet();
            ds.Clear();
            sQLHelper.RunSQL(query, ref ds);

            // 格式化和添加查询结果到 RichTextBox 中
            foreach (DataRow row in ds.Tables[0].Rows) {
                string message = row["msg"].ToString();

                //string formattedMessage = $"{date:yyyy-MM-dd HH:mm:ss}: {message}\n";
                string date = ((DateTime)row["dtedate"]).ToString("yyyy-MM-dd HH:mm:ss");

                richTextBox1.AppendText("\n");

                richTextBox1.SelectionFont = new Font("Bahnschrift SemiBold", 12);
                richTextBox1.SelectionColor = Color.Purple;
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.AppendText(date);

                richTextBox1.AppendText("\n");

                if (row["from_user"].ToString() == Properties.Settings.Default.Username) {
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
                    if (row.IsNull("picture")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.AppendText(message); // 纯文本消息
                    }
                    else if (!row.IsNull("picture")) {
                        richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                        richTextBox1.SelectionBackColor = Color.Transparent;
                        richTextBox1.SelectionColor = Color.Green;
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.AppendText("[图片]"); // 图片消息
                        if (!row.IsNull("msg")) {
                            richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBox1.AppendText(message); // 纯文本消息
                        }
                    }
                }

                //richTextBox1.AppendText(formattedMessage);
            }

            // 设置 RichTextBox 的滚动位置等等
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void LoadNewMessages() {
            string selectedNo = Properties.Settings.Default.SelectedNo;
            string myStuNo = Properties.Settings.Default.Username;
            haveNewMessage = false;

            string query = $@"
        SELECT [msg], [from_user], [dtedate], [to_user], [content_type], [picture]
        FROM tblMsgs
        WHERE [status] = 4
        AND (([from_user] = '{myStuNo}' AND [to_user] = '{selectedNo}') OR ([from_user] = '{selectedNo}' AND [to_user] = '{myStuNo}'))
        AND [dtedate] > DATEADD(SECOND, -1, GETDATE())
        ORDER BY [dtedate];
    ";

            // 清空，以备下一次查询
            DataSet ds = new DataSet();
            ds.Clear();
            sQLHelper.RunSQL(query, ref ds);

            // 格式化和添加查询结果到 RichTextBox 中
            foreach (DataRow row in ds.Tables[0].Rows) {
                string message = row["msg"].ToString();

                //string formattedMessage = $"{date:yyyy-MM-dd HH:mm:ss}: {message}\n";
                string date = ((DateTime)row["dtedate"]).ToString("yyyy-MM-dd HH:mm:ss");

                if (!richTextBox1.Text.Contains(date)) {

                    haveNewMessage = true;
                    richTextBox1.AppendText("\n");

                    richTextBox1.SelectionFont = new Font("Bahnschrift SemiBold", 12);
                    richTextBox1.SelectionColor = Color.Purple;
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                    richTextBox1.AppendText(date);

                    richTextBox1.AppendText("\n");

                    if (row["from_user"].ToString() == Properties.Settings.Default.Username) {
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
                        if (row.IsNull("picture")) {
                            richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBox1.AppendText(message); // 纯文本消息
                        }
                        else if (!row.IsNull("picture")) {
                            richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                            richTextBox1.SelectionBackColor = Color.Transparent;
                            richTextBox1.SelectionColor = Color.Green;
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBox1.AppendText("[图片]"); // 图片消息
                            if (!row.IsNull("msg")) {
                                richTextBox1.SelectionFont = new Font("思源宋体 CN", 15);
                                richTextBox1.SelectionBackColor = Color.Transparent;
                                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                                richTextBox1.AppendText(message); // 纯文本消息
                            }
                        }
                    }
                }
                if (date != null & scrollToEnd & haveNewMessage) {
                    richTextBox1.ScrollToCaret();
                }
            }
        }
        private void btnSendPrivateMsg_Click(object sender, EventArgs e) {
            if (status == 1) {
                // Get the message content
                string message = textBox1.Text.Trim();

                // Check if the message content is empty
                if (string.IsNullOrEmpty(message)) {
                    MessageBox.Show("消息内容不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try {
                    // Use SQL command to insert the message into the table
                    string insertQuery = "INSERT INTO tblMsgs ([msg], [from_user], [dtedate], [content_type], [status], [to_user]) VALUES (@msg, @from_user, " + "GETDATE()" + ", @content_type, @status, @to_user)";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@msg", SqlDbType.NVarChar) { Value = message },
                    new SqlParameter("@from_user", SqlDbType.NVarChar) { Value = Properties.Settings.Default.Username },
                    // new SqlParameter("@dtedate", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@content_type", SqlDbType.Int) { Value = 0 }, // Assuming content type as plain text message, you can modify this based on your actual needs
                    new SqlParameter("@status", SqlDbType.Int) { Value = 4 }, // Set status to 4 for private messages
                    new SqlParameter("@to_user", SqlDbType.NVarChar) { Value = selectedNo }
                    };

                    int rowsAffected = sQLHelper.RunSQL(insertQuery, parameters);

                    // Check if the insertion was successful
                    if (rowsAffected > 0) {
                        // Insertion successful, refresh the message list or update the UI as needed
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
            else if (status == 0) {
                MessageBox.Show("对方已离线，无法发送消息！");
            }
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

        private void SaveImageToDatabase(byte[] imageData) {

            try {
                // 使用 SQLHelper 或其他方式执行数据库插入操作
                string insertSql = "INSERT INTO tblMsgs ([msg], [picture], [from_user], [to_user], [status], [content_type], [dtedate]) " +
                                   "VALUES (@msg, @picture, @from_user, @to_user, @status, @content_type, "+"GETDATE()"+")";

                // 假设message变量中存放的是消息内容
                string message = textBox1.Text.Trim();

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@msg", SqlDbType.NVarChar) { Value = message },
                    new SqlParameter("@picture", SqlDbType.VarBinary) { Value = imageData },
                    new SqlParameter("@from_user", SqlDbType.NVarChar) { Value = Properties.Settings.Default.Username },
                    new SqlParameter("@to_user", SqlDbType.NVarChar) { Value = selectedNo },
                    new SqlParameter("@status", SqlDbType.Int) { Value = 4 },
                    new SqlParameter("@content_type", SqlDbType.Int) { Value = 1 },
                    // new SqlParameter("@dtedate", SqlDbType.DateTime) { Value = DateTime.Now }
                };

                sQLHelper.RunSQL(insertSql, parameters);
            }

            catch (Exception ex) {
                Console.WriteLine($"Error saving image to tblMsgs table: {ex.Message}");
                // 发生保存失败时，可以处理异常或者显示相应提示
            }
        }

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

        // 设置更新在线状态的函数
        private void RefreshStudentStatus() {
            try {
                string selectStatusSql = "SELECT status FROM tbltopstudents WHERE studentNo = '" + selectedNo + "'";
                int status = Convert.ToInt32(sQLHelper.RunSelectSQLToScalar(selectStatusSql));

                if (status == 1) {
                    lblStatus.Text = "在线";
                    lblStatus.ForeColor = Color.ForestGreen;
                    btnSendPrivateMsg.Text = "发送";

                    // 如果之前显示过消息框，则重置标志变量
                    isOfflineMessageShown = false;
                }
                else {
                    lblStatus.Text = "离线";
                    lblStatus.ForeColor = Color.Gray;
                    btnSendPrivateMsg.Text = "留言";

                    /* 如果之前未显示过消息框，则显示消息框，并设置标志变量为 true
                    if (!isOfflineMessageShown) {
                        MessageBox.Show("对方已下线！您现在无法发送消息，只可留言。");
                        isOfflineMessageShown = true;
                    }*/
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error refreshing student status: {ex.Message}");
            }
        }


        private void richTextBox1_Enter(object sender, EventArgs e) {
            scrollToEnd = false;
        }

        private void richTextBox1_Leave(object sender, EventArgs e) {
            scrollToEnd = true;
        }

        private void richTextBox1_SelectionChanged_1(object sender, EventArgs e) {
            string selectedText = richTextBox1.SelectedText.Trim(); // 获取选中的文本并去除首尾空格

            // 检查选中文本是否为时间格式 "2023-07-19 14:18:16"
            if (DateTime.TryParseExact(selectedText, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDateTime)) {
                // 使用 SQL 命令查询对应时间点的消息类型
                string query = "SELECT [content_type], [picture] FROM tblMsgs WHERE CONVERT(VARCHAR, [dtedate], 108) LIKE @selectedTime + '%'";
                SqlParameter parameter = new SqlParameter("@selectedTime", SqlDbType.VarChar) { Value = selectedDateTime.ToString("HH:mm:ss") };

                try {
                    // 执行 SQL 查询并获取结果
                    DataTable resultTable = sQLHelper.RunSQLWithDataTable(query, new SqlParameter[] { parameter });

                    // 确保查询结果非空且包含了所需的列
                    if (resultTable != null && resultTable.Rows.Count > 0 && resultTable.Columns.Contains("content_type") && resultTable.Columns.Contains("picture")) {
                        int contentType = Convert.ToInt32(resultTable.Rows[0]["content_type"]);

                        // 检查消息类型是否为图片类型
                        try {
                            // 获取图片数据，并加载到 pictureBox1 中
                            byte[] imageData = (byte[])resultTable.Rows[0]["picture"];
                            using (MemoryStream memoryStream = new MemoryStream(imageData)) {
                                pictureBox2.Image = Image.FromStream(memoryStream);
                                pictureBox2.Visible = true;
                            }
                        }
                        catch {
                            // 如果消息类型不是图片，则清空 pictureBox1
                            pictureBox2.Image = null;
                            pictureBox2.Visible = false;
                        }
                    }
                    else {
                        // 如果查询结果为空或者缺少所需的列，则清空 pictureBox1
                        pictureBox2.Image = null;
                        pictureBox2.Visible = false;
                    }
                }
                catch (Exception ex) {
                    // 处理异常
                    Console.WriteLine("Error executing SQL query: " + ex.Message);
                    pictureBox2.Image = null;
                    pictureBox2.Visible = false;
                }
            }
            else {
                // 如果选中文本不是时间格式，则清空 pictureBox1
                pictureBox2.Image = null;
                pictureBox2.Visible = false;
            }
        }
        private Dictionary<string, string> backgroundMap = new Dictionary<string, string>
{
    {"默认白", "Default"},
    {"藤蔓", "leaves"},
    {"信封", "envelope"},
    {"气泡", "bubble"}
};

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            // 获取当前用户的学号，假设学号保存在 selectedNo 变量中
            string currentUser = stuName;

            // 获取用户选择的背景选项的文本
            string selectedOption = comboBox1.SelectedItem.ToString();

            // 根据选项的文本从字典中获取对应的背景名称
            string selectedBackground = backgroundMap[selectedOption];

            // 更新配置文件中的配置项
            Properties.Settings.Default[$"{currentUser}"] = selectedBackground;
            Properties.Settings.Default.Save();

            // 根据用户的选择设置窗体的背景图像
            switch (selectedBackground) {
                case "Default":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
                case "leaves":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.leaves; // 藤蔓背景
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
            switch (Properties.Settings.Default[$"{stuName}"]) {
                case "Default":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.white; // 默认白色背景
                    break;
                case "leaves":
                    this.BackgroundImage = global::WindowsFormsApp0703.Properties.Resources.leaves; // 藤蔓背景
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


                        // 合并消息内容与 stuNo，并按照一定的格式拼接
                        string formattedMessage = $"[from_user]: {Properties.Settings.Default.Username}, [msg]: {message}, [dtedate]: {DateTime.Now}, [status] = 4";

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

        private void textBox1_DragDrop_1(object sender, DragEventArgs e) {
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


                        // 合并消息内容与 stuNo，并按照一定的格式拼接
                        string formattedMessage = $"[from_user]: {Properties.Settings.Default.Username}, [msg]: {message}, [dtedate]: {DateTime.Now}, [status] = 4";

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
    }
}
