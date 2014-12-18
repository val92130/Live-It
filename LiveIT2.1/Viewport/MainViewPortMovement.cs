using LiveIT2._1.Animals;
using LiveIT2._1.Enums;
using LiveIT2._1.Terrain;
using LiveIT2._1.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1.Viewport
{
    public partial class MainViewPort
    {
        /// <summary>
        ///     The move with mouse.
        /// </summary>
        /// 
        public void MoveWithMouse()
        {
            var cursorPos = new Rectangle(Cursor.Position, new Size(10, 10));
            const int Speed = 45;
            if (!this.map.IsPlayer)
            {
                if (cursorPos.IntersectsWith(this.screenTop))
                {
                    this.MoveY(-Speed);
                }

                if (cursorPos.IntersectsWith(this.screenBottom))
                {
                    this.MoveY(Speed);
                }

                if (cursorPos.IntersectsWith(this.screenLeft))
                {
                    this.MoveX(-Speed);
                }

                if (cursorPos.IntersectsWith(this.screenRight))
                {
                    this.MoveX(Speed);
                }
            }
        }

        public int MoveXLerp( int goalX, int currentX, int delta )
        {
            int diff = goalX - currentX;
            if( diff > delta )
                return currentX + delta;
            if( diff < delta )
                return currentX - delta;
            return goalX;
        }

        /// <summary>
        /// The move x.
        /// </summary>
        /// <param name="centimeters">
        /// The centimeters.
        /// </param>
        public void MoveX(int centimeters)
        {
            if (!this.map.IsPlayer)
            {
                this.Offset(new Point(centimeters, 0));
            }
            else
            {
                if (this.map.IsPlayer && this.map.IsInCar == false)
                {
                    if (centimeters <= 0)
                    {
                        this.player.EMovingDirection = EMovingDirection.Left;
                        if( player.LeftCollide == false )
                        {
                            this.player.Position = new Point( this.player.Position.X - player.Speed, this.player.Position.Y );
                        }
                        
                    }
                    else
                    {
                        this.player.EMovingDirection = EMovingDirection.Right;
                        if( player.RightCollide == false )
                        {
                            this.player.Position = new Point( this.player.Position.X + player.Speed, this.player.Position.Y );
                        }
                        
                    }
                    this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                    this.viewPort.X = this.player.Area.X - (this.viewPort.Size.Width / 2) + (this.player.Area.Width / 2);
                    this.viewPort.Y = this.player.Area.Y - (this.viewPort.Size.Height / 2)
                                      + (this.player.Area.Height / 2);
                }
                else if (this.map.IsPlayer && this.map.IsInCar)
                {
                    if (centimeters > 0)
                    {
                        this.player.Car.EMovingDirection = EMovingDirection.Right;
                        this.player.Car.Position = new Point(
                        this.player.Car.Position.X + this.player.Car.Speed,
                        this.player.Car.Position.Y );
                    }
                    else
                    {
                        this.player.Car.EMovingDirection = EMovingDirection.Left;
                        this.player.Car.Position = new Point(
                        this.player.Car.Position.X - this.player.Car.Speed,
                        this.player.Car.Position.Y );
                    }

                    this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                    this.viewPort.X = this.player.Car.Area.X - (this.viewPort.Size.Width / 2)
                                      + (this.player.Car.Area.Width / 2);
                    this.viewPort.Y = this.player.Car.Area.Y - (this.viewPort.Size.Height / 2)
                                      + (this.player.Car.Area.Height / 2);
                }
            }
        }



        /// <summary>
        /// The move y.
        /// </summary>
        /// <param name="centimeters">
        /// The centimeters.
        /// </param>
        public void MoveY(int centimeters)
        {
            if (!this.map.IsPlayer)
            {
                this.Offset(new Point(0, centimeters));
            }
            else if (this.map.IsPlayer && this.map.IsInCar == false)
            {
                if (centimeters > 0)
                {
                    this.player.EMovingDirection = EMovingDirection.Down;
                    if( player.DownCollide == false )
                    {
                        this.player.Position = new Point( this.player.Position.X, this.player.Position.Y + this.player.Speed );
                    }
                    
                }
                else
                {
                    this.player.EMovingDirection = EMovingDirection.Up;
                    if( player.UpCollide == false )
                    {
                        this.player.Position = new Point( this.player.Position.X, this.player.Position.Y - this.player.Speed );
                    }
                    
                }
                
                this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                this.viewPort.X = this.player.Area.X - (this.viewPort.Size.Width / 2) + (this.player.Area.Width / 2);
                this.viewPort.Y = this.player.Area.Y - (this.viewPort.Size.Height / 2) + (this.player.Area.Height / 2);
            }
            else if (this.map.IsPlayer && this.map.IsInCar)
            {
                if (centimeters > 0)
                {
                    this.player.Car.EMovingDirection = EMovingDirection.Down;
                    this.player.Car.Position = new Point(
                    this.player.Car.Position.X,
                    this.player.Car.Position.Y + this.Player.Car.Speed );
                }
                else
                {
                    this.player.Car.EMovingDirection = EMovingDirection.Up;
                    this.player.Car.Position = new Point(
                    this.player.Car.Position.X,
                    this.player.Car.Position.Y - this.Player.Car.Speed );
                }

                this.viewPort.Size = new Size(this.screen.Width * 2, this.screen.Height * 2);
                this.viewPort.X = this.player.Car.Area.X - (this.viewPort.Size.Width / 2)
                                   + (this.player.Car.Area.Width / 2);
                this.viewPort.Y = this.player.Car.Area.Y - (this.viewPort.Size.Height / 2)
                                   + (this.player.Car.Area.Height / 2);
            }
        }

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="delta">
        /// The delta.
        /// </param>
        public void Offset(Point delta)
        {
            if (!this.map.IsPlayer)
            {
                this.viewPort.Location = new Point(Lerp(viewPort.Location.X + delta.X, viewPort.Location.X, this.CameraSmoothness), Lerp(viewPort.Location.Y + delta.Y,viewPort.Location.Y, this.CameraSmoothness) );
                //this.viewPort.Offset(delta);
                if (this.viewPort.X < 0)
                {
                    this.viewPort.X = 0;
                }

                if (this.viewPort.Y < 0)
                {
                    this.viewPort.Y = 0;
                }

                if (this.viewPort.Right > this.map.MapSize)
                {
                    this.viewPort.X = this.map.MapSize - this.viewPort.Width;
                }

                if (this.viewPort.Bottom > this.map.MapSize)
                {
                    this.viewPort.Y = this.map.MapSize - this.viewPort.Height;
                }
            }
        }
        /// <summary>
        /// The zoom.
        /// </summary>
        /// <param name="meters">
        /// The meters.
        /// </param>
        public void Zoom(int meters)
        {
            if (!this.map.IsPlayer)
            {
                this.viewPort.Width += meters;
                this.viewPort.Height += meters;
                if (this.viewPort.Width < MinimalWidthInCentimeter || this.viewPort.Height < MinimalWidthInCentimeter)
                {
                    this.viewPort.Width = MinimalWidthInCentimeter;
                    this.viewPort.Height = MinimalWidthInCentimeter;
                }

                if (this.viewPort.Width > this.map.MapSize)
                {
                    this.viewPort.Height = this.map.MapSize;
                    this.viewPort.Width = this.map.MapSize;
                }
            }
        }
    }
}
