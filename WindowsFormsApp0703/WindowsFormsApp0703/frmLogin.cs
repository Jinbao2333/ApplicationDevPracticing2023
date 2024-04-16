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

namespace WindowsFormsApp0703 {
    public partial class frmLogin : Form {

        string stuID;
        string stuPassword;
        int countdownSeconds;
        private WatermarkTextbox txtID;
        private WatermarkTextbox txtPassword;
        private CheckBox chkRemember;
        private CheckBox chkAutoLogin;
        private CheckBox chkShowPassword;
        private Label lblCountdown;
        private bool autoLoginNow;

        public frmLogin() {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile("C:\\Users\\Administrator.JINBAOSLAPTOP\\Desktop\\loginback.png");

            stuID = string.Empty;
            stuPassword = string.Empty;
            txtID = new WatermarkTextbox();

            if (Properties.Settings.Default.Username.Length > 0) {
                txtID.Text = Properties.Settings.Default.Username;
                stuID = Properties.Settings.Default.Username;
            }
            txtID.WatermarkText = "ID";
            txtID.Size = new System.Drawing.Size(300, 40);
            txtID.Font = new Font("Bahnschrift SemiBold", 15);
            Controls.Add(txtID);

            txtPassword = new WatermarkTextbox();
            if (Properties.Settings.Default.Password.Length > 0) {
                txtPassword.Text = Properties.Settings.Default.Password;
                stuPassword = Properties.Settings.Default.Password;
            }
            txtPassword.WatermarkText = "Password";
            txtPassword.Size = new System.Drawing.Size(300, 40);
            txtPassword.Font = new Font("Bahnschrift SemiBold", 15);
            Controls.Add(txtPassword);

            // 计算文本框在窗体中的位置
            int x = (this.ClientSize.Width - txtID.Width) / 2 + 30;
            int y = (this.ClientSize.Height - txtID.Height - txtPassword.Height) / 2;

            // 设置文本框的位置
            txtID.Location = new Point(x - 60, y - 80);
            txtPassword.Location = new Point(x - 60, y + txtID.Height - 70); // 假设在两个文本框之间有10像素的间距
            btnLogin.Size = new System.Drawing.Size(300, 80);
            btnLogin.Location = new Point(x - 60, y + 2 * txtID.Height - 60);

            chkRemember = new CheckBox();
            chkRemember.Text = "记住信息";
            chkRemember.Checked = Properties.Settings.Default.RememberPassword;
            chkRemember.Font = new Font("思源黑体 CN Light", 12);
            chkRemember.Size = new System.Drawing.Size(100, 40);
            chkRemember.Location = new Point(x - 55, y + 4 * txtID.Height - 40); // 设置复选框的位置
            chkRemember.BackColor = Color.Transparent;
            Controls.Add(chkRemember);

            chkAutoLogin = new CheckBox();
            chkAutoLogin.Text = "自动登录";
            chkAutoLogin.Checked = Properties.Settings.Default.RememberPassword;
            chkAutoLogin.Font = new Font("思源黑体 CN Light", 12);
            chkAutoLogin.Size = new System.Drawing.Size(100, 40);
            chkAutoLogin.Location = new Point(x + 50, y + 4 * txtID.Height - 40); // 设置复选框的位置
            chkAutoLogin.BackColor = Color.Transparent;
            chkAutoLogin.Checked = Properties.Settings.Default.AutoLoginState;
            Controls.Add(chkAutoLogin);

            chkShowPassword = new CheckBox();
            chkShowPassword.Text = "显示密码";
            chkShowPassword.Checked = false;
            chkShowPassword.Font = new Font("思源黑体 CN Light", 12);
            chkShowPassword.Size = new System.Drawing.Size(100, 40);
            chkShowPassword.Location = new Point(x + 150, y + 4 * txtID.Height - 40); // 设置复选框的位置
            chkShowPassword.BackColor = Color.Transparent;
            txtPassword.UseSystemPasswordChar = true;
            Controls.Add(chkShowPassword);

            txtID.TextChanged += textBoxID_TextChanged;
            txtPassword.TextChanged += textBoxPassword_TextChanged;
            chkShowPassword.Click += ChkShowPassword_Click;
            chkRemember.Click += ChkRemember_Click;

            if (txtPassword.Text.Length != 0) {
                txtPassword.BackColor = Color.LightGreen;
            }
            else {
                txtPassword.BackColor = Color.White;
            }

            if (txtID.Text.Length == 11) {
                txtID.BackColor = Color.LightGreen;
            }
            else {
                txtID.BackColor = Color.White;
            }

            if (Properties.Settings.Default.AutoLoginState) {
                // 创建并设置倒计时标签
                //lblCountdown = new Label();
                //lblCountdown.Font = new Font("思源黑体 CN ExtraLight", 14, FontStyle.Bold);
                //lblCountdown.ForeColor = Color.FromArgb(160, 160, 160);
                //lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
                //lblCountdown.Size = new Size(100, 30);
                //lblCountdown.Location = new Point(x - 55, y + 4 * txtID.Height);
                autoLoginNow = true;
                btnLogin.Text = "还有" + countdownSeconds.ToString() + "秒自动登录";
                Controls.Add(lblCountdown);

                // 启动倒计时
                StartCountdown();
            }
        }


        private void ChkRemember_Click(object sender, EventArgs e) {
            if (chkRemember.Checked) {
                // 保存帐号和密码
                Properties.Settings.Default.Username = txtID.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.RememberPassword = true;
                Properties.Settings.Default.Save();
                if (chkAutoLogin.Checked) {
                    Properties.Settings.Default.AutoLoginState = true;
                    Properties.Settings.Default.Save();
                }
                else {
                    Properties.Settings.Default.AutoLoginState = false;
                    Properties.Settings.Default.Save();
                }
            }
            else {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.RememberPassword = false;
                Properties.Settings.Default.Save();
            }
        }

        private void ChkShowPassword_Click(object sender, EventArgs e) {
            if (chkShowPassword.Checked) {
                txtPassword.UseSystemPasswordChar = false;
            }
            else {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e) {
            stuID = txtID.Text;
            autoLoginNow = false;
            if (txtID.Text.Length == 11) {
                txtID.BackColor = Color.LightGreen;
            }
            else {
                txtID.BackColor = Color.White;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e) {
            stuPassword = txtPassword.Text;
            //txtPassword.UseSystemPasswordChar = true;
            autoLoginNow = false;
            if (txtPassword.Text.Length != 0) {
                txtPassword.BackColor = Color.LightGreen;
            }
            else {
                txtPassword.BackColor = Color.White;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            autoLoginNow = false;

            if (chkRemember.Checked) {
                // 保存帐号和密码
                Properties.Settings.Default.Username = txtID.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.RememberPassword = true;
                Properties.Settings.Default.Save();
                if (chkAutoLogin.Checked) {
                    Properties.Settings.Default.AutoLoginState = true;
                    Properties.Settings.Default.Save();
                }
                else {
                    Properties.Settings.Default.AutoLoginState = false;
                    Properties.Settings.Default.Save();
                }
            }
            else {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.RememberPassword = false;
                Properties.Settings.Default.Save();
                if (chkAutoLogin.Checked) {
                    MessageBox.Show("自动登录前必须勾选保存密码！");
                    return;
                }
                else {
                    Properties.Settings.Default.AutoLoginState = false;
                    Properties.Settings.Default.Save();
                }
            }

            Properties.Settings.Default.AutoLoginState = chkAutoLogin.Checked;

            if (stuID.Length == 11 & stuPassword.Length > 0) {

                btnLogin.Text = "登录中...";
                //MessageBox.Show("暂时性的胜利！");
                SQLHelper sh = new SQLHelper();
                string standardPassword = sh.RunSelectSQLToScalar("SELECT password FROM tblTopStudents WHERE studentNo = '" + stuID + "'");
                try {
                    string encryptedPassword = EncodePassword.GetSHA256Hash(stuPassword);
                    if (encryptedPassword == standardPassword) {
                        frmMsg fs = new frmMsg();
                        fs.Show();
                        Close();
                    }
                    else {
                        MessageBox.Show("帐号或密码输入错误！");
                        btnLogin.Text = "登录";
                        return;
                    }

                }
                catch (Exception ex) {
                    MessageBox.Show("登录失败，原因：数据库异常！详情如下：\n" + ex.Message);
                    btnLogin.Text = "登录";
                }
            }
            else {
                MessageBox.Show("请输入完整的ID和密码！");
            }
        }

        private async void StartCountdown() {
            countdownSeconds = 5;
            while (countdownSeconds > 0) {
                await Task.Delay(1000); // 延迟一秒钟
                if (autoLoginNow == false) {
                    btnLogin.Text = "登录";
                    return;
                }
                countdownSeconds--;

                if (countdownSeconds == 0) {
                    btnLogin.Text = "自动登录中...";
                }
                else {
                    btnLogin.Text = "还有" + countdownSeconds.ToString() + "秒自动登录";
                }
            }
            // 倒计时结束后执行自动登录
            DoAutoLogin();
        }

        private void DoAutoLogin() {
            // 获取保存的帐号和密码
            string savedUsername = Properties.Settings.Default.Username;
            string savedPassword = Properties.Settings.Default.Password;

            // 自动填充用户名和密码
            txtID.Text = savedUsername;
            txtPassword.Text = savedPassword;

            // 执行登录操作
            btnLogin_Click(null, null);
        }
    }
}
