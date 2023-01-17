using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Muhasebe : Form
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

        public Muhasebe()
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

        private void Muhasebe_Load(object sender, EventArgs e)
        {
            muhasebeListele();
        }
        string gelirgider;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            gelirgider = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            if (gelirgider == "Gelir")
            {
                radioButton1.Checked = true;
            }
            else if (gelirgider == "Gider")
            {
                radioButton2.Checked = true;
            }
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            muhasebeSıfırla();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            muhasebeListele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                gelirgider = "Gelir";
            }
            else if (radioButton2.Checked == true)
            {
                gelirgider = "Gider";
            }

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE muhasebe SET islemyapanisim=@islemyapanisim,islemaciklama=@islemaciklama,islemtür=@islemtür,islemtutar=@islemtutar,islemtarih=@islemtarih WHERE islemno=@islemno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("islemno", textBox1.Text);
            komut.Parameters.AddWithValue("islemyapanisim", textBox2.Text);
            komut.Parameters.AddWithValue("islemaciklama", richTextBox1.Text);
            komut.Parameters.AddWithValue("islemtür", gelirgider);
            komut.Parameters.AddWithValue("islemtutar", textBox3.Text);
            komut.Parameters.AddWithValue("islemtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            baglanti.Close();

            muhasebeSıfırla();
            muhasebeListele();
        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from muhasebe where islemno=@islemno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@islemno", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            muhasebeSıfırla();
            muhasebeListele();
        }

        private void eklebuton_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                gelirgider = "Gelir";
            }
            else if (radioButton2.Checked == true)
            {
                gelirgider = "Gider";
            }

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "insert into muhasebe(islemno,islemyapanisim,islemaciklama,islemtür,islemtutar,islemtarih) values " +
                "(@islemno,@islemyapanisim,@islemaciklama,@islemtür,@islemtutar,@islemtarih)";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@islemno", textBox1.Text);
            komut.Parameters.AddWithValue("@islemyapanisim", textBox2.Text);
            komut.Parameters.AddWithValue("@islemaciklama", richTextBox1.Text);
            komut.Parameters.AddWithValue("@islemtür", gelirgider);
            komut.Parameters.AddWithValue("@islemtutar", textBox3.Text);
            komut.Parameters.AddWithValue("@islemtarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            baglanti.Close();

            muhasebeSıfırla();
            muhasebeListele();
        }

        string gelir = "Gelir";
        string gider = "Gider";
        int pozitiftoplam;
        int negatiftoplam;
        int i = 1;
        int a;
        int b;
        private void günlükbuton_Click(object sender, EventArgs e)
        {
            pozitifhesapla();
            negatifhesapla();
            muhasebeListele();
            toplamyazdır();
            muhasebeSıfırla();
        }
        string date1;
        string date2;
        private void button1_Click(object sender, EventArgs e)
        {
            date1 = dateTimePicker2.Value.Year + " - " + dateTimePicker2.Value.Month + " - " + dateTimePicker2.Value.Day + " " + dateTimePicker2.Value.Hour + " : " + dateTimePicker2.Value.Minute + " : " + dateTimePicker2.Value.Second;
            date2 = dateTimePicker3.Value.Year + " - " + dateTimePicker3.Value.Month + " - " + dateTimePicker3.Value.Day + " " + dateTimePicker3.Value.Hour + " : " + dateTimePicker3.Value.Minute + " : " + dateTimePicker3.Value.Second;

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT islemno as 'İşlem Numarası', islemyapanisim as 'Ad', islemaciklama as 'Açıklama', islemtür as 'Tür', islemtutar as 'Tutar', islemtarih as 'Tarih' FROM muhasebe WHERE islemtarih BETWEEN '" + date1 + "' AND '" + date2 + "' ";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        void muhasebeListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT islemno as 'İşlem Numarası', islemyapanisim as 'Ad', islemaciklama as 'Açıklama', islemtür as 'Tür', islemtutar as 'Tutar', islemtarih as 'Tarih' FROM muhasebe";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void muhasebeSıfırla()
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            textBox3.Clear();
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        void pozitifhesapla()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT islemno as 'İşlem Numarası', islemyapanisim as 'Ad', islemaciklama as 'Açıklama', islemtür as 'Tür', islemtutar as 'Tutar', islemtarih as 'Tarih' FROM muhasebe WHERE islemtür LIKE '" + gelir + "' AND islemtarih BETWEEN '" + date1 + "' AND '" + date2 + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

            int pozitiftoplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                pozitiftoplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            a = pozitiftoplam;
            MessageBox.Show("Gelir Toplamı =" + pozitiftoplam.ToString(), "Bakiye");
            pozitiftoplam = Convert.ToInt32(pozitiftoplam);

        }

        void negatifhesapla()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT islemno as 'İşlem Numarası', islemyapanisim as 'Ad', islemaciklama as 'Açıklama', islemtür as 'Tür', islemtutar as 'Tutar', islemtarih as 'Tarih' FROM muhasebe WHERE islemtür LIKE '" + gider + "' AND islemtarih BETWEEN '" + date1 + "' AND '" + date2 + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

            int negatiftoplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                negatiftoplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            b = negatiftoplam;
            MessageBox.Show("Gider Toplamı =" + negatiftoplam.ToString(), "Bakiye");
            negatiftoplam = Convert.ToInt32(negatiftoplam);
        }

        void toplamyazdır()
        {
            int c = a - b;
            MessageBox.Show("Mevcut Bakiye =" + c.ToString(), "Bakiye");
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
