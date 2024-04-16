using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    public partial class frmChoice : Form {
        int numbersOfQ = 12;
        Choice[] choices = new Choice[12];
        Button[] button = new Button[12];
        Regex regex = new Regex(@"\d+"); // 匹配连续的数字部分
        string json_choice = string.Empty;
        int currentNo = 0;
        float finalScore = 100;
        private Dictionary<int, Button> buttonDictionary = new Dictionary<int, Button>();
        private Button[] buttons;
        private int countdownSeconds = 100; // 倒计时总秒数
        private Timer countdownTimer = new Timer();


        private void CountdownTimer_Tick(object sender, EventArgs e) {
            countdownSeconds--;

            int minutes = countdownSeconds / 60;
            int seconds = countdownSeconds % 60;

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            countdownLabel.Text = "本场考试剩余时间：" + formattedTime;
            if (countdownSeconds <= 90) {// 设置高亮提醒阈值
                countdownLabel.Font = new Font(countdownLabel.Font.FontFamily, 13, FontStyle.Bold);
                countdownLabel.Text = "请注意剩余时间：" + formattedTime;
                countdownLabel.ForeColor = Color.Red;

            }

            if (countdownSeconds == 0) {
                countdownTimer.Stop();
                float totalScore = 0;
                for (int i = 0; i < numbersOfQ; i++) {
                    if (choices[i].FlagTF) {
                        totalScore += choices[i].Score;
                    }
                }

                MessageBox.Show("已强制收卷，您的总得分是：" + totalScore, "时间到！");
                this.Close();
            }

        }

        public frmChoice() {
            InitializeComponent();
            json_choice = "1 + 1 = ||2|8#";
            json_choice += "苹果肉的颜色是什么？|A.红色;B.黄色;C.绿色;D.蓝色;E.紫色|B|8#";
            json_choice += "香蕉的颜色是什么？|A.红色;B.黄色;C.绿色;D.蓝色|B|8#";
            json_choice += "老虎会游泳吗？|A.会;B.不会|A|8#";
            json_choice += "1 * 2 = |A.1;B.2;C.3;D.4|B|8#";
            json_choice += "鸟会下蛋吗？|A.会;B.不会|A|8#";
            json_choice += "西瓜皮的颜色是什么？|A.红色;B.黄色;C.绿色;D.蓝色|C|8#";
            json_choice += "1 + 8 = ||9|8#";
            json_choice += "草是绿色的吗？|A.是;B.不是|A|8#";
            json_choice += "2 * 2 = ||4|8#";
            json_choice += "橘子的颜色是什么？|A.红色;B.橙色;C.绿色;D.蓝色|B|10#";
            json_choice += "狗会爬树吗？|A.会;B.不会|B|10";

            title.Location = new Point(20 + indx.Width + 80, 20);
            countdownLabel.Location = new Point(this.ClientSize.Width - countdownLabel.Width - 10, 20);

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            countdownTimer.Start();

            initChoice();
            initButton();
            showTopic(currentNo);
        }

        private void initChoice() {
            string[] substr = json_choice.Split('#');
            for (int i = 0; i < substr.Length; i++) {
                _ = new Button();
                Choice ctemp = new Choice();
                string[] sub_substr = substr[i].Split('|');
                ctemp.Title = sub_substr[0];
                if (sub_substr[1].Length == 0) {
                    ctemp.IsChoice = false;
                }
                else {
                    ctemp.IsChoice = true;
                    ctemp.Option = sub_substr[1].Split(';');
                }
                ctemp.Key = sub_substr[2];
                ctemp.Answer = "";
                ctemp.Flag = false;
                ctemp.FlagTF = false;
                ctemp.ButtonIndex = i;
                float score;
                if (float.TryParse(sub_substr[3], out score)) {
                    ctemp.Score = score;
                }
                else {
                    ctemp.Score = finalScore / substr.Length;
                }
                choices[i] = ctemp;
            }
            this.Refresh();
        }

        private void initButton() {
            buttons = new Button[choices.Length];
            for (int i = 0; i < choices.Length; i++) {
                Button bd = new Button();
                bd.Text = "第" + (i + 1) + "题";
                bd.Size = new Size(80, 40);
                bd.Location = new Point(20, 16 + i * bd.Height);
                bd.Tag = i; // 设置按钮的Tag属性为对应的题目索引
                bd.Click += new System.EventHandler(this.button_Click);
                choices[i].ButtonIndex = i; // 设置按钮索引到Choice对象
                button[i] = bd;
                buttonDictionary[i] = bd;
                this.Controls.Add(bd);
            }
            handInButton.Location = new Point(this.ClientSize.Width - handInButton.Width - 10, this.ClientSize.Height - handInButton.Height - 10);
            this.Refresh();
        }

        private void button_Click(object sender, EventArgs e) {
            Button button = (Button)sender;
            Regex regex = new Regex(@"\d+"); // 匹配连续的数字部分
            Match match = regex.Match(button.Text);
            currentNo = int.Parse(match.Value) - 1;

            /*if (choices[currentNo].IsChoice == false) {
                if (fillBlank.Text.Length != 0) {
                    choices[currentNo].Flag = true;// 标注做题了
                    choices[currentNo].Answer = fillBlank.Text;// 存入答案
                    if (choices[currentNo].Answer != choices[currentNo].Key) {
                        choices[currentNo].FlagTF = false;// 这题错了不算分
                    }
                    else if (choices[currentNo].Answer == choices[currentNo].Key) {
                        choices[currentNo].FlagTF = true;// 对了算分
                    }
                    if (choices[currentNo].Flag2 == false) {// 没有标黄就绿色
                        GreenButton(currentNo);
                    }
                }
                if (fillBlank.Text.Length != 0) {
                    choices[currentNo].Flag = true;
                    choices[currentNo].Answer = fillBlank.Text;
                    if (choices[currentNo].Answer != choices[currentNo].Key) {
                        choices[currentNo].FlagTF = false;
                    }
                    else if (choices[currentNo].Answer == choices[currentNo].Key) {
                        choices[currentNo].FlagTF = true;
                    }
                    choices[currentNo].Flag = true;
                }
            }*/
            showTopic(currentNo);
        }


        private void showTopic(int no) {
            this.panel1.Controls.Clear();
            title.Text = choices[no].Title;
            scoreLabel.Text = "本题 " + choices[no].Score + "分";
            scoreLabel.Location = new Point(20, this.panel1.Height - 50); // 设置 Label 的位置
            scoreLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right; // 设置 Label 的 Anchor 属性
            panel1.Controls.Add(scoreLabel); // 将 Label 添加到 panel1 控件中
            if (choices[no].IsChoice == true) {
                if (choices[no].Flag == false) {
                    choices[no].AnswerNo = -1;
                }
                indx.Text = "第" + (no + 1) + "题";
                fillBlank.Visible = false;
                for (int i = 0; i < choices[no].Option.Length; i++) {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Click += radioButton1_Click;
                    radioButton.Text = choices[no].Option[i];
                    radioButton.Checked = false;
                    radioButton.Location = new Point(20, 20 + 30 * i);
                    if (choices[no].AnswerNo == i) {
                        radioButton.Checked = true;
                    }
                    panel1.Controls.Add(radioButton);

                }
            }
            else {
                if (choices[no].Flag == false) {
                    choices[no].AnswerNo = -1;
                }
                indx.Text = "第" + (no + 1) + "题";
                if (choices[no].Answer.Length != 0) {
                    fillBlank.Text = choices[no].Answer;
                }
                else {
                    fillBlank.Text = "";
                }
                Button saveAns = new Button();
                saveAns.Text = "保存答案";
                saveAns.Size = new Size(120, 30);
                saveAns.Location = new Point(310, 10);
                fillBlank.Visible = true;
                fillBlank.Location = new Point(10, 10);
                fillBlank.Size = new Size(600, 60);
                fillBlank.Font = new Font(fillBlank.Font.FontFamily, 20, fillBlank.Font.Style);


                panel1.Controls.Add(fillBlank);

            }
            CheckBox newCheckBox = new CheckBox();
            newCheckBox.Text = "标记这道题";
            newCheckBox.AutoSize = true;
            newCheckBox.Location = new Point(20, this.panel1.Height - 30);
            newCheckBox.Checked = choices[no].Flag2;
            newCheckBox.CheckedChanged += new EventHandler(markUncertain_CheckedChanged);
            panel1.Controls.Add(newCheckBox);
        }



        private void markUncertain_CheckedChanged(object sender, EventArgs e) {
            CheckBox checkBox = (CheckBox)sender;
            choices[currentNo].Flag2 = checkBox.Checked;

            int buttonIndex = choices[currentNo].ButtonIndex;
            Button button = buttonDictionary[buttonIndex];
            if (checkBox.Checked) {
                button.BackColor = Color.Yellow;
            }
            else if (choices[currentNo].Flag) {
                button.BackColor = Color.Green;
            }
            else {
                button.BackColor = Color.White;
            }
        }


        private void handInButton_Click(object sender, EventArgs e) {
            // 处理提交按钮的逻辑
            float totalScore = 0;
            string noticeMsg = "";
            for (int i = 0; i < numbersOfQ; i++) {
                if (choices[i].FlagTF) {
                    totalScore += choices[i].Score;
                }
                if (choices[i].Flag == false) {
                    noticeMsg += " ";
                    noticeMsg += (i + 1);
                }
            }
            //MessageBox.Show(currentNo + "总得分：" + choices[currentNo].AnswerNo);
            if (noticeMsg.Length != 0) {
                DialogResult result = MessageBox.Show("您还有" + noticeMsg + "题未作答，是否返回检查？", "注意！", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                }
                else {
                    MessageBox.Show("您的总得分是：" + totalScore, "本场考试已结束");
                    this.Close();
                }
            }
            else {
                MessageBox.Show("您的总得分是：" + totalScore, "本场考试已结束");
                this.Close();
            }

        }

        private void radioButton1_Click(object sender, EventArgs e) {
            RadioButton radioButton = (RadioButton)sender;
            choices[currentNo].Answer = radioButton.Text.Substring(0, 1);
            string tempAns = choices[currentNo].Answer;
            if (tempAns.Length == 1 && char.IsLetter(tempAns[0])) {
                choices[currentNo].AnswerNo = (int)tempAns[0] - 65;
            }
            if (choices[currentNo].Answer != choices[currentNo].Key) {
                choices[currentNo].FlagTF = false;
            }
            else if (choices[currentNo].Answer == choices[currentNo].Key) {
                choices[currentNo].FlagTF = true;
            }
            choices[currentNo].Flag = true;
            if (choices[currentNo].Flag2 == false) {
                button[currentNo].BackColor = Color.Green;
            }

        }

        /*private void SaveAns_Click(object sender, EventArgs e) {
            choices[currentNo].Answer = fillBlank.Text;// 存入答案
            if (fillBlank.Text.Length != 0) {
                choices[currentNo].Flag = true;// 标注做题了

                if (choices[currentNo].Answer != choices[currentNo].Key) {
                    choices[currentNo].FlagTF = false;// 这题错了不算分
                }
                else if (choices[currentNo].Answer == choices[currentNo].Key) {
                    choices[currentNo].FlagTF = true;// 对了算分
                }
                if (choices[currentNo].Flag2 == false) {// 没有标黄就绿色
                    button[currentNo].BackColor = Color.Green;
                }
            }
            else if (fillBlank.Text.Length == 0) {
                if (choices[currentNo].Flag2 == false) {// 没有标黄就绿色
                    button[currentNo].BackColor = Color.White;
                }
            }

        }*/
        private void frmChoice_SizeChanged(object sender, EventArgs e) {
            handInButton.Location = new Point(this.ClientSize.Width - handInButton.Width - 10, this.ClientSize.Height - handInButton.Height - 10);
            countdownLabel.Location = new Point(this.ClientSize.Width - countdownLabel.Width - 10, 20);

        }

        private void GreenButton(int currentNo) {
            button[currentNo].BackColor = Color.Green;
        }

        private void fillBlank_TextChanged(object sender, EventArgs e) {
            choices[currentNo].Answer = fillBlank.Text;// 存入答案
            if (fillBlank.Text.Length != 0) {
                choices[currentNo].Flag = true;// 标注做题了

                if (choices[currentNo].Answer != choices[currentNo].Key) {
                    choices[currentNo].FlagTF = false;// 这题错了不算分
                }
                else if (choices[currentNo].Answer == choices[currentNo].Key) {
                    choices[currentNo].FlagTF = true;// 对了算分
                }
                if (choices[currentNo].Flag2 == false) {// 没有标黄就绿色
                    button[currentNo].BackColor = Color.Green;
                }
            }
            else if (fillBlank.Text.Length == 0) {
                if (choices[currentNo].Flag2 == false) {// 没有标黄就绿色
                    button[currentNo].BackColor = Color.White;
                }
            }
        }
    }

    public class Choice {
        private float _score;
        private string _title;
        private string[] _option;
        private string _key;
        private string _answer;
        private int _answerNo;
        private bool _flag;//做题了吗
        private bool _flag2;//标注了吗
        private bool _flagTF;//做对了吗
        private int _buttonIndex;
        private string _selectedOption; // 新增属性，用于存储选项的选择结果
        private bool _isChoice;

        public float Score {
            get => _score; set => _score = value;
        }
        public string Key {
            get => _key; set => _key = value;
        }
        public string Answer {
            get => _answer; set => _answer = value;
        }
        public bool Flag {
            get => _flag; set => _flag = value;
        }
        public string Title {
            get => _title; set => _title = value;
        }
        public string[] Option {
            get => _option; set => _option = value;
        }
        public int ButtonIndex {
            get => _buttonIndex; set => _buttonIndex = value;
        }
        public string SelectedOption {
            get => _selectedOption; set => _selectedOption = value;
        } // 新增属性，用于存储选项的选择结果

        public int SelectedOptionIndex {
            get; set;
        } // 保存选择的选项索引
        public bool Flag2 {
            get => _flag2;
            set => _flag2 = value;
        }
        public bool FlagTF {
            get => _flagTF;
            set => _flagTF = value;
        }
        public int AnswerNo {
            get => _answerNo;
            set => _answerNo = value;
        }
        public bool IsChoice {
            get => _isChoice;
            set => _isChoice = value;
        }
    }

}
