// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rabbit.cs" company="">
//   
// </copyright>
// <summary>
//   The rabbit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Animals
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    /// The rabbit.
    /// </summary>
    [Serializable]
    public class Rabbit : Herbivorous
    {
        #region Fields

        /// <summary>
        /// The timer.
        /// </summary>
        private Timer timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rabbit"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Rabbit(Map map, Point startPosition)
            : base(map, startPosition)
        {
            this.Texture = EAnimalTexture.Rabbit;
            this.Position = startPosition;
            this.Size = new Size(100, 100);
            this.Speed = 4200;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.Hunger = 49;
        }

        public Rabbit(Map map, Point startPosition, bool IsNewBorn)
            : base(map, startPosition, true)
        {
            _finalSize = new Size(100, 100);
            this.Texture = EAnimalTexture.Rabbit;
            this.Position = startPosition;
            this.Size = new Size(_finalSize.Width / 2, _finalSize.Height / 2);
            this.Speed = 4200;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.Hunger = 49;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();
            if (this.Hunger >= this.MaxHunger)
            {
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    if (this.BoxList[i].Ground == EBoxGround.Grass)
                    {
                        this.Speed = 0;
                        this._isEating = true;
                        this.Eat();
                        this.BoxList[i].Ground = EBoxGround.Grass2;
                    }
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
        /// The eat.
        /// </summary>
        public void Eat()
        {
            this.timer = new Timer { Interval = 5000 };
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
                this.Hunger -= 20;
            }
        }

        #endregion
    }
}