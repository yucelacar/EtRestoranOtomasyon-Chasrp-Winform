using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtRestoran
{
    public partial class frmEtRestoran : Form
    {
        //Restoran Form Kısmı
        Helper yardim = new Helper();


        public frmEtRestoran()
        {
            InitializeComponent();
            yardim.KontrolleriTemizle(this);    //tüm kontroller temizlenip sıfırlanarak baslıyor.
            txtTopla.Enabled = false;
        }


        private void frmEtRestoran_Load(object sender, EventArgs e)
        {
            try
            {
                CheckBoxTagEkle();
                ComboBaxItemEkle();
                btnHesapla.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Sınıftan nesne üretirken hata oluştu.");
            }

        }

        //ekstra sınıfından uretilen nesneleri checkBox'ın tag'ine atıcaz onları sonra kullanıcağımız için.
        private void CheckBoxTagEkle()
        {
            chbTuz.Tag = EkstraGidaUret("Tuz", 0); ;
            chbPatates.Tag = EkstraGidaUret("Patates", 3);
            chbSalatalik.Tag = EkstraGidaUret("Salatalık", 1);
            chbSogan.Tag = EkstraGidaUret("Soğan", 0.5);
            chcTursu.Tag = EkstraGidaUret("Turşu", 1);
            chbMayonez.Tag = EkstraGidaUret("Mayonez", 0);
            chbKetcap.Tag = EkstraGidaUret("Ketçap", 0);
            chbHardal.Tag = EkstraGidaUret("Hardal", 0.5);
            chbDomates.Tag = EkstraGidaUret("Domates", 1);
            chbBarbeku.Tag = EkstraGidaUret("Barbekü", 0.5);
        }

        //Menü sınıfından üretilen menüleri ComboBax içine atıyoruz. Menü sınıfında to string override olduğundan sadece adı görünecek.
        private void ComboBaxItemEkle()
        {
            cmbMenu.Items.Add(MenuUret("Tavuk Menü", 12));
            cmbMenu.Items.Add(MenuUret("Et Menü", 17));
            cmbMenu.Items.Add(MenuUret("İskender Menü", 20));
            cmbMenu.Items.Add(MenuUret("Vejeteryan Menü", 14));
        }

        //Sadece aldıgı isim ve fiyat ile bir ekstraGıda sınıfından nesne üretiyor. Üretilen nesneyi geri dönüyor.
        private EkstraGıda EkstraGidaUret(string ad, double fiyat)
        {

            EkstraGıda ekstragıda = new EkstraGıda();
            ekstragıda.Ad = ad;
            ekstragıda.Fiyat = fiyat;
            return ekstragıda;
        }

        //Menü Sınıfından nesne üretip ürettiği nesneyi dönüyor.
        private Menü MenuUret(string ad, double fiyat)
        {
            Menü Uretilen = new Menü();
            Uretilen.Ad = ad;
            Uretilen.Fiyat = fiyat;
            return Uretilen;
        }


        private void btnHesapla_Click(object sender, EventArgs e)
        {
            HesapTopla();
        }

        string seciliCheckBox = ""; //ListBox eklemek için hesaplanan ekstra gıda fiyatlarını global değişkene atıyoruz.
        private void HesapTopla()
        {
            seciliCheckBox = ""; //Her hesapla butonuna tıkladığında içini boşaltıyoruz.
            double toplam = 0;
            if (cmbMenu.SelectedIndex > -1)
            {
                double menuFiyatı = ((Menü)cmbMenu.SelectedItem).Fiyat;//menü fiyatını secili cmb item dan alıyor.

                if (rdbTam.Checked)//Radio Button Kontrolü
                {
                    foreach (CheckBox item in gprEkstra.Controls)
                    {
                        if (item.Checked)
                        {
                            toplam += ((EkstraGıda)item.Tag).Fiyat; //checkBox tag inden üretiğimiz nesnenin fiyatını alıyoruz.
                            seciliCheckBox = seciliCheckBox + " " + ((EkstraGıda)item.Tag).Ad;  //checkBox tag inden üretiğimiz nesnenin adını alıyoruz.
                        }
                    }
                }

                else if (rdbYarim.Checked)//Radio Button Kontrolü
                {
                    menuFiyatı = menuFiyatı / 2;
                    foreach (CheckBox item in gprEkstra.Controls)
                    {
                        if (item.Checked)
                        {
                            toplam += ((EkstraGıda)item.Tag).Fiyat;
                            seciliCheckBox = seciliCheckBox + " " + ((EkstraGıda)item.Tag).Ad;
                        }
                    }
                }
                toplam = toplam + menuFiyatı; //ekstralar + Menü Fiyatı
                toplam = toplam * (double)nmbAdet.Value; //hesap * Adet sayısı
                txtTopla.Text = toplam.ToString(); //textBox Yazdırma.
                btnEkle.Enabled = true; //Btn Ekle aktif
            }
            else
            {
                MessageBox.Show("Menü Seciniz");
            }


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            ListBoxEKle();
        }

        private double OdenecekToplamTutar;// ListBox'a eklenen siparişin Siparişi tamamladıktan sonra toplam tutarını mesajbox ta göstermesi için global değişken.

        private void ListBoxEKle() //ListBox ekleme Ekle buttonu clicklendiğinde çalışıcak.
        {

            string menuAdi = ((Menü)cmbMenu.SelectedItem).Ad; //secili ComboBox tan ismini aldık.

            if (rdbTam.Checked)
            {
                listSiparis.Items.Add(menuAdi + " (" + seciliCheckBox + ") " + rdbTam.Text + " Porsiyon " + nmbAdet.Value + " Adet" + "---" + txtTopla.Text + " TL");
                OdenecekToplamTutar += Convert.ToDouble(txtTopla.Text);
            }
            else
            {
                listSiparis.Items.Add(menuAdi + " (" + seciliCheckBox + ") " + rdbYarim.Text + " Porsiyon " + nmbAdet.Value + " Adet" + "---" + txtTopla.Text + " TL");
                OdenecekToplamTutar += Convert.ToDouble(txtTopla.Text);
            }

            seciliCheckBox = "";
            yardim.KontrolleriTemizle(gprEkstra); //Ekstra Group Box Controlleri Temizleniyor.
            yardim.KontrolleriTemizle(cmbMenu); //ComboBox secili öğesi -1 oluyor.
            btnTamamla.Enabled = true; //Sipariş tamamla butonu aktif oluyor.
            btnEkle.Enabled = false; //hesaplanmadan ekleme olmayacak.

        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            HesapGoster();
        }

        private void HesapGoster() //Sipariş Tamamla Butonu Aktiflestiriyor.
        {
            MessageBox.Show("Toplam Ödenecek Tutar: " + OdenecekToplamTutar.ToString());
            OdenecekToplamTutar = 0;
            yardim.KontrolleriTemizle(this); //Formdaki Tüm kontrolleri Temizliyor.
            btnHesapla.Enabled = true; //Btn Hesapla Aktıf oluyor.
        }
    }
}
