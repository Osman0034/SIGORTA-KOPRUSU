using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
namespace proje007
{
    public partial class login : Form
    {

        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tcno.Text))
            {
                MessageBox.Show("TC Kimlik No 11 haneli olmalı ve boş bırakılmamalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tcno.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txt_sifre.Text))
            {
                MessageBox.Show("Lütfen adınızı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sifre.Focus();
            }
            else
            {
           

                string user = txt_tcno.Text;
                string password = txt_sifre.Text;
                con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_sistem;Integrated Security=True;Encrypt=False");
                com = new SqlCommand();
                con.Open();
                com.Connection = con;
                com.CommandText = "Select ID, AD, SOYAD From müsteri where TCNO='" + txt_tcno.Text + "' and SİFRE='" + txt_sifre.Text + "'";
            


                dr = com.ExecuteReader();

                if (dr.Read())
                {
                    int ID = Convert.ToInt32(dr["ID"]);
                    string tamAd = dr["AD"].ToString() + " " + dr["SOYAD"].ToString();

                    

                    anasayfa gecis = new anasayfa(ID, tamAd);
                    gecis.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("HATALI TC NO ve ŞİFRE");
                }
                con.Close();

            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register reg = new register();  
            reg.Show();
            this.Hide();
        }
    } }
