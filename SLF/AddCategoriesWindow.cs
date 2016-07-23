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
        
        private GameData data;

        private ArrayList ccpList;
        private Button startBtn;

        public AddCategoriesWindow(GameData data)
        {
            this.data = data;
            this.data.client.CatListReceivedEvent += Client_CatListReceivedEvent;

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

        private void Client_CatListReceivedEvent(List<Category> catList)
        {
            Thread t = new Thread(openGameWindow);
            t.Start();
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
                if(((CategoryCreatorPanel)obj).enabled && ((CategoryCreatorPanel)obj).getButtonText() != "Hinzufügen") data.catList.Add(new Category(((CategoryCreatorPanel)obj).getText()));
            }

            data.client.Send(MessageToolkit.CreateMessage(Net.Message.ReadyToSendCat, true));

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
            Application.Run(new GameWindow(data));
        }

    }
}
