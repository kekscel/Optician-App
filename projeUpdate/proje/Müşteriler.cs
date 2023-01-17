using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
//Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;

namespace proje
{
    public partial class Müşteriler : Form
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

        public Müşteriler()
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

        string sph = "SPH";
        string cyl = "CYL";
        string axe = "AXE";

        string sol = "SOL";
        string sağ = "SAĞ";

        string uzak = "UZAK";
        string yakın = "YAKIN";

        string gözdurum;
        string solgöz;
        string sağgöz;

        private void Müşteriler_Load(object sender, EventArgs e)
        {
            müsteriListele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            richTextBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            müsteritc = textBox1.Text;
        }

        public static string müsteritc = "";

        private void sıfırlabuton_Click(object sender, EventArgs e)
        {
            Sıfırla();
        }

        private void listelebuton_Click(object sender, EventArgs e)
        {
            müsteriListele();
        }

        private void güncellebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "UPDATE musteri SET misim=@misim,msoyad=@msoyad,mtelefon=@mtelefon,mgözdurum=@mgözdurum,mnot=@mnot WHERE mtc=@mtc";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("mtc", textBox1.Text);
            komut.Parameters.AddWithValue("misim", textBox2.Text);
            komut.Parameters.AddWithValue("msoyad", textBox3.Text);
            komut.Parameters.AddWithValue("mtelefon", textBox4.Text);
            komut.Parameters.AddWithValue("mgözdurum", gözdurum);
            komut.Parameters.AddWithValue("mnot", richTextBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            Sıfırla();
            müsteriListele();
        }

        private void silbuton_Click(object sender, EventArgs e)
        {
            müşteriödemesil();

            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from musteri where mtc=@mtc";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@mtc", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            Sıfırla();
            müsteriListele();
        }


        private void eklebuton_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "insert into musteri(mtc, misim, msoyad, mtelefon, mgözdurum, mnot) values (@mtc, @misim, @msoyad, @mtelefon, @mgözdurum, @mnot)";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("mtc", textBox1.Text);
            komut.Parameters.AddWithValue("misim", textBox2.Text);
            komut.Parameters.AddWithValue("msoyad", textBox3.Text);
            komut.Parameters.AddWithValue("mtelefon", textBox4.Text);
            komut.Parameters.AddWithValue("mgözdurum", gözdurum);
            komut.Parameters.AddWithValue("mnot", richTextBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            Sıfırla();
            müsteriListele();
        }

        private void çıkışbuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void arabuton_Click(object sender, EventArgs e)
        {
            müsteriAra();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox2.Clear();
        }

        private void ödeme_Click(object sender, EventArgs e)
        {
            Ödeme öd = new Ödeme();
            if (öd.ShowDialog() != DialogResult.OK)
                return;
        }

        void müsteriListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT mtc as 'Kimlik Numarası', misim as 'İsim', msoyad as 'Soyad', mtelefon as 'Telefon', mgözdurum as 'Göz Durum', mnot as 'Not' FROM musteri";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void müsteriAra()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM musteri WHERE mtc LIKE '" + textBox7.Text + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void Sıfırla()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            textBox9.Clear();
            textBox10.Clear();
            richTextBox2.Clear();
            soluzaksph.Clear();
            soluzakcyl.Clear();
            soluzakaxe.Clear();
            solyakınsph.Clear();
            solyakıncyl.Clear();
            solyakınaxe.Clear();
            sağuzaksph.Clear();
            sağuzakcyl.Clear();
            sağuzakaxe.Clear();
            sağyakınsph.Clear();
            sağyakıncyl.Clear();
            sağyakınaxe.Clear();
        }

        void müşteriödemesil()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            string sqlkomut = "delete from ödeme where mtcno=@mtcno";
            komut = new SqlCommand(sqlkomut, baglanti);
            komut.Parameters.AddWithValue("@mtcno", Convert.ToInt64(textBox1.Text));
            komut.ExecuteNonQuery();
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

        private void adsoyadarabuton_Click(object sender, EventArgs e)
        {
            if(textBox9.Text.Length != 0 & textBox10.Text.Length != 0)
            {
                adsoyadara();
            }
            else if(textBox9.Text.Length !=0 & textBox10.Text.Length == 0)
            {
                baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
                baglanti.Open();
                sqlkomut = "SELECT * FROM musteri WHERE misim LIKE '" + textBox9.Text + "'";
                derleyici = new SqlDataAdapter(sqlkomut, baglanti);
                DataTable tablo = new DataTable();
                derleyici.Fill(tablo);
                dataGridView1.DataSource = tablo;
                baglanti.Close();
            }
            else if(textBox9.Text.Length == 0 & textBox10.Text.Length != 0)
            {
                baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
                baglanti.Open();
                sqlkomut = "SELECT * FROM musteri WHERE msoyad LIKE '" + textBox10.Text + "'";
                derleyici = new SqlDataAdapter(sqlkomut, baglanti);
                DataTable tablo = new DataTable();
                derleyici.Fill(tablo);
                dataGridView1.DataSource = tablo;
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Ad ve Soyadı Kontrol Edip Tekrar Deneyiniz.");
            }
        }

        void adsoyadara()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM musteri WHERE misim LIKE '" + textBox9.Text + "' AND msoyad LIKE '" + textBox10.Text + "'";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            DataTable tablo = new DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void soluzaksph_TextChanged(object sender, EventArgs e)
        {
            solyakınsph.Clear();
            solyakıncyl.Clear();
            solyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void soluzakcyl_TextChanged(object sender, EventArgs e)
        {
            solyakınsph.Clear();
            solyakıncyl.Clear();
            solyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void soluzakaxe_TextChanged(object sender, EventArgs e)
        {
            solyakınsph.Clear();
            solyakıncyl.Clear();
            solyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağuzaksph_TextChanged(object sender, EventArgs e)
        {
            sağyakınsph.Clear();
            sağyakıncyl.Clear();
            sağyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağuzakcyl_TextChanged(object sender, EventArgs e)
        {
            sağyakınsph.Clear();
            sağyakıncyl.Clear();
            sağyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağuzakaxe_TextChanged(object sender, EventArgs e)
        {
            sağyakınsph.Clear();
            sağyakıncyl.Clear();
            sağyakınaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void solyakınsph_TextChanged(object sender, EventArgs e)
        {
            soluzaksph.Clear();
            soluzakcyl.Clear();
            soluzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void solyakıncyl_TextChanged(object sender, EventArgs e)
        {
            soluzaksph.Clear();
            soluzakcyl.Clear();
            soluzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void solyakınaxe_TextChanged(object sender, EventArgs e)
        {
            soluzaksph.Clear();
            soluzakcyl.Clear();
            soluzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağyakınsph_TextChanged(object sender, EventArgs e)
        {
            sağuzaksph.Clear();
            sağuzakcyl.Clear();
            sağuzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağyakıncyl_TextChanged(object sender, EventArgs e)
        {
            sağuzaksph.Clear();
            sağuzakcyl.Clear();
            sağuzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        private void sağyakınaxe_TextChanged(object sender, EventArgs e)
        {
            sağuzaksph.Clear();
            sağuzakcyl.Clear();
            sağuzakaxe.Clear();
            karşılaştırma();
            gözdurum = textBox5.Text;
        }

        void solgöztanımlama()
        {
            if((soluzaksph.Text.Length != 0 || soluzakcyl.Text.Length != 0 || soluzakaxe.Text.Length != 0) && (solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length == 0))
            {
                solgöz = "SOL";
                if(soluzaksph.Text.Length != 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + sph + " " + soluzaksph.Text + ")";
                }
                else if (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length != 0 && soluzakaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + cyl + " " + soluzakcyl.Text + ")";
                }
                else if (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + axe + " " + soluzakaxe.Text + ")";
                }
                else if (soluzaksph.Text.Length != 0 && soluzakcyl.Text.Length != 0 && soluzakaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + sph + " " + soluzaksph.Text + ", " + cyl + " " + soluzakcyl.Text + ")";
                }
                else if (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length != 0 && soluzakaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + cyl + " " + soluzakcyl.Text + ", " + axe + " " + soluzakaxe.Text + ")";
                }
                else if (soluzaksph.Text.Length != 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + sph + " " + soluzaksph.Text + ", " + axe + " " + soluzakaxe.Text + ")";
                }
                else if (soluzaksph.Text.Length != 0 && soluzakcyl.Text.Length != 0 && soluzakaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + uzak + "(" + sph + " " + soluzaksph.Text + ", " + cyl + " " + soluzakcyl.Text + ", " + axe + " " + soluzakaxe.Text + ")";
                }
                else if (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length == 0)
                {
                    textBox5.Clear();
                }
                else
                {
                    textBox5.Clear();
                }

            }
            else if ((solyakınsph.Text.Length != 0 || solyakıncyl.Text.Length != 0 || solyakınaxe.Text.Length != 0) && (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length == 0))
            {
                solgöz = "SOL";
                if (solyakınsph.Text.Length != 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + sph + " " + solyakınsph.Text + ")";
                }
                else if (solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length != 0 && solyakınaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + cyl + " " + solyakıncyl.Text + ")";
                }
                else if (solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + axe + " " + solyakınaxe.Text + ")";
                }
                else if (solyakınsph.Text.Length != 0 && solyakıncyl.Text.Length != 0 && solyakınaxe.Text.Length == 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + sph + " " + solyakınsph.Text + ", " + cyl + " " + solyakıncyl.Text + ")";
                }
                else if (solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length != 0 && solyakınaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + cyl + " " + solyakıncyl.Text + ", " + axe + " " + solyakınaxe.Text + ")";
                }
                else if (solyakınsph.Text.Length != 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + sph + " " + solyakınsph.Text + ", " + axe + " " + solyakınaxe.Text + ")";
                }
                else if (solyakınsph.Text.Length != 0 && solyakıncyl.Text.Length != 0 && solyakınaxe.Text.Length != 0)
                {
                    solgöz = solgöz + " " + yakın + "(" + sph + " " + solyakınsph.Text + ", " + cyl + " " + solyakıncyl.Text + ", " + axe + " " + solyakınaxe.Text + ")";
                }
                else if (solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length == 0)
                {
                    textBox5.Clear();
                }
                else
                {
                    textBox5.Clear();
                }
            }
            else
            {
                textBox5.Clear();
            }
        }

        void sağgöztanımlama()
        {
            if ((sağuzaksph.Text.Length != 0 || sağuzakcyl.Text.Length != 0 || sağuzakaxe.Text.Length != 0) && (sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length == 0))
            {
                sağgöz = "SAĞ";
                if (sağuzaksph.Text.Length != 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + sph + " " + sağuzaksph.Text + ")";
                }
                else if (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length != 0 && sağuzakaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + cyl + " " + sağuzakcyl.Text + ")";
                }
                else if (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + axe + " " + sağuzakaxe.Text + ")";
                }
                else if (sağuzaksph.Text.Length != 0 && sağuzakcyl.Text.Length != 0 && sağuzakaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + sph + " " + sağuzaksph.Text + ", " + cyl + " " + sağuzakcyl.Text + ")";
                }
                else if (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length != 0 && sağuzakaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + cyl + " " + sağuzakcyl.Text + ", " + axe + " " + sağuzakaxe.Text + ")";
                }
                else if (sağuzaksph.Text.Length != 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + sph + " " + sağuzaksph.Text + ", " + axe + " " + sağuzakaxe.Text + ")";
                }
                else if (sağuzaksph.Text.Length != 0 && sağuzakcyl.Text.Length != 0 && sağuzakaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + uzak + "(" + sph + " " + sağuzaksph.Text + ", " + cyl + " " + sağuzakcyl.Text + ", " + axe + " " + sağuzakaxe.Text + ")";
                }
                else if (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length == 0)
                {
                    textBox5.Clear();
                }
                else
                {
                    textBox5.Clear();
                }

            }
            else if ((sağyakınsph.Text.Length != 0 || sağyakıncyl.Text.Length != 0 || sağyakınaxe.Text.Length != 0) && (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length == 0))
            {
                sağgöz = "SAĞ";
                if (sağyakınsph.Text.Length != 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + sph + " " + sağyakınsph.Text + ")";
                }
                else if (sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length != 0 && sağyakınaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + cyl + " " + sağyakıncyl.Text + ")";
                }
                else if (sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + axe + " " + sağyakınaxe.Text + ")";
                }
                else if (sağyakınsph.Text.Length != 0 && sağyakıncyl.Text.Length != 0 && sağyakınaxe.Text.Length == 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + sph + " " + sağyakınsph.Text + ", " + cyl + " " + sağyakıncyl.Text + ")";
                }
                else if (sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length != 0 && sağyakınaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + cyl + " " + sağyakıncyl.Text + ", " + axe + " " + sağyakınaxe.Text + ")";
                }
                else if (sağyakınsph.Text.Length != 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + sph + " " + sağyakınsph.Text + ", " + axe + " " + sağyakınaxe.Text + ")";
                }
                else if (sağyakınsph.Text.Length != 0 && sağyakıncyl.Text.Length != 0 && sağyakınaxe.Text.Length != 0)
                {
                    sağgöz = sağgöz + " " + yakın + "(" + sph + " " + sağyakınsph.Text + ", " + cyl + " " + sağyakıncyl.Text + ", " + axe + " " + sağyakınaxe.Text + ")";
                }
                else if (sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length == 0)
                {
                    textBox5.Clear();
                }
                else
                {
                    textBox5.Clear();
                }
            }
            else
            {
                textBox5.Clear();
            }
        }

        void solyazdır()
        {
            if((soluzaksph.Text.Length != 0 || soluzakcyl.Text.Length != 0 || soluzakaxe.Text.Length != 0 || solyakınsph.Text.Length != 0 || solyakıncyl.Text.Length != 0 || solyakınaxe.Text.Length != 0) && (sağuzaksph.Text.Length == 0 && sağuzakcyl.Text.Length == 0 && sağuzakaxe.Text.Length == 0 && sağyakınsph.Text.Length == 0 && sağyakıncyl.Text.Length == 0 && sağyakınaxe.Text.Length == 0))
            {
                solgöztanımlama();
                textBox5.Text = solgöz;
            }
        }

        void sağyazdır()
        {
            if ((sağuzaksph.Text.Length != 0 || sağuzakcyl.Text.Length != 0 || sağuzakaxe.Text.Length != 0 || sağyakınsph.Text.Length != 0 || sağyakıncyl.Text.Length != 0 || sağyakınaxe.Text.Length != 0) && (soluzaksph.Text.Length == 0 && soluzakcyl.Text.Length == 0 && soluzakaxe.Text.Length == 0 && solyakınsph.Text.Length == 0 && solyakıncyl.Text.Length == 0 && solyakınaxe.Text.Length == 0))
            {
                sağgöztanımlama();
                textBox5.Text = sağgöz;
            }
        }

        void ikiliyazdır()
        {
            if((soluzaksph.Text.Length != 0 || soluzakcyl.Text.Length != 0 || soluzakaxe.Text.Length != 0 || solyakınsph.Text.Length != 0 || solyakıncyl.Text.Length != 0 || solyakınaxe.Text.Length != 0) && (sağuzaksph.Text.Length != 0 || sağuzakcyl.Text.Length != 0 || sağuzakaxe.Text.Length != 0 || sağyakınsph.Text.Length != 0 || sağyakıncyl.Text.Length != 0 || sağyakınaxe.Text.Length != 0))
            {
                solgöztanımlama();
                sağgöztanımlama();
                textBox5.Text = solgöz + " ve " + sağgöz;
            }
        }

        void karşılaştırma()
        {
            solgöztanımlama();
            sağgöztanımlama();
            solyazdır();
            sağyazdır();
            ikiliyazdır();
        }
    }
}
