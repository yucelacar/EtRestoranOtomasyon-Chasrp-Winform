using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtRestoran
{
    class Gıda
    {
        //Base Sınıf Ad için kapsuleme yapılıyor.
        Helper yardım = new Helper();
        private string _ad;

        public string Ad
        {
            get
            {
                return _ad;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)&&yardım.StringİfadeKontrolEt(value,""))
                {
                    _ad = value;
                }
                else
                {
                    throw new Exception("hatalı");
                }
            }
        }

    }
}
