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
    [Serializable]
    public class Texture
    {
        Bitmap _textureGrass, _textureDesert, _textureForest, _textureSnow,
            _textureDirt, _textureWaterAnimated,_textureRabbit,_textureElephant,
            _textureCow, _textureCat, _textureDog, _textureEagle,_textureGazelle,
            _textureGiraffe,_textureLion;
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
            _textureGrass = new Bitmap( @"..\..\..\assets\Grass.jpg");

            _textureForest = new Bitmap( @"..\..\..\assets\Forest.jpg" );
            _textureSnow = new Bitmap( @"..\..\..\assets\Snow.jpg" );
            _textureDesert = new Bitmap( @"..\..\..\assets\Desert.jpg" );
            _textureDirt = new Bitmap( @"..\..\..\assets\Dirt.jpg" );

            _textureRabbit = new Bitmap( @"..\..\..\assets\Animal\Rabbit.png" );
            _textureElephant = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );            
            _textureElephant.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureCow = new Bitmap( @"..\..\..\assets\Animal\Cow.png" );
            _textureCow.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureCat = new Bitmap( @"..\..\..\assets\Animal\Cat.png" );
            _textureCat.MakeTransparent(Color.White);
            _textureCat.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureLion = new Bitmap(@"..\..\..\assets\Animal\Lion.png");
            _textureLion.MakeTransparent(Color.White);
            _textureLion.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureEagle = new Bitmap(@"..\..\..\assets\Animal\Eagle.png");
            _textureEagle.MakeTransparent(Color.White);
            _textureEagle.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureGazelle = new Bitmap( @"..\..\..\assets\Animal\Lion.png" );
            _textureGazelle.MakeTransparent(Color.White);
            _textureGazelle.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureDog = new Bitmap( @"..\..\..\assets\Animal\Dog.png" );
            _textureDog.MakeTransparent(Color.White);
            _textureDog.RotateFlip(RotateFlipType.Rotate180FlipY);

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
        public Bitmap GetTexture( Box box )
        {
            switch( box.Ground )
            {
                case BoxGround.Grass:
                    return _textureGrass;
                case BoxGround.Water:
                    return _textureWaterAnimated;
                case BoxGround.Forest:
                    return _textureForest;
                case BoxGround.Snow:
                    return _textureSnow;
                case BoxGround.Dirt:
                    return _textureDirt;
                case BoxGround.Desert:
                    return _textureDesert;
                default:
                    return _textureGrass;
            }
        }

        public Brush GetColor( Box box )
        {
            switch( box.Ground )
            {
                case BoxGround.Grass:
                    return _brushGrass;
                case BoxGround.Water:
                    return _brushWater;
                case BoxGround.Forest:
                    return _brushForest;
                case BoxGround.Snow:
                    return _brushSnow;
                case BoxGround.Dirt:
                    return _brushDirt;
                case BoxGround.Desert:
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
                    return _textureCat;
                case "Elephant":
                    return _textureElephant;
                case "Lion":
                    return _textureLion;
                case "Cow":
                    return _textureCow;
                case "Dog":
                    return _textureDog;
                case "Eagle":
                    return _textureEagle;
                case "Gazelle":
                    return _textureGazelle;
                default:
                    return _textureGrass;
            }
        }
    }
}
