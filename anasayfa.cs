using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using LiveCharts; 
using LiveCharts.Wpf;

namespace proje007
{
    public partial class anasayfa : Form
    {

        int aktifMusteriID;
        string aktifKullaniciAd;


        public anasayfa(int id,string ad)
        {
            InitializeComponent();
            aktifMusteriID = id;
            aktifKullaniciAd =ad;
        }
        
        static string constring = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_sistem;Integrated Security=True;Encrypt=False";
        SqlConnection connect = new SqlConnection(constring);

        public void kayıtları_getir() 
            {
            string getir = "SELECT POLİCEID,BRANS,SİRKET,FİYAT,BASTARİH,BİTTARİH,ACIKLAMA FROM police WHERE MID=@id";

            SqlCommand komut = new SqlCommand(getir,connect);
            komut.Parameters.AddWithValue("@id", aktifMusteriID);
            
            
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                if (row.Cells["BİTTARİH"].Value != null && row.Cells["BİTTARİH"].Value != DBNull.Value)
                {
                    DateTime bitisTarihi = Convert.ToDateTime(row.Cells["BİTTARİH"].Value);
                    TimeSpan fark = bitisTarihi - DateTime.Now;

                   
                    if (fark.TotalDays <= 15 && fark.TotalDays >= 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange; 
                        row.DefaultCellStyle.ForeColor = Color.White; 
                    }
                    
                    else if (fark.TotalDays < 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }

            connect.Close();

            }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed) connect.Open();

                // Seçilen tarihe göre filtreleme sorgusu
                string sorgu = "SELECT * FROM police WHERE MID=@id AND BİTTARİH=@secilenTarih";
                SqlDataAdapter da = new SqlDataAdapter(sorgu, connect);
                da.SelectCommand.Parameters.AddWithValue("@id", aktifMusteriID);
                da.SelectCommand.Parameters.AddWithValue("@secilenTarih", e.Start.ToShortDateString());

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
        public void TakvimdePoliceleriIsaretle()
        {
            if (connect.State == ConnectionState.Closed) connect.Open();
            SqlCommand komut = new SqlCommand("SELECT BİTTARİH FROM police WHERE MID=@id", connect);
            komut.Parameters.AddWithValue("@id", aktifMusteriID);

            SqlDataReader dr = komut.ExecuteReader();
            List<DateTime> tarihler = new List<DateTime>();

            while (dr.Read())
            {
                tarihler.Add(Convert.ToDateTime(dr["BİTTARİH"]));
            }
            dr.Close();
            connect.Close();

            
            monthCalendar1.BoldedDates = tarihler.ToArray();
        }
        public void YakinBitisliPoliceleriIleGetir()
        {
            try
            {
                if (connect.State == ConnectionState.Closed) connect.Open();

                
                string sorgu = "SELECT BRANS, BİTTARİH FROM police WHERE MID=@id";
                SqlCommand komut = new SqlCommand(sorgu, connect);
                komut.Parameters.AddWithValue("@id", aktifMusteriID);

                SqlDataReader dr = komut.ExecuteReader();

                
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.View = View.Details; 
                listView1.FullRowSelect = true; 

                
                listView1.Columns.Add("Poliçe  ", 120);
                listView1.Columns.Add("Bitiş Tarihi  ", 125);

                while (dr.Read())
                {
                    if (dr["BİTTARİH"] != DBNull.Value)
                    {
                        DateTime bitisTarihi = Convert.ToDateTime(dr["BİTTARİH"]);
                        TimeSpan fark = bitisTarihi - DateTime.Now;

                        
                        if (fark.TotalDays <= 15)
                        {
                            ListViewItem satir = new ListViewItem(dr["BRANS"].ToString());
                            satir.SubItems.Add(bitisTarihi.ToShortDateString());

                            
                            if (fark.TotalDays < 0)
                            {
                                
                                satir.ForeColor = Color.Red;
                                satir.Font = new Font(listView1.Font, FontStyle.Bold);
                            }
                            else
                            {
                                
                                satir.ForeColor = Color.DarkOrange;
                            }

                            listView1.Items.Add(satir);
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open) connect.Close();
            }
        }

        public void BransGrafigiCiz()
        {
            try
            {
                if (connect.State == ConnectionState.Closed) connect.Open();

                string sorgu = "SELECT BRANS, COUNT(*) as Adet FROM police WHERE MID=@id GROUP BY BRANS";
                SqlCommand komut = new SqlCommand(sorgu, connect);
                komut.Parameters.AddWithValue("@id", aktifMusteriID);

                SqlDataReader dr = komut.ExecuteReader();
                SeriesCollection series = new SeriesCollection();

                while (dr.Read())
                {
                    series.Add(new PieSeries
                    {
                        Title = dr["BRANS"].ToString(),
                        Values = new ChartValues<int> { Convert.ToInt32(dr["Adet"]) },
                        DataLabels = true,
                        PushOut = 2, 
                        LabelPoint = chartPoint => string.Format("{0}", chartPoint.Y) 
                    });
                }
                dr.Close();

                
                pieChart1.Series.Clear();
                pieChart1.Series = series;

                
                pieChart1.InnerRadius = 40;
                pieChart1.LegendLocation = LegendLocation.Bottom; 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Grafik hatası: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open) connect.Close();
            }
        }
       

public void SirketGrafigiCiz()
    {
        try
        {
            if (connect.State == ConnectionState.Closed) connect.Open();

            string sorgu = "SELECT SİRKET, COUNT(*) as Adet FROM police WHERE MID=@id GROUP BY SİRKET";
            SqlCommand komut = new SqlCommand(sorgu, connect);
            komut.Parameters.AddWithValue("@id", aktifMusteriID);

            SqlDataReader dr = komut.ExecuteReader();

            List<string> sirketIsimleri = new List<string>();
            ChartValues<int> adetler = new ChartValues<int>();

            while (dr.Read())
            {
                sirketIsimleri.Add(dr["SİRKET"].ToString());
                adetler.Add(Convert.ToInt32(dr["Adet"]));
            }
            dr.Close();

            
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();

            cartesianChart1.Series = new SeriesCollection
        {
            new ColumnSeries 
            {
                Title = "Poliçe Sayısı",
                Values = adetler,
                DataLabels = true 
            }
        };

            
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Sigorta Şirketleri",
                Labels = sirketIsimleri
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show("Şirket grafiği hatası: " + ex.Message);
        }
        finally
        {
            connect.Close();
        }
    }
    public void ToplamMaliyetHesapla()
        {
            try
            {
                if (connect.State == ConnectionState.Closed) connect.Open();

               
                string sorgu = "SELECT SUM(FİYAT) FROM police WHERE MID=@id";
                SqlCommand komut = new SqlCommand(sorgu, connect);
                komut.Parameters.AddWithValue("@id", aktifMusteriID);

                object sonuc = komut.ExecuteScalar();

                if (sonuc != DBNull.Value && sonuc != null)
                {
                    decimal toplam = Convert.ToDecimal(sonuc);
                    LblToplamMaliyet.Text = toplam.ToString("C2"); 
                }
                else
                {
                    LblToplamMaliyet.Text = "0,00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Maliyet hesaplanırken hata: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public void PoliceAdetHesapla()
        {
            try
            {
                if (connect.State == ConnectionState.Closed) connect.Open();

                
                string sorgu = "SELECT COUNT(*) FROM police WHERE MID=@id";
                SqlCommand komut = new SqlCommand(sorgu, connect);
                komut.Parameters.AddWithValue("@id", aktifMusteriID);

                
                int adet = Convert.ToInt32(komut.ExecuteScalar());

                
                LblPoliceAdet.Text = adet.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Poliçe adeti hesaplanırken hata: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open) connect.Close();
            }
        }

        public void verisil(int id) 
        {
            string sil = "DELETE From police WHERE POLİCEID=@id";
            SqlCommand komut = new SqlCommand(sil,connect);
            
            connect.Open();
            komut.Parameters.AddWithValue("@id", id);

            komut.ExecuteNonQuery();
            connect.Close();




        }



        private void anasayfa_Load(object sender, EventArgs e)
        {
            kayıtları_getir();
            ToplamMaliyetHesapla();
            PoliceAdetHesapla();
            BransGrafigiCiz();
            SirketGrafigiCiz();
            YakinBitisliPoliceleriIleGetir();
            TakvimdePoliceleriIsaretle();
            label1.Text = "HOŞGELDİNİZ, " + aktifKullaniciAd;


            comboBox1.Items.Add("TRAFİK");
            comboBox1.Items.Add("KASKO");
            comboBox1.Items.Add("DASK");
            comboBox1.Items.Add("KONUT");
            comboBox1.Items.Add("SEYAHAT");
            comboBox1.Items.Add("ÖZEL SAĞLIK");
            comboBox1.Items.Add("HAYAT");
            comboBox1.Items.Add("FERDİ KAZA");

            comboBox2.Items.Add("AXA SİGORTA");
            comboBox2.Items.Add("ALLİANZ SİGORTA");
            comboBox2.Items.Add("AK SİGORTA");
            comboBox2.Items.Add("TÜRKİYE SİGORTA");
            comboBox2.Items.Add("DOĞA SİGORTA");
            comboBox2.Items.Add("ETHİCA SİGORTA");
            comboBox2.Items.Add("HEPİYİ SİGORTA");
            comboBox2.Items.Add("KORU SİGORTA");
            comboBox2.Items.Add("MAPFRE SİGORTA");
            comboBox2.Items.Add("NEOVA SİGORTA");
            comboBox2.Items.Add("RAY SİGORTA");
            comboBox2.Items.Add("SOMPO JAPAN SİGORTA");
            comboBox2.Items.Add("UNİCO SİGORTA");



          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed) 
                {
                    connect.Open();
                    string kayit = "insert into police (BRANS,BASTARİH,BİTTARİH,FİYAT,SİRKET,MID,ACIKLAMA) values(@BRANS,@BASTARİH,@BİTTARİH,@FİYAT,@SİRKET,@MID,@ACIKLAMA) ";
                    SqlCommand komut = new SqlCommand(kayit, connect);
                    komut.Parameters.AddWithValue("@BRANS", comboBox1.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@SİRKET", comboBox2.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@FİYAT", textBox1.Text);
                    komut.Parameters.AddWithValue("@BASTARİH", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@BİTTARİH", dateTimePicker2.Value);
                    komut.Parameters.AddWithValue("@MID", aktifMusteriID);
                    komut.Parameters.AddWithValue("@ACIKLAMA", textBox2.Text);

                    komut.ExecuteNonQuery();

                    
                }

            }
            catch(Exception hata)
            { 
                MessageBox.Show("Hata"+hata.Message);
            }
            BransGrafigiCiz();
            SirketGrafigiCiz();
            YakinBitisliPoliceleriIleGetir();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            kayıtları_getir();
            ToplamMaliyetHesapla();
            PoliceAdetHesapla();
            BransGrafigiCiz();
            SirketGrafigiCiz();
            YakinBitisliPoliceleriIleGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                verisil(id);
                kayıtları_getir();
                BransGrafigiCiz();

            }
        }
        int i = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (connect.State == ConnectionState.Closed) connect.Open();
            string guncelle = "UPDATE police SET BRANS=@BRANS, SİRKET=@SİRKET, FİYAT=@FİYAT, ACIKLAMA=@ACIKLAMA, BASTARİH=@BASTARİH, BİTTARİH=@BİTTARİH WHERE POLİCEID=@id"; SqlCommand komut = new SqlCommand(guncelle, connect);
            komut.Parameters.AddWithValue("@BRANS", comboBox1.Text);
            komut.Parameters.AddWithValue("@SİRKET", comboBox2.Text);
            komut.Parameters.AddWithValue("@FİYAT", textBox1.Text);
            komut.Parameters.AddWithValue("@BASTARİH", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@BİTTARİH", dateTimePicker2.Value);
            komut.Parameters.AddWithValue("@ACIKLAMA", textBox2.Text);
            komut.Parameters.AddWithValue("@id",dataGridView1.Rows[i].Cells[0].Value);
            komut.ExecuteNonQuery();
            
            connect.Close();
            kayıtları_getir();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            i = e.RowIndex;

            comboBox1.Text = dataGridView1.Rows[i].Cells["BRANS"].Value?.ToString();
            comboBox2.Text = dataGridView1.Rows[i].Cells["SİRKET"].Value?.ToString();
            textBox1.Text = dataGridView1.Rows[i].Cells["FİYAT"].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells["ACIKLAMA"].Value?.ToString();
            if (DateTime.TryParse(dataGridView1.Rows[i].Cells["BASTARİH"].Value?.ToString(), out DateTime dt1))
                dateTimePicker1.Value = dt1;

            if (DateTime.TryParse(dataGridView1.Rows[i].Cells["BİTTARİH"].Value?.ToString(), out DateTime dt2))
                dateTimePicker2.Value = dt2;


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
