// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Eagle.cs" company="">
//   
// </copyright>
// <summary>
//   The eagle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Animals
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    /// The eagle.
    /// </summary>
    [Serializable]
    public class Eagle : Wild
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Eagle"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Eagle(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Position = starPosition;
            var r = new Random();
            int _random = r.Next(150, 350);
            this.Texture = EAnimalTexture.Eagle;
            this.Size = new Size(_random, _random);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 8000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.TargetAnimals = new List<EAnimalTexture>
                                     {
                                         EAnimalTexture.Rabbit, 
                                         EAnimalTexture.Cow, 
                                         EAnimalTexture.Gazelle
                                     };
        }

        public Eagle(Map map, Point starPosition, bool IsNewBorn)
            : base(map, starPosition, true)
        {
            
            this.Position = starPosition;
            var r = new Random();
            int _random = r.Next(150, 350);
            _finalSize = new Size(_random, _random);

            this.Texture = EAnimalTexture.Eagle;
            this.Size = new Size(_random / 2, _random / 2);
            this.FavoriteEnvironnment = EBoxGround.Forest;
            this.Speed = 8000;
            this.DefaultSpeed = this.Speed;
            this.ViewDistance = 400;
            this.TargetAnimals = new List<EAnimalTexture>
                                     {
                                         EAnimalTexture.Rabbit, 
                                         EAnimalTexture.Cow, 
                                         EAnimalTexture.Gazelle
                                     };
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
                return 70;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();
            if (this.Hunger > this.MaxHunger)
            {
                if (this.AnimalsAround.Count != 0)
                {
                    for (int i = 0; i < this.AnimalsAround.Count(); i++)
                    {
                        if (this.TargetAnimals.Contains(this.AnimalsAround[i].Texture))
                        {
                            this.ChangePosition(this.AnimalsAround[i].Position);
                            if (this.Area.IntersectsWith(this.AnimalsAround[i].Area))
                            {
                                this.AnimalsAround[i].Die();
                                this.Hunger -= 50;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}