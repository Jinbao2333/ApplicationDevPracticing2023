using SQL;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp0703 {
    public partial class frmTestSQL : Form {

        string stuno;
        string oldpswd;
        string newpsd1;
        string newpsd2;
        string stuuuuuid;
        private string connectionString = "your_mysql_connection_string"; // Replace with your MySQL connection string
        private string tableName = "tblTopStudents"; // Replace with your table name
        private string columnName = "face"; // Replace with your column name

        public frmTestSQL() {
            InitializeComponent();
            InitializeControlLayout();
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                SQLHelper sh = new SQLHelper();
                string ret = "0";
                ret = sh.RunSelectSQLToScalar("select studentName from tblTopStudents where studentNo = '10225501447'");
                MessageBox.Show(ret);
            }catch(Exception ex) {
                MessageBox.Show("数据库异常！详情如下：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e) {

            if (stuno.Length == 11 && oldpswd.Length != 0 && newpsd1.Length != 0 && newpsd2.Length != 0) {
                if (newpsd1 == newpsd2) {
                    SQLHelper sh = new SQLHelper();
                    string oldfromsql = sh.RunSelectSQLToScalar("SELECT password FROM tblTopStudents WHERE studentNo = '" + stuno + "'");
                    string oldtest = EncodePassword.GetSHA256Hash(oldpswd);
                    if (oldfromsql == oldtest) {
                        try {
                            string encryptedPassword = EncodePassword.GetSHA256Hash(newpsd1);
                            string updateSQL = "UPDATE tblTopStudents SET password = '" + encryptedPassword + "' WHERE studentNo = '" + stuno + "'";
                            sh.RunSQL(updateSQL);
                            string ret = sh.RunSelectSQLToScalar("SELECT password FROM tblTopStudents WHERE studentNo = '" + stuno + "'");
                            MessageBox.Show("密码更新成功！新密码为: " + ret);
                        }
                        catch (Exception ex) {
                            MessageBox.Show("数据库异常！详情如下：" + ex.Message);
                        }
                    }
                    else {
                        MessageBox.Show("原密码错误！");
                    }
                }
                else {
                    MessageBox.Show("两次新密码不一致！请重新输入！");
                }
            }
            else {
                MessageBox.Show("请输入正确的数据以后再点击重置！");
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e) {
            stuno = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            oldpswd = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            newpsd1 = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e) {
            newpsd2 = textBox4.Text;
        }

        private void InitializeControlLayout() {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Padding = new Padding(20);
            //tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 6;

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            tableLayoutPanel.Controls.Add(button1, 0, 4);
            tableLayoutPanel.Controls.Add(button2, 1, 4);

            tableLayoutPanel.Controls.Add(label1, 0, 0);
            tableLayoutPanel.Controls.Add(label2, 0, 1);
            tableLayoutPanel.Controls.Add(label3, 0, 2);
            tableLayoutPanel.Controls.Add(label4, 0, 3);

            tableLayoutPanel.Controls.Add(textBox1, 1, 0);
            tableLayoutPanel.Controls.Add(textBox2, 1, 1);
            tableLayoutPanel.Controls.Add(textBox3, 1, 2);
            tableLayoutPanel.Controls.Add(textBox4, 1, 3);

            this.Controls.Add(tableLayoutPanel);
        }

        private void button3_Click(object sender, EventArgs e) {
            //string connectionString = "your_mysql_connection_string"; // Replace with your MySQL connection string
            string imagePath = @"C:\Users\Administrator.JINBAOSLAPTOP\Downloads\icon.jpg"; // Replace with the actual path of your image file
            string tableName = "tblTopStudents"; // Replace with your table name
            string columnName = "face"; // Replace with your column name
            string studentNo = "10225501447"; // Replace with the specific studentNo you want to update

            // Convert image to byte array
            byte[] imageData = File.ReadAllBytes(imagePath);

            // Use the SQLHelper class to execute the SQL command and update the image for the specified studentNo
            SQLHelper sqlHelper = new SQLHelper();

            try {
                string updateSql = $"UPDATE {tableName} SET {columnName} = @imageData WHERE studentNo = @studentNo";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    sqlHelper.CreateInParam("@imageData", SqlDbType.VarBinary, imageData.Length, imageData),
                    sqlHelper.CreateInParam("@studentNo", SqlDbType.VarChar, 50, studentNo)
                };

                int rowsAffected = sqlHelper.RunSQL(updateSql, parameters);
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally {
                sqlHelper.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            string studentNo = stuuuuuid; // Replace with the specific studentNo you want to retrieve the image for

            // Use the SQLHelper class to execute the SQL command and retrieve the image for the specified studentNo
            SQLHelper sqlHelper = new SQLHelper();

            try {
                string selectSql = $"SELECT {columnName} FROM {tableName} WHERE studentNo = @studentNo";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    sqlHelper.CreateInParam("@studentNo", SqlDbType.VarChar, 50, studentNo)
                };

                byte[] imageData = null;
                sqlHelper.RunSQL(selectSql, parameters, out SqlDataReader dataReader);

                if (dataReader != null && dataReader.Read()) {
                    if (!dataReader.IsDBNull(0)) {
                        imageData = (byte[])dataReader.GetValue(0);
                    }
                }

                dataReader.Close();
                sqlHelper.Close();

                if (imageData != null) {
                    // Convert byte array to image and display it in pictureBox1
                    using (MemoryStream memoryStream = new MemoryStream(imageData)) {
                        Image image = Image.FromStream(memoryStream);
                        pictureBox1.Image = image;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else {
                    // If no image data found, display a placeholder image or handle the case as needed.
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally {
                sqlHelper.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e) {
            stuuuuuid = textBox5.Text;
        }
    }
}

