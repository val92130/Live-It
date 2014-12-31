// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Elephant.cs" company="">
//   
// </copyright>
// <summary>
//   The elephant.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Animals
{
    using System;
    using System.Drawing;
    using System.Linq;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    /// The elephant.
    /// </summary>
    [Serializable]
    public class Elephant : Herbivorous
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Elephant"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="starPosition">
        /// The star position.
        /// </param>
        public Elephant(Map map, Point starPosition)
            : base(map, starPosition)
        {
            this.Texture = EAnimalTexture.Elephant;
            this.Size = new Size(500, 500);
            this.Speed = 7;
            this.ViewDistance = 800;
        }

        public Elephant(Map map, Point starPosition, bool IsNewBorn)
            : base(map, starPosition, true)
        {
            _finalSize = new Size(500, 500);
            this.Texture = EAnimalTexture.Elephant;
            this.Size = new Size(_finalSize.Width / 2, _finalSize.Height/2);
            this.Speed = 7000;
            this.ViewDistance = 800;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The behavior.
        /// </summary>
        public override void Behavior()
        {
            base.Behavior();
            if (this.AnimalsAround.Count != 0)
            {
                for (int i = 0; i < this.AnimalsAround.Count(); i++)
                {
                    if (this.AnimalsAround[i].Texture == EAnimalTexture.Rabbit)
                    {
                        this.ChangePosition(this.AnimalsAround[i].Position);
                        if (this.Area.IntersectsWith(this.AnimalsAround[i].Area))
                        {
                            this.AnimalsAround[i].Die();
                        }
                    }
                }
            }
        }

        #endregion
    }
}