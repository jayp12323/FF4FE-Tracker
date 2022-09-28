using System;

namespace FF4FE
{
    public class Sprites
    {
        public Dictionary<String, Bitmap> sprites;

        public Class1()
        {
            sprites = new Dictionary<String, Bitmap>();
            loadSprites()
    

    }
        void loadSprites()
        {

            string[] fileEntries = Directory.GetFiles("sprites/");
            foreach (string fileName in fileEntries)
                sprites.Add(fileName, new Bitmap(fileName));
        }


    }
}