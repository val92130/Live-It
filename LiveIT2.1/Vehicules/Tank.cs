// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tank.cs" company="">
//   
// </copyright>
// <summary>
//   The tank.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LiveIT2._1.Vehicules
{
    using System.Collections.Generic;
    using System.Drawing;

    using LiveIT2._1.Enums;
    using LiveIT2._1.Terrain;
    using LiveIT2._1.Textures;

    /// <summary>
    /// The tank.
    /// </summary>
    public partial class Tank : Car
    {
        #region Fields

        /// <summary>
        /// The _map.
        /// </summary>
        private readonly Map _map;

        /// <summary>
        /// The _missiles.
        /// </summary>
        private readonly List<Missile> _missiles;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tank"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="startPosition">
        /// The start position.
        /// </param>
        public Tank(Map map, Point startPosition)
            : base(map, startPosition)
        {
            this.Position = startPosition;
            this._map = map;
            this.Texture = ECarTexture.Tank;
            this.Size = new Size(1000, 1000);
            this.Speed = 20000;
            this._missiles = new List<Missile>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The draw.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="viewPort">
        /// The view port.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        /// <param name="texture">
        /// The texture.
        /// </param>
        public override void Draw(
            Graphics g, 
            Rectangle target, 
            Rectangle viewPort, 
            Rectangle targetMiniMap, 
            Rectangle viewPortMiniMap, 
            Texture texture)
        {
            if (this._map.ViewPort.HasClicked)
            {
                this.Shoot();
            }

            base.Draw(g, target, viewPort, targetMiniMap, viewPortMiniMap, texture);

            for (int i = 0; i < this._missiles.Count; i++)
            {
                if (this._missiles[i].Area.X > this._map.MapSize || this._missiles[i].Area.X < 0
                    || this._missiles[i].Area.Y > this._map.MapSize || this._missiles[i].Area.Y < 0)
                {
                    this._missiles.Remove(this._missiles[i]);
                }
                else
                {
                    this._missiles[i].Draw(g, target, viewPort, targetMiniMap, viewPortMiniMap, texture);
                }
            }
        }

        /// <summary>
        /// The shoot.
        /// </summary>
        public void Shoot()
        {
            if (this.EMovingDirection == EMovingDirection.Right)
            {
                var m =
                    new Missile(
                        new Point(this.Position.X + this.Area.Width, this.Position.Y + (this.Area.Width / 2)), 
                        this._map);
                this._missiles.Add(m);

                m.Shoot(new Point(this.Position.X + this.Area.Width + 800, this.Position.Y + (this.Area.Width / 2)));
            }

            if (this.EMovingDirection == EMovingDirection.Left)
            {
                var m = new Missile(new Point(this.Position.X, this.Position.Y + (this.Area.Height / 2)), this._map);
                this._missiles.Add(m);

                m.Shoot(new Point(this.Position.X - 800, this.Position.Y + (this.Area.Height / 2)));
            }

            if (this.EMovingDirection == EMovingDirection.Up)
            {
                var m = new Missile(new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y), this._map);
                this._missiles.Add(m);
                m.Shoot(new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y - 800));
            }

            if (this.EMovingDirection == EMovingDirection.Down)
            {
                var m =
                    new Missile(
                        new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y + this.Area.Height), 
                        this._map);
                this._missiles.Add(m);

                m.Shoot(new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y + 800 + this.Area.Height));
            }

            if (this.EMovingDirection == EMovingDirection.Idle)
            {
                var m = new Missile(new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y), this._map);
                this._missiles.Add(m);
                m.Shoot(new Point(this.Position.X + (this.Area.Width / 2), this.Position.Y - 800));
            }
        }

        #endregion
    }
}