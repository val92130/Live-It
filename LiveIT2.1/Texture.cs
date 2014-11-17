using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LiveIT2._1
{
    public class Texture
    {
        Bitmap _textureGrass, _textureWater, _textureDesert, _textureForest, _textureSnow, _textureWater2,_textureWater3,_textureWater4,_textureWater5, _textureWaterAnimated;
        Brush _brushGrass, _brushWater, _brushDesert, _brushForest, _brushSnow;
        Bitmap _textureWater6, _textureWater7, _textureWater8, _textureWater9, _textureWater10, _textureWater11, _textureWater12;
       Timer _animate;
        List <Bitmap> _waterList = new List<Bitmap>();
        int count = 0;
        public Texture()
        {
            
            _animate = new Timer();
            _animate.Start();
            _animate.Interval = 10;
            _animate.Tick += new EventHandler(T_animateTick);
            _textureGrass = new Bitmap(@"..\..\..\assets\Grass.jpg");

            _textureForest = new Bitmap(@"..\..\..\assets\Forest.jpg");
            _textureSnow = new Bitmap(@"..\..\..\assets\Snow.jpg");
            _textureDesert = new Bitmap(@"..\..\..\assets\Desert.jpg");

            _brushGrass = new SolidBrush(Color.FromArgb(59, 138, 33));
            _brushWater = new SolidBrush(Color.FromArgb(64, 85, 213));
            _brushDesert = new SolidBrush(Color.FromArgb(173, 128, 109));
            _brushForest = new SolidBrush(Color.FromArgb(110, 121, 53));
            _brushSnow = new SolidBrush(Color.FromArgb(207, 206, 212));

            _textureWaterAnimated = new Bitmap(@"..\..\..\assets\Water\Water.jpg");

            AddTexturesFromFolderToList(@"..\..\..\assets\Animated\", _waterList);
        }

        private void T_animateTick(object sender, EventArgs e)
        {
            //Random rnd = new Random();
            //int r = rnd.Next(_waterList.Count);
            //_textureWaterAnimated = _waterList[r];

            
            if (count + 1  <= _waterList.Count)
            {
                _textureWaterAnimated = _waterList[count];
                count++;
            }
            else
            {
                count = 0;
            }
        }

        public void AddTexturesFromFolderToList(string Directory, List<Bitmap> list)
        {
            string[] filePaths = System.IO.Directory.GetFiles(Directory);
            Array.Sort<string>(filePaths);
            foreach (string images in filePaths)
            {
                Bitmap _tempBmp = new Bitmap(images);
                list.Add(_tempBmp);
            }
        }
        public Bitmap LoadTexture(Box box)
        {
            switch( box.Ground.ToString() )
            {
                case "Grass" :
                    return _textureGrass;
                case "Water" :
                    return _textureWaterAnimated;
                case "Forest" :
                    return _textureForest;
                case "Snow" :
                    return _textureSnow;
                case "Desert" :
                    return _textureDesert;
                default :
                    return _textureGrass;
            }
        }

        public Brush LoadColor(Box box)
        {
            switch (box.Ground.ToString())
            {
                case "Grass":
                    return _brushGrass;
                case "Water":
                    return _brushWater;
                case "Forest":
                    return _brushForest;
                case "Snow":
                    return _brushSnow;
                case "Desert":
                    return _brushDesert;
                default:
                    return _brushGrass;
            }
        }



    }
}
