    using System;
using System.Net.WebSockets;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using System.Net.Sockets;
using System.Net;
using WatsonWebsocket;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Text;

namespace FF4FE
{

    public partial class Form1 : Form
    {

        Constants constants = new Constants();
        Dictionary<string, Town> towns ;
        String current_view_state="none";
        PrivateFontCollection fontCollection;
        public Form1()
        {
            InitializeComponent();
            fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile("font/kongtext.ttf");
            Sprites sprite_list = new Sprites();
            towns = new();
            foreach (string town in constants.town_names)
                towns.Add(town, new Town());
            location_buttons();
            //pictureBox1.Image = sprite_list.spritemap["dagger"];

            server();
        }



        public void server()
        {

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            server.Start();

            Byte[] bytes = new Byte[128];
            String data = null;

            // Enter the listening loop.
            Task myTask = new Task(() =>
            {

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
                        data += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    }
                    process_data(data);

                    client.Close();
                }
            });
            myTask.Start();


        }

        public void process_data(String data)
        {

            //Debug.WriteLine("Received: {0}", data);

            List<string> item_list = data.Split(",").ToList();

            int location = int.Parse(item_list[0]);
            item_list.RemoveAt(0);

            string location_name = constants.shop_names[location];
            string town_name = location_name.Split(" ")[0];
            towns[town_name].add_shop(location, item_list);

            if (town_name.Equals(current_view_state))
            {
                Debug.WriteLine("matched");

                updateItemsPane();

            }



        }

        public void shopTextBoxes(String location, List<string> items)
        {

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {

                }


            }



        }

        public void location_buttons()
        {

            TableLayoutPanel buttons_panel_table = new();
            buttons_panel_table.RowCount = 7;
            buttons_panel_table.ColumnCount = 2;
            buttons_panel_table.AutoSize = true;



            int y = 10;

            int zzz = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    string location = constants.town_names[zzz];

                    Button newButton = new();
                    newButton.Name = location;
                    newButton.Text = location;
                    newButton.Size = new System.Drawing.Size(90, 35);
                    newButton.Click += buttonChangeView;
                    buttons_panel_table.Controls.Add(newButton, j,i);


                    y += 40;
                    zzz += 1;
                }

                buttonsPanel.Controls.Add(buttons_panel_table);

            }

            //string[] other_buttons = { "Weapons", "Armor", "Items" };



        }

        public void buttonChangeView(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string name = btn.Name;
            Debug.WriteLine(name);

            updateItemsPane(name);

        }


        public void updateItemsPane()
        {
            if (current_view_state.Equals("none"))
            {
                return;
            }

            updateItemsPane(current_view_state);
        }
        public void updateItemsPane(string name)
        {

            List<string> others = new List<string>() { "Weapons", "Armor", "Items" };
            if(!others.Contains(name))
            {
                currentViewingTextbox.Text = "Viewing Items for: " + name;

                TableLayoutPanel items_panel_table = new();
                items_panel_table.RowCount = 2;
                items_panel_table.ColumnCount = 2;
                items_panel_table.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                items_panel_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                items_panel_table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                items_panel_table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                items_panel_table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                items_panel_table.Dock = DockStyle.Fill;


                Town town = towns[name];
                Debug.WriteLine(town.weapon_shop.ToString());

                
                TextBox weapontextBox = new TextBox();
                weapontextBox.Multiline = true;
                weapontextBox.Dock = DockStyle.Fill;
                weapontextBox.Text = town.weapon_shop.ToString();
                weapontextBox.Font = new Font(FontFamily.GenericMonospace, 14,FontStyle.Bold);
                weapontextBox.BackColor = Color.DarkBlue;
                weapontextBox.ForeColor = Color.White;
               
                TextBox armortextBox = new TextBox();
                armortextBox.Multiline = true;
                armortextBox.Dock = DockStyle.Fill;
                armortextBox.Text = town.armor_shop.ToString();
                armortextBox.Font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold);
                armortextBox.BackColor = Color.DarkBlue;
                armortextBox.ForeColor = Color.White;

                TextBox itemstextBox = new TextBox();
                itemstextBox.Multiline = true;
                itemstextBox.Dock = DockStyle.Fill;
                itemstextBox.Text = town.item_shop.ToString();
                itemstextBox.Font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold);
                itemstextBox.BackColor = Color.DarkBlue;
                itemstextBox.ForeColor = Color.White;

                TextBox othertextBox = new TextBox();
                othertextBox.Multiline = true;
                othertextBox.Dock = DockStyle.Fill;
                othertextBox.Text = town.other_shop.ToString();
                othertextBox.Font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold);
                othertextBox.BackColor = Color.DarkBlue;
                othertextBox.ForeColor = Color.White;


                items_panel_table.Controls.Add(weapontextBox, 0, 0);
                items_panel_table.Controls.Add(armortextBox, 1, 0);
                items_panel_table.Controls.Add(itemstextBox, 0, 1);
                items_panel_table.Controls.Add(othertextBox, 1, 1);

                itemsPanel.Invoke((MethodInvoker)delegate
                {
                    itemsPanel.Controls.Clear();
                    itemsPanel.Controls.Add(items_panel_table);
                });
                current_view_state = name;
            }


        }

    }
}