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
    public partial class frmBubblesort : Form
    {
        Label[] labels = new Label[10];
        int[] array = new int[10];
        bool sorting = false;
        int speed = 100;

        public frmBubblesort()
        {
            InitializeComponent();
            initArray();
            addControl();
        }

        private void addControl()
        {
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbltemp = new Label();
                this.panel1.Controls.Add(lbltemp);
                lbltemp.Text = array[i].ToString();
                labels[i] = lbltemp;
                labels[i].BackColor = Color.FromArgb(0, 255, 212);
                labels[i].Size = new Size(30, 18);
                labels[i].Location = new Point(30 + i * 40, 18);
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void newLine(int line)
        {
            for (int i = 0; i < labels.Length - line; i++)
            {
                Label lbltemp = new Label();
                this.panel1.Controls.Add(lbltemp);
                lbltemp.Text = array[i].ToString();
                labels[i] = lbltemp;
                labels[i].BackColor = Color.FromArgb(0, 255, 212);
                labels[i].Size = new Size(30, 18);
                labels[i].Location = new Point(30 + i * 40, 18 + 22 * line);
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
            }
            for (int i = labels.Length - line; i < labels.Length; i++)
            {
                Label lbltemp = new Label();
                this.panel1.Controls.Add(lbltemp);
                lbltemp.Text = array[i].ToString();
                labels[i] = lbltemp;
                labels[i].BackColor = Color.FromArgb(172, 0, 0);
                labels[i].ForeColor = Color.White;
                labels[i].Size = new Size(30, 18);
                labels[i].Location = new Point(30 + i * 40, 18 + 22 * line);
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void initArray()
        {
            Random rd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rd.Next(0, 200);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;
                button2.Enabled = false;
                button1.Enabled = false;
                await StartSortingAsync();
                sorting = false;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!sorting)
            {
                this.panel1.Controls.Clear();
                this.Refresh();
                initArray();
                addControl();
                button2.Enabled = true;
            }
        }

        private async Task StartSortingAsync()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (i > 0) newLine(i);
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    labels[j].BackColor = Color.Pink;
                    labels[j + 1].BackColor = Color.Pink;
                    this.Refresh();
                    await Task.Delay(2 * speed);

                    if (array[j] > array[j + 1])
                    {
                        labels[j].BackColor = Color.DeepPink;
                        labels[j + 1].BackColor = Color.DeepPink;
                        await Task.Delay(2 * speed);
                        Swap(j, j + 1);
                        this.Refresh();
                    }
                    await Task.Delay(3 * speed);
                    labels[j].BackColor = Color.FromArgb(0, 168, 136);
                    labels[j + 1].BackColor = Color.FromArgb(0, 168, 136);
                    this.Refresh();

                }
                this.Refresh();
            }
            sumUp();
        }


        private void Swap(int index1, int index2)
        {
            int temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;

            // Update visually
            labels[index1].Text = array[index1].ToString();
            labels[index2].Text = array[index2].ToString();
            this.Refresh();
        }

        private async void sumUp()
        {
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbltemp = new Label();
                this.panel1.Controls.Add(lbltemp);
                lbltemp.Text = array[i].ToString();
                labels[i] = lbltemp;
                labels[i].BackColor = Color.FromArgb(112-7*i, 173-5*i, 70-6*i);
                labels[i].ForeColor = Color.White;
                labels[i].Size = new Size(30, 18);
                labels[i].Location = new Point(30 + i * 40, 18 + 22 * (labels.Length - 1));
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                await Task.Delay(speed);
            }

        }

        private void spdplus_Click(object sender, EventArgs e)
        {
            if (speed > 10) speed /= 2;
        }

        private void spdminus_Click(object sender, EventArgs e)
        {
            if (speed < 20000) speed *= 2;
        }

        private void nmspd_Click(object sender, EventArgs e)
        {
            speed = 100;
        }
    }
}
