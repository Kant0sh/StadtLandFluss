using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLF
{
    public class Category
    {

        private string CatText;

        public Category(string CatText)
        {
            this.CatText = CatText;
        }

        public string getCatText()
        {
            return CatText;
        }

    }
}
