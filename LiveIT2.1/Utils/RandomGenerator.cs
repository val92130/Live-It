// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomGenerator.cs" company="">
//   
// </copyright>
// <summary>
//   The random generator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Utils
{
    using System;

    /// <summary>
    /// The random generator.
    /// </summary>
    public static class RandomGenerator
    {
        #region Static Fields

        /// <summary>
        /// The random.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The sync lock.
        /// </summary>
        private static readonly object SyncLock = new object();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The next int.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int NextInt(int min, int max)
        {
            lock (SyncLock)
            {
                // synchronize
                return Random.Next(min, max);
            }
        }

        #endregion
    }
}