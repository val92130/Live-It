// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dog.cs" company="">
//   
// </copyright>
// <summary>
//   The dog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animals
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using LiveIT2._1.Enums;

    /// <summary>
    ///     The dog.
    /// </summary>
    [Serializable]
    public class Dog : Wild
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dog"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Dog(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Texture = EAnimalTexture.Dog;
            this.Size = new Size(150, 150);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 15000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.TargetAnimals = new List<EAnimalTexture> { EAnimalTexture.Rabbit, EAnimalTexture.Cow };
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
                return 55;
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
            if (this.Hunger <= this.MaxHunger)
            {
                return;
            }

            if (this.AnimalsAround.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.AnimalsAround.Count(); i++)
            {
                if (!this.TargetAnimals.Contains(this.AnimalsAround[i].Texture))
                {
                    continue;
                }

                this.ChangePosition(this.AnimalsAround[i].Position);
                if (!this.Area.IntersectsWith(this.AnimalsAround[i].Area))
                {
                    continue;
                }

                this.AnimalsAround[i].Die();
                this.Hunger -= 50;
            }
        }

        #endregion
    }
}