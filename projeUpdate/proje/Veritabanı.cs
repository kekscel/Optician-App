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
using System.IO;
using ExcelDataReader;
using Z.Dapper.Plus;
using ClosedXML.Excel;

namespace proje
{
    public partial class Veritabanı : Form
    {
        public Veritabanı()
        {
            InitializeComponent();
        }

        SqlDataAdapter derleyici;
        SqlCommand komut;
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
        public string sqlkomut;

        private void Veritabanı_Load(object sender, EventArgs e)
        {
            exportcombobox.Items.Add("Firma");
            exportcombobox.Items.Add("Müşteri");
            exportcombobox.Items.Add("Camlar");
            exportcombobox.Items.Add("Çerçeveler");
            exportcombobox.Items.Add("Muhasebe");
            exportcombobox.Items.Add("Ödeme");

            importcombobox.Items.Add("Firma");
            importcombobox.Items.Add("Müşteri");
            importcombobox.Items.Add("Camlar");
            importcombobox.Items.Add("Çerçeveler");
            importcombobox.Items.Add("Muhasebe");
            importcombobox.Items.Add("Ödeme");
        }

        private void closebuton_Click(object sender, EventArgs e)
        {
            Close();
        }

        DataTableCollection tableCollection;

        private void button1_Click(object sender, EventArgs e)
        {
            if(exportcombobox.SelectedItem.ToString() == "Muhasebe")
            {
                muhasebeEXPORT();
            }
            else if(exportcombobox.SelectedItem.ToString() == "Ödeme")
            {
                ödemeEXPORT();
            }
            else
            {
                tabloexport();
            }
        }

        private void exportcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(exportcombobox.SelectedItem.ToString() == "Firma")
            {
                FirmaListele();
            }
            else if(exportcombobox.SelectedItem.ToString() == "Müşteri")
            {
                MüşteriListele();
            }
            else if (exportcombobox.SelectedItem.ToString() == "Camlar")
            {
                CamlarıListele();
            }
            else if (exportcombobox.SelectedItem.ToString() == "Çerçeveler")
            {
                ÇerçeveListele();
            }
            else if (exportcombobox.SelectedItem.ToString() == "Muhasebe")
            {
                MuhasebeListele();
            }
            else if (exportcombobox.SelectedItem.ToString() == "Ödeme")
            {
                ÖdemeListele();
            }
            else
            {
                Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (importcombobox.SelectedItem.ToString() == "Firma")
            {
                firmaImport1();
                firmaImport2();
            }
            else if (importcombobox.SelectedItem.ToString() == "Müşteri")
            {
                musteriImport1();
                musteriImport2();
            }
            else if (importcombobox.SelectedItem.ToString() == "Camlar")
            {
                camIImport1();
                camImport2();
            }
            else if (importcombobox.SelectedItem.ToString() == "Çerçeveler")
            {
                çerçeveImport1();
                çerçeveImport2();
            }
            else if (importcombobox.SelectedItem.ToString() == "Muhasebe")
            {
                muhasebeImport1();
                muhasebeImport2();
            }
            else if (importcombobox.SelectedItem.ToString() == "Ödeme")
            {
                ödemeImport1();
                ödemeImport2();
            }
            else
            {
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                comboBox1.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        void FirmaListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM firma";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void MüşteriListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM musteri";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void CamlarıListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM camlar";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void ÇerçeveListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM cerceveler";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void MuhasebeListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM muhasebe";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void ÖdemeListele()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;");
            baglanti.Open();
            sqlkomut = "SELECT * FROM ödeme";
            derleyici = new SqlDataAdapter(sqlkomut, baglanti);
            System.Data.DataTable tablo = new System.Data.DataTable();
            derleyici.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        void tabloexport()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }

                StartRow++;

                //Write datagridview content
                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void camIImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<CamlarIMPORT> camlarEXP = new List<CamlarIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CamlarIMPORT cam = new CamlarIMPORT();
                    cam.camtürno = dt.Rows[i]["camtürno"].ToString();
                    cam.camfirma = dt.Rows[i]["camfirma"].ToString();
                    cam.camtürad = dt.Rows[i]["camtürad"].ToString();
                    cam.camtürözellik = dt.Rows[i]["camtürözellik"].ToString();
                    cam.camadet = dt.Rows[i]["camadet"].ToString();
                    camlarEXP.Add(cam);
                }
                dataGridView1.DataSource = camlarEXP;
            }
        }

        void camImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<CamlarIMPORT>().Table("camlar");
                List<CamlarIMPORT> camlarEXP = dataGridView1.DataSource as List<CamlarIMPORT>;
                if (camlarEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(camlarEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void çerçeveImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<ÇerçevelerIMPORT> çerçevelerEXP = new List<ÇerçevelerIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ÇerçevelerIMPORT çerçeve = new ÇerçevelerIMPORT();
                    çerçeve.certürno = dt.Rows[i]["certürno"].ToString();
                    çerçeve.cercevefirma = dt.Rows[i]["cercevefirma"].ToString();
                    çerçeve.cercevead = dt.Rows[i]["cercevead"].ToString();
                    çerçeve.cerceveözellik = dt.Rows[i]["cerceveözellik"].ToString();
                    çerçeve.cerceveadet = dt.Rows[i]["cerceveadet"].ToString();
                    çerçevelerEXP.Add(çerçeve);
                }
                dataGridView1.DataSource = çerçevelerEXP;
            }
        }

        void çerçeveImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<ÇerçevelerIMPORT>().Table("cerceveler");
                List<ÇerçevelerIMPORT> çerçevelerEXP = dataGridView1.DataSource as List<ÇerçevelerIMPORT>;
                if (çerçevelerEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(çerçevelerEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void firmaImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<FirmaIMPORT> firmaEXP = new List<FirmaIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FirmaIMPORT firma = new FirmaIMPORT();
                    firma.firmano = dt.Rows[i]["firmano"].ToString();
                    firma.firmaad = dt.Rows[i]["firmaad"].ToString();
                    firma.firmatür = dt.Rows[i]["firmatür"].ToString();
                    firma.firmatelefon = dt.Rows[i]["firmatelefon"].ToString();
                    firmaEXP.Add(firma);
                }
                dataGridView1.DataSource = firmaEXP;
            }
        }

        void firmaImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<FirmaIMPORT>().Table("firma");
                List<FirmaIMPORT> firmaEXP = dataGridView1.DataSource as List<FirmaIMPORT>;
                if (firmaEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(firmaEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void muhasebeImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<MuhasebeIMPORT> muhasebeEXP = new List<MuhasebeIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MuhasebeIMPORT muhasebe = new MuhasebeIMPORT();
                    muhasebe.islemno = dt.Rows[i]["islemno"].ToString();
                    muhasebe.islemyapanisim = dt.Rows[i]["islemyapanisim"].ToString();
                    muhasebe.islemaciklama = dt.Rows[i]["islemaciklama"].ToString();
                    muhasebe.islemtür = dt.Rows[i]["islemtür"].ToString();
                    muhasebe.islemtutar = dt.Rows[i]["islemtutar"].ToString();
                    muhasebeEXP.Add(muhasebe);
                }
                dataGridView1.DataSource = muhasebeEXP;
            }
        }

        void muhasebeImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<MuhasebeIMPORT>().Table("muhasebe");
                List<MuhasebeIMPORT> muhasebeEXP = dataGridView1.DataSource as List<MuhasebeIMPORT>;
                if (muhasebeEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(muhasebeEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void musteriImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<MusteriIMPORT> musteriEXP = new List<MusteriIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MusteriIMPORT musteri = new MusteriIMPORT();
                    musteri.mtc = dt.Rows[i]["mtc"].ToString();
                    musteri.misim = dt.Rows[i]["misim"].ToString();
                    musteri.msoyad = dt.Rows[i]["msoyad"].ToString();
                    musteri.mtelefon = dt.Rows[i]["mtelefon"].ToString();
                    musteri.mgözdurum = dt.Rows[i]["mgözdurum"].ToString();
                    musteri.mnot = dt.Rows[i]["mnot"].ToString();
                    musteriEXP.Add(musteri);
                }
                dataGridView1.DataSource = musteriEXP;
            }
        }

        void musteriImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<MusteriIMPORT>().Table("musteri");
                List<MusteriIMPORT> musteriEXP = dataGridView1.DataSource as List<MusteriIMPORT>;
                if (musteriEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(musteriEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ödemeImport1()
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<ÖdemeIMPORT> ödemeEXP = new List<ÖdemeIMPORT>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ÖdemeIMPORT ödeme = new ÖdemeIMPORT();
                    ödeme.mtcno = dt.Rows[i]["mtcno"].ToString();
                    ödeme.fiyat = dt.Rows[i]["fiyat"].ToString();
                    ödeme.alınan = dt.Rows[i]["alınan"].ToString();
                    ödeme.kalan = dt.Rows[i]["kalan"].ToString();
                    ödemeEXP.Add(ödeme);
                }
                dataGridView1.DataSource = ödemeEXP;
            }
        }

        void ödemeImport2()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True;";
                DapperPlusManager.Entity<ÖdemeIMPORT>().Table("ödeme");
                List<ÖdemeIMPORT> ödemeEXP = dataGridView1.DataSource as List<ÖdemeIMPORT>;
                if (ödemeEXP != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(ödemeEXP);
                    }
                }
                MessageBox.Show("Başarı ile içe aktarıldı");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void muhasebeEXPORT()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }

                StartRow++;

                //Write datagridview content
                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ödemeEXPORT()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }

                StartRow++;

                //Write datagridview content
                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
