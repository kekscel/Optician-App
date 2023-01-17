using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Çerçeveler : Form
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

        public Çerçeveler()
        {
            InitializeComponent();
            //rounded corners 
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        SqlConnection baglanti;
        SqlDataAdapter derleyici;
        SqlDataReader okuyucu;
        SqlCommand komut;
        public string sqlkomut;

        private void Çerçeveler_Load(object sender, EventArgs e)
        {
            çerçeveListele();

            cercevefirmalariekle();
            hepsicercevefirmalariekle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void eklebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "insert into cerceveler(certürno, cercevefirma, cercevead, cerceveözellik, cerceveadet) values " +
                "('" + textBox1.Text + "', '" + comboBox2.SelectedItem + "', '" + textBox2.Text + "', '" + richTextBox1.Text + "', '" + textBox3.Text + "')";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            çerçeveTemizle();
            çerçeveListele();
        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from cerceveler where certürno=@certürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@certürno", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();

            çerçeveTemizle();
            çerçeveListele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE cerceveler SET cercevefirma=@cercevefirma, cercevead=@cercevead, cerceveözellik=@cerceveözellik, cerceveadet=@cerceveadet WHERE certürno=@certürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("certürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("cercevefirma", comboBox2.Text);
            komut.Parameters.AddWithValue("cercevead", textBox2.Text);
            komut.Parameters.AddWithValue("cerceveözellik", richTextBox1.Text);
            komut.Parameters.AddWithValue("cerceveadet", textBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            çerçeveTemizle();
            çerçeveListele();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            çerçeveListele();
        }

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            çerçeveTemizle();
            çerçeveListele();
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        int eskistok;
        int eklenecekstok;
        int yenistok;
        private void adeteklebuton_Click(object sender, EventArgs e)
        {
            eskistok = Convert.ToInt32(textBox3.Text);
            eklenecekstok = Convert.ToInt32(textBox4.Text);
            yenistok = eskistok + eklenecekstok;

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE cerceveler SET cerceveadet=@cerceveadet WHERE certürno=@certürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("certürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("cerceveadet", yenistok);
            komut.ExecuteNonQuery();
            baglanti.Close();

            çerçeveTemizle();
            çerçeveListele();
        }

        private void adetcıkarbuton_Click(object sender, EventArgs e)
        {
            eskistok = Convert.ToInt32(textBox3.Text);
            eklenecekstok = Convert.ToInt32(textBox4.Text);
            yenistok = eskistok - eklenecekstok;

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE cerceveler SET cerceveadet=@cerceveadet WHERE certürno=@certürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("certürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("cerceveadet", yenistok);
            komut.ExecuteNonQuery();
            baglanti.Close();

            çerçeveTemizle();
            çerçeveListele();
        }

        void çerçeveTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        void çerçeveListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT certürno as 'Tür Numarası', cercevefirma as 'Firma Numarası', cercevead as 'Ad', cerceveözellik as 'Özellik', cerceveadet as 'Adet' FROM cerceveler";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
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

        string cercevefirmalar = "Çerçeve";
        string hepsidahilcerfirma = "Hepsi";
        void cercevefirmalariekle()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            string sqlkomut = "SELECT * FROM firma WHERE firmatür LIKE '" + cercevefirmalar + "' ";
            komut = new SqlCommand(sqlkomut, baglanti);
            baglanti.Open();
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu[1].ToString());
                comboBox2.Items.Add(okuyucu[0].ToString());
            }
            baglanti.Close();
        }

        void hepsicercevefirmalariekle()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            string sqlkomut = "SELECT * FROM firma WHERE firmatür LIKE '" + hepsidahilcerfirma + "' ";
            komut = new SqlCommand(sqlkomut, baglanti);
            baglanti.Open();
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                comboBox1.Items.Add(okuyucu[1].ToString());
                comboBox2.Items.Add(okuyucu[0].ToString());
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cerfirmalistele();
            firmacercevelerilistele();
        }

        string firmanumara;
        void cerfirmalistele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT firmano as 'Firma Numarası', firmaad as 'Firma Adı', firmatür as 'Tür', firmatelefon as 'Telefon' FROM firma WHERE firmaad LIKE '" + comboBox1.Text + "' ";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();

            firmanumara = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        void firmacercevelerilistele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT certürno as 'Tür Numarası', cercevefirma as 'Firma Numarası', cercevead as 'Ad', cerceveözellik as 'Özellik', cerceveadet as 'Adet' FROM cerceveler WHERE cercevefirma LIKE '" + firmanumara + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
