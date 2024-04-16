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

namespace WindowsFormsApp0703 {
    public partial class frmPicture : Form {
        string[] pics;
        int current = 0;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        Timer timer = new Timer();
        bool autoplayEnabled = false;
        int timeintv;

        public frmPicture() {
            InitializeComponent();
            this.Width = 800;
            this.Height = 600;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            textBox1.KeyPress += TextBox1_KeyPress;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Picture File ( *.jpg, *.gif, *.png, *.jpeg )|*.jpg;*.gif;*.png;*.jpeg";
            openFileDialog1.RestoreDirectory = true;
            progressBar1.Style = ProgressBarStyle.Continuous;
            timeintv = 2000;
            timer.Interval = timeintv;
            timer.Tick += Timer_Tick;
            loadfile.Location = new Point(loadfile.Location.X, this.ClientSize.Height - loadfile.Height - 10);
            label1.Location = new Point(this.ClientSize.Width - label1.Width - 250, this.ClientSize.Height - label1.Height - 35);
        }

        private void loadfile_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                pics = openFileDialog1.FileNames;
            }
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }
            progressBar1.Maximum = pics.Length;
            label1.Text = "Total: " + pics.Length;
            ShowCurrentImage();
        }

        private void picPrev_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }

            if (current < 1) {
                MessageBox.Show("This is already the FIRST picture!", "Notice", MessageBoxButtons.OK);
            }
            else {
                current--;
            }
            ShowCurrentImage();
        }

        private void picNext_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }

            if (current >= pics.Length - 1) {
                DialogResult result = MessageBox.Show("This is already the LAST picture!\nDo you want to jump to the first picture?", "Notice", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    current = 0;
                }
            }
            else {
                current++;
            }
            ShowCurrentImage();
        }

        private void ShowCurrentImage() {
            if (pics != null && pics.Length > 0) {
                string imagePath = pics[current];
                if (File.Exists(imagePath)) {
                    using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read)) {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                progressBar1.Value = current + 1;
                label1.Text = "Total: " + pics.Length + " Current: " + (current + 1);
            }
        }

        private void jumpto_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }

            int crt = int.Parse(textBox1.Text);
            current = crt - 1;
            if (current >= pics.Length || current < 0) {
                MessageBox.Show("Please input a valid number between 1 and " + pics.Length, "Warning!", MessageBoxButtons.OK);
            }
            else {
                ShowCurrentImage();
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void autoplay_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }
            if (!autoplayEnabled) {
                this.BackColor = Color.FromArgb(100, 100, 100);
                this.WindowState = FormWindowState.Maximized;
                label1.ForeColor = Color.White;
                autoplay.Location = new Point(10, this.ClientSize.Height - autoplay.Height - 10);
                picPrev.Visible = false;
                picNext.Visible = false;
                jumpto.Visible = false;
                textBox1.Visible = false;
                spdp.Location = new Point(autoplay.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
                spdm.Location = new Point(autoplay.Width + 10, this.ClientSize.Height - spdp.Height - 10);
                timenorm.Location = new Point(autoplay.Width + spdm.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
                autoplayEnabled = true;
                autoplay.Text = "Stop Autoplay";
                timer.Start();
            }
            else {
                this.BackColor = Color.White;
                this.WindowState = FormWindowState.Normal;
                label1.ForeColor = Color.Black;
                picPrev.Visible = true;
                picNext.Visible = true;
                jumpto.Visible = true;
                textBox1.Visible = true;
                autoplay.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + 10, this.ClientSize.Height - autoplay.Height - 10);
                spdp.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
                spdm.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + 10, this.ClientSize.Height - spdp.Height - 10);
                timenorm.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + spdm.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
                autoplayEnabled = false;
                autoplay.Text = "Start Autoplay";
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e) {
            current++;
            if (current >= pics.Length) {
                current = 0;
            }
            ShowCurrentImage();
        }

        private void spdp_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }
            if (timeintv >= 251)
                timeintv -= 250;
            timer.Interval = timeintv;
        }

        private void spdm_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }
            if (timeintv <= 10000)
                timeintv += 250;
            timer.Interval = timeintv;
        }

        private void timenorm_Click(object sender, EventArgs e) {
            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }
            timeintv = 2000;
            timer.Interval = timeintv;
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e) {
            int leftWidth = pictureBox1.Width / 2;
            int rightWidth = pictureBox1.Width - leftWidth;

            if (pics == null || pics.Length == 0) {
                MessageBox.Show("Please load images first!", "Notice", MessageBoxButtons.OK);
                return;
            }

            if (e.X < leftWidth) {
                if (current < 1) {
                    MessageBox.Show("This is already the FIRST picture!", "Notice", MessageBoxButtons.OK);
                }
                else {
                    current--;
                }
                ShowCurrentImage();
            }
            else if (e.X > rightWidth) {
                if (current >= pics.Length - 1) {
                    DialogResult result = MessageBox.Show("This is already the LAST picture!\nDo you want to jump to the first picture?", "Notice", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) {
                        current = 0;
                    }
                }
                else {
                    current++;
                }
                ShowCurrentImage();
            }
        }

        private void Picture_SizeChanged(object sender, EventArgs e) {
            loadfile.Location = new Point(this.ClientSize.Width - loadfile.Width - 10, this.ClientSize.Height - loadfile.Height - 10);
            progressBar1.Location = new Point(this.ClientSize.Width - progressBar1.Width - 70, this.ClientSize.Height - progressBar1.Height - 10);
            label1.Location = new Point(this.ClientSize.Width - label1.Width - 70, this.ClientSize.Height - label1.Height - 35);
            picPrev.Location = new Point(10, this.ClientSize.Height - picPrev.Height - 10);
            picNext.Location = new Point(picPrev.Width + 10, this.ClientSize.Height - picPrev.Height - 10);
            textBox1.Location = new Point(picNext.Width + picPrev.Width + 10, this.ClientSize.Height - picNext.Height - 10);
            jumpto.Location = new Point(picNext.Width + picPrev.Width + 10, this.ClientSize.Height - textBox1.Height - 10);
            autoplay.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + 10, this.ClientSize.Height - autoplay.Height - 10);
            spdp.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
            spdm.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + 10, this.ClientSize.Height - spdp.Height - 10);
            timenorm.Location = new Point(picNext.Width + picPrev.Width + jumpto.Width + autoplay.Width + spdm.Width + 10, this.ClientSize.Height - spdp.Height - spdm.Height - 15);
            pictureBox1.Size = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - timenorm.Height - 20);
        }
    }
}
