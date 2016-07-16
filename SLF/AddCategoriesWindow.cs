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
using System.Threading;

namespace SLF
{
    public partial class AddCategoriesWindow : Form
    {

        private Thread thread;

        private ArrayList katList;

        private ArrayList kepList;
        private Button startBtn;

        public AddCategoriesWindow()
        {

            katList = new ArrayList();

            InitializeComponent();

            this.kepList = new ArrayList();

            this.kepList.Add(new KategorieErstellerPanel(kepList.Count, this.kepList, this, 323, 32, 12));
            this.kepList.Add(new KategorieErstellerPanel(kepList.Count, this.kepList, this, 323, 32, 12).Disable());

            foreach (object obj in kepList)
            {
                this.Controls.Add((KategorieErstellerPanel)obj);
            }
            this.startBtn.Location = new System.Drawing.Point(((KategorieErstellerPanel)kepList[kepList.Count - 1]).Location.X, ((KategorieErstellerPanel)kepList[kepList.Count - 1]).getPadding() + ((KategorieErstellerPanel)kepList[kepList.Count - 1]).getHeight() + ((KategorieErstellerPanel)kepList[kepList.Count - 1]).Location.Y);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {

            foreach(object obj in kepList)
            {
                if(((KategorieErstellerPanel)obj).getButtonText() == "Fertig")
                {
                    MessageBox.Show("Eine Kategorie wird noch bearbeitet!");
                    return;
                }
            }

            foreach(object obj in kepList)
            {

                katList.Add(new Kategorie(((KategorieErstellerPanel)obj).getText()));

            }

            this.Dispose(true);
            thread = new Thread(openGameWindow);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        public void Reinit()
        {
            foreach (object obj in kepList)
            {
                this.Controls.Add((KategorieErstellerPanel)obj);
            }

            this.startBtn.Location = new System.Drawing.Point(((KategorieErstellerPanel)kepList[kepList.Count - 1]).Location.X, ((KategorieErstellerPanel)kepList[kepList.Count - 1]).getPadding() + ((KategorieErstellerPanel)kepList[kepList.Count - 1]).getHeight() + ((KategorieErstellerPanel)kepList[kepList.Count - 1]).Location.Y);

        }

        private void openGameWindow()
        {
            Application.Run(new GameWindow(katList));
        }

    }
}
