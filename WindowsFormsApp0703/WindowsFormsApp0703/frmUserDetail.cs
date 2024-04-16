using SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    public partial class frmUserDetail : Form {
        public frmUserDetail() {
            InitializeComponent();
            loadDetails();
        }

        private void loadDetails() {
            string selectedNo = Properties.Settings.Default.SelectedNo;
            string query = "SELECT [studentName],[Gender],[Birthday],[Major],[QQ],[Email],[Phone],[Intro],[Province],[LoginTimes],[face],[status] FROM tblTopStudents WHERE studentNo = '" + selectedNo + "'";
            DataSet ds = new DataSet();
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.RunSQL(query, ref ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                DataRow row = ds.Tables[0].Rows[0];
                string studentName = row["studentName"].ToString();
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

                int status = Convert.ToInt32(row["status"]);

                lblName.Text = studentName;
                if (gender == "True") {
                    lblGender.Text = "♂";
                }
                else {
                    lblGender.Text = "♀";
                }
                lblBirthday.Text = "生日: " + birthday;
                lblMajor.Text = "专业: " + major;
                lblQQ.Text = "QQ: " + qq;
                lblEmail.Text = "邮箱: " + email;
                lblPhoneNumber.Text = "电话号码: " + phone;
                lblIntro.Text = "个性签名: " + intro;
                lblProvince.Text = "来自" + province;

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
            }

            string queryFrom = sqlHelper.RunSelectSQLToScalar("SELECT COUNT(*) FROM tblMsgs WHERE from_user = '" + selectedNo +"' AND status != 4");
            string queryTo = sqlHelper.RunSelectSQLToScalar("SELECT COUNT(*) FROM tblMsgs WHERE to_user = '" + selectedNo + "' AND status != 4");
            lblAtTimes.Text = "被@" + queryTo + "次";
            lblSendTimes.Text = "发帖" + queryFrom + "次";
        }

        private void btnRanking_Click(object sender, EventArgs e) {
            frmRanking Rankingfrm = new frmRanking();
            Rankingfrm.ShowDialog();
        }
    }
}
