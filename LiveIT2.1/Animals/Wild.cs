// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wild.cs" company="">
//   
// </copyright>
// <summary>
//   The wild.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LiveIT2._1.Animals
{
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;

    /// <summary>
    ///     The wild.
    /// </summary>
    public class Wild : Animal
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Wild"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        public Wild(Map map, Point position)
            : base(map, position)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the target animals.
        /// </summary>
        public List<EAnimalTexture> TargetAnimals { get; protected set; }

        #endregion
    }
}