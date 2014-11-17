using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    public class Texture
    {
        Bitmap _textureGrass, _textureWater, _textureDesert, _textureForest, _textureSnow;
        Brush _brushGrass, _brushWater, _brushDesert, _brushForest, _brushSnow;

        public Bitmap LoadTexture(Box box)
        {
            switch( box.Ground.ToString() )
            {
                case "Grass" :
                    return _textureGrass;
                case "Water" :
                    return _textureWater;
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

        public Texture()
        {
            _textureGrass = new Bitmap(@"..\..\..\assets\Grass.jpg");
            _textureWater = new Bitmap( @"..\..\..\assets\Water.jpg" );
            _textureForest = new Bitmap( @"..\..\..\assets\Forest.jpg" );
            _textureSnow = new Bitmap( @"..\..\..\assets\Snow.jpg" );
            _textureDesert = new Bitmap( @"..\..\..\assets\Desert.jpg" );

            _brushGrass = new SolidBrush( Color.FromArgb( 59, 138, 33 ) );
            _brushWater = new SolidBrush( Color.FromArgb(64, 85, 213) );
            _brushDesert = new SolidBrush( Color.FromArgb( 173, 128, 109 ) );
            _brushForest = new SolidBrush( Color.FromArgb( 110, 121, 53 ) );
            _brushSnow = new SolidBrush( Color.FromArgb( 207, 206, 212 ) );
        }

    }
}
