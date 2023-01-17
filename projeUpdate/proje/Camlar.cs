using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Camlar : Form
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

        public Camlar()
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

        private void Camlar_Load(object sender, EventArgs e)
        {
            camListele();

            camfirmalariekle();
            hepsifirmalariekle();
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
            string sqlkomut = "insert into camlar(camtürno, camfirma, camtürad, camtürözellik, camadet) values " +
                "('" + textBox1.Text + "', '" + comboBox2.SelectedItem + "', '" + textBox2.Text + "', '" + richTextBox1.Text + "', '" + textBox3.Text + "')";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            camTemizle();
            camListele();
        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from camlar where camtürno=@camtürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@camtürno", Convert.ToInt32(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();

            camTemizle();
            camListele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE camlar SET camfirma=@camfirma, camtürad=@camtürad, camtürözellik=@camtürözellik, camadet=@camadet WHERE camtürno=@camtürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("camtürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("camfirma", comboBox2.Text);
            komut.Parameters.AddWithValue("camtürad", textBox2.Text);
            komut.Parameters.AddWithValue("camtürözellik", richTextBox1.Text);
            komut.Parameters.AddWithValue("camadet", textBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            camTemizle();
            camListele();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            camListele();
        }

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            camTemizle();
            camListele();
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
            string sqlkomut = "UPDATE camlar SET camadet=@camadet WHERE camtürno=@camtürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("camtürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("camadet", yenistok);
            komut.ExecuteNonQuery();
            baglanti.Close();

            camTemizle();
            camListele();
        }

        private void adetcıkarbuton_Click(object sender, EventArgs e)
        {
            eskistok = Convert.ToInt32(textBox3.Text);
            eklenecekstok = Convert.ToInt32(textBox4.Text);
            yenistok = eskistok - eklenecekstok;

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE camlar SET camadet=@camadet WHERE camtürno=@camtürno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("camtürno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("camadet", yenistok);
            komut.ExecuteNonQuery();
            baglanti.Close();

            camTemizle();
            camListele();
        }

        void camTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        void camListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT camtürno as 'Tür Numarası', camfirma as 'Firma Numarası', camtürad as 'Ad', camtürözellik as 'Özellik', camadet as 'Adet' FROM camlar";
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

        string camfirmalari = "Cam";
        string hepsidahilfirma = "Hepsi";
        void camfirmalariekle()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            string sqlkomut = "SELECT * FROM firma WHERE firmatür LIKE '" + camfirmalari + "' ";
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

        void hepsifirmalariekle()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            string sqlkomut = "SELECT * FROM firma WHERE firmatür LIKE '" + hepsidahilfirma + "' ";
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
            firmalistele();
            firmacamlarılistele();
        }

        string firmanumarası;
        void firmalistele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT firmano as 'Firma Numarası', firmaad as 'Firma Adı', firmatür as 'Tür', firmatelefon as 'Telefon' FROM firma WHERE firmaad LIKE '" + comboBox1.Text + "' ";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();

            firmanumarası = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        void firmacamlarılistele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT camtürno as 'Tür Numarası', camfirma as 'Firma Numarası', camtürad as 'Ad', camtürözellik as 'Özellik', camadet as 'Adet' FROM camlar WHERE camfirma LIKE '" + firmanumarası + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
