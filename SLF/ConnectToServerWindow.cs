﻿using SLF.Net;
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
        
        private GameData data;
        private ServerData srvData;

        public ConnectToServerWindow()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e) // Connect
        {

            Client client = CreateClient(textBox1.Text, 59687);

            data = new GameData(client);

            Close();
            Dispose();
            CreateAddCatWindowThread();

        }

        private void button2_Click(object sender, EventArgs e) // Create Server
        {

            Server server = CreateServer(59687);
            Client client = CreateClient("127.0.0.1", 59687);

            data = new GameData(client);
            srvData = new ServerData(server);

            Close();
            Dispose();
            CreateAddCatWindowThread();

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


        private Server CreateServer(int port)
        {
            Thread t = new Thread(RunServer);
            Server srv = new Server(port); 
            t.Start(srv);
            return srv;
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
            Application.Run(new AddCategoriesWindow(data));
        }

    }
}
