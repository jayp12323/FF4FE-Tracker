using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FE
{
    public class Town
    {

        public Shops weapon_shop { get; set; }
        public Shops item_shop { get; set; }
        public Shops armor_shop { get; set; }
        public Shops other_shop { get; set; }

        Constants constants = new Constants();


        public Town()
        {
            weapon_shop = new Shops();
            item_shop = new Shops();
            armor_shop = new Shops();
            other_shop = new Shops();
        }

        public void add_shop (int shop_location,List<string> item_list)
        {
            string shop_type;
            if (shop_location == 24)
                shop_type = "other";
            else
                shop_type = determineShopType(item_list[0]);

            switch (shop_type)
            {
                case "weapon":
                    weapon_shop = new Shops(item_list);
                    break;
                case "armor":
                    armor_shop = new Shops(item_list);
                    break;
                case "item":
                    item_shop = new Shops(item_list);
                    break;
                case "other":
                    other_shop = new Shops(item_list);
                    break;
            }

        }




        private string determineShopType(string item_code)
        {
            int item_code_int = int.Parse(item_code, System.Globalization.NumberStyles.HexNumber);
            if (item_code_int < 96)
                return "weapon";
            else if (item_code_int >= 97 && item_code_int < 176)
                return "armor";
            else
                return "item";


        }

        public override string ToString()
        {
            
            string str="";

            str += "Weapon shop contains: " + System.Environment.NewLine;
            str += weapon_shop.ToString();

            str += "Armor shop contains: " + System.Environment.NewLine;
            str += armor_shop.ToString();

            str += "Item shop contains: " + System.Environment.NewLine;
            str += item_shop.ToString();


            str += "Other shop contains: " + System.Environment.NewLine;
            str += other_shop.ToString();

            return str;


        }


    }
}
