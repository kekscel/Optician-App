using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace proje
{
    public partial class Ödeme : Form
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

        public Ödeme()
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

        private void Ödeme_Load(object sender, EventArgs e)
        {
            textBox1.Text = Müşteriler.müsteritc;

            KimlikNoSakla();
            ödemeListele();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            ödemeSıfırla();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            ödemeListele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE ödeme SET fiyat=@fiyat,alınan=@alınan,kalan=@kalan,ödemetarih=@ödemetarih WHERE mtcno=@mtcno AND fiyat LIKE '" + textBox2.Text + "'";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("mtcno", Convert.ToInt64(textBox1.Text));
            komut.Parameters.AddWithValue("fiyat", textBox2.Text);
            komut.Parameters.AddWithValue("alınan", textBox3.Text);
            komut.Parameters.AddWithValue("kalan", textBox4.Text);
            komut.Parameters.AddWithValue("ödemetarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            baglanti.Close();

            ödemeSıfırla();
            ödemeListele();
        }

        private void eklebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "insert into ödeme(mtcno, fiyat, alınan, kalan, ödemetarih) values (@mtcno,@fiyat,@alınan,@kalan,@ödemetarih)";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@mtcno", textBox1.Text);
            komut.Parameters.AddWithValue("@fiyat", textBox2.Text);
            komut.Parameters.AddWithValue("@alınan", textBox3.Text);
            komut.Parameters.AddWithValue("@kalan", textBox4.Text);
            komut.Parameters.AddWithValue("@ödemetarih", dateTimePicker1.Value);
            komut.ExecuteNonQuery();
            baglanti.Close();

            ödemeSıfırla();
            ödemeListele();
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        int alınan;
        int kalan;
        int alınanödeme;
        int yenialınan;
        int yenikalan;
        private void button1_Click(object sender, EventArgs e)
        {
            alınan = Convert.ToInt32(textBox3.Text);
            kalan = Convert.ToInt32(textBox4.Text);
            alınanödeme = Convert.ToInt32(textBox5.Text);
            yenialınan = alınan + alınanödeme;
            yenikalan = kalan - alınanödeme;

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE ödeme SET alınan=@alınan,kalan=@kalan WHERE mtcno=@mtcno AND fiyat LIKE '" + textBox2.Text + "'";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("mtcno", Convert.ToInt64(textBox1.Text));
            komut.Parameters.AddWithValue("alınan", yenialınan);
            komut.Parameters.AddWithValue("kalan", yenikalan);
            komut.ExecuteNonQuery();
            baglanti.Close();

            ödemeSıfırla();
            ödemeListele();
        }

        void KimlikNoSakla()
        {
            string müsteritc;
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            string sqlkomut = "SELECT * FROM musteri WHERE mtc LIKE '" + textBox1.Text + "'";
            komut = new SqlCommand(sqlkomut, baglanti);
            baglanti.Open();
            okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                müsteritc = okuyucu["mtc"].ToString();
                textBox1.Text = müsteritc;
            }
            baglanti.Close();
        }

        void ödemeListele()//reçetelistele fonksiyonu
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT mtcno as 'Kimlik Numarası', fiyat as 'Fiyat', alınan as 'Alınan', kalan as 'Kalan', ödemetarih as 'Tarih' FROM  ödeme WHERE mtcno LIKE '" + textBox1.Text + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void ödemeSıfırla()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.ResetText();
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
