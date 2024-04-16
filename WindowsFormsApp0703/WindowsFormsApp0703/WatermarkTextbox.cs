using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp0703 {
    internal class WatermarkTextbox : TextBox {
        private const int EM_SETCUEBANNER = 0x1501;

        private string watermarkText;
        public string WatermarkText {
            get {
                return watermarkText;
            }
            set {
                watermarkText = value;
                UpdateWatermark();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private void UpdateWatermark() {
            if (IsHandleCreated && watermarkText != null) {
                SendMessage(Handle, EM_SETCUEBANNER, 0, watermarkText);
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            UpdateWatermark();
        }
    }
}
