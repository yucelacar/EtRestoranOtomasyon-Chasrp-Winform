using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtRestoran
{
    class Helper
    {
        //Helper Sınıfı toplam 5 metot bulunuyor.
        // Kontrolleri temizle 3 tane overload Metot var. Biri Form alıyor, biri group Box, biri params olarak control. Form ve GroupBox kendi içinde params Control olanı cağırıyor. 
        //Kapsulleme yaparken kullandığım Sayısal ifade kontrol et ve String ifade kontrol et adında 2 tane daha metot var.
        public void KontrolleriTemizle(Form frm)
        {
            foreach (Control item in frm.Controls)
            {

                if (item is GroupBox)
                {
                    GroupBox grp = (GroupBox)item;
                    KontrolleriTemizle(grp);
                }

                else
                {
                    KontrolleriTemizle(item);
                }
            }

        }

        public void KontrolleriTemizle(params Control[] controller)
        {
            foreach (Control item in controller)
            {
                if (item is TextBox)
                {
                    TextBox txtBox = (TextBox)item;
                    txtBox.Clear();
                }
                else if (item is ListBox)
                {
                    ListBox lstBox = (ListBox)item;
                    lstBox.Items.Clear();
                }
                else if (item is PictureBox)
                {
                    PictureBox picBox = (PictureBox)item;
                    picBox.Image = null;
                    picBox.BackgroundImage = Image.FromFile("gri.jpg");
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)item;
                    dtp.Value = DateTime.Now;
                }
                else if (item is ComboBox)
                {
                    ComboBox cmBox = (ComboBox)item;
                    cmBox.SelectedIndex = -1;
                }
                else if (item is MaskedTextBox)
                {
                    MaskedTextBox mTxtBox = (MaskedTextBox)item;
                    mTxtBox.Clear();
                }
                else if (item is NumericUpDown)
                {
                    NumericUpDown num = (NumericUpDown)item;
                    num.Value = num.Minimum;
                }
                else if (item is CheckBox)
                {
                    CheckBox cBox = (CheckBox)item;
                    cBox.Checked = false;
                }
                else if (item is Button)
                {
                    Button btn = (Button)item;
                    btn.Enabled = false;
                }
            }

        }

        public void KontrolleriTemizle(GroupBox groupBox)
        {
            foreach (Control item in groupBox.Controls)
            {
                KontrolleriTemizle(item);
            }

        }

        //Eklenecekler kısmı artı bir ifade eklemek istediğimiz ornegin email de '@' ve '.' için ikinci parametre olarak alınıyor.
        public bool StringİfadeKontrolEt(string ifade, string eklenecekler)
        {
            Regex r = new Regex("[a-zA-ZğüşıöçĞÜŞİÖÇ+" + eklenecekler + "]");          
            if (r.IsMatch(ifade))
            {
                return true;
            }
            return false;
        }

        //regex ile string ifade kontrolü oldugu için gönderilicek sayisal değer Tostring edilmeli. 
        public bool SayısalİfadeKontrolEt(string ifade)
        {
            Regex r = new Regex("[1234567890]");            
            if (r.IsMatch(ifade))
            {
                return true;
            }
            return false;           
        }


    }
}
