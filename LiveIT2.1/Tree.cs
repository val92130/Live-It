// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company="">
//   
// </copyright>
// <summary>
//   The tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;

    /// <summary>
    /// The tree.
    /// </summary>
    [Serializable]
    public class Tree : Vegetation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="StartPosition">
        /// The start position.
        /// </param>
        public Tree(Map map, Point StartPosition)
            : base(map, StartPosition)
        {
            var r = new Random();
            int _random = r.Next(400, 650);
            var RandomVegList = new List<EVegetationTexture>
                                    {
                                        EVegetationTexture.Tree, 
                                        EVegetationTexture.Tree2, 
                                        EVegetationTexture.Tree3
                                    };
            var r2 = new Random();

            this.Texture = RandomVegList[r2.Next(0, RandomVegList.Count)];
            this.Size = new Size(_random, _random);
        }

        #endregion
    }
}