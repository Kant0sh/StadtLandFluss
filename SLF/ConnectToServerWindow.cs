using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLF
{
    public partial class ConnectToServerWindow : Form
    {

        public ConnectToServerWindow()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e) // Connect
        {
            Close();
            Dispose();
            CreateAddCatWindowThread();
        }

        private void button2_Click(object sender, EventArgs e) // Create Server
        {

        }

        private void CreateClient(string ipAddress, int port)
        {
            Thread t = new Thread(RunClient);
            t.Start(new Client(ipAddress, port));
        }

        private void RunClient(object cli)
        {
            ((Client)cli).Run();
        }


        private void CreateServer(int port)
        {
            Thread t = new Thread(RunServer);
            t.Start(new Server(port));
        }

        private void RunServer(object srv)
        {
            ((Server)srv).Run();
        }
        private void CreateAddCatWindowThread()
        {
            Thread t = new Thread(OpenAddCatWindow);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void OpenAddCatWindow()
        {
            Application.Run(new AddCategoriesWindow());
        }

    }
}
