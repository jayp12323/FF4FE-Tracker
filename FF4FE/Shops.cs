using System;
using System.Diagnostics;

namespace FF4FE
{
	public class Shops
	{
        Constants constants = new Constants();
        private List<string> item_list { get; set; }
        private List<string> item_name_list { get; set; }

        int item_count { get; set; }

        public Shops()
		{
            item_list = new List<string>();
            item_name_list = new List<string>();
        }

        public Shops(List<string> item_list)
		{
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


		public override string ToString()
		{

			string str = "";
			foreach (string item in item_name_list)
				str += item + System.Environment.NewLine;
            return str;


		}
		


	}
}