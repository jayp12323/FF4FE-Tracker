using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FE
{

    public class Sprites
    {

       public Dictionary<String, Bitmap> spritemap;
        public Sprites()
        {
            spritemap = new Dictionary<String, Bitmap>();
        }

        public Dictionary<String, Bitmap> loadSprites()
        {

            string[] fileEntries = Directory.GetFiles("sprites/");
            foreach (string fileName in fileEntries)
            {
                Bitmap sprite = new Bitmap(fileName);
                Bitmap resized = new Bitmap(sprite, new Size(16,16));
                spritemap.Add(fileName.Substring(8).ToLower().Replace(".gif", ""), resized);
            }
            return spritemap;
        }
    }

}
