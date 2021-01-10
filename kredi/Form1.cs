using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kredi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da;

        //Kişileri listelemek için
        void listele()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=basvuru.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select * From BASVURULAR", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        //Form yüklendiğinde metodu çağırmak için
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            this.bASVURULARTableAdapter.Fill(this.basvuruDataSet.BASVURULAR);

        }
        //Aylık ve Toplam Tutar Hesaplama İşlemleri
        private void Button1_Click(object sender, EventArgs e)
        {
            int kredimiktar = Convert.ToInt32(TextBox6.Text);
            int vade = Convert.ToInt32(TextBox7.Text);
            int kreditur = Convert.ToInt32(ComboBox2.SelectedIndex);
            double faiz, aylikodeme, toplamodeme;
            if (kreditur == 0)
            {
                faiz = 0.0108;
                aylikodeme = (kredimiktar * faiz) / (1 - 1 / Math.Pow((1 + faiz), vade));
                toplamodeme = aylikodeme * vade;
                Label14.Text = aylikodeme.ToString() + "TL";
                Label16.Text = toplamodeme.ToString() + "TL";

            }
            else if (kreditur == 1)
            {
                faiz = 0.0107;
                aylikodeme = (kredimiktar * faiz) / (1 - 1 / Math.Pow((1 + faiz), vade));
                toplamodeme = aylikodeme * vade;
                Label14.Text = aylikodeme.ToString() + "TL";
                Label16.Text = toplamodeme.ToString() + "TL";

            }
            else if (kreditur == 2)
            {
                faiz = 0.0105;
                aylikodeme = (kredimiktar * faiz) / (1 - 1 / Math.Pow((1 + faiz), vade));
                toplamodeme = aylikodeme * vade;
                Label14.Text = aylikodeme.ToString() + "TL";
                Label16.Text = toplamodeme.ToString() + "TL";

            }
            else if (kreditur == 3)
            {
                faiz = 0.0102;
                aylikodeme = (kredimiktar * faiz) / (1 - 1 / Math.Pow((1 + faiz), vade));
                toplamodeme = aylikodeme * vade;
                Label14.Text = aylikodeme.ToString() + "TL";
                Label16.Text = toplamodeme.ToString() + "TL";

            }
            else if (kreditur == 4)
            {
                faiz = 0.0100;
                aylikodeme = (kredimiktar * faiz) / (1 - 1 / Math.Pow((1 + faiz), vade));
                toplamodeme = aylikodeme * vade;
                Label14.Text = aylikodeme.ToString() + "TL";
                Label16.Text = toplamodeme.ToString() + "TL";
            }
            else
            {
                MessageBox.Show("Lütfen Kredi Türü Seçiniz...", "Uyarı");
            }

        }
        //Formda hesaplama bölümündeki verileri temizlemek için
        private void Button4_Click(object sender, EventArgs e)
        {
            TextBox6.Text = "0";
            TextBox7.Text = "0";
            ComboBox2.Text = "Seçiniz...";
            Label14.Text = "0";
            Label16.Text = "0";
        }
        //Listeye kişi ekleme 
        private void Button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Insert into BASVURULAR (tc_kimlik_no,adi,soyadi,dogum_tarihi,cinsiyet,cep_no,kredimiktar,vade,kreditur,taksit,top_tutar) values (@tc_kimlik_no,@adi,@soyadi,@dogum_tarihi,@cinsiyet,@cep_no,@kredimiktar,@vade,@kreditur,@taksit,@top_tutar)";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@tc_kimlik_no", Convert.ToInt64(TextBox1.Text));
            komut.Parameters.AddWithValue("@adi", TextBox2.Text);
            komut.Parameters.AddWithValue("@soyadi", TextBox3.Text);
            komut.Parameters.AddWithValue("@dogum_tarihi", DateTimePicker1.Value);
            komut.Parameters.AddWithValue("@cinsiyet", ComboBox1.Text);
            komut.Parameters.AddWithValue("@cep_no", Convert.ToInt64(TextBox4.Text));
            komut.Parameters.AddWithValue("@kredimiktar", Convert.ToInt64(TextBox6.Text));
            komut.Parameters.AddWithValue("@vade", Convert.ToInt64(TextBox7.Text));
            komut.Parameters.AddWithValue("@kreditur", ComboBox2.Text);
            komut.Parameters.AddWithValue("@taksit", Label14.Text);
            komut.Parameters.AddWithValue("@top_tutar", Label16.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            temizle();
        }
        //Silme işlemi
        private void Button2_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete From BASVURULAR Where tc_kimlik_no=@tc_kimlik_no";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@tc_kimlik_no", dataGridView1.CurrentRow.Cells[1].Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }
         //Güncelleme İşlemi
        private void button6_Click(object sender, EventArgs e)
        {
            string sorgu = "Update BASVURULAR Set adi=@adi,soyadi=@soyadi,dogum_tarihi=@dogum_tarihi,cinsiyet=@cinsiyet,cep_no=@cep_no,kredimiktar=@kredimiktar,vade=@vade,kreditur=@kreditur,taksit=@taksit,top_tutar=@top_tutar Where tc_kimlik_no=@tc_kimlik_no";
            komut = new OleDbCommand(sorgu, baglanti);
           
            komut.Parameters.AddWithValue("@adi", TextBox2.Text);
            komut.Parameters.AddWithValue("@soyadi", TextBox3.Text);
            komut.Parameters.AddWithValue("@dogum_tarihi", DateTimePicker1.Value);
            komut.Parameters.AddWithValue("@cinsiyet", ComboBox1.Text);
            komut.Parameters.AddWithValue("@cep_no", Convert.ToInt64(TextBox4.Text));
            komut.Parameters.AddWithValue("@kredimiktar", Convert.ToInt64(TextBox6.Text));
            komut.Parameters.AddWithValue("@vade", Convert.ToInt64(TextBox7.Text));
            komut.Parameters.AddWithValue("@kreditur", ComboBox2.Text);
            komut.Parameters.AddWithValue("@taksit", Label14.Text);
            komut.Parameters.AddWithValue("@top_tutar", Label16.Text); 
            komut.Parameters.AddWithValue("@tc_kimlik_no", Convert.ToInt64(TextBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            temizle();
        }
        //Formu kapatma
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
        void temizle()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            Label14.Text = "0";
            Label16.Text = "0";
            ComboBox1.Text = "Seçiniz...";
            ComboBox2.Text = "Seçiniz...";
        }
    }
  }
