// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Herbivorous.cs" company="">
//   
// </copyright>
// <summary>
//   The herbivorous.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Animals
{
    using System.Drawing;

    using LiveIT2._1.Terrain;

    /// <summary>
    /// The herbivorous.
    /// </summary>
    public class Herbivorous : Animal
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Herbivorous"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        public Herbivorous(Map map, Point position)
            : base(map, position)
        {
        }

        #endregion
    }
}