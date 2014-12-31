// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cat.cs" company="">
//   
// </copyright>
// <summary>
//   The cat.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animals
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Utils;

    /// <summary>
    ///     The cat.
    /// </summary>
    [Serializable]
    public class Cat : Wild
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cat"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Cat(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Texture = EAnimalTexture.Cat;
            this.Size = new Size(120, 120);
            this.FavoriteEnvironnment = EBoxGround.Grass;
            this.Speed = 5000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 300;
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit };
        }

        public Cat(Map map, Point starPosition, bool IsNewBorn)
            :base(map, starPosition, true)
        {
            _finalSize = new Size(120, 120);
            this.Texture = EAnimalTexture.Cat;
            this.Size = new Size(_finalSize.Width / 2, _finalSize.Height/2);
            this.FavoriteEnvironnment = EBoxGround.Grass;
            this.Speed = 5000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 300;
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the max hunger.
        /// </summary>
        public override int MaxHunger
        {
            get
            {
                return RandomGenerator.NextInt(50, 75);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The behavior.
        /// </summary>
        public override void Behavior()
        {
            // TODO : use abstract class instead of base
            base.Behavior();

            // Specific call
            if (this.Hunger <= this.MaxHunger)
            {
                return;
            }

            if (this.AnimalsAround.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.AnimalsAround.Count; i++)
            {
                if (!this.TargetAnimals.Contains(this.AnimalsAround[i].Texture))
                {
                    continue;
                }

                this.ChangePosition(this.AnimalsAround[i].Position);
                this.AnimalsAround[i].Speed = (int)(this.Speed * 2.5);
                if (this.Area.IntersectsWith(this.AnimalsAround[i].Area))
                {
                    Random r = new Random();
                    this.AnimalsAround[i].Die();
                    this.Hunger -= r.Next(30, 40);
                }
            }
        }

        #endregion
    }
}