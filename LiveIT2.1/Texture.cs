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
        Bitmap _textureGrass, _textureWater, _textureDesert, _textureForest, _textureSnow,
            _textureDirt, _textureWaterAnimated,_textureRabbit,_textureElephant,
            _textureCow, _textureCat, _textureDog, _textureEagle,_textureFinch,_textureFlamingo,
            _textureGiraffe, _textureLeopard,_textureLion,_textureLizards, _textureMonkey,
            _textureRaccons, _textureTiger,_textureZebra;
        Brush _brushGrass, _brushWater, _brushDesert, _brushForest, _brushSnow, _brushDirt;
        Timer _animate;
        List <Bitmap> _waterList = new List<Bitmap>();
        int count = 0;
        public Texture()
        {

            _animate = new Timer();
            _animate.Start();
            _animate.Interval = 10;
            _animate.Tick += new EventHandler( T_animateTick );
            _textureGrass = new Bitmap( @"..\..\..\assets\Grass.jpg" );

            _textureForest = new Bitmap( @"..\..\..\assets\Forest.jpg" );
            _textureSnow = new Bitmap( @"..\..\..\assets\Snow.jpg" );
            _textureDesert = new Bitmap( @"..\..\..\assets\Desert.jpg" );
            _textureDirt = new Bitmap( @"..\..\..\assets\Dirt.jpg" );

            _textureRabbit = new Bitmap( @"..\..\..\assets\Animal\Rabbit.png" );
            _textureElephant = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );

            _textureCow = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureCat = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureDog = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureEagle = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureFinch = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureFlamingo = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureGiraffe = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureLion = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureLizards = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureMonkey = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureLeopard = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureRaccons = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureTiger = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
            _textureZebra = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );

            _brushGrass = new SolidBrush( Color.FromArgb( 59, 138, 33 ) );
            _brushDirt = new SolidBrush( Color.FromArgb( 169, 144, 104 ) );
            _brushWater = new SolidBrush( Color.FromArgb( 64, 85, 213 ) );
            _brushDesert = new SolidBrush( Color.FromArgb( 173, 128, 109 ) );
            _brushForest = new SolidBrush( Color.FromArgb( 110, 121, 53 ) );
            _brushSnow = new SolidBrush( Color.FromArgb( 207, 206, 212 ) );

            _textureWaterAnimated = new Bitmap( @"..\..\..\assets\Water\Water.jpg" );

            AddTexturesFromFolderToList( @"..\..\..\assets\Animated\", _waterList );
        }

        private void T_animateTick( object sender, EventArgs e )
        {
            if( count + 1 <= _waterList.Count )
            {
                _textureWaterAnimated = _waterList[count];
                count++;
            }
            else
            {
                count = 0;
            }
        }

        public void AddTexturesFromFolderToList( string Directory, List<Bitmap> list )
        {
            string[] filePaths = System.IO.Directory.GetFiles( Directory );
            Array.Sort<string>( filePaths );
            foreach( string images in filePaths )
            {
                Bitmap _tempBmp = new Bitmap( images );
                list.Add( _tempBmp );
            }
        }
        public Bitmap LoadTexture( Box box )
        {
            switch( box.Ground.ToString() )
            {
                case "Grass":
                    return _textureGrass;
                case "Water":
                    return _textureWaterAnimated;
                case "Forest":
                    return _textureForest;
                case "Snow":
                    return _textureSnow;
                case "Dirt":
                    return _textureDirt;
                case "Desert":
                    return _textureDesert;
                default:
                    return _textureGrass;
            }
        }

        public Brush LoadColor( Box box )
        {
            switch( box.Ground.ToString() )
            {
                case "Grass":
                    return _brushGrass;
                case "Water":
                    return _brushWater;
                case "Forest":
                    return _brushForest;
                case "Snow":
                    return _brushSnow;
                case "Dirt":
                    return _brushDirt;
                case "Desert":
                    return _brushDesert;
                default:
                    return _brushGrass;
            }
        }
        public Bitmap LoadTexture( Animal animal )
        {
            switch( animal.Texture.ToString() )
            {
                case "Rabbit":
                    return _textureRabbit;
                case "Cat":
                    return _textureWaterAnimated;
                case "Elephant":
                    return _textureElephant;
                case "Lion":
                    return _textureSnow;
                case "Cow":
                    return _textureDirt;
                case "Dog":
                    return _textureDesert;
                default:
                    return _textureGrass;
            }
        }


    }
}
