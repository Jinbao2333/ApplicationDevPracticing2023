using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQL;
using System.Data.SqlClient;
using System.Drawing.Imaging;

namespace WindowsFormsApp0703 {
    public partial class frmHistory : Form {
        private DataGridView dataGridView;
        private WatermarkTextbox textBoxSearch;
        private WatermarkTextbox textBoxSearchFrom;
        private WatermarkTextbox textBoxSearchTo;
        private Button buttonSearch;
        private Button buttonPrevPage;
        private ComboBox comboBox;
        private Button buttonNextPage;
        private Button buttonRefresh;
        private Button buttonSave;
        private Button buttonCancel;
        private Label lblPageIndex;
        string url1;
        string nameu;
        int rangeHour = 2;
        private int itemsPerPage = 15; // 每页显示的条目数
        private int pageIndex = 0; // 当前页数，初始值为 0
        private int totalPageCount = 0; // 总页数，初始值为 0
        private string searchKeyword = "";// 搜索关键字，初始为空
        private string searchF = "";
        private string searchT = "";
        ToolTip toolTip1 = new ToolTip();
        DataTable dt = new DataTable();
        DataSet dataSet = new DataSet();
        SQLHelper sqlHelper = new SQLHelper();

        public frmHistory() {
            InitializeComponent();
            InitializeComponents();
            DataColumn dc = new DataColumn();//创建空列
            dt.Columns.Add(dc);
            dt.Columns.Add("title", System.Type.GetType("System.String"));
            dt.Columns.Add("detail", typeof(String));
            dt.Columns.Add("datetime", typeof(String));
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            buttonSearch.Click += ButtonSearch_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            buttonPrevPage.Click += ButtonPrevPage_Click;
            buttonNextPage.Click += ButtonNextPage_Click;
            buttonSave.Click += ButtonSave_Click;
            buttonCancel.Click += ButtonCancel_Click;
            toolTip1.SetToolTip(buttonCancel, "清空搜索条件");

            // 加载历史消息
            dataSet = LoadHistoryMessages();

        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            textBoxSearch.Text = string.Empty;
            textBoxSearchFrom.Text = string.Empty;
            textBoxSearchTo.Text = string.Empty;
        }

        // 加载历史消息
        private DataSet LoadHistoryMessages() {
            try {
                // 构建查询语句以检索历史消息
                string query = "SELECT [msg], [from_user], [dtedate], [to_user], [content_type], [picture], [status] FROM tblMsgs WHERE [status] != 4 AND [dtedate] > DATEADD(HOUR, -" + rangeHour + ", GETDATE())";

                // 如果搜索关键字非空，将其添加到查询条件中
                if (!string.IsNullOrEmpty(searchKeyword)) {
                    // 使用 OR 连接多个模糊搜索条件，用分号分隔
                    string[] keywords = searchKeyword.Split('；');
                    string orCondition = string.Join(" OR ", keywords.Select(keyword => $"[msg] LIKE '%{keyword.Trim()}%'"));
                    query += $" AND ({orCondition})";
                }

                if (!string.IsNullOrEmpty(searchF)) {
                    string searchFr = GetStudentNoByUserName(searchF);
                    query += $" AND [from_user] = '{searchFr}'";
                }

                if (!string.IsNullOrEmpty(searchT)) {
                    string searchTo = GetStudentNoByUserName(searchT);
                    query += $" AND [to_user] = '{searchTo}'";
                }

                query += " ORDER BY [dtedate]";

                dataSet.Clear();
                sqlHelper.RunSQL(query, ref dataSet);

                // 返回加载的 DataSet
                return dataSet;
            }
            catch (Exception ex) {
                // 处理在数据库查询过程中可能发生的任何异常
                MessageBox.Show("加载历史消息时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // 返回 null 表示加载失败
            }
        }

        // 点击搜索按钮时执行的操作
        private void ButtonSearch_Click(object sender, EventArgs e) {
            dt.Clear();
            dataGridView.ClearSelection();
            dataGridView.Rows.Clear();


            if (textBoxSearch.Text.Length != 0) {
                // 获取用户输入的搜索关键字
                searchKeyword = textBoxSearch.Text.Trim();
            }
            else {
                // 如果搜索关键字为空，将其设置为空字符串
                searchKeyword = "";
            }

            if (textBoxSearchFrom.Text.Length != 0) {
                searchF = textBoxSearchFrom.Text.Trim();
            }
            else {
                searchF = "";
            }

            if (textBoxSearchTo.Text.Length != 0) {
                searchT = textBoxSearchTo.Text.Trim();
            }
            else {
                searchT = "";
            }

            try {
                // 调用 LoadHistoryMessages 函数加载基于搜索关键字的消息
                DataSet dataSet = LoadHistoryMessages();

                // 将查询结果绑定到 DataGridView 控件
                BindDG(dataSet, "Table");
            }
            catch (Exception ex) {
                // 处理在加载历史消息过程中可能发生的异常
                MessageBox.Show("加载历史消息时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ButtonRefresh_Click(object sender, EventArgs e) {
            dt.Clear();
            dataGridView.ClearSelection();
            dataGridView.Rows.Clear();

            if (comboBox.Text == "过去1小时") {
                rangeHour = 1;
            }
            else if (comboBox.Text == "过去2小时") {
                rangeHour = 2;
            }
            else if (comboBox.Text == "过去1天") {
                rangeHour = 24;
            }
            else if (comboBox.Text == "过去3天") {
                rangeHour = 72;
            }
            else if (comboBox.Text == "全部") {
                rangeHour = 240;
            }
            else {
                rangeHour = 240;
            }

            // 调用加载历史消息函数
            LoadHistoryMessages();

            // 绑定数据到控件
            BindDG(dataSet, "Table");
        }


        private void ButtonSave_Click(object sender, EventArgs e) {
            if (dataGridView.SelectedRows.Count == 0) {
                MessageBox.Show("请先选择要导出的内容！可以使用Ctrl多选。");
                return;
            }

            if (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow) {
                MessageBox.Show("请先搜索历史记录。");
            }
            else {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Markdown 文件|*.md";
                saveFileDialog.Title = "选择保存文件的路径";
                saveFileDialog.FileName = textBoxSearch.Text + "聊天记录.md";

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string filePath = saveFileDialog.FileName;

                    StringBuilder sb = new StringBuilder();

                    // 添加文档标题
                    string documentTitle = textBoxSearch.Text;
                    sb.AppendLine($"# {documentTitle}聊天记录");
                    sb.AppendLine();

                    // 添加聊天记录
                    foreach (DataGridViewRow row in dataGridView.SelectedRows) {
                        string time = row.Cells["timeColumn"].Value?.ToString();
                        string message = row.Cells["msgColumn"].Value?.ToString();
                        string fromUser = row.Cells["fromColumn"].Value?.ToString();
                        string toUser = row.Cells["toColumn"].Value?.ToString();
                        string contentType = row.Cells["contenttypeColumn"].Value?.ToString();
                        string status = row.Cells["statusColumn"].Value?.ToString();

                        // 将时间、发送者、接收者等信息添加到导出文本中
                        sb.AppendLine($"### 时间：{time}, {fromUser}发给{toUser}");
                        sb.AppendLine();

                        // 判断图片列是否包含图片信息
                        DataGridViewImageCell imageCell = row.Cells["pictureColumn"] as DataGridViewImageCell;
                        if (imageCell?.Value != null) {
                            try {
                                Image image = (Image)imageCell.Value;
                                // 将图片以 Image 对象添加到导出文本中
                                sb.AppendLine($"![图片]({GetImageBase64Data(image)})");
                            }
                            catch { }
                        }
                        else {
                            // 如果图片列不包含图片信息，直接将消息内容添加到导出文本中
                            sb.AppendLine($"{message}");
                        }

                        sb.AppendLine();
                        sb.AppendLine("---"); // 用分隔线分隔不同的聊天记录
                    }

                    // 将数据写入文件
                    File.WriteAllText(filePath, sb.ToString());

                    MessageBox.Show("导出成功！");
                }
            }
        }

        // Helper function to get the Base64 data of the image
        private string GetImageBase64Data(Image image) {
            try {
                using (MemoryStream ms = new MemoryStream()) {
                    // 优先尝试保存为PNG格式
                    using (Image newImage = new Bitmap(image)) {
                        newImage.Save(ms, ImageFormat.Png);
                    }

                    // 获取图片数据的字节数组
                    byte[] imageBytes = ms.ToArray();

                    // 尝试将图片保存为JPEG格式
                    using (MemoryStream jpegMs = new MemoryStream()) {
                        using (Image newImage = new Bitmap(image)) {
                            newImage.Save(jpegMs, ImageFormat.Jpeg);
                            imageBytes = jpegMs.ToArray();
                        }
                    }

                    // 返回Base64编码的图片数据
                    return $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
                }
            }
            catch (Exception ex) {
                // 出现异常时返回空字符串
                Console.WriteLine($"Error saving image: {ex.Message}");
                return string.Empty;
            }
        }






        private void InitializeComponents() {
            // 设置窗体大小
            this.Size = new Size(1000, 500);

            int margin = 10;
            int textBoxHeight = 30;
            int buttonWidth = 75;
            int comboBoxWidth = 100;

            // 创建并设置 DataGridView
            dataGridView = new DataGridView();
            dataGridView.Font = new Font(dataGridView.Font.FontFamily, 12); // 设置文本大小
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ClearSelection();
            //dataGridView.ReadOnly = true;
            this.Controls.Add(dataGridView);

            // 创建并设置 TextBox
            textBoxSearch = new WatermarkTextbox();
            textBoxSearch.WatermarkText = "请输入聊天记录关键字";
            textBoxSearch.Font = new Font(textBoxSearch.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(textBoxSearch);
            textBoxSearch.Multiline = false;

            // 创建并设置 TextBox
            textBoxSearchFrom = new WatermarkTextbox();
            textBoxSearchFrom.WatermarkText = "发送者";
            textBoxSearchFrom.Font = new Font(textBoxSearchFrom.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(textBoxSearchFrom);
            textBoxSearchFrom.Multiline = false;

            // 创建并设置 TextBox
            textBoxSearchTo = new WatermarkTextbox();
            textBoxSearchTo.WatermarkText = "接收者";
            textBoxSearchTo.Font = new Font(textBoxSearchTo.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(textBoxSearchTo);
            textBoxSearchTo.Multiline = false;

            // 创建并设置搜索按钮!
            buttonSearch = new Button();
            buttonSearch.Text = "搜索";
            buttonSearch.Font = new Font(buttonSearch.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonSearch);

            // 创建并设置清空按钮
            buttonCancel = new Button();
            buttonCancel.Text = "🧹";
            buttonCancel.Font = new Font(buttonCancel.Font.FontFamily, 15);
            this.Controls.Add(buttonCancel);

            // 创建并设置 ComboBox
            comboBox = new ComboBox();
            comboBox.Font = new Font(comboBox.Font.FontFamily, 12); // 设置文本大小
            string[] options = { "全部", "过去1小时", "过去2小时", "过去1天", "过去3天" };
            comboBox.Items.AddRange(options);
            this.Controls.Add(comboBox);

            // 创建并设置刷新按钮
            buttonRefresh = new Button();
            buttonRefresh.Text = "刷新";
            buttonRefresh.Font = new Font(buttonRefresh.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonRefresh);

            // 创建并设置上一页按钮!
            buttonPrevPage = new Button();
            buttonPrevPage.Text = "上一页";
            buttonPrevPage.Font = new Font(buttonPrevPage.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonPrevPage);

            // 创建并设置下一页按钮
            buttonNextPage = new Button();
            buttonNextPage.Text = "下一页";
            buttonNextPage.Font = new Font(buttonNextPage.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonNextPage);

            // 创建并设置页数标签
            lblPageIndex = new Label();
            lblPageIndex.Text = "";
            lblPageIndex.Font = new Font(lblPageIndex.Font.FontFamily, 12);

            // 创建并设置导出按钮
            buttonSave = new Button();
            buttonSave.Text = "导出";
            buttonSave.Font = new Font(buttonSave.Font.FontFamily, 12);
            this.Controls.Add(buttonSave);

            // 添加列
            dataGridView.Columns.Add("timeColumn", "时间");
            dataGridView.Columns.Add("msgColumn", "消息内容");
            dataGridView.Columns.Add("fromColumn", "发送者");
            dataGridView.Columns.Add("toColumn", "接收者");
            dataGridView.Columns.Add("contenttypeColumn", "消息类型");
            //dataGridView.Columns.Add("pictureColumn", "图片");
            dataGridView.Columns.Add("statusColumn", "状态");

            // Create a new DataGridViewImageColumn
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "pictureColumn"; // Give the column a unique name
            imageColumn.HeaderText = "图片"; // Set the column header text
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView.Columns.Add(imageColumn);

            // 设置列宽度
            dataGridView.Columns["timeColumn"].Width = 80; // 时间列宽度固定为 150
            dataGridView.Columns["msgColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["fromColumn"].Width = 100;
            dataGridView.Columns["toColumn"].Width = 100;
            dataGridView.Columns["contenttypeColumn"].Width = 1;
            dataGridView.Columns["pictureColumn"].Width = 60;
            dataGridView.Columns["statusColumn"].Width = 1;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // 自动调整行高以适应内容
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // 自动换行

            // 设置列1的文本大小和字体
            DataGridViewCellStyle column1Style = new DataGridViewCellStyle();
            column1Style.Font = new Font("Bahnschrift Light Condensed", 14, FontStyle.Regular);
            dataGridView.Columns["timeColumn"].DefaultCellStyle = column1Style;

            // 设置列2的文本大小和字体
            DataGridViewCellStyle column2Style = new DataGridViewCellStyle();
            column2Style.Font = new Font("思源宋体 CN Light", 14, FontStyle.Regular);
            dataGridView.Columns["msgColumn"].DefaultCellStyle = column2Style;

            // 设置列3的文本大小和字体
            DataGridViewCellStyle column3Style = new DataGridViewCellStyle();
            column3Style.Font = new Font("思源黑体 CN Heavy", 14, FontStyle.Regular);
            dataGridView.Columns["fromColumn"].DefaultCellStyle = column3Style;

            // 设置列4的文本大小和字体
            DataGridViewCellStyle column4Style = new DataGridViewCellStyle();
            column4Style.Font = new Font("思源黑体 CN Heavy", 14, FontStyle.Regular);
            dataGridView.Columns["toColumn"].DefaultCellStyle = column4Style;

            // 设置列5的文本大小和字体
            DataGridViewCellStyle column5Style = new DataGridViewCellStyle();
            column5Style.Font = new Font("思源黑体 CN Heavy", 14, FontStyle.Regular);
            dataGridView.Columns["contenttypeColumn"].DefaultCellStyle = column5Style;

            // 设置列6的文本大小和字体
            DataGridViewCellStyle column6Style = new DataGridViewCellStyle();
            column6Style.Font = new Font("思源黑体 CN Heavy", 14, FontStyle.Regular);
            dataGridView.Columns["toColumn"].DefaultCellStyle = column6Style;

            // 设置列7的文本大小和字体
            DataGridViewCellStyle column7Style = new DataGridViewCellStyle();
            column7Style.Font = new Font("思源黑体 CN Heavy", 14, FontStyle.Regular);
            dataGridView.Columns["toColumn"].DefaultCellStyle = column7Style;

            dataGridView.Location = new Point(margin, textBoxHeight + 2 * margin);
            dataGridView.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 4 * margin - 2 * textBoxHeight);
            dataGridView.Refresh();

            // 设置刷新按钮位置和大小
            buttonRefresh.Location = new Point(this.ClientSize.Width - buttonWidth - margin, margin);
            buttonRefresh.Size = new Size(buttonWidth, textBoxHeight);
            // 设置 ComboBox 位置和大小
            comboBox.Location = new Point(buttonRefresh.Left - comboBox.Width - margin, margin);
            comboBox.Size = new Size(comboBoxWidth, textBoxHeight);
            // 设置下一页按钮位置和大小
            buttonNextPage.Location = new Point(comboBox.Left - buttonWidth - margin, margin);
            buttonNextPage.Size = new Size(buttonWidth, textBoxHeight);
            // 设置上一页按钮位置和大小
            buttonPrevPage.Location = new Point(buttonNextPage.Left - buttonWidth - margin, margin);
            buttonPrevPage.Size = new Size(buttonWidth, textBoxHeight);
            // 设置搜索按钮位置和大小
            buttonSearch.Location = new Point(buttonPrevPage.Left - buttonWidth - margin, margin);
            buttonSearch.Size = new Size(buttonWidth, textBoxHeight);
            // 设置接收者搜索框位置和大小
            textBoxSearchTo.Location = new Point(buttonSearch.Left - buttonWidth - margin, margin);
            textBoxSearchTo.Size = new Size(buttonWidth, textBoxHeight);
            // 设置发送者搜索框位置和大小
            textBoxSearchFrom.Location = new Point(textBoxSearchTo.Left - buttonWidth - margin, margin);
            textBoxSearchFrom.Size = new Size(buttonWidth, textBoxHeight);
            // 设置 TextBox 位置和大小
            textBoxSearch.Location = new Point(2 * margin + textBoxHeight, margin);
            textBoxSearch.Size = new Size(textBoxSearchFrom.Left - 3 * margin-textBoxHeight, textBoxHeight);
            // 设置清空按钮位置和大小
            buttonCancel.Location = new Point(margin, margin);
            buttonCancel.Size = new Size(textBoxHeight, textBoxHeight);

            // 设置导出按钮位置和大小
            buttonSave.Location = new Point(this.ClientSize.Width - buttonWidth - margin, this.ClientSize.Height - textBoxHeight - margin);
            buttonSave.Size = new Size(buttonWidth, textBoxHeight);

            // 订阅窗体大小改变事件
            this.ClientSizeChanged += frmPoems_ClientSizeChanged;

            // 初始化控件位置和大小
            AdjustControlLayout();
        }

        private void ButtonPrevPage_Click(object sender, EventArgs e) {
            if (pageIndex > 0) {
                dt.Clear();
                dataGridView.ClearSelection();
                dataGridView.Rows.Clear();
                pageIndex -= 1;
                BindDG(dataSet, "Table");
            }
        }

        private void ButtonNextPage_Click(object sender, EventArgs e) {
            if (pageIndex < totalPageCount - 1) {
                dt.Clear();
                dataGridView.ClearSelection();
                dataGridView.Rows.Clear();
                pageIndex += 1;
                BindDG(dataSet, "Table");
            }
        }


        // 将消息内容绑定到控件
        private void BindDG(DataSet dataSet, string tableName) {
            dataGridView.Rows.Clear();
            dataGridView.ClearSelection();

            if (dataSet.Tables.Contains(tableName)) {
                DataTable dataTable = dataSet.Tables[tableName];

                int totalItems = dataTable.Rows.Count;
                totalPageCount = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // 确保 pageIndex 不越界
                pageIndex = Math.Max(0, Math.Min(pageIndex, totalPageCount - 1));

                int startIndex = pageIndex * itemsPerPage;
                int endIndex = Math.Min(startIndex + itemsPerPage, totalItems);

                for (int i = startIndex; i < endIndex; i++) {
                    DataRow row = dataTable.Rows[i];
                    int col_index = dataGridView.Rows.Add();

                    // 将数据逐个绑定到对应列中
                    dataGridView.Rows[col_index].Cells["timeColumn"].Value = row["dtedate"].ToString();
                    dataGridView.Rows[col_index].Cells["msgColumn"].Value = row["msg"].ToString();

                    // 查询发送者的姓名并替换学号字段值
                    string fromUserNo = row["from_user"].ToString();
                    string fromUser = GetUserNameByStudentNo(fromUserNo);
                    dataGridView.Rows[col_index].Cells["fromColumn"].Value = fromUser;

                    // 查询接收者的姓名并替换学号字段值
                    string toUserNo = row["to_user"].ToString();
                    string toUser = GetUserNameByStudentNo(toUserNo);
                    dataGridView.Rows[col_index].Cells["toColumn"].Value = toUser;

                    dataGridView.Rows[col_index].Cells["contenttypeColumn"].Value = row["content_type"].ToString();

                    // 判断 picture 是否为空，如果为空，则在图像列中显示 空 字样
                    if (row.IsNull("picture")) {
                        dataGridView.Rows[col_index].Cells["pictureColumn"].Value = null;
                    }
                    else {
                        // 将图片数据转换为 Image 并在图像列中显示
                        byte[] imageData = (byte[])row["picture"];
                        try {
                            using (MemoryStream ms = new MemoryStream(imageData)) {
                                dataGridView.Rows[col_index].Cells["pictureColumn"].Value = Image.FromStream(ms);
                            }
                        }
                        catch {
                        }
                    }

                    dataGridView.Rows[col_index].Cells["statusColumn"].Value = row["status"].ToString();
                }
            }
        }



        // 根据学号查询对应的姓名
        private string GetUserNameByStudentNo(string studentNo)     {
            try {
                string query = "SELECT studentName FROM tbltopstudents WHERE studentNo = '" + studentNo + "'";
                string userName = sqlHelper.RunSelectSQLToScalar(query);
                return string.IsNullOrEmpty(userName) ? studentNo : userName;
            }
            catch (Exception ex) {
                // 处理数据库查询异常，或返回默认学号
                Console.WriteLine("查询学号对应姓名时发生错误：" + ex.Message);
                return studentNo;
            }
        }

        // 根据姓名查询对应的学号
        private string GetStudentNoByUserName(string userName) {
            try {
                string query = "SELECT studentNo FROM tbltopstudents WHERE studentName = '" + userName + "'";
                string studentNo = sqlHelper.RunSelectSQLToScalar(query);
                return string.IsNullOrEmpty(userName) ? userName : studentNo;
            }
            catch (Exception ex) {
                // 处理数据库查询异常，或返回默认学号
                Console.WriteLine("查询学号对应姓名时发生错误：" + ex.Message);
                return userName;
            }
        }

        private void frmPoems_ClientSizeChanged(object sender, EventArgs e) {
            // 调整控件位置和大小
            AdjustControlLayout();
        }

        private void AdjustControlLayout() {
            int margin = 10;
            int textBoxHeight = 30;
            int buttonWidth = 75;

            if (!this.IsHandleCreated) {
                return;
            }

            //await Task.Delay(100);

            // 设置 DataGridView 位置和大小
            dataGridView.Location = new Point(margin, textBoxHeight + 2 * margin);
            dataGridView.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 4 * margin - 2 * textBoxHeight);
            //dataGridView.BringToFront();
            dataGridView.Refresh();

            buttonRefresh.Location = new Point(this.ClientSize.Width - buttonWidth - margin, margin);
            comboBox.Location = new Point(buttonRefresh.Left - comboBox.Width - margin, margin);
            buttonNextPage.Location = new Point(comboBox.Left - buttonWidth - margin, margin);
            buttonPrevPage.Location = new Point(buttonNextPage.Left - buttonWidth - margin, margin);
            buttonSearch.Location = new Point(buttonPrevPage.Left - buttonWidth - margin, margin);
            textBoxSearchTo.Location = new Point(buttonSearch.Left - buttonWidth - margin, margin);
            textBoxSearchFrom.Location = new Point(textBoxSearchTo.Left - buttonWidth - margin, margin);
            textBoxSearch.Location = new Point(2 * margin + textBoxHeight, margin);
            textBoxSearch.Size = new Size(textBoxSearchFrom.Left - 3 * margin - textBoxHeight, textBoxHeight);
            buttonCancel.Location = new Point(margin, margin);
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e) {
            string keywords = textBoxSearch.Text; // 获取文本框的输入内容
        }
    }
}
