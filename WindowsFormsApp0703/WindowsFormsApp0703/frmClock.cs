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
    public partial class frmClock : Form {
        private float secondAngle = 0f; // 秒针初始角度
        private float minuteAngle = 0f; // 分针初始角度
        private float hourAngle = 0f;   // 时针初始角度
        private Timer timer;

        public frmClock() {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer() {
            timer = new Timer();
            timer.Interval = 1000; // 每隔1000毫秒触发一次
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            // 获取当前时间
            DateTime currentTime = DateTime.Now;

            // 计算秒针、分针和时针的角度
            secondAngle = 6f * currentTime.Second;
            minuteAngle = 6f * (currentTime.Minute + currentTime.Second / 60f);
            hourAngle = 30f * (currentTime.Hour % 12) + currentTime.Minute / 2f;

            // 重新绘制控件
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            // 获取绘图对象
            Graphics g = e.Graphics;

            // 设置旋转中心
            float centerX = panel1.Width / 2f;
            float centerY = panel1.Height / 2f;
            g.TranslateTransform(centerX, centerY);

            // 设置绘制的圆形的边框颜色和线宽
            Pen pen = new Pen(Color.Black, 2);

            // 计算圆形的位置和尺寸
            int radius = Math.Min(panel1.Width, panel1.Height) / 2 - 10;

            // 绘制圆形
            g.DrawEllipse(pen, centerX, centerY, 2 * radius, 2 * radius);

            // 绘制时针
            Pen hourPen = new Pen(Color.Black, 8);
            g.RotateTransform(hourAngle);
            g.DrawLine(hourPen, 0, 0, 0, -50); // 时针长度为50

            // 绘制分针
            Pen minutePen = new Pen(Color.DarkBlue, 5);
            g.RotateTransform(minuteAngle - hourAngle); // 分针相对于时针的角度偏移
            g.DrawLine(minutePen, 0, 0, 0, -80); // 分针长度为80

            // 绘制秒针
            Pen secondPen = new Pen(Color.Red, 2);
            g.RotateTransform(secondAngle - minuteAngle); // 秒针相对于分针的角度偏移
            g.DrawLine(secondPen, 0, 0, 0, -100); // 秒针长度为100

            // 恢复默认的绘图变换
            g.ResetTransform();
        }
    }
}
