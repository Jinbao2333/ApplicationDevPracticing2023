using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frm_choice1 : Form
    {
        Choice[] choices = new Choice[10];
        Button[] buttons = new Button[10];
        string json_choice = string.Empty;
        int curr_no = 0;//放置当前题目的序号
        int finalScore = 100;
        int your_score = 0;
        public frm_choice1()
        {
            InitializeComponent();
            //这里补充题目数量
            json_choice = "1+1=：|A.2;B.3;C.4;D.5|A#";
            json_choice += "1+2=：|A.2;B.3;C.4;D.5|B#";
            json_choice += "1+3=：|A.2;B.3;C.4;D.5|C#";
            json_choice += "1+4=：|A.2;B.3;C.4;D.5|D#";
            json_choice += "1+5=：|A.6;B.3;C.4;D.5|A#";
            json_choice += "1+7=：|A.2;8.3;C.4;D.5|B#";
            json_choice += "1+8=：|A.9;B.3;C.4;D.5|A#";
            json_choice += "1+9=：|A.2;B.3;C.10;D.5|C#";
            json_choice += "1+10=：|A.11;B.3;C.4;D.5|A#";
            json_choice += "1+13=：|A.2;B.3;C.4;D.14|D";
            initChoice();
            initButton();
            showTopic(curr_no);
        }
        /// <summary>
        /// 将文本化的
        /// </summary>
        private void initChoice()
        {
            string[] substr = json_choice.Split('#');//分开每个题目
            for (int i = 0; i < substr.Length; i++)
            {
                Choice ctemp = new Choice();//创建一个新的choice对象
                string[] sub_substr = substr[i].Split('|');//题干|选项|标准答案
                ctemp.Title = sub_substr[0];//给Title赋值，执行的是内部的set方法
                ctemp.Option = sub_substr[1].Split(';');
                ctemp.Key = sub_substr[2];
                ctemp.Answer = "";
                ctemp.Flag = false;
                ctemp.Score = finalScore / substr.Length;
                choices[i] = ctemp;
            }
        }
        /// <summary>
        /// 根据选择题的数量自动初始化按钮布局
        /// </summary>
        private void initButton()
        {
            for (int i = 0; i < choices.Length; i++)
            {
                Button bd = new Button();
                bd.Text = "第" + (i + 1) + "题";
                bd.Size = new Size(64, 39);
                bd.Location = new Point(120 + i * bd.Width, 547);
                buttons[i] = bd;
                bd.Click += new System.EventHandler(this.button1_Click);
                this.Controls.Add(bd);
            }
            this.Refresh();
        }

        private void showTopic(int no)
        {
            this.panel1.Controls.Clear();
            lblTitle.Text = choices[no].Title;
            lblNo.Text = "第" + (no + 1) + "题";
            lblScore.Text = "(" + choices[no].Score + "分)";
            chkFlag.Checked = choices[no].Flag;
            for (int i = 0; i < choices[no].Option.Length; i++)
            {
                RadioButton rd = new RadioButton();
                rd.Text = choices[no].Option[i];
                rd.Location = new Point(47, 29 + i * 40);
                rd.Click += new System.EventHandler(this.radioButton1_Click);
                this.panel1.Controls.Add(rd);
            }
            this.Refresh();
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            //MessageBox.Show(rd.Text);
            choices[curr_no].Answer = rd.Text.Substring(0, 1);//从0开始截取长度为1的字符串
            //这里请补充得分的逻辑，对score赋值
            if (choices[curr_no].Answer != choices[curr_no].Key)
            {
                choices[curr_no].Score = 0;
            }
            choices[curr_no].Flag = true;
            buttons[curr_no].BackColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button bd = (Button)sender;
            //MessageBox.Show(bd.Text);
            //第一题，截取之后 1  修改一下Bug，第10题截取之后也是1
            if (bd.Text.Length == 3)
            {
                curr_no = int.Parse(bd.Text.Substring(1, 1)) - 1;
            }
            else
            {
                curr_no = int.Parse(bd.Text.Substring(1, 2)) - 1;
            }
            showTopic(curr_no);
        }

        private void chkFlag_Click(object sender, EventArgs e)
        {
            buttons[curr_no].BackColor = Color.Red;
        }

        public class Choice
        {
            private int _score;
            private string _title;
            private string[] _option;
            private string _key;
            private string _answer;
            private bool _flag;
            /// <summary>
            /// 该选择题打标签
            /// </summary>
            public int Score { get => _score; set => _score = value; }
            public string Title { get => _title; set => _title = value; }
            public string[] Option { get => _option; set => _option = value; }
            public string Key { get => _key; set => _key = value; }
            public string Answer { get => _answer; set => _answer = value; }
            public bool Flag { get => _flag; set => _flag = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sum_score = 0;
            for(int i = 0; i < 4; i++)
            {
                if (choices[i].Flag)
                {
                    sum_score += choices[i].Score;
                }
            }
            lblscores.Text = sum_score.ToString();
        }
    }
}
