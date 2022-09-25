    using System;
using System.Net.WebSockets;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using System.Net.Sockets;
using System.Net;
using WatsonWebsocket;

namespace FF4FE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            server();
        }

        public async void client()
        {

            using (var ws = new ClientWebSocket())
            {
                await ws.ConnectAsync(new Uri("ws://localhost:8765"), CancellationToken.None);
                var buffer = new byte[256];
                while (ws.State == WebSocketState.Open)
                {
                    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    }
                    else
                    {
                        var resp = Encoding.ASCII.GetString(buffer, 0, result.Count);
                        Debug.WriteLine(resp);
                        textBox1.Text = textBox1.Text + resp;
                    }
                }
            }



        }


        public void server()
        {

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            server.Start();

            Byte[] bytes = new Byte[16];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Debug.Write("Waiting for a connection... ");

                TcpClient client = server.AcceptTcpClient();
                Debug.WriteLine("Connected!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Debug.WriteLine("Received: {0}", data);
                }

                client.Close();
            }


        }






    }
}