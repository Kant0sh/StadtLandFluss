using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLF
{
    class Kategorie
    {

        private string KatText;

        public Kategorie(string KatText)
        {
            this.KatText = KatText;
        }

        public string getKatText()
        {
            return KatText;
        }

    }
}
