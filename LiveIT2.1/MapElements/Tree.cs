// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company="">
//   
// </copyright>
// <summary>
//   The tree.
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
    ///     The tree.
    /// </summary>
    [Serializable]
    public class Tree : MapElement
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Tree(Map map, Point startPosition)
            : base(map, startPosition)
        {
            var r = new Random();
            int random = r.Next(400, 650);
            var randomVegList = new List<EmapElements>
                                    {
                                        EmapElements.Tree, 
                                        EmapElements.Tree2, 
                                        EmapElements.Tree3
                                    };
            var r2 = new Random();

            this.Texture = randomVegList[r2.Next(0, randomVegList.Count)];
            this.Size = new Size(random, random);
        }

        #endregion
    }
}