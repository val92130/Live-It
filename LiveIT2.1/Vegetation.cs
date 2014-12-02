using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Vegetation
    {
        Point _position, _relativePosition;
        Size _size, _relativeSize;
        VegetationTexture _texture;
        Map _map;
        public Vegetation(Map map, Point StartPosition)
        {
            _map = map;
            _position = StartPosition;
            _texture = VegetationTexture.Tree;
            _size = new Size( 400, 400 );
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Point RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public Size RelativeSize
        {
            get { return _relativeSize; }
            set { _relativeSize = value; }
        }

        public VegetationTexture Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Rectangle Area
        {
            get { return new Rectangle(this.Position, this.Size); }
        }

        public virtual void Draw( Graphics g, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap, Texture texture )
        {
            int newWidth = (int)(((double)this.Area.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)this.Area.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPort.Width) * target.Width)));

            this.RelativePosition = new Point( newXpos, newYpos );
            this.RelativeSize = new Size( newWidth, newHeight );

            int newSizeMini = (int)(((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)this.Area.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(this.Area.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(this.Area.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (this.Area.Width / (((double)this.Area.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            if( this.Area.IntersectsWith( viewPort ) )
            {
                g.DrawImage( texture.GetTexture( this ), new Rectangle( newXpos + target.X, newYpos + target.Y, newWidth, newHeight ) );
            }      
            g.DrawRectangle( Pens.Brown, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
    }
}
