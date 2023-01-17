using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace proje
{

    public partial class Firmalar : Form
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


        public Firmalar()
        {
            InitializeComponent();
            //rounded corners 
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        SqlConnection baglanti;
        SqlDataAdapter derleyici;
        SqlCommand komut;
        public string sqlkomut;

        private void Firmalar_Load(object sender, EventArgs e)
        {
            firmalistele();
        }

        string firmatür;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            firmatür = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if(firmatür == "Cam")
            {
                radioButton1.Checked = true;
            }
            else if (firmatür == "Çerçeve")
            {
                radioButton2.Checked = true;
            }
            else if (firmatür == "Hepsi")
            {
                radioButton3.Checked = true;
            }
        }

        private void eklebuton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                firmatür = "Cam";
            }
            else if (radioButton2.Checked == true)
            {
                firmatür = "Çerçeve";
            }
            else if (radioButton3.Checked == true)
            {
                firmatür = "Hepsi";
            }

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "insert into firma(firmano, firmaad, firmatür, firmatelefon) values (@firmano, @firmaad, @firmatür, @firmatelefon)";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@firmano", textBox1.Text);
            komut.Parameters.AddWithValue("@firmaad", textBox2.Text);
            komut.Parameters.AddWithValue("@firmatür", firmatür);
            komut.Parameters.AddWithValue("@firmatelefon", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            firmatemizle();
            firmalistele();
        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            camlarısil();
            çerçevelerisil();
            firmasil();

            firmatemizle();
            firmalistele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                firmatür = "Cam";
            }
            else if (radioButton2.Checked == true)
            {
                firmatür = "Çerçeve";
            }
            else if (radioButton3.Checked == true)
            {
                firmatür = "Hepsi";
            }

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE firma SET firmaad=@firmaad, firmatür=@firmatür, firmatelefon=@firmatelefon WHERE firmano=@firmano";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("firmano", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("firmaad", textBox2.Text);
            komut.Parameters.AddWithValue("firmatür", firmatür);
            komut.Parameters.AddWithValue("firmatelefon", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            firmatemizle();
            firmalistele();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            firmalistele();
        }

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            firmatemizle();
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        void firmatemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox4.Clear();
        }

        void firmalistele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT firmano as 'Firma Numarası', firmaad as 'Firma Adı', firmatür as 'Tür', firmatelefon as 'Telefon' FROM firma";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void firmasil()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from firma where firmano=@firmano";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@firmano", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        void camlarısil()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from camlar where camfirma=@camfirma";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@camfirma", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        void çerçevelerisil()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from cerceveler where cercevefirma=@cercevefirma";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@cercevefirma", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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
