// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rock.cs" company="">
//   
// </copyright>
// <summary>
//   The rock.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Vegetation
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    ///     The rock.
    /// </summary>
    [Serializable]
    public class Rock : Vegetation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rock"/> class.
        /// </summary>
        /// <param name="_map">
        /// The _map.
        /// </param>
        /// <param name="StartPosition">
        /// The start position.
        /// </param>
        public Rock(Map _map, Point StartPosition)
            : base(_map, StartPosition)
        {
            var r = new Random();
            int _random = r.Next(50, 300);
            var RandomVegList = new List<EVegetationTexture>
                                    {
                                        EVegetationTexture.Rock, 
                                        EVegetationTexture.Rock2, 
                                        EVegetationTexture.Rock3
                                    };
            var r2 = new Random();
            this.Texture = RandomVegList[r2.Next(0, RandomVegList.Count)];
            this.Size = new Size(_random, _random);
        }

        #endregion
    }
}