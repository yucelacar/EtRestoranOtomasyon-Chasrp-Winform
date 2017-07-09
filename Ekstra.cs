using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtRestoran
{
    class EkstraGıda:Gıda
    {
        //EkstraGıda Sınıfı Fiyat için Kapsulleme yapılıyor.
        Helper yardım = new Helper();
        private double _fiyat;
        public double Fiyat
        {
            get { return _fiyat; }
            set
            {
                if (yardım.SayısalİfadeKontrolEt(value.ToString())&&value>=0)
                {
                    _fiyat = value;
                }
            }
        }
    }
}
