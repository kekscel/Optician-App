using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Giriş : Form
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

        public Giriş()
        {
            InitializeComponent();
            this.Size = new Size(500, 400);
            //rounded corners
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        string kullanıcıad = "eyesoptik";
        string sifre = "admin";
        private void girişbuton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == kullanıcıad && textBox2.Text == sifre)
            {
                Menü m = new Menü();
                if (m.ShowDialog() != DialogResult.OK)
                    return;
            }
            else if (textBox1.Text != kullanıcıad && textBox2.Text == sifre)
            {
                MessageBox.Show("Kullanıcı Adını Kontrol Ediniz");
            }
            else if (textBox1.Text == kullanıcıad && textBox2.Text != sifre)
            {
                MessageBox.Show("Şifreyi Kontrol Ediniz");
            }
            else
                MessageBox.Show("Kullanıcı Adı ve Şifreyi Kontrol Ediniz");
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UnHideButon_Click_1(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                HideButon.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        private void HideButon_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                UnHideButon.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Kullanıcı Adınızı Girin")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.FromArgb(231, 246, 242);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Kullanıcı Adınızı Girin";
                textBox1.ForeColor = Color.LightGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Veritabanı v = new Veritabanı();
            if (v.ShowDialog() != DialogResult.OK)
                return;
        }
    }
}
