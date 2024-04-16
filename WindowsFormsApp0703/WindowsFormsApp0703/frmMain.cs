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
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
            AddButtons();

        }

        private void AddButtons() {

            Button button1 = new Button();
            button1.Text = "Bubble Sort";
            button1.Size = new Size(100, 30);
            button1.Location = new Point(10, 10);
            button1.Click += Button1_Click;

            Button button2 = new Button();
            button2.Text = "Choice";
            button2.Size = new Size(100, 30);
            button2.Location = new Point(10, button1.Bottom + 10);
            button2.Click += Button2_Click;

            Button button3 = new Button();
            button3.Text = "Clock";
            button3.Size = new Size(100, 30);
            button3.Location = new Point(10, button2.Bottom + 10);
            button3.Click += Button3_Click;

            Button button4 = new Button();
            button4.Text = "Hello";
            button4.Size = new Size(100, 30);
            button4.Location = new Point(10, button3.Bottom + 10);
            button4.Click += Button4_Click;

            Button button5 = new Button();
            button5.Text = "Picture";
            button5.Size = new Size(100, 30);
            button5.Location = new Point(10, button4.Bottom + 10);
            button5.Click += Button5_Click;

            Button button6 = new Button();
            button6.Text = "Poems";
            button6.Size = new Size(100, 30);
            button6.Location = new Point(10, button5.Bottom + 10);
            button6.Click += Button6_Click;

            Button button7 = new Button();
            button7.Text = "Spider";
            button7.Size = new Size(100, 30);
            button7.Location = new Point(10, button6.Bottom + 10);
            button7.Click += Button7_Click;

            Button button8 = new Button();
            button8.Text = "TestSQL";
            button8.Size = new Size(100, 30);
            button8.Location = new Point(10, button7.Bottom + 10);
            button8.Click += Button8_Click;

            Button button9 = new Button();
            button9.Text = "Chat";
            button9.Size = new Size(100, 30);
            button9.Location = new Point(10, button8.Bottom + 10);
            button9.Click += Button9_Click;

            // 将按钮添加到窗体中
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(button6);
            Controls.Add(button7);
            Controls.Add(button8);
            Controls.Add(button9);
        }

        private void Button1_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 1 clicked!");
        }

        private void Button2_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 2 clicked!");
        }

        private void Button3_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 3 clicked!");
        }

        private void Button4_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 4 clicked!");
        }

        private void Button5_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 5 clicked!");
        }

        private void Button6_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 6 clicked!");
        }

        private void Button7_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 7 clicked!");
        }

        private void Button8_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 8 clicked!");
        }

        private void Button9_Click(object sender, EventArgs e) {
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
        }

    }
}
