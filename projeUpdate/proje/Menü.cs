using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Menü : Form
    {
        //rounded corners start
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );
        //rounded corners end

        public Menü()
        {
            InitializeComponent();
            //rounded corners 
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void müsteributon_Click(object sender, EventArgs e)
        {
            Müşteriler müs = new Müşteriler();
            if (müs.ShowDialog() != DialogResult.OK)
                return;
        }

        private void firmabuton_Click(object sender, EventArgs e)
        {
            Firmalar f = new Firmalar();
            if (f.ShowDialog() != DialogResult.OK)
                return;
        }

        private void cambuton_Click(object sender, EventArgs e)
        {
            Camlar cam = new Camlar();
            if (cam.ShowDialog() != DialogResult.OK)
                return;
        }

        private void çerçevebuton_Click(object sender, EventArgs e)
        {
            Çerçeveler cer = new Çerçeveler();
            if (cer.ShowDialog() != DialogResult.OK)
                return;
        }

        private void muhasebebuton_Click(object sender, EventArgs e)
        {
            Muhasebe muh = new Muhasebe();
            if (muh.ShowDialog() != DialogResult.OK)
                return;
        }

        private void yardımbuton_Click(object sender, EventArgs e)
        {
            Yardım y = new Yardım();
            if (y.ShowDialog() != DialogResult.OK)
                return;
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; ;
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
            }
        }
    }
}
