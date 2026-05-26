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
namespace proje007
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        static string constring = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_sistem;Integrated Security=True;Encrypt=False";
        SqlConnection connect = new SqlConnection(constring);

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tcno.Text) || txt_tcno.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik No 11 haneli olmalı ve boş bırakılmamalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tcno.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_ad.Text))
            {
                MessageBox.Show("Lütfen adınızı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_ad.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_soyad.Text))
            {
                MessageBox.Show("Lütfen soyadınızı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_soyad.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_sifre.Text))
            {
                MessageBox.Show("Şifre en az 4 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sifre.Focus();
            }
            else
            {
                try
                {
                    if (connect.State == ConnectionState.Closed)
                        connect.Open();

                    string kayit = "insert into müsteri (TCNO,AD,SOYAD,SİFRE) values(@TCNO,@AD,@SOYAD,@SİFRE)";
                    SqlCommand komut = new SqlCommand(kayit, connect);

                    komut.Parameters.AddWithValue("@TCNO", txt_tcno.Text);

                    komut.Parameters.AddWithValue("@AD", txt_ad.Text);

                    komut.Parameters.AddWithValue("@SOYAD", txt_soyad.Text);

                    komut.Parameters.AddWithValue("@SİFRE", txt_sifre.Text);

                    komut.ExecuteNonQuery();

                    connect.Close();
                    MessageBox.Show("KAYIT BAŞARIYLA EKLENDİ");


                }
                catch (Exception hata)
                {
                    MessageBox.Show("HATA MEYDANA GELDİ" + hata.Message);


                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

       
    }
}
