using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    public partial class frmSpider : Form {
        string url1 = "https://tieba.baidu.com/f?kw=%E5%8D%8E%E4%B8%9C%E5%B8%88%E8%8C%83%E5%A4%A7%E5%AD%A6&ie=utf-8";
        DataTable dt = new DataTable();

        public frmSpider() {
            InitializeComponent();
            DataColumn dc = new DataColumn();//创建空列
            dt.Columns.Add(dc);
            dt.Columns.Add("title", System.Type.GetType("System.String"));
            dt.Columns.Add("detail", typeof(String));
            dt.Columns.Add("datetime", typeof(String));
        }

        //根据网址爬取内容
        private string spider1(string url) {
            WebClient client = new WebClient();//用webclient爬取数据
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;");
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            return s;
        }

        private void btn_run_Click(object sender, EventArgs e) {
            string content = spider1(url1);//根据url返回爬虫爬取内容
            doEvents(content);//对内容进行结构化处理
            bindDG(dt.Select());//把处理结果绑定到datagridview控件中
        }

        private void doEvents(string response) {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();//声明实例
            doc.LoadHtml(response);//加载HTML文档
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//*[@class=\"col2_right j_threadlist_li_right \"]");
            StringBuilder sb = new StringBuilder();
            foreach (HtmlAgilityPack.HtmlNode item in collection) {
                try {
                    HtmlAgilityPack.HtmlNode divtitle = item.SelectNodes("div[1]/div[1]/a")[0];
                    HtmlAgilityPack.HtmlNode divdetail = item.SelectNodes("div[2]/div[1]/div")[0];
                    HtmlAgilityPack.HtmlNode divtime = item.SelectNodes("div[2]/div[2]/span[2]")[0];
                    dt.Rows.Add(divtitle.InnerText, divdetail.InnerText, divtime.InnerText);
                }
                catch { }
            }
        }

        private void bindDG(DataRow[] drs) {
            dataGridView1.Rows.Clear();
            int col_index = 0;
            for (int i = 0; i < drs.Length; i++) {
                col_index = dataGridView1.Rows.Add();
                dataGridView1.Rows[col_index].Cells[0].Value = drs[i][0].ToString(); // 第2列
                dataGridView1.Rows[col_index].Cells[1].Value = drs[i][1].ToString(); // 第3列
                dataGridView1.Rows[col_index].Cells[2].Value = drs[i][2].ToString(); // 第4列
            }
        }


        private void btn_refresh_Click(object sender, EventArgs e) {
            DataRow[] drs;
            if (comboBox1.Text == "危险") {
                drs = dt.Select("title like '%私聊%' or title like '%举报%' or detail like '%私聊%' or detail like '%举报%'");
            }
            else if (comboBox1.Text == "家教") {
                drs = dt.Select("title like '%家教%' or title like '%兼职%' or detail like '%老师%' or detail like '%招聘%'");
            }
            else if (comboBox1.Text == "宿舍") {
                drs = dt.Select("title like '%宿舍%' or title like '%寝室%' or detail like '%宿舍%' or detail like '%寝室%'");
            }
            else if (comboBox1.Text == "全部") {
                drs = dt.Select();
            }
            else {
                drs = dt.Select();
            }
            bindDG(drs);
        }

    }
}
