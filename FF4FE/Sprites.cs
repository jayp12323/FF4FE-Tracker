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
            loadSprites();

    


        }

        void loadSprites()
        {

            string[] fileEntries = Directory.GetFiles("sprites/");
            foreach (string fileName in fileEntries)
                spritemap.Add(fileName.Substring(8).ToLower().Replace(".gif",""), new Bitmap(fileName));
        }
    }

}
