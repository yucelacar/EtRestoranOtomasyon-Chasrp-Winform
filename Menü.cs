using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtRestoran
{
    class Menü:Gıda
    {
        //Menü Class fiyat için kapsulleme yapılıyor.
        Helper yardım = new Helper();
        private double _fiyat;
        
        public double Fiyat {
            get { return _fiyat; }
            set
            {
                if (yardım.SayısalİfadeKontrolEt(value.ToString())&&value>0)
                {
                    _fiyat = value;
                }
                else
                {
                    throw new Exception("Hatalı değer");
                }
            }
        }

        //ToString Metodu override edilyor ComboBox'ta sadece adı görünsün amacı.
        public override string ToString()
        {
            return this.Ad;
        }
    }
}
