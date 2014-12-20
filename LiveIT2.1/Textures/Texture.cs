// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Texture.cs" company="">
//   
// </copyright>
// <summary>
//   The texture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Textures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using LiveIT2._1.Animals;
    using LiveIT2._1.Enums;
    using LiveIT2._1.Player;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Vegetation;
    using LiveIT2._1.Vehicules;

    /// <summary>
    ///     The texture.
    /// </summary>
    [Serializable]
    public class Texture
    {
        #region Fields

        /// <summary>
        ///     The _brush desert.
        /// </summary>
        private readonly Brush _brushDesert;

        /// <summary>
        ///     The _brush dirt.
        /// </summary>
        private readonly Brush _brushDirt;

        /// <summary>
        ///     The _brush forest.
        /// </summary>
        private readonly Brush _brushForest;

        /// <summary>
        ///     The _brush grass.
        /// </summary>
        private readonly Brush _brushGrass;

        /// <summary>
        ///     The _brush snow.
        /// </summary>
        private readonly Brush _brushSnow;

        /// <summary>
        ///     The _brush water.
        /// </summary>
        private readonly Brush _brushWater;

       


        /// <summary> 
        ///     The _car down list.
        /// </summary>
        private readonly List<Bitmap> _carDownList = new List<Bitmap>();

        /// <summary>
        ///     The _car left list.
        /// </summary>
        private readonly List<Bitmap> _carLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _car right list.
        /// </summary>
        private readonly List<Bitmap> _carRightList = new List<Bitmap>();

        /// <summary>
        ///     The _car up list.
        /// </summary>
        private readonly List<Bitmap> _carUpList = new List<Bitmap>();

        /// <summary>
        ///     The _cat down list.
        /// </summary>
        private readonly List<Bitmap> _catDownList = new List<Bitmap>();

        /// <summary>
        ///     The _cat left list.
        /// </summary>
        private readonly List<Bitmap> _catLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _cat right list.
        /// </summary>
        private readonly List<Bitmap> _catRightList = new List<Bitmap>();

        /// <summary>
        ///     The _cat up list.
        /// </summary>
        private readonly List<Bitmap> _catUpList = new List<Bitmap>();

        /// <summary>
        ///     The _cow down list.
        /// </summary>
        private readonly List<Bitmap> _cowDownList = new List<Bitmap>();

        /// <summary>
        ///     The _cow left list.
        /// </summary>
        private readonly List<Bitmap> _cowLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _cow right list.
        /// </summary>
        private readonly List<Bitmap> _cowRightList = new List<Bitmap>();

        /// <summary>
        ///     The _cow up list.
        /// </summary>
        private readonly List<Bitmap> _cowUpList = new List<Bitmap>();

        /// <summary>
        ///     The _dog down list.
        /// </summary>
        private readonly List<Bitmap> _dogDownList = new List<Bitmap>();

        /// <summary>
        ///     The _dog left list.
        /// </summary>
        private readonly List<Bitmap> _dogLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _dog right list.
        /// </summary>
        private readonly List<Bitmap> _dogRightList = new List<Bitmap>();

        /// <summary>
        ///     The _dog up list.
        /// </summary>
        private readonly List<Bitmap> _dogUpList = new List<Bitmap>();

        /// <summary>
        ///     The _eagle down list.
        /// </summary>
        private readonly List<Bitmap> _eagleDownList = new List<Bitmap>();

        /// <summary>
        ///     The _eagle left list.
        /// </summary>
        private readonly List<Bitmap> _eagleLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _eagle right list.
        /// </summary>
        private readonly List<Bitmap> _eagleRightList = new List<Bitmap>();

        /// <summary>
        ///     The _eagle up list.
        /// </summary>
        private readonly List<Bitmap> _eagleUpList = new List<Bitmap>();

        /// <summary>
        ///     The _elephant down list.
        /// </summary>
        private readonly List<Bitmap> _elephantDownList = new List<Bitmap>();

        /// <summary>
        ///     The _elephant left list.
        /// </summary>
        private readonly List<Bitmap> _elephantLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _elephant right list.
        /// </summary>
        private readonly List<Bitmap> _elephantRightList = new List<Bitmap>();

        /// <summary>
        ///     The _elephant up list.
        /// </summary>
        private readonly List<Bitmap> _elephantUpList = new List<Bitmap>();

        /// <summary>
        ///     The _gazelle down list.
        /// </summary>
        private readonly List<Bitmap> _gazelleDownList = new List<Bitmap>();

        /// <summary>
        ///     The _gazelle left list.
        /// </summary>
        private readonly List<Bitmap> _gazelleLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _gazelle right list.
        /// </summary>
        private readonly List<Bitmap> _gazelleRightList = new List<Bitmap>();

        /// <summary>
        ///     The _gazelle up list.
        /// </summary>
        private readonly List<Bitmap> _gazelleUpList = new List<Bitmap>();

        /// <summary>
        ///     The _lion down list.
        /// </summary>
        private readonly List<Bitmap> _lionDownList = new List<Bitmap>();

        /// <summary>
        ///     The _lion left list.
        /// </summary>
        private readonly List<Bitmap> _lionLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _lion right list.
        /// </summary>
        private readonly List<Bitmap> _lionRightList = new List<Bitmap>();

        /// <summary>
        ///     The _lion up list.
        /// </summary>
        private readonly List<Bitmap> _lionUpList = new List<Bitmap>();

        /// <summary>
        ///     The _player car texture.
        /// </summary>
        private readonly Bitmap _playerCarTexture;

        private readonly Bitmap _floorTexture;

        private readonly Bitmap _floor2Texture;

        private readonly Bitmap _wallTexture;

        private readonly Bitmap _exitTexture;
        /// <summary>
        ///     The _player down list.
        /// </summary>
        private readonly List<Bitmap> _playerDownList = new List<Bitmap>();

        /// <summary>
        ///     The _player left list.
        /// </summary>
        private readonly List<Bitmap> _playerLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _player right list.
        /// </summary>
        private readonly List<Bitmap> _playerRightList = new List<Bitmap>();

        /// <summary>
        ///     The _player texture.
        /// </summary>
        private readonly Bitmap _playerTexture;

        private readonly List<Bitmap> spiList = new List<Bitmap>();

        /// <summary>
        /// Missiles images used for the tank
        /// </summary>
        private readonly Bitmap _missileUp, _missileDown, _missileLeft, _missileRight;

        /// <summary>
        ///     The _player up list.
        /// </summary>
        private readonly List<Bitmap> _playerUpList = new List<Bitmap>();

        /// <summary>
        ///     The _rabbit down list.
        /// </summary>
        private readonly List<Bitmap> _rabbitDownList = new List<Bitmap>();

        /// <summary>
        ///     The _rabbit left list.
        /// </summary>
        private readonly List<Bitmap> _rabbitLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _rabbit right list.
        /// </summary>
        private readonly List<Bitmap> _rabbitRightList = new List<Bitmap>();

        /// <summary>
        ///     The _rabbit up list.
        /// </summary>
        private readonly List<Bitmap> _rabbitUpList = new List<Bitmap>();

        /// <summary>
        ///     The _rain list.
        /// </summary>
        private readonly List<Bitmap> _rainList = new List<Bitmap>();

        /// <summary>
        ///     The _texture blood.
        /// </summary>
        private readonly Bitmap _textureBlood;

        /// <summary>
        ///     The _texture bush.
        /// </summary>
        private readonly Bitmap _textureBush;

        /// <summary>
        ///     The _texture desert.
        /// </summary>
        private readonly Bitmap _textureDesert;

        /// <summary>
        ///     The _texture dirt.
        /// </summary>
        private readonly Bitmap _textureDirt;

        /// <summary>
        ///     The _texture forest.
        /// </summary>
        private readonly Bitmap _textureForest;

        /// <summary>
        ///     The _texture grass.
        /// </summary>
        private readonly Bitmap _textureGrass;

        /// <summary>
        ///     The _texture grass 2.
        /// </summary>
        private readonly Bitmap _textureGrass2;

        /// <summary>
        ///     The _texture mountain.
        /// </summary>
        private readonly Bitmap _textureMountain;

        /// <summary>
        ///     The _texture player idle.
        /// </summary>
        private readonly Bitmap _texturePlayerIdle;

        /// <summary>
        ///     The _texture rock.
        /// </summary>
        private readonly Bitmap _textureRock;

        /// <summary>
        ///     The _texture rock 2.
        /// </summary>
        private readonly Bitmap _textureRock2;

        /// <summary>
        ///     The _texture rock 3.
        /// </summary>
        private readonly Bitmap _textureRock3;

        private readonly Bitmap _textureHouse;
        private readonly Bitmap _chair;
        private readonly Bitmap _barrel;
        private readonly Bitmap _flowerPot;
        private readonly Bitmap _flowerPot2;
        private readonly Bitmap _shelf;
        private readonly Bitmap _table;
        private readonly Bitmap _table2;

        /// <summary>
        ///     The _texture snow.
        /// </summary>
        private readonly Bitmap _textureSnow;

        /// <summary>
        ///     The _texture tank down.
        /// </summary>
        private readonly Bitmap _textureTankDown;

        /// <summary>
        ///     The _texture tank left.
        /// </summary>
        private readonly Bitmap _textureTankLeft;

        /// <summary>
        ///     The _texture tank right.
        /// </summary>
        private readonly Bitmap _textureTankRight;

        /// <summary>
        ///     The _texture tank up.
        /// </summary>
        private readonly Bitmap _textureTankUp;

        /// <summary>
        ///     The _texture tree.
        /// </summary>
        private readonly Bitmap _textureTree;

        /// <summary>
        ///     The _texture tree 2.
        /// </summary>
        private readonly Bitmap _textureTree2;

        /// <summary>
        ///     The _texture tree 3.
        /// </summary>
        private readonly Bitmap _textureTree3;

        /// <summary>
        ///     The _thunder list.
        /// </summary>
        private readonly List<Bitmap> _thunderList = new List<Bitmap>();

        /// <summary>
        ///     The _water list.
        /// </summary>
        private readonly List<Bitmap> _waterList = new List<Bitmap>();

        /// <summary>
        ///     The _animate.
        /// </summary>
        private Timer _animate;

        /// <summary>
        ///     The _animate animal.
        /// </summary>
        private Timer _animateAnimal;

        /// <summary>
        ///     The _animate car.
        /// </summary>
        private Timer _animateCar;

        /// <summary>
        ///     The _animate player.
        /// </summary>
        private Timer _animatePlayer;

        /// <summary>
        ///     The _count animal.
        /// </summary>
        private int _countAnimal;


        /// <summary>
        ///     The _rain timer.
        /// </summary>
        private Timer _rainTimer;

        /// <summary>
        ///     The _tank down list.
        /// </summary>
        private List<Bitmap> _tankDownList = new List<Bitmap>();

        /// <summary>
        ///     The _tank left list.
        /// </summary>
        private List<Bitmap> _tankLeftList = new List<Bitmap>();

        /// <summary>
        ///     The _tank right list.
        /// </summary>
        private List<Bitmap> _tankRightList = new List<Bitmap>();

        /// <summary>
        ///     The _tank up list.
        /// </summary>
        private List<Bitmap> _tankUpList = new List<Bitmap>();

        /// <summary>
        ///     The _texture car down.
        /// </summary>
        private Bitmap _textureCarDown;

        /// <summary>
        ///     The _texture car left.
        /// </summary>
        private Bitmap _textureCarLeft;

        /// <summary>
        ///     The _texture car right.
        /// </summary>
        private Bitmap _textureCarRight;

        /// <summary>
        ///     The _texture car up.
        /// </summary>
        private Bitmap _textureCarUp;

        /// <summary>
        ///     The _texture cat.
        /// </summary>
        private Bitmap _textureCat;

        /// <summary>
        ///     The _texture cat down.
        /// </summary>
        private Bitmap _textureCatDown;

        /// <summary>
        ///     The _texture cat left.
        /// </summary>
        private Bitmap _textureCatLeft;

        /// <summary>
        ///     The _texture cat right.
        /// </summary>
        private Bitmap _textureCatRight;

        /// <summary>
        ///     The _texture cat up.
        /// </summary>
        private Bitmap _textureCatUp;

        /// <summary>
        ///     The _texture cow.
        /// </summary>
        private Bitmap _textureCow;

        /// <summary>
        ///     The _texture cow down.
        /// </summary>
        private Bitmap _textureCowDown;

        /// <summary>
        ///     The _texture cow left.
        /// </summary>
        private Bitmap _textureCowLeft;

        /// <summary>
        ///     The _texture cow right.
        /// </summary>
        private Bitmap _textureCowRight;

        /// <summary>
        ///     The _texture cow up.
        /// </summary>
        private Bitmap _textureCowUp;

        /// <summary>
        ///     The _texture dog.
        /// </summary>
        private Bitmap _textureDog;

        /// <summary>
        ///     The _texture dog down.
        /// </summary>
        private Bitmap _textureDogDown;

        /// <summary>
        ///     The _texture dog left.
        /// </summary>
        private Bitmap _textureDogLeft;

        /// <summary>
        ///     The _texture dog right.
        /// </summary>
        private Bitmap _textureDogRight;

        /// <summary>
        ///     The _texture dog up.
        /// </summary>
        private Bitmap _textureDogUp;

        /// <summary>
        ///     The _texture eagle.
        /// </summary>
        private Bitmap _textureEagle;

        /// <summary>
        ///     The _texture eagle down.
        /// </summary>
        private Bitmap _textureEagleDown;

        /// <summary>
        ///     The _texture eagle left.
        /// </summary>
        private Bitmap _textureEagleLeft;

        /// <summary>
        ///     The _texture eagle right.
        /// </summary>
        private Bitmap _textureEagleRight;

        /// <summary>
        ///     The _texture eagle up.
        /// </summary>
        private Bitmap _textureEagleUp;

        /// <summary>
        ///     The _texture elephant.
        /// </summary>
        private Bitmap _textureElephant;

        /// <summary>
        ///     The _texture elephant down.
        /// </summary>
        private Bitmap _textureElephantDown;

        /// <summary>
        ///     The _texture elephant left.
        /// </summary>
        private Bitmap _textureElephantLeft;

        /// <summary>
        ///     The _texture elephant right.
        /// </summary>
        private Bitmap _textureElephantRight;

        /// <summary>
        ///     The _texture elephant up.
        /// </summary>
        private Bitmap _textureElephantUp;

        /// <summary>
        ///     The _texture gazelle.
        /// </summary>
        private Bitmap _textureGazelle;

        /// <summary>
        ///     The _texture gazelle down.
        /// </summary>
        private Bitmap _textureGazelleDown;

        /// <summary>
        ///     The _texture gazelle left.
        /// </summary>
        private Bitmap _textureGazelleLeft;

        /// <summary>
        ///     The _texture gazelle right.
        /// </summary>
        private Bitmap _textureGazelleRight;

        /// <summary>
        ///     The _texture gazelle up.
        /// </summary>
        private Bitmap _textureGazelleUp;

        /// <summary>
        ///     The _texture giraffe.
        /// </summary>
        private Bitmap _textureGiraffe;

        /// <summary>
        ///     The _texture lion.
        /// </summary>
        private Bitmap _textureLion;

        /// <summary>
        ///     The _texture lion down.
        /// </summary>
        private Bitmap _textureLionDown;

        /// <summary>
        ///     The _texture lion left.
        /// </summary>
        private Bitmap _textureLionLeft;

        /// <summary>
        ///     The _texture lion right.
        /// </summary>
        private Bitmap _textureLionRight;

        /// <summary>
        ///     The _texture lion up.
        /// </summary>
        private Bitmap _textureLionUp;

        /// <summary>
        ///     The _texture player down.
        /// </summary>
        private Bitmap _texturePlayerDown;

        /// <summary>
        ///     The _texture player left.
        /// </summary>
        private Bitmap _texturePlayerLeft;

        /// <summary>
        ///     The _texture player right.
        /// </summary>
        private Bitmap _texturePlayerRight;

        /// <summary>
        ///     The _texture player up.
        /// </summary>
        private Bitmap _texturePlayerUp;

        /// <summary>
        ///     The _texture rabbit.
        /// </summary>
        private Bitmap _textureRabbit;

        /// <summary>
        ///     The _texture rabbit down.
        /// </summary>
        private Bitmap _textureRabbitDown;

        /// <summary>
        ///     The _texture rabbit left.
        /// </summary>
        private Bitmap _textureRabbitLeft;

        /// <summary>
        ///     The _texture rabbit right.
        /// </summary>
        private Bitmap _textureRabbitRight;

        /// <summary>
        ///     The _texture rabbit up.
        /// </summary>
        private Bitmap _textureRabbitUp;

        /// <summary>
        ///     The _texture rain.
        /// </summary>
        private Bitmap _textureRain;

        /// <summary>
        ///     The _texture thunder.
        /// </summary>
        private Bitmap _textureThunder;

        /// <summary>
        ///     The _texture water animated.
        /// </summary>
        private Bitmap _textureWaterAnimated;

        /// <summary>
        ///     The _thunder timer.
        /// </summary>
        private Timer _thunderTimer;

        /// <summary>
        ///     The count.
        /// </summary>
        private int count;

        /// <summary>
        ///     The count 2.
        /// </summary>
        private int count2;

        /// <summary>
        ///     The count 3.
        /// </summary>
        private int count3;

        /// <summary>
        ///     The count car.
        /// </summary>
        private int countCar;

        /// <summary>
        ///     The count player.
        /// </summary>
        private int countPlayer;

        private Bitmap _spiTexture;

        private Timer animateSpi;

        private int countSpi;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Texture" /> class.
        /// </summary>
        public Texture()
        {
            this._thunderTimer = new Timer();
            this._animate = new Timer();
            this._animate.Start();
            this._animate.Interval = 10;
            this._rainTimer = new Timer();

            this._animateAnimal = new Timer();
            this._animateAnimal.Interval = 15;
            this._animateAnimal.Start();
            this._animateAnimal.Tick += this.T_Cat_Anim;

            this._animatePlayer = new Timer();
            this._animatePlayer.Interval = 10;
            this._animatePlayer.Start();
            this._animatePlayer.Tick += this.PlayerAnim;

            this._animateCar = new Timer();
            this._animateCar.Interval = 10;
            this._animateCar.Start();
            this._animateCar.Tick += this.T_Car_Anim;

            this.animateSpi = new Timer();
            this.animateSpi.Interval = 10;
            this.animateSpi.Start();
            this.animateSpi.Tick += this.T_Spi_Anim;

            this._rainTimer.Start();
            this._rainTimer.Interval = 10;
            this._rainTimer.Tick += this.T_rain_tick;

            this._thunderTimer.Start();
            this._thunderTimer.Interval = 10;
            this._thunderTimer.Tick += this.T_Thunder_tick;

            this._animate.Tick += this.T_animateTick;
            this._textureGrass = new Bitmap(@"..\..\..\assets\Grass.jpg");
            this._textureGrass2 = new Bitmap(@"..\..\..\assets\Grass2.jpg");
            this._textureForest = new Bitmap(@"..\..\..\assets\Forest.jpg");
            this._textureSnow = new Bitmap(@"..\..\..\assets\Snow.jpg");
            this._textureDesert = new Bitmap(@"..\..\..\assets\Desert.jpg");
            this._textureDirt = new Bitmap(@"..\..\..\assets\Dirt.jpg");
            this._textureMountain = new Bitmap(@"..\..\..\assets\Mountain.jpg");

            this._textureTree = new Bitmap(@"..\..\..\assets\Vegetation\Tree1.gif");
            this._textureTree2 = new Bitmap(@"..\..\..\assets\Vegetation\Tree2.gif");
            this._textureTree3 = new Bitmap(@"..\..\..\assets\Vegetation\Tree3.gif");
            this._textureBush = new Bitmap(@"..\..\..\assets\Vegetation\Bush1.gif");
            this._textureRock = new Bitmap(@"..\..\..\assets\Vegetation\Rock1.gif");
            this._textureRock2 = new Bitmap(@"..\..\..\assets\Vegetation\Rock2.gif");
            this._textureRock3 = new Bitmap(@"..\..\..\assets\Vegetation\Rock3.gif");
            this._textureHouse = new Bitmap(@"..\..\..\assets\Houses\house.png");
            this._chair = new Bitmap(@"..\..\..\assets\Houses\Furnitures\Chair.png");
            this._table = new Bitmap(@"..\..\..\assets\Houses\Furnitures\Table.png");
            this._table2 = new Bitmap(@"..\..\..\assets\Houses\Furnitures\Table2.png");
            this._shelf = new Bitmap(@"..\..\..\assets\Houses\Furnitures\Shelf.png");
            this._flowerPot = new Bitmap(@"..\..\..\assets\Houses\Furnitures\FlowerPot.png");
            this._flowerPot2 = new Bitmap(@"..\..\..\assets\Houses\Furnitures\FlowerPot2.png");
            this._barrel = new Bitmap(@"..\..\..\assets\Houses\Furnitures\Barrel.png");
            this._missileDown = new Bitmap( @"..\..\..\assets\Tank\Missile-Down\a.png" );
            this._missileUp = new Bitmap( @"..\..\..\assets\Tank\Missile-Up\a.png" );
            this._missileLeft = new Bitmap( @"..\..\..\assets\Tank\Missile-Left\a.png" );
            this._missileRight = new Bitmap( @"..\..\..\assets\Tank\Missile-Right\a.png" );

            this._textureRabbit = new Bitmap(@"..\..\..\assets\Animal\Rabbit.png");
            this._textureElephant = new Bitmap(@"..\..\..\assets\Animal\Elephant.png");
            this._textureElephant.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureCow = new Bitmap(@"..\..\..\assets\Animal\Cow.png");
            this._textureCow.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureCat = new Bitmap(@"..\..\..\assets\Animal\Cat.png");
            this._textureCat.MakeTransparent(Color.White);
            this._textureCat.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureCatDown = new Bitmap(@"..\..\..\assets\Animal\Cat\Cat-Down\a.png");
            this._textureCatUp = new Bitmap(@"..\..\..\assets\Animal\Cat\Cat-Up\a.png");
            this._textureCatLeft = new Bitmap(@"..\..\..\assets\Animal\Cat\Cat-Left\a.png");
            this._textureCatRight = new Bitmap(@"..\..\..\assets\Animal\Cat\Cat-Right\a.png");

            this._playerTexture = new Bitmap(@"..\..\..\assets\Player\Player-Down\a.png");
            this._playerCarTexture = new Bitmap(@"..\..\..\assets\Car\car.png");
            this._textureCarDown = new Bitmap(@"..\..\..\assets\Car\Car-Down\a.png");
            this._textureCarUp = new Bitmap(@"..\..\..\assets\Car\Car-Up\a.png");
            this._textureCarLeft = new Bitmap(@"..\..\..\assets\Car\Car-Left\a.png");
            this._textureCarRight = new Bitmap(@"..\..\..\assets\Car\Car-Right\a.png");

            this._textureTankUp = new Bitmap(@"..\..\..\assets\Tank\Tank-Up\b.png");
            this._textureTankDown = new Bitmap(@"..\..\..\assets\Tank\Tank-Down\b.png");
            this._textureTankLeft = new Bitmap(@"..\..\..\assets\Tank\Tank-Left\b.png");
            this._textureTankRight = new Bitmap(@"..\..\..\assets\Tank\Tank-Right\b.png");

            this._floorTexture = new Bitmap(@"..\..\..\assets\Houses\Textures\Floor.jpg");
            this._floor2Texture = new Bitmap(@"..\..\..\assets\Houses\Textures\TileFloor.jpg");
            this._wallTexture = new Bitmap(@"..\..\..\assets\Houses\Textures\Wall.jpg");
            this._exitTexture = new Bitmap( @"..\..\..\assets\Houses\Textures\Exit.jpg" );

            this._textureDogDown = new Bitmap(@"..\..\..\assets\Animal\Dog\Dog-Down\a.png");
            this._textureDogUp = new Bitmap(@"..\..\..\assets\Animal\Dog\Dog-Up\a.png");
            this._textureDogLeft = new Bitmap(@"..\..\..\assets\Animal\Dog\Dog-Left\a.png");
            this._textureDogRight = new Bitmap(@"..\..\..\assets\Animal\Dog\Dog-Right\a.png");

            this._textureEagleDown = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Down\a.png");
            this._textureEagleUp = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Up\a.png");
            this._textureEagleLeft = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Left\a.png");
            this._textureEagleRight = new Bitmap(@"..\..\..\assets\Animal\Eagle\Eagle-Right\a.png");

            this._textureCowDown = new Bitmap(@"..\..\..\assets\Animal\Cow\Cow-Down\a.png");
            this._textureCowUp = new Bitmap(@"..\..\..\assets\Animal\Cow\Cow-Up\a.png");
            this._textureCowLeft = new Bitmap(@"..\..\..\assets\Animal\Cow\Cow-Left\a.png");
            this._textureCowRight = new Bitmap(@"..\..\..\assets\Animal\Cow\Cow-Right\a.png");

            this._textureLionDown = new Bitmap(@"..\..\..\assets\Animal\Lion\Lion-Down\a.png");
            this._textureLionUp = new Bitmap(@"..\..\..\assets\Animal\Lion\Lion-Up\a.png");
            this._textureLionLeft = new Bitmap(@"..\..\..\assets\Animal\Lion\Lion-Left\a.png");
            this._textureLionRight = new Bitmap(@"..\..\..\assets\Animal\Lion\Lion-Right\a.png");

            this._textureRabbitDown = new Bitmap(@"..\..\..\assets\Animal\Rabbit\Rabbit-Down\a.png");
            this._textureRabbitUp = new Bitmap(@"..\..\..\assets\Animal\Rabbit\Rabbit-Up\a.png");
            this._textureRabbitLeft = new Bitmap(@"..\..\..\assets\Animal\Rabbit\Rabbit-Left\a.png");
            this._textureRabbitRight = new Bitmap(@"..\..\..\assets\Animal\Rabbit\Rabbit-Right\a.png");

            this._textureElephantDown = new Bitmap(@"..\..\..\assets\Animal\Elephant\Elephant-Down\a.png");
            this._textureElephantUp = new Bitmap(@"..\..\..\assets\Animal\Elephant\Elephant-Up\a.png");
            this._textureElephantLeft = new Bitmap(@"..\..\..\assets\Animal\Elephant\Elephant-Left\a.png");
            this._textureElephantRight = new Bitmap(@"..\..\..\assets\Animal\Elephant\Elephant-Right\a.png");

            this._textureGazelleDown = new Bitmap(@"..\..\..\assets\Animal\Gazelle\Gazelle-Down\a.png");
            this._textureGazelleUp = new Bitmap(@"..\..\..\assets\Animal\Gazelle\Gazelle-Up\a.png");
            this._textureGazelleLeft = new Bitmap(@"..\..\..\assets\Animal\Gazelle\Gazelle-Left\a.png");
            this._textureGazelleRight = new Bitmap(@"..\..\..\assets\Animal\Gazelle\Gazelle-Right\a.png");

            this._texturePlayerDown = new Bitmap(@"..\..\..\assets\Player\Player-Down\a.png");
            this._texturePlayerUp = new Bitmap(@"..\..\..\assets\Player\Player-Up\a.png");
            this._texturePlayerLeft = new Bitmap(@"..\..\..\assets\Player\Player-Left\a.png");
            this._texturePlayerRight = new Bitmap(@"..\..\..\assets\Player\Player-Right\a.png");
            this._texturePlayerIdle = new Bitmap(@"..\..\..\assets\Player\Player-Down\a.png");

            this._textureLion = new Bitmap(@"..\..\..\assets\Animal\Lion.png");
            this._textureLion.MakeTransparent(Color.White);
            this._textureLion.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureEagle = new Bitmap(@"..\..\..\assets\Animal\Eagle.png");
            this._textureEagle.MakeTransparent(Color.White);
            this._textureEagle.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureGazelle = new Bitmap(@"..\..\..\assets\Animal\Gazelle.png");
            this._textureGazelle.MakeTransparent(Color.White);
            this._textureGazelle.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureDog = new Bitmap(@"..\..\..\assets\Animal\Dog.png");
            this._textureDog.MakeTransparent(Color.White);
            this._textureDog.RotateFlip(RotateFlipType.Rotate180FlipY);

            this._textureGiraffe = new Bitmap(@"..\..\..\assets\Animal\Elephant.png");

            this._textureBlood = new Bitmap(@"..\..\..\assets\Blood\Blood.gif");

            this._brushGrass = new SolidBrush(Color.FromArgb(59, 138, 33));
            this._brushDirt = new SolidBrush(Color.FromArgb(169, 144, 104));
            this._brushWater = new SolidBrush(Color.FromArgb(64, 85, 213));
            this._brushDesert = new SolidBrush(Color.FromArgb(173, 128, 109));
            this._brushForest = new SolidBrush(Color.FromArgb(110, 121, 53));
            this._brushSnow = new SolidBrush(Color.FromArgb(207, 206, 212));

            this._textureRain = new Bitmap(@"..\..\..\assets\Rain\0.gif");
            this._textureRain.MakeTransparent(Color.Black);

            this._textureWaterAnimated = new Bitmap(@"..\..\..\assets\Water\Water.jpg");

            this._textureThunder = new Bitmap(@"..\..\..\assets\Thunder\0.gif");
            this._textureThunder.MakeTransparent(Color.Black);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animated\", this._waterList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Rain2\", this._rainList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Thunder\", this._thunderList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cat\Cat-Left\", this._catLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cat\Cat-Right\", this._catRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cat\Cat-Down\", this._catDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cat\Cat-Up\", this._catUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Dog\Dog-Left\", this._dogLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Dog\Dog-Right\", this._dogRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Dog\Dog-Down\", this._dogDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Dog\Dog-Up\", this._dogUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Left\", this._eagleLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Right\", this._eagleRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Down\", this._eagleDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Eagle\Eagle-Up\", this._eagleUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cow\Cow-Left\", this._cowLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cow\Cow-Right\", this._cowRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cow\Cow-Down\", this._cowDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Cow\Cow-Up\", this._cowUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Lion\Lion-Left\", this._lionLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Lion\Lion-Right\", this._lionRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Lion\Lion-Down\", this._lionDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Lion\Lion-Up\", this._lionUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Rabbit\Rabbit-Left\", this._rabbitLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Rabbit\Rabbit-Right\", this._rabbitRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Rabbit\Rabbit-Down\", this._rabbitDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Rabbit\Rabbit-Up\", this._rabbitUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Elephant\Elephant-Left\", this._elephantLeftList);
            this.AddTexturesFromFolderToList(
                @"..\..\..\assets\Animal\Elephant\Elephant-Right\", 
                this._elephantRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Elephant\Elephant-Down\", this._elephantDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Elephant\Elephant-Up\", this._elephantUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Gazelle\Gazelle-Left\", this._gazelleLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Gazelle\Gazelle-Right\", this._gazelleRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Gazelle\Gazelle-Down\", this._gazelleDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Animal\Gazelle\Gazelle-Up\", this._gazelleUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Player\Player-Left\", this._playerLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Player\Player-Right\", this._playerRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Player\Player-Down\", this._playerDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Player\Player-Up\", this._playerUpList);

            this.AddTexturesFromFolderToList(@"..\..\..\assets\Car\Car-Left\", this._carLeftList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Car\Car-Right\", this._carRightList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Car\Car-Down\", this._carDownList);
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Car\Car-Up\", this._carUpList);


            this._spiTexture = new Bitmap(@"..\..\..\assets\Spi\a.png");
            this.AddTexturesFromFolderToList(@"..\..\..\assets\Spi\", this.spiList);
        }

        private void T_Spi_Anim(object sender, EventArgs e)
        {
            if (this.countSpi + 1 <= this.spiList.Count)
            {
                this._spiTexture = this.spiList[this.countSpi];
                this.countSpi++;
            }
            else
            {
                this.countSpi = 0;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add textures from folder to list.
        /// </summary>
        /// <param name="Directory">
        /// The directory.
        /// </param>
        /// <param name="list">
        /// The list.
        /// </param>
        public void AddTexturesFromFolderToList(string Directory, List<Bitmap> list)
        {
            string[] filePaths = System.IO.Directory.GetFiles(Directory);
            Array.Sort(filePaths);
            foreach (string images in filePaths)
            {
                var _tempBmp = new Bitmap(images);
                list.Add(_tempBmp);
            }
        }

        /// <summary>
        ///     The get blood.
        /// </summary>
        /// <returns>
        ///     The <see cref="Bitmap" />.
        /// </returns>
        public Bitmap GetBlood()
        {
            return this._textureBlood;
        }

        /// <summary>
        /// The get color.
        /// </summary>
        /// <param name="box">
        /// The box.
        /// </param>
        /// <returns>
        /// The <see cref="Brush"/>.
        /// </returns>
        public Brush GetColor(Box box)
        {
            switch (box.Ground)
            {
                case EBoxGround.Grass:
                    return this._brushGrass;
                case EBoxGround.Grass2:
                    return this._brushGrass;
                case EBoxGround.Water:
                    return this._brushWater;
                case EBoxGround.Forest:
                    return this._brushForest;
                case EBoxGround.Snow:
                    return this._brushSnow;
                case EBoxGround.Dirt:
                    return this._brushDirt;
                case EBoxGround.Desert:
                    return this._brushDesert;
                case EBoxGround.Mountain:
                    return this._brushDirt;
                case EBoxGround.Floor:
                    return this._brushDirt;
                case EBoxGround.Floor2:
                    return this._brushDirt;
                case EBoxGround.Wall:
                    return this._brushSnow;
                case EBoxGround.Exit:
                    return this._brushDirt;
                default:
                    return this._brushGrass;
            }
        }

        /// <summary>
        ///     The get rain.
        /// </summary>
        /// <returns>
        ///     The <see cref="Bitmap" />.
        /// </returns>
        public Bitmap GetRain()
        {
            return this._textureRain;
        }

        /// <summary>
        /// The get texture.
        /// </summary>
        /// <param name="box">
        /// The box.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public Bitmap GetTexture(Box box)
        {
            switch (box.Ground)
            {
                case EBoxGround.Grass:
                    return this._textureGrass;
                case EBoxGround.Grass2:
                    return this._textureGrass2;
                case EBoxGround.Water:
                    return this._textureWaterAnimated;
                case EBoxGround.Forest:
                    return this._textureForest;
                case EBoxGround.Snow:
                    return this._textureSnow;
                case EBoxGround.Dirt:
                    return this._textureDirt;
                case EBoxGround.Desert:
                    return this._textureDesert;
                case EBoxGround.Mountain:
                    return this._textureMountain;
                case EBoxGround.Floor:
                    return this._floorTexture;
                case EBoxGround.Floor2:
                    return this._floor2Texture;
                case EBoxGround.Wall:
                    return this._wallTexture;
                case EBoxGround.Exit:
                    return this._exitTexture;
                default:
                    return this._textureGrass;
            }
        }

        /// <summary>
        /// The get texture.
        /// </summary>
        /// <param name="vegetation">
        /// The vegetation.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public Bitmap GetTexture(MapElement vegetation)
        {
            switch (vegetation.Texture)
            {
                case EmapElements.Tree:
                    return this._textureTree;
                case EmapElements.Tree2:
                    return this._textureTree2;
                case EmapElements.Tree3:
                    return this._textureTree3;
                case EmapElements.Bush:
                    return this._textureBush;
                case EmapElements.Rock:
                    return this._textureRock;
                case EmapElements.Rock2:
                    return this._textureRock2;
                case EmapElements.Rock3:
                    return this._textureRock3;
                case EmapElements.House:
                    return this._textureHouse;
                case EmapElements.Chair:
                    return this._chair;
                case EmapElements.Barrel:
                    return this._barrel;
                case EmapElements.Shelf:
                    return this._shelf;
                case EmapElements.FlowerPot:
                    return this._flowerPot;
                case EmapElements.Table:
                    return this._table;
                case EmapElements.Spi:
                    return this._spiTexture;
                    
                default:
                    return this._textureTree;
            }
        }

        /// <summary>
        ///     The get thunder.
        /// </summary>
        /// <returns>
        ///     The <see cref="Bitmap" />.
        /// </returns>
        public Bitmap GetThunder()
        {
            return this._textureThunder;
        }

        /// <summary>
        /// The load texture.
        /// </summary>
        /// <param name="animal">
        /// The animal.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public Bitmap LoadTexture(Animal animal)
        {
            switch (animal.Texture)
            {
                case EAnimalTexture.Rabbit:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureRabbitLeft;
                        case EMovingDirection.Up:
                            return this._textureRabbitUp;
                        case EMovingDirection.Down:
                            return this._textureRabbitDown;
                        case EMovingDirection.Right:
                            return this._textureRabbitRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Cat:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureCatLeft;
                        case EMovingDirection.Up:
                            return this._textureCatUp;
                        case EMovingDirection.Down:
                            return this._textureCatDown;
                        case EMovingDirection.Right:
                            return this._textureCatRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Eagle:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureEagleLeft;
                        case EMovingDirection.Up:
                            return this._textureEagleUp;
                        case EMovingDirection.Down:
                            return this._textureEagleDown;
                        case EMovingDirection.Right:
                            return this._textureEagleRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Elephant:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureElephantLeft;
                        case EMovingDirection.Up:
                            return this._textureElephantUp;
                        case EMovingDirection.Down:
                            return this._textureElephantDown;
                        case EMovingDirection.Right:
                            return this._textureElephantRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Lion:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureLionLeft;
                        case EMovingDirection.Up:
                            return this._textureLionUp;
                        case EMovingDirection.Down:
                            return this._textureLionDown;
                        case EMovingDirection.Right:
                            return this._textureLionRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Cow:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureCowLeft;
                        case EMovingDirection.Up:
                            return this._textureCowUp;
                        case EMovingDirection.Down:
                            return this._textureCowDown;
                        case EMovingDirection.Right:
                            return this._textureCowRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Dog:
                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureDogLeft;
                        case EMovingDirection.Up:
                            return this._textureDogUp;
                        case EMovingDirection.Down:
                            return this._textureDogDown;
                        case EMovingDirection.Right:
                            return this._textureDogRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                case EAnimalTexture.Gazelle:

                    switch (animal.EMovingDirection)
                    {
                        case EMovingDirection.Left:
                            return this._textureGazelleLeft;
                        case EMovingDirection.Up:
                            return this._textureGazelleUp;
                        case EMovingDirection.Down:
                            return this._textureGazelleDown;
                        case EMovingDirection.Right:
                            return this._textureGazelleRight;
                        default:
                            throw new NotSupportedException("No texture found for this direction");
                    }

                default:
                    return this._textureGrass;
            }
        }

        /// <summary>
        /// The load texture.
        /// </summary>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public Bitmap LoadTexture(Player player)
        {
            switch (player.Texture)
            {
                case EPlayerTexture.MainPlayer:
                    switch (player.EMovingDirection)
                    {
                        case EMovingDirection.Up:
                            return this._texturePlayerUp;
                        case EMovingDirection.Down:
                            return this._texturePlayerDown;
                        case EMovingDirection.Left:
                            return this._texturePlayerLeft;
                        case EMovingDirection.Right:
                            return this._texturePlayerRight;
                        case EMovingDirection.Idle:
                            return this._texturePlayerIdle;
                        default:
                            throw new ArgumentException( "Not handled" );
                    }

                default:
                    return this._playerTexture;
            }
        }

        public Bitmap LoadMissileTexture( EMovingDirection d )
        {
            switch( d )
            {
                case EMovingDirection.Left:
                    return this._missileLeft;
                case EMovingDirection.Right:
                    return this._missileRight;
                case EMovingDirection.Up:
                    return this._missileUp;
                case EMovingDirection.Down:
                    return this._missileDown;
                default :
                    return this._missileUp;
                
            }
        }

        /// <summary>
        /// The load texture.
        /// </summary>
        /// <param name="car">
        /// The car.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public Bitmap LoadTexture(Car car)
        {
            switch (car.Texture)
            {
                case ECarTexture.MainPlayerCar:
                    switch (car.EMovingDirection)
                    {
                        case EMovingDirection.Up:
                            return this._textureCarUp;
                        case EMovingDirection.Down:
                            return this._textureCarDown;
                        case EMovingDirection.Left:
                            return this._textureCarLeft;
                        case EMovingDirection.Right:
                            return this._textureCarRight;

                        default:
                            return this._textureCarRight;
                    }

                case ECarTexture.Tank:
                    switch (car.EMovingDirection)
                    {
                        case EMovingDirection.Up:
                            return this._textureTankUp;
                        case EMovingDirection.Down:
                            return this._textureTankDown;
                        case EMovingDirection.Left:
                            return this._textureTankLeft;
                        case EMovingDirection.Right:
                            return this._textureTankRight;

                        default:
                            return this._textureTankUp;
                    }

                default:
                    return this._playerCarTexture;
            }



            
        }
        public void DisposeAll()
        {
            _textureGrass.Dispose();
            _textureGrass2.Dispose();
            _textureWaterAnimated.Dispose();
        }

        

        #endregion

        #region Methods

        /// <summary>
        /// The t_ car_ anim.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_Car_Anim(object sender, EventArgs e)
        {
            if (this.countCar + 1 <= this._carLeftList.Count)
            {
                this._textureCarDown = this._carDownList[this.countCar];
                this._carDownList[this.countCar].MakeTransparent(Color.White);

                this._textureCarLeft = this._carLeftList[this.countCar];
                this._carLeftList[this.countCar].MakeTransparent(Color.White);

                this._textureCarRight = this._carRightList[this.countCar];
                this._carRightList[this.countCar].MakeTransparent(Color.White);

                this._textureCarUp = this._carUpList[this.countCar];
                this._carUpList[this.countCar].MakeTransparent(Color.White);

                this.countCar++;
            }
            else
            {
                this.countCar = 0;
            }
        }

        /// <summary>
        /// The t_ cat_ anim.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_Cat_Anim(object sender, EventArgs e)
        {
            if (this._countAnimal + 1 <= this._catDownList.Count)
            {
                this._textureCatDown = this._catDownList[this._countAnimal];
                this._catDownList[this._countAnimal].MakeTransparent(Color.White);

                this._textureCatLeft = this._catLeftList[this._countAnimal];
                this._catLeftList[this._countAnimal].MakeTransparent(Color.White);

                this._textureCatRight = this._catRightList[this._countAnimal];
                this._catRightList[this._countAnimal].MakeTransparent(Color.White);

                this._textureCatUp = this._catUpList[this._countAnimal];
                this._catUpList[this._countAnimal].MakeTransparent(Color.White);

                this._textureDogDown = this._dogDownList[this._countAnimal];

                this._textureDogLeft = this._dogLeftList[this._countAnimal];

                this._textureDogRight = this._dogRightList[this._countAnimal];

                this._textureDogUp = this._dogUpList[this._countAnimal];

                this._textureEagleDown = this._eagleDownList[this._countAnimal];

                this._textureEagleLeft = this._eagleLeftList[this._countAnimal];

                this._textureEagleRight = this._eagleRightList[this._countAnimal];

                this._textureEagleUp = this._eagleUpList[this._countAnimal];

                this._textureCowDown = this._cowDownList[this._countAnimal];

                this._textureCowLeft = this._cowLeftList[this._countAnimal];

                this._textureCowRight = this._cowRightList[this._countAnimal];

                this._textureCowUp = this._cowUpList[this._countAnimal];

                this._textureLionDown = this._lionDownList[this._countAnimal];

                this._textureLionLeft = this._lionLeftList[this._countAnimal];

                this._textureLionRight = this._lionRightList[this._countAnimal];

                this._textureLionUp = this._lionUpList[this._countAnimal];

                this._textureRabbitDown = this._rabbitDownList[this._countAnimal];

                this._textureRabbitLeft = this._rabbitLeftList[this._countAnimal];

                this._textureRabbitRight = this._rabbitRightList[this._countAnimal];

                this._textureRabbitUp = this._rabbitUpList[this._countAnimal];

                this._textureElephantDown = this._elephantDownList[this._countAnimal];

                this._textureElephantLeft = this._elephantLeftList[this._countAnimal];

                this._textureElephantRight = this._elephantRightList[this._countAnimal];

                this._textureElephantUp = this._elephantUpList[this._countAnimal];

                this._textureGazelleDown = this._gazelleDownList[this._countAnimal];

                this._textureGazelleLeft = this._gazelleLeftList[this._countAnimal];

                this._textureGazelleRight = this._gazelleRightList[this._countAnimal];

                this._textureGazelleUp = this._gazelleUpList[this._countAnimal];

                this._countAnimal++;
            }
            else
            {
                this._countAnimal = 0;
            }
        }

        /// <summary>
        /// The t_ player_ anim.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void PlayerAnim(object sender, EventArgs e)
        {
            if (this.countPlayer + 1 <= this._playerLeftList.Count)
            {
                this._texturePlayerDown = this._playerDownList[this.countPlayer];
                this._playerDownList[this.countPlayer].MakeTransparent(Color.White);

                this._texturePlayerLeft = this._playerLeftList[this.countPlayer];
                this._playerLeftList[this.countPlayer].MakeTransparent(Color.White);

                this._texturePlayerRight = this._playerRightList[this.countPlayer];
                this._playerRightList[this.countPlayer].MakeTransparent(Color.White);

                this._texturePlayerUp = this._playerUpList[this.countPlayer];
                this._playerUpList[this.countPlayer].MakeTransparent(Color.White);

                this.countPlayer++;
            }
            else
            {
                this.countPlayer = 0;
            }

           
        }

        /// <summary>
        /// The t_ thunder_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_Thunder_tick(object sender, EventArgs e)
        {
            if (this.count3 + 1 <= this._thunderList.Count)
            {
                this._thunderList[this.count3].MakeTransparent(Color.FromArgb(1, 2, 3));
                this._textureThunder = this._thunderList[this.count3];

                this.count3++;
            }
            else
            {
                this.count3 = 0;
            }
        }

        /// <summary>
        /// The t_animate tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_animateTick(object sender, EventArgs e)
        {
            if (this.count + 1 <= this._waterList.Count)
            {
                this._textureWaterAnimated = this._waterList[this.count];
                this.count++;
            }
            else
            {
                this.count = 0;
            }
        }

        /// <summary>
        /// The t_rain_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_rain_tick(object sender, EventArgs e)
        {
            if (this.count2 + 1 <= this._rainList.Count)
            {
                this._rainList[this.count2].MakeTransparent(Color.White);
                this._textureRain = this._rainList[this.count2];

                this.count2++;
            }
            else
            {
                this.count2 = 0;
            }
        }

        #endregion
    }
}