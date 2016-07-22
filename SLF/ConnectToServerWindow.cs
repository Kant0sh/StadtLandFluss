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

            Client client = CreateClient(textBox1.Text, 59687);

            Close();
            Dispose();
            CreateAddCatWindowThread(client);

        }

        private void button2_Click(object sender, EventArgs e) // Create Server
        {

            CreateServer(59687);
            Client client = CreateClient("127.0.0.1", 59687);

            Close();
            Dispose();
            CreateAddCatWindowThread(client);

        }

        private Client CreateClient(string ipAddress, int port)
        {
            Thread t = new Thread(RunClient);
            Client cli = new Client(ipAddress, port);
            t.Start(cli);
            return cli;
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
        private void CreateAddCatWindowThread(Client client)
        {
            Thread t = new Thread(OpenAddCatWindow);
            t.SetApartmentState(ApartmentState.STA);
            t.Start(client);
        }

        private void OpenAddCatWindow(object arg)
        {
            Application.Run(new AddCategoriesWindow((Client) arg));
        }

    }
}
