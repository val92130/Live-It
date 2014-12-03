﻿using System;
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
        Bitmap _textureGrass,_textureGrass2, _textureDesert, _textureForest, _textureSnow, _textureMountain,
            _textureDirt, _textureWaterAnimated,_textureRabbit,_textureElephant,
            _textureCow, _textureCat, _textureDog, _textureEagle,_textureGazelle,
            _textureGiraffe,_textureLion, _textureRain,_textureThunder ;
        Bitmap _textureTree, _textureTree2, _textureTree3, _textureBush, _textureRock, _textureRock2, _textureRock3;
        Bitmap _textureCatLeft, _textureCatUp, _textureCatDown, _textureCatRight;
        Bitmap _textureDogLeft, _textureDogUp, _textureDogDown, _textureDogRight;
        Bitmap _textureEagleLeft, _textureEagleUp, _textureEagleDown, _textureCowRight;

        Brush _brushGrass, _brushWater, _brushDesert, _brushForest, _brushSnow, _brushDirt;
        Timer _animate, _rainTimer, _thunderTimer;
        Timer _animateCat;
        List <Bitmap> _waterList = new List<Bitmap>();
        List<Bitmap> _rainList = new List<Bitmap>();
        List<Bitmap> _thunderList = new List<Bitmap>();

        List<Bitmap> _catLeftList = new List<Bitmap>();
        List<Bitmap> _catUpList = new List<Bitmap>();
        List<Bitmap> _catDownList = new List<Bitmap>();
        List<Bitmap> _catRightList = new List<Bitmap>();

        List<Bitmap> _dogLeftList = new List<Bitmap>();
        List<Bitmap> _dogUpList = new List<Bitmap>();
        List<Bitmap> _dogDownList = new List<Bitmap>();
        List<Bitmap> _dogRightList = new List<Bitmap>();

        List<Bitmap> _eagleLeftList = new List<Bitmap>();
        List<Bitmap> _eagleUpList = new List<Bitmap>();
        List<Bitmap> _eagleDownList = new List<Bitmap>();
        List<Bitmap> _eagleRightList = new List<Bitmap>();

        int count = 0;
        int count2 = 0;
        int count3 = 0;

        int _countAnimal;

        public Texture()
        {
            _thunderTimer = new Timer();
            _animate = new Timer();
            _animate.Start();
            _animate.Interval = 10;
            _rainTimer = new Timer();

            _animateCat = new Timer();
            _animateCat.Interval = 10;
            _animateCat.Start();
            _animateCat.Tick += new EventHandler( T_Cat_Anim );

            _rainTimer.Start();
            _rainTimer.Interval = 10;
            _rainTimer.Tick += new EventHandler( T_rain_tick );

            _thunderTimer.Start();
            _thunderTimer.Interval = 10;
            _thunderTimer.Tick += new EventHandler(T_Thunder_tick);

            _animate.Tick += new EventHandler( T_animateTick );
            _textureGrass = new Bitmap( @"..\..\..\assets\Grass.jpg");
            _textureGrass2 = new Bitmap(@"..\..\..\assets\Grass2.jpg");
            _textureForest = new Bitmap( @"..\..\..\assets\Forest.jpg" );
            _textureSnow = new Bitmap( @"..\..\..\assets\Snow.jpg" );
            _textureDesert = new Bitmap( @"..\..\..\assets\Desert.jpg" );
            _textureDirt = new Bitmap( @"..\..\..\assets\Dirt.jpg" );
            _textureMountain = new Bitmap(@"..\..\..\assets\Mountain.jpg");

            _textureTree = new Bitmap( @"..\..\..\assets\Vegetation\Tree1.gif" );
            _textureTree2 = new Bitmap( @"..\..\..\assets\Vegetation\Tree2.gif" );
            _textureTree3 = new Bitmap( @"..\..\..\assets\Vegetation\Tree3.gif" );
            _textureBush = new Bitmap( @"..\..\..\assets\Vegetation\Bush1.gif" );
            _textureRock = new Bitmap( @"..\..\..\assets\Vegetation\Rock1.gif" );
            _textureRock2 = new Bitmap( @"..\..\..\assets\Vegetation\Rock2.gif" );
            _textureRock3 = new Bitmap( @"..\..\..\assets\Vegetation\Rock3.gif" );

            _textureRabbit = new Bitmap( @"..\..\..\assets\Animal\Rabbit.png" );
            _textureElephant = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );            
            _textureElephant.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureCow = new Bitmap( @"..\..\..\assets\Animal\Cow.png" );
            _textureCow.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureCat = new Bitmap( @"..\..\..\assets\Animal\Cat.png" );
            _textureCat.MakeTransparent(Color.White);
            _textureCat.RotateFlip(RotateFlipType.Rotate180FlipY);


            _textureCatDown = new Bitmap( @"..\..\..\assets\Animal\Cat\Cat-Down\a.png" );
            _textureCatUp = new Bitmap( @"..\..\..\assets\Animal\Cat\Cat-Up\a.png" );
            _textureCatLeft = new Bitmap( @"..\..\..\assets\Animal\Cat\Cat-Left\a.png" );
            _textureCatRight = new Bitmap( @"..\..\..\assets\Animal\Cat\Cat-Right\a.png" );

               

            _textureDogDown = new Bitmap( @"..\..\..\assets\Animal\Dog\Dog-Down\a.png" );
            _textureDogUp = new Bitmap( @"..\..\..\assets\Animal\Dog\Dog-Up\a.png" );
            _textureDogLeft = new Bitmap( @"..\..\..\assets\Animal\Dog\Dog-Left\a.png" );
            _textureDogRight = new Bitmap( @"..\..\..\assets\Animal\Dog\Dog-Right\a.png" );

            _textureEagleDown = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Down\a.png");
            _textureEagleUp = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Up\a.png");
            _textureEagleLeft = new Bitmap(@"..\..\..\assets\Animal\Eagle\Dog-Left\a.png");
            _textureCowRight = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Right\a.png");

            _textureLion = new Bitmap(@"..\..\..\assets\Animal\Lion.png");
            _textureLion.MakeTransparent(Color.White);
            _textureLion.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureEagle = new Bitmap(@"..\..\..\assets\Animal\Eagle.png");
            _textureEagle.MakeTransparent(Color.White);
            _textureEagle.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureGazelle = new Bitmap( @"..\..\..\assets\Animal\Gazelle.png" );
            _textureGazelle.MakeTransparent(Color.White);
            _textureGazelle.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureDog = new Bitmap( @"..\..\..\assets\Animal\Dog.png" );
            _textureDog.MakeTransparent(Color.White);
            _textureDog.RotateFlip(RotateFlipType.Rotate180FlipY);

            _textureGiraffe = new Bitmap( @"..\..\..\assets\Animal\Elephant.png" );
 

            _brushGrass = new SolidBrush( Color.FromArgb( 59, 138, 33 ) );
            _brushDirt = new SolidBrush( Color.FromArgb( 169, 144, 104 ) );
            _brushWater = new SolidBrush( Color.FromArgb( 64, 85, 213 ) );
            _brushDesert = new SolidBrush( Color.FromArgb( 173, 128, 109 ) );
            _brushForest = new SolidBrush( Color.FromArgb( 110, 121, 53 ) );
            _brushSnow = new SolidBrush( Color.FromArgb( 207, 206, 212 ) );

           _textureRain = new Bitmap( @"..\..\..\assets\Rain\0.gif" );
           _textureRain.MakeTransparent( Color.Black );

            _textureWaterAnimated = new Bitmap( @"..\..\..\assets\Water\Water.jpg" );

            _textureThunder = new Bitmap(@"..\..\..\assets\Thunder\0.gif");
            _textureThunder.MakeTransparent(Color.Black);

            AddTexturesFromFolderToList( @"..\..\..\assets\Animated\", _waterList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Rain\", _rainList );
            AddTexturesFromFolderToList(@"..\..\..\assets\Thunder\", _thunderList);

            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Cat\Cat-Left\", _catLeftList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Cat\Cat-Right\", _catRightList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Cat\Cat-Down\", _catDownList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Cat\Cat-Up\", _catUpList );

            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Dog\Dog-Left\", _dogLeftList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Dog\Dog-Right\", _dogRightList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Dog\Dog-Down\", _dogDownList );
            AddTexturesFromFolderToList( @"..\..\..\assets\Animal\Dog\Dog-Up\", _dogUpList );

            AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Left\", _dogLeftList);
            AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Right\", _dogRightList);
            AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Down\", _dogDownList);
            AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Up\", _dogUpList);
        }

        private void T_Cat_Anim( object sender, EventArgs e )
        {
            if( _countAnimal + 1 <= _catDownList.Count )
            {
                _textureCatDown = _catDownList[_countAnimal];
                _catDownList[_countAnimal].MakeTransparent( Color.White );

                _textureCatLeft = _catLeftList[_countAnimal];
                _catLeftList[_countAnimal].MakeTransparent( Color.White );

                _textureCatRight = _catRightList[_countAnimal];
                _catRightList[_countAnimal].MakeTransparent( Color.White );

                _textureCatUp = _catUpList[_countAnimal];
                _catUpList[_countAnimal].MakeTransparent( Color.White );

                _textureDogDown = _dogDownList[_countAnimal];

                _textureDogLeft = _dogLeftList[_countAnimal];

                _textureDogRight = _dogRightList[_countAnimal];

                _textureDogUp = _dogUpList[_countAnimal];
                _countAnimal++;
            }
            else
            {
                _countAnimal = 0;
            }
        }

        private void T_rain_tick( object sender, EventArgs e )
        {
            if( count2 + 1 <= _rainList.Count )
            {
                _rainList[count2].MakeTransparent( Color.Black );
                _textureRain = _rainList[count2];

                count2++;
            }
            else
            {
                count2 = 0;
            }
        }
        private void T_Thunder_tick(object sender, EventArgs e)
        {
            if (count3 + 1 <= _thunderList.Count)
            {
                _thunderList[count3].MakeTransparent(Color.FromArgb(1, 2, 3));
                _textureThunder = _thunderList[count3];

                count3++;
            }
            else
            {
                count3 = 0;
            }
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
                case BoxGround.Grass2:
                    return _textureGrass2;
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
                case BoxGround.Mountain:
                    return _textureMountain;
                default:
                    return _textureGrass;
            }
        }

        public Bitmap GetTexture( Vegetation vegetation )
        {
            switch( vegetation.Texture )
            {
                case VegetationTexture.Tree:
                    return _textureTree;
                case VegetationTexture.Tree2:
                    return _textureTree2;
                case VegetationTexture.Tree3:
                    return _textureTree3;
                case VegetationTexture.Bush :
                    return _textureBush;
                case VegetationTexture.Rock :
                    return _textureRock;
                case VegetationTexture.Rock2:
                    return _textureRock2;
                case VegetationTexture.Rock3:
                    return _textureRock3;
                default:
                    return _textureTree;
            }
        }

        public Brush GetColor( Box box )
        {
            switch( box.Ground )
            {
                case BoxGround.Grass:
                    return _brushGrass;
                case BoxGround.Grass2:
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
                case BoxGround.Mountain:
                    return _brushDirt;
                default:
                    return _brushGrass;
            }
        }
        public Bitmap LoadTexture( Animal animal )
        {
            switch( animal.Texture )
            {
                case AnimalTexture.Rabbit:
                    return _textureRabbit;
                case AnimalTexture.Cat:
                    switch( animal.MovingDirection )
                    {
                        case MovingDirection.Left :
                            return _textureCatLeft;
                        case MovingDirection.Up :
                            return _textureCatUp;
                        case MovingDirection.Down:
                            return _textureCatDown;
                        case MovingDirection.Right :
                            return _textureCatRight;
                        default :
                            throw new NotSupportedException( "No texture found for this direction" );
                    }
                case AnimalTexture.Elephant:
                    return _textureElephant;
                case AnimalTexture.Lion:
                    return _textureLion;
                case AnimalTexture.Cow:
                    return _textureCow;
                case AnimalTexture.Dog:
                    switch( animal.MovingDirection )
                    {
                        case MovingDirection.Left:
                            return _textureDogLeft;
                        case MovingDirection.Up:
                            return _textureDogUp;
                        case MovingDirection.Down:
                            return _textureDogDown;
                        case MovingDirection.Right:
                            return _textureDogRight;
                        default:
                            throw new NotSupportedException( "No texture found for this direction" );
                    }
                case AnimalTexture.Eagle:
                    return _textureEagle;
                case AnimalTexture.Gazelle:
                    return _textureGazelle;
                default:
                    return _textureGrass;
            }
        }

        public Bitmap GetRain()
        {
            return _textureRain;
        }
        public Bitmap GetThunder()
        {
            return _textureThunder;
        }
    }
}
