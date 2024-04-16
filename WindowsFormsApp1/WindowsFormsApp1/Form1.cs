using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbl.Text = "Please input some words...";
        }


        private void btn_Click(object sender, EventArgs e)
        {
            if (lbl.Text.Length > 0)
                lbl.Text = textBox1.Text;
            else
            {
                MessageBox.Show("You must input some words!!!");
                lbl.BackColor = Color.Green;
                lbl.Focus();
            }
            /*string text = textBox1.Text;
            string inputText = text; // 获取文本框中的输入文本
            lbl.Text = inputText; // 在标签中显示相同的文本*/
        }
    }
}

