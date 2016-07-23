using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLF
{
    public partial class GameWindow : Form
    {
        private ArrayList katList;

        public GameWindow(GameData data)
        {

            this.katList = katList;

            InitializeComponent();
        }

        private char ZufaelligerChar()
        {
            return (char)((new Random().Next(0, 25)) + 65);
        }

    }
}
