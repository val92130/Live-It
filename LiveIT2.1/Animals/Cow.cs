// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cow.cs" company="">
//   
// </copyright>
// <summary>
//   The cow.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animals
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Utils;

    /// <summary>
    ///     The cow.
    /// </summary>
    [Serializable]
    public class Cow : Herbivorous
    {
        #region Fields

        [NonSerialized]
        /// <summary>
        ///     The timer.
        /// </summary>
        private Timer timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cow"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Cow(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Position = starPosition;
            this.Texture = EAnimalTexture.Cow;
            this.Size = new Size(230, 230);
            this.Speed = 10;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.FavoriteEnvironnment = EBoxGround.Grass;
        }

        public Cow(Map map, Point startPosition, bool IsNewBorn)
            : base(map, startPosition, true)
        {
            _finalSize = new Size(230, 230);
            this.Position = startPosition;
            this.Texture = EAnimalTexture.Cow;
            this.Size = new Size(_finalSize.Width/2, _finalSize.Width/2);
            this.Speed = 20000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.FavoriteEnvironnment = EBoxGround.Grass;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the max hunger.
        /// </summary>
        public override int MaxHunger
        {
            get
            {
                return RandomGenerator.NextInt(50, 100);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();

            if (this.Hunger >= this.MaxHunger)
            {
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    if (this.BoxList[i].Ground != EBoxGround.Grass)
                    {
                        continue;
                    }

                    this.Speed = 0;
                    this._isEating = true;
                    this.Eat();
                    this.BoxList[i].Ground = EBoxGround.Dirt;
                }
            }
            else
            {
                this.Speed = this.DefaultSpeed;
                if (this.timer != null && this.timer.Enabled)
                {
                    this.timer.Stop();
                }

                this._isEating = false;
            }
        }

        /// <summary>
        ///     The eat.
        /// </summary>
        public void Eat()
        {
            this.timer = new Timer();
            this.timer.Interval = 2000;
            this.timer.Start();
            this.timer.Tick += this.T_eat_tick;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The t_eat_tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void T_eat_tick(object sender, EventArgs e)
        {
            if (this._isEating)
            {
                this.Hunger -= 2;
            }
        }

        #endregion
    }
}