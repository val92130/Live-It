// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lion.cs" company="">
//   
// </copyright>
// <summary>
//   The lion.
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
    ///     The lion.
    /// </summary>
    [Serializable]
    public class Lion : Wild
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Lion"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Lion(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Position = starPosition;
            this.Texture = EAnimalTexture.Lion;
            this.Size = new Size(250, 250);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 15000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.Hunger = 49;
            this.TargetAnimals = new List<EAnimalTexture>
                                     {
                                         EAnimalTexture.Rabbit, 
                                         EAnimalTexture.Cow, 
                                         EAnimalTexture.Elephant, 
                                         EAnimalTexture.Gazelle
                                     };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();
            if (this.Hunger > this.MaxHunger)
            {
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
        }

        #endregion
    }
}