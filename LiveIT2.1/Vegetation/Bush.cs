// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bush.cs" company="">
//   
// </copyright>
// <summary>
//   The bush.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Vegetation
{
    using System;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    ///     The bush.
    /// </summary>
    [Serializable]
    public class Bush : MapElement
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bush"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="StartPosition">
        /// The start position.
        /// </param>
        public Bush(Map map, Point StartPosition)
            : base(map, StartPosition)
        {
            var r = new Random();
            int _random = r.Next(100, 250);
            this.Texture = EmapElements.Bush;
            this.Size = new Size(_random, _random);
        }

        #endregion
    }
}