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
using SLF.Net;

namespace SLF
{
    public partial class AddCategoriesWindow : Form
    {

        private Client client;
        private GameData data;

        private ArrayList catList;

        private ArrayList ccpList;
        private Button startBtn;

        public AddCategoriesWindow(Client client, GameData data)
        {

            this.client = client;
            this.data = data;

            catList = new ArrayList();

            InitializeComponent();

            this.ccpList = new ArrayList();

            this.ccpList.Add(new CategoryCreatorPanel(ccpList.Count, this.ccpList, this, 323, 32, 12));
            this.ccpList.Add(new CategoryCreatorPanel(ccpList.Count, this.ccpList, this, 323, 32, 12).Disable());

            foreach (object obj in ccpList)
            {
                this.Controls.Add((CategoryCreatorPanel)obj);
            }
            this.startBtn.Location = new System.Drawing.Point(((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).Location.X, ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).getPadding() + ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).getHeight() + ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).Location.Y);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {

            foreach(object obj in ccpList)
            {
                if(((CategoryCreatorPanel)obj).getButtonText() == "Fertig")
                {
                    MessageBox.Show("Eine Kategorie wird noch bearbeitet!");
                    return;
                }
            }

            foreach(object obj in ccpList)
            {

                catList.Add(new Category(((CategoryCreatorPanel)obj).getText()));

            }

            this.Dispose(true);
            Thread thread = new Thread(openGameWindow);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        public void Reinit()
        {
            foreach (object obj in ccpList)
            {
                this.Controls.Add((CategoryCreatorPanel)obj);
            }

            this.startBtn.Location = new System.Drawing.Point(((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).Location.X, ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).getPadding() + ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).getHeight() + ((CategoryCreatorPanel)ccpList[ccpList.Count - 1]).Location.Y);

        }

        private void openGameWindow()
        {
            Application.Run(new GameWindow(catList));
        }

    }
}
