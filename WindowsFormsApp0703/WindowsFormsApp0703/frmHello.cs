using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp0703
{
    public partial class frmHello : Form
    {
        private string defaultText = "请输入内容";
        private bool isDefaultText = true;

        public frmHello()
        {
            InitializeComponent();

            // 初始化文本框的提示文字
            SetDefaultText();
            this.BackColor = Color.Aquamarine;
        }

        private void SetDefaultText()
        {
            txtbox.Text = defaultText;
            txtbox.ForeColor = Color.Gray;
            isDefaultText = true;
        }

        private void txtbox_Enter(object sender, EventArgs e)
        {
            // 文本框获得焦点时，清空内容并将文字颜色改为黑色
            if (isDefaultText)
            {
                txtbox.Text = "";
                txtbox.ForeColor = Color.Black;
                isDefaultText = false;
            }
        }

        private void txtbox_Leave(object sender, EventArgs e)
        {
            // 文本框失去焦点且内容为空时，显示提示文字并将文字颜色改为灰色
            if (string.IsNullOrEmpty(txtbox.Text))
            {
                SetDefaultText();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // 获取文本框中的内容
            string inputText = isDefaultText ? "" : txtbox.Text;

            // 在文字框中显示输入的内容
            wordbox.Text = inputText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

