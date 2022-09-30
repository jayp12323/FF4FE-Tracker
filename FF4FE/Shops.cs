using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;

namespace FF4FE
{
	public class Shops
	{
        Constants constants = new Constants();

        private List<string> item_list { get; set; }
        private List<string> item_name_list { get; set; }
        Sprites sprites = new Sprites();
		Dictionary<String, Bitmap> sprite_list;

        int item_count { get; set; }

        public Shops()
		{
            item_list = new List<string>();
            item_name_list = new List<string>();
        }

        public Shops(List<string> item_list)
		{
            sprite_list = sprites.loadSprites();

            this.item_list = item_list;
			this.item_name_list = new();


            int count = 0;
			foreach (string item in item_list)
			{ 
				if (!item.Equals("0"))
				{
					item_name_list.Add(constants.itemsNameFromHex[item]);
					count = count + 1;
				}

			}
            this.item_count = count;



		}

		public List<Tuple<string,Bitmap>> return_items()
		{
			List<Tuple<string, Bitmap>> items = new();

            for (int i = 0; i < item_count; i++)
			{
				Bitmap sprite;
				string item_id = item_list[i];
				string sprite_hex = constants.spritefromhex[item_id];
                if (sprite_list.ContainsKey(sprite_hex))
				{
					sprite = sprite_list[sprite_hex];
				}
				else
				{
                    sprite = new(16, 16);
                    sprite.MakeTransparent();
                }

                Tuple<string, Bitmap> item = new Tuple<string, Bitmap>(item_name_list[i], sprite );

				items.Add(item);

			}

			return items;

		}

		public override string ToString()
		{

			string str = "";
			foreach (string item in item_name_list)
				str += item + System.Environment.NewLine;
            return str;


		}
		


	}
}