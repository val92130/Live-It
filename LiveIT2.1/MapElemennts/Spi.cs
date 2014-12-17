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
    public class Spi : MapElement
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
        public Spi( Map map, Point StartPosition )
            : base( map, StartPosition )
        {

            this.Texture = EmapElements.Spi;
            this.Size = new Size( 100, 210 );
        }

        #endregion
    }
}