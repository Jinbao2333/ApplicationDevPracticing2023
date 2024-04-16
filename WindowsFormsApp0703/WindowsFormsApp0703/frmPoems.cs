using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    public partial class frmPoems : Form {
        private DataGridView dataGridView;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private ComboBox comboBox;
        private Button buttonNextPage;
        private Button buttonRefresh;
        private Button buttonSave;
        string url1;
        string nameu;
        string name1u;
        int pageIndex;
        DataTable dt = new DataTable();

        public frmPoems() {
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
            buttonNextPage.Click += ButtonNextPage_Click;
            buttonSave.Click += ButtonSave_Click;
        }

        // 设置保存按钮按下的逻辑
        private void ButtonSave_Click(object sender, EventArgs e) {
            if (dataGridView.SelectedRows.Count == 0) {
                MessageBox.Show("请先选择要导出的行！可以使用Ctrl多选。");
                return;
            }

            if (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow) {
                MessageBox.Show("请先爬取内容。");
            }
            else {
            

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Markdown 文件|*.md";
            saveFileDialog.Title = "选择保存文件的路径";
            saveFileDialog.FileName = textBoxSearch.Text + "诗歌选集.md";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string filePath = saveFileDialog.FileName;

                StringBuilder sb = new StringBuilder();

                // 添加文档标题
                string documentTitle = textBoxSearch.Text;
                sb.AppendLine($"# {documentTitle}诗歌选集");
                sb.AppendLine();

                // 添加选中行的数据
                foreach (DataGridViewRow row in dataGridView.SelectedRows) {
                    string poemTitle = row.Cells["TitleColumn"].Value?.ToString();
                    poemTitle = "## 《" + poemTitle.Trim() + "》";
                    //string result = poemTitle.Replace(" ", "").Replace("\n", "");
                    string poetDynasty = row.Cells["DynastyColumn"].Value?.ToString();
                    string poemContent = row.Cells["ContentColumn"].Value?.ToString();
                    poemTitle = poemTitle.Trim();
                    //string pattern = @"(\.)";
                    //string replacement = "$1<br>";
                    //poemContent = Regex.Replace(poemContent, pattern, replacement);
                    poemContent = poemContent.Replace("。", "。<br>");

                    sb.AppendLine(poemTitle);
                    sb.AppendLine();
                    sb.AppendLine($"### {poetDynasty + textBoxSearch.Text}");
                    sb.AppendLine();
                    sb.AppendLine(poemContent);
                    sb.AppendLine();
                    sb.AppendLine("---");
                }

                // 将数据写入文件
                File.WriteAllText(filePath, sb.ToString());

                MessageBox.Show("导出成功！");
                }
            }
        }

        // 设置更多内容按下的逻辑
        private void ButtonNextPage_Click(object sender, EventArgs e) {
            pageIndex += 3;
            dt.Clear();
            dataGridView.Rows.Clear();
            if (textBoxSearch.Text.Length != 0) {
                for (int i = pageIndex; i < 3 + pageIndex; i++) {
                    GenerateUrl(nameu, name1u, i);
                    string content = spider1(url1);//根据url返回爬虫爬取内容
                    doEvents(content);//对内容进行结构化处理
                    bindDG(dt.Select());//把处理结果绑定到datagridview控件中
                }
            }
        }

        // 设置刷新按钮按下的逻辑
        private void ButtonRefresh_Click(object sender, EventArgs e) {
            DataRow[] drs;
            if (comboBox.Text == "关于“月”") {
                drs = dt.Select("title like '%月%' or title like '%中秋%' or detail like '%月%' or detail like '%中秋%'");
            }
            else if (comboBox.Text == "关于“花”") {
                drs = dt.Select("title like '%花%' or title like '%红%' or detail like '%花%' or detail like '%红%'");
            }
            else if (comboBox.Text == "关于“山”") {
                drs = dt.Select("title like '%山%' or title like '%水%' or detail like '%山%' or detail like '%水%'");
            }
            else if (comboBox.Text == "全部") {
                drs = dt.Select();
            }
            else {
                drs = dt.Select();
            }
            bindDG(drs);
        }

        // 爬虫的主要实现
        private string spider1(string url) {
            WebClient client = new WebClient();//用webclient爬取数据
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;");
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            return s;
        }
        
        // 设置搜索按钮按下的逻辑
        private void ButtonSearch_Click(object sender, EventArgs e) {
            dt.Clear();
            dataGridView.ClearSelection();
            pageIndex = 1;
            dataGridView.Rows.Clear();
            if (textBoxSearch.Text.Length != 0) {
                for (int i = pageIndex; i < 3 + pageIndex; i++) {
                    GenerateUrl(nameu, name1u, i);
                    string content = spider1(url1);//根据url返回爬虫爬取内容
                    doEvents(content);//对内容进行结构化处理
                    bindDG(dt.Select());//把处理结果绑定到datagridview控件中
                }
            }
            else {
                MessageBox.Show("请先输入作者名再爬取！");
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
            textBoxSearch = new TextBox();
            textBoxSearch.Font = new Font(textBoxSearch.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(textBoxSearch);
            textBoxSearch.Multiline = true;

            // 创建并设置搜索按钮
            buttonSearch = new Button();
            buttonSearch.Text = "搜索";
            buttonSearch.Font = new Font(buttonSearch.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonSearch);

            // 创建并设置 ComboBox
            comboBox = new ComboBox();
            comboBox.Font = new Font(comboBox.Font.FontFamily, 12); // 设置文本大小
            string[] options = { "全部", "关于“月”", "关于“花”", "关于“山”" };
            comboBox.Items.AddRange(options);
            this.Controls.Add(comboBox);

            // 创建并设置刷新按钮
            buttonRefresh = new Button();
            buttonRefresh.Text = "刷新";
            buttonRefresh.Font = new Font(buttonRefresh.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonRefresh);

            // 创建并设置下一页按钮
            buttonNextPage = new Button();
            buttonNextPage.Text = "下一页";
            buttonNextPage.Font = new Font(buttonNextPage.Font.FontFamily, 12); // 设置文本大小
            this.Controls.Add(buttonNextPage);

            // 创建并设置导出按钮
            buttonSave = new Button();
            buttonSave.Text = "导出";
            buttonSave.Font = new Font(buttonSave.Font.FontFamily, 12);
            this.Controls.Add(buttonSave);

            // 添加三列
            dataGridView.Columns.Add("TitleColumn", "诗歌标题");
            dataGridView.Columns.Add("DynastyColumn", "作者朝代");
            dataGridView.Columns.Add("ContentColumn", "诗歌内容");

            // 设置列宽度
            dataGridView.Columns["TitleColumn"].Width = 250; // 标题列宽度固定为 150
            dataGridView.Columns["DynastyColumn"].Width = 90; // 朝代列宽度固定为 150
            dataGridView.Columns["ContentColumn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // 内容列自动调整宽度以填充剩余空间
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // 自动调整行高以适应内容
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // 自动换行

            // 设置列1的文本大小和字体
            DataGridViewCellStyle column1Style = new DataGridViewCellStyle();
            column1Style.Font = new Font("方正小标宋简体", 14, FontStyle.Bold);
            dataGridView.Columns["TitleColumn"].DefaultCellStyle = column1Style;

            // 设置列2的文本大小和字体
            DataGridViewCellStyle column2Style = new DataGridViewCellStyle();
            column2Style.Font = new Font("宋体", 14, FontStyle.Italic);
            dataGridView.Columns["DynastyColumn"].DefaultCellStyle = column2Style;

            // 设置列3的文本大小和字体
            DataGridViewCellStyle column3Style = new DataGridViewCellStyle();
            column3Style.Font = new Font("宋体", 10, FontStyle.Regular);
            dataGridView.Columns["ContentColumn"].DefaultCellStyle = column3Style;

            dataGridView.Location = new Point(margin, textBoxHeight + 2 * margin);
            dataGridView.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 4 * margin - 2 * textBoxHeight);
            dataGridView.Refresh();

            // 设置 TextBox 位置和大小
            textBoxSearch.Location = new Point(margin, margin);
            textBoxSearch.Size = new Size(this.ClientSize.Width - 8 * margin - 3 * buttonWidth - comboBoxWidth, textBoxHeight);

            // 设置搜索按钮位置和大小
            buttonSearch.Location = new Point(textBoxSearch.Right + margin, margin);
            buttonSearch.Size = new Size(buttonWidth, textBoxHeight);

            // 设置 ComboBox 位置和大小
            comboBox.Location = new Point(this.ClientSize.Width - comboBox.Width - buttonWidth - 2 * margin, margin);
            comboBox.Size = new Size(comboBoxWidth, textBoxHeight);

            // 设置刷新按钮位置和大小
            buttonRefresh.Location = new Point(this.ClientSize.Width - buttonWidth - margin, margin);
            buttonRefresh.Size = new Size(buttonWidth, textBoxHeight);

            // 设置更多按钮位置和大小
            buttonNextPage.Location = new Point(buttonSearch.Right + margin, margin);
            buttonNextPage.Size = new Size(buttonWidth, textBoxHeight);

            // 设置导出按钮位置和大小
            buttonSave.Location = new Point(this.ClientSize.Width - buttonWidth - margin, this.ClientSize.Height - textBoxHeight - margin);
            buttonSave.Size = new Size(buttonWidth, textBoxHeight);

            // 订阅窗体大小改变事件
            this.ClientSizeChanged += frmPoems_ClientSizeChanged;

            // 初始化控件位置和大小
            AdjustControlLayout();
        }

        // 将爬取内容解析
        private void doEvents(string response) {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();//声明实例
            doc.LoadHtml(response);//加载HTML文档
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//*[@class=\"cont\"]");
            StringBuilder sb = new StringBuilder();
            foreach (HtmlAgilityPack.HtmlNode item in collection) {
                try {
                    HtmlAgilityPack.HtmlNode divtitle = item.SelectNodes("div[2]/p[1]/a")[0];
                    HtmlAgilityPack.HtmlNode divdynasty = item.SelectNodes("div[2]/p[2]/a[2]")[0];
                    HtmlAgilityPack.HtmlNode divtext = item.SelectNodes("div[2]/div/p")[0];
                    dt.Rows.Add(divtitle.InnerText, divdynasty.InnerText, divtext.InnerText);
                }
                catch { }
            }
        }

        // 将解析内容绑定到 dataGridView 控件中
        private void bindDG(DataRow[] drs) {
            dataGridView.Rows.Clear();
            dataGridView.ClearSelection();

            int col_index = 0;
            for (int i = 0; i < drs.Length; i++) {
                col_index = dataGridView.Rows.Add();
                dataGridView.Rows[col_index].Cells[0].Value = drs[i][0].ToString();
                dataGridView.Rows[col_index].Cells[1].Value = drs[i][1].ToString();
                dataGridView.Rows[col_index].Cells[2].Value = drs[i][2].ToString();
            }
        }

        // 窗体大小改变时对内部控件的操作
        private void frmPoems_ClientSizeChanged(object sender, EventArgs e) {
            // 调整控件位置和大小
            AdjustControlLayout();
        }

        // 随时更新控件在窗体中的位置大小
        private void AdjustControlLayout() {
            int margin = 10;
            int textBoxHeight = 30;
            int buttonWidth = 75;
            int comboBoxWidth = 100;

            if (!this.IsHandleCreated) {
                return;
            }


            // 设置 DataGridView 位置和大小
            dataGridView.Location = new Point(margin, textBoxHeight + 2 * margin);
            dataGridView.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 4 * margin - 2*textBoxHeight);
            //dataGridView.BringToFront();
            dataGridView.Refresh();

            // 设置 TextBox 位置和大小
            textBoxSearch.Location = new Point(margin, margin);
            textBoxSearch.Size = new Size(this.ClientSize.Width - 8 * margin - 3 * buttonWidth - comboBoxWidth, textBoxHeight);

            // 设置搜索按钮位置和大小
            buttonSearch.Location = new Point(textBoxSearch.Right + margin, margin);

            // 设置 ComboBox 位置和大小
            comboBox.Location = new Point(this.ClientSize.Width - comboBox.Width- buttonWidth - 2 * margin, margin);
            comboBox.Size = new Size(comboBoxWidth, textBoxHeight);

            // 设置刷新按钮位置和大小
            buttonRefresh.Location = new Point(this.ClientSize.Width - buttonWidth - margin, margin);

            //设置更多按钮位置和大小
            buttonNextPage.Location = new Point(buttonSearch.Right + margin, margin);

            //设置导出按钮位置和大小
            buttonSave.Location = new Point(this.ClientSize.Width - buttonWidth - margin, this.ClientSize.Height - textBoxHeight - margin);
        }

        // 输入框改变时的逻辑
        private void TextBoxSearch_TextChanged(object sender, EventArgs e) {
            string name = textBoxSearch.Text; // 获取文本框的输入内容
            string name1 = GetFirstCharacter(name); // 取出第一个字保存为name1
            nameu = Uri.EscapeDataString(name); // 将name转换为URL格式
            name1u = Uri.EscapeDataString(name1); // 将name1转换为URL格式
        }

        // 获取第一个字符（生成url时使用）
        private string GetFirstCharacter(string input) {
            // 获取字符串的第一个字符
            if (!string.IsNullOrEmpty(input)) {
                return input.Substring(0, 1);
            }
            return string.Empty;
        }

        // 根据网站规则生成对应作者的url
        private string GenerateUrl(string nameu, string name1u, int count) {
            url1 = "https://so.gushiwen.cn/search.aspx?type=author&page=" + count + "&value=" + nameu + "&valuej=" + name1u;
            return url1;
        }

        // 根据第一列字数排序
        private void SortByFirstColumnLength() {
            // 获取 DataGridView 中的数据源
            var dataSource = dataGridView.DataSource as DataTable;

            // 确保数据源不为空且至少有一列
            if (dataSource != null && dataSource.Columns.Count > 0) {
                // 使用 LINQ 查询对数据源进行排序
                var sortedRows = dataSource.AsEnumerable()
                    .OrderBy(row => row.Field<string>(0)?.Length); // 按第一列的字数升序排序

                // 创建一个新的 DataTable 用于保存排序后的数据
                DataTable sortedTable = dataSource.Clone();

                // 将排序后的数据复制到新的 DataTable
                foreach (var row in sortedRows) {
                    sortedTable.ImportRow(row);
                }

                // 将排序后的数据重新绑定到 DataGridView
                dataGridView.DataSource = sortedTable;
            }
        }

    }
}
