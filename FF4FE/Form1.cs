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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using Button = System.Windows.Forms.Button;

namespace FF4FE
{

    public partial class Form1 : Form
    {

        Constants constants = new Constants();
        Dictionary<string, Town> towns ;
        String current_view_state="none";
        List<string> others = new List<string>() { "Weapons", "Armor", "Items" };

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(700, 600);
            towns = new();
            foreach (string town in constants.town_names)
                towns.Add(town, new Town());
            location_buttons();
            
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

            updateItemsPane(current_view_state);



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


            }
            buttonsPanel.Controls.Add(buttons_panel_table);

            string[] other_buttons = { "Weapons", "Armor", "Items" };

            int bottom_height = buttonsPanel.Size.Height;
            Point buttons_location = buttonsPanel.Location;
            TableLayoutPanel item_type_panel = new();
            item_type_panel.RowCount = 3;
            item_type_panel.ColumnCount = 1;
            item_type_panel.AutoSize = true;
            item_type_panel.Location= new Point(buttons_location.X+40,buttons_location.Y+20+ bottom_height);
            Debug.WriteLine(buttons_location.X + " "+ buttons_location.Y);

            //foreach(string button in other_buttons)
            for (var i = 0; i < 3; i++)
            {
                Button newButton = new();
                newButton.Name = other_buttons[i];
                newButton.Text = other_buttons[i];
                newButton.Size = new System.Drawing.Size(90, 35);
                newButton.Click += buttonChangeView;
                item_type_panel.Controls.Add(newButton,0,i);

            }


            buttonsPanel.Controls.Add(item_type_panel);


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

            current_view_state = name;

            if (others.Contains(name))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (this.Size.Width != 1500)
                        this.Size = new Size(1500, 900);
                });
                itemsPanel.Invoke((MethodInvoker)delegate
                {
                    if (itemsPanel.Size.Width != 1200)
                        itemsPanel.Size = new Size(1200, 700);

                });
                currentViewingTextbox.Text = name;



                Dictionary<string, List<Tuple<string, Bitmap>>> all_item_type = return_all_type(name);
                TableLayoutPanel stores_table_panel = new();

                stores_table_panel.RowCount = 3;
                stores_table_panel.ColumnCount = 5;
                stores_table_panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                stores_table_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                stores_table_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                stores_table_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                stores_table_panel.Dock = DockStyle.Fill;


                int store_count = all_item_type.Count;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {

                        if(i*5+j > store_count-1)
                        {
                            break;
                        }

                        TableLayoutPanel store_panel = new();

                        store_panel.RowCount = 2;
                        store_panel.ColumnCount = 1;

                        store_panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
                        store_panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        store_panel.Dock = DockStyle.Fill;
                        store_panel.BackColor = Color.DarkBlue;


                        List<Tuple<string, Bitmap>> shop = all_item_type.ElementAt(i * 5 + j).Value;
                        string town_name = all_item_type.ElementAt(i * 5 + j).Key;
                        int count = shop.Count;


                        TextBox town_textbox = new TextBox();

                        town_textbox.Dock = DockStyle.Top;
                        town_textbox.Text = town_name;
                        town_textbox.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Bold);
                        town_textbox.BackColor = Color.Blue;
                        town_textbox.ForeColor = Color.White;
                        town_textbox.BorderStyle = BorderStyle.None;
                        town_textbox.Height = 12;
                        town_textbox.Margin = new Padding(0);


                        TableLayoutPanel item_panel = new();
                        item_panel.BackColor = Color.DarkBlue;
                        item_panel.RowCount = 8;
                        item_panel.ColumnCount = 2;
                        item_panel.Dock = DockStyle.Fill;

                        item_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
                        item_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));



                        for (int k = 0; k < count; k++)
                        {

                            TextBox textbox = new TextBox();

                            //textbox.Dock = DockStyle.Fill;
                            textbox.Text = shop[k].Item1; 
                            textbox.Font = new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold);
                            textbox.BackColor = Color.DarkBlue;
                            textbox.ForeColor = Color.White;
                            textbox.BorderStyle = BorderStyle.None;
                            textbox.Height = 16;
                            textbox.Margin = new Padding(0);


                            PictureBox sprite = new PictureBox();
                            sprite.Image = shop[k].Item2;
                            sprite.Size = new Size(14, 14);
                            sprite.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

                            item_panel.Controls.Add(sprite, 0, k);
                            item_panel.Controls.Add(textbox, 1, k);
                        }

                        store_panel.Controls.Add(town_textbox,0,0);
                        store_panel.Controls.Add(item_panel,1,0);

                        stores_table_panel.Controls.Add(store_panel, j, i);

                    }

                }
                itemsPanel.Invoke((MethodInvoker)delegate
                {
                    itemsPanel.Controls.Clear();
                    itemsPanel.Controls.Add(stores_table_panel);
                });


            }
            else
            {
                currentViewingTextbox.Text = name;

                TableLayoutPanel stores_table_panel = new();
                stores_table_panel.RowCount = 2;
                stores_table_panel.ColumnCount = 2;
                stores_table_panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                stores_table_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                stores_table_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                stores_table_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                stores_table_panel.Dock = DockStyle.Fill;


                Town town = towns[name];
                List<List<Tuple<string, Bitmap>>> shops = new();
                shops.Add(town.weapon_shop.return_items());
                shops.Add(town.item_shop.return_items());
                shops.Add(town.armor_shop.return_items());
                shops.Add(town.other_shop.return_items());


                int i = 0;
                foreach(List<Tuple<string, Bitmap>> shop  in shops)
                {
                    string i_bin = Convert.ToString(i, 2).PadLeft(2, '0');
                    int x = int.Parse(i_bin.Substring(0,1));
                    int y = int.Parse(i_bin.Substring(1, 1));
                    int count = shop.Count();

                    TableLayoutPanel item_panel = new();
                    item_panel.BackColor = Color.DarkBlue;
                    item_panel.RowCount = 8;
                    item_panel.ColumnCount = 2;
                    item_panel.Dock = DockStyle.Fill;


                    item_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
                    item_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
                    
                    for (int j = 0; j < count; j++)
                    {
                        TextBox textbox = new TextBox();
                        textbox.Dock = DockStyle.Fill;
                        textbox.Text = shop[j].Item1; ;
                        textbox.Font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold);
                        textbox.BackColor = Color.DarkBlue;
                        textbox.ForeColor = Color.White;
                        textbox.BorderStyle = BorderStyle.None;
                        textbox.Height=16;
                        textbox.Margin = new Padding(0);


                        PictureBox sprite = new PictureBox();
                        sprite.Image = shop[j].Item2;
                        sprite.Size = new Size(16, 16);
                        sprite.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

                        item_panel.Controls.Add(sprite, 0, j);
                        item_panel.Controls.Add(textbox, 1, j);

                    }
                    stores_table_panel.Controls.Add(item_panel, x, y);

                    i += 1;

                }

                itemsPanel.Invoke((MethodInvoker)delegate
                {
                    itemsPanel.Controls.Clear();
                    itemsPanel.Size = new Size(400, 475);

                    itemsPanel.Controls.Add(stores_table_panel);
                });
                this.Invoke((MethodInvoker)delegate
                {
                    this.Size = new Size(700, 600);
                });

                current_view_state = name;
            }


        }


        public Dictionary<string, List<Tuple<string, Bitmap>>> return_all_type(string type)
        {

            Dictionary<string, List<Tuple<string, Bitmap>>> shops = new();

            foreach(string town in constants.town_names)
            {

                switch(type)
                {
                    case "Weapons":
                        shops.Add(town,towns[town].weapon_shop.return_items());
                        break;

                    case "Armor":
                        if(town.Equals("Fabul"))
                            shops.Add(town, towns[town].weapon_shop.return_items());
                        else
                            shops.Add(town, towns[town].armor_shop.return_items());
                        break;

                    case "Items":
                        shops.Add(town, towns[town].item_shop.return_items());
                        if (town.Equals("Troia"))
                            shops.Add(town+" Cafe", towns[town].other_shop.return_items());
                        break;
                }




            }


            return shops;
        }



    }
}