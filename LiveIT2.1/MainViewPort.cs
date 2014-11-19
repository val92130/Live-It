using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveIT2._1
{
    public class MainViewPort
    {
        const int _minimalWidthInCentimeter = 600;
        List<Box> _boxList, _boxListMini;

        private Rectangle _viewPort, _screen, _miniMap, _miniMapViewPort;
        Rectangle _mouseRect = new Rectangle(new Point(Cursor.Position.X, Cursor.Position.Y), new Size(0,0));
        Rectangle t = new Rectangle( 0, 0, 20, 100 );
        Map _map;
        List<Box> _selectedBoxes;
        Texture _texture;
        bool _changeTexture, _fillTexture;
        public MainViewPort( Map map)
        {
            _map = map;                   
            _texture = new Texture();
            _selectedBoxes = new List<Box>();
            _screen = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            _viewPort = new Rectangle(0, 0, 800, 800);
            _miniMap = new Rectangle( 0,0, 250, 250 );
            _miniMap.Y = _screen.Bottom - _miniMap.Height;
            _miniMapViewPort = new Rectangle( 0, 0, _map.MapSize, _map.MapSize );
        }

        public void Draw( Graphics g )
        {

            t.X += 10;
            t.Y += 5;
            _boxList = _map.GetOverlappedBoxes(_viewPort);
            _boxListMini = _map.GetOverlappedBoxes( _miniMapViewPort );
            _mouseRect.X = Cursor.Position.X - (_mouseRect.Width / 2);
            _mouseRect.Y = Cursor.Position.Y - (_mouseRect.Height / 2);
            foreach( Box boxs in _boxList )
            {
                boxs.Draw(g, _screen, _texture, _viewPort);               
            }
            foreach( Box boxs in _boxListMini )
            {
                boxs.DrawMiniMap( g, _miniMap, _texture, _miniMapViewPort );
            }
            g.DrawRectangle( Pens.White, new Rectangle(_miniMap.X, _miniMap.Y, _miniMap.Width, _miniMap.Height + 20) );

            if (_changeTexture) DrawMouseSelector(g);
            if (_fillTexture) FillMouseSelector(g);

            DrawRectangleInViewPort( g, t, _screen, _viewPort, _miniMap, _miniMapViewPort );
            DrawViewPortMiniMap( g, _viewPort, _miniMap, _miniMapViewPort );
        }


        public double ZoomFactor
        {
            get { return 1.0 - ((double)_viewPort.Width / (double)_map.MapSize); }
            set
            {
                int newWidth = (int)Math.Round( _map.MapSize * value );
                Debug.Assert( newWidth <= _map.MapSize );
                if( newWidth < _minimalWidthInCentimeter ) newWidth = _minimalWidthInCentimeter;
                int deltaW = newWidth - _viewPort.Width;
                if( deltaW != 0 )
                {
                    int newHeight = (int)Math.Round( (double)_viewPort.Height * (double)newWidth / (double)_viewPort.Width );
                    int deltaH = newHeight - _viewPort.Height;
                    _viewPort.X -= deltaW / 2;
                    _viewPort.Y -= deltaH / 2;
                    _viewPort.Height = newHeight;
                    _viewPort.Width = newWidth;
                }
            }
        }

        public void Zoom( int meters )
        {
            _viewPort.Width += meters;
            _viewPort.Height += meters;
            if( _viewPort.Width < _minimalWidthInCentimeter && _viewPort.Height < _minimalWidthInCentimeter )
            {
                _viewPort.Width = _minimalWidthInCentimeter;
                _viewPort.Height = _minimalWidthInCentimeter;
            }

            if( _viewPort.Width > _map.MapSize )
            {
                _viewPort.Height = _map.MapSize;
                _viewPort.Width = _map.MapSize;
            }
                       
        }

        public void Offset( Point delta )
        {
            _viewPort.Offset( delta );
            if( _viewPort.X < 0 ) _viewPort.X = 0;
            if( _viewPort.Y < 0 ) _viewPort.Y = 0;
            if( _viewPort.Right > _map.MapSize ) _viewPort.X = _map.MapSize - _viewPort.Width;
            if( _viewPort.Bottom > _map.MapSize ) _viewPort.Y = _map.MapSize - _viewPort.Height;
        }

        public void LoadMap(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Draw the selection rectangle to change the textures
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mouseRect"></param>
        public void DrawMouseSelector(Graphics g)
        {
            _selectedBoxes.Clear();
            for (int i = 0; i < _boxList.Count; i++)
            {
                if (!_mouseRect.IntersectsWith(_miniMap))
                {
                    if (_mouseRect.IntersectsWith(new Rectangle(_boxList[i].RelativePosition, _boxList[i].RelativeSize)))
                    {
                        _selectedBoxes.Add(_map[_boxList[i].Line, _boxList[i].Column]);
                        g.DrawRectangle(Pens.White, new Rectangle(_boxList[i].RelativePosition, _boxList[i].RelativeSize));
                        g.DrawString("Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font("Arial", 10f), Brushes.Aqua, _boxList[i].RelativePosition);
                    }
                }
            }
        }

        public Rectangle MouseSelector
        {
            get { return _mouseRect; }
            set { _mouseRect = value; }
        }

        /// <summary>
        /// Draw the selection rectangle to fill textures
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mouseRect"></param>
        public void FillMouseSelector(Graphics g)
        {
            int count = 0;
            _selectedBoxes.Clear();
            for (int i = 0; i < _boxList.Count; i++)
            {
                _mouseRect.Width = _boxList[i].RelativeSize.Width / 4;
                _mouseRect.Height = _boxList[i].RelativeSize.Height / 4;
                if (_mouseRect.IntersectsWith(new Rectangle(_boxList[i].RelativePosition, _boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    _selectedBoxes.Add(_map[_boxList[i].Line, _boxList[i].Column]);
                    g.FillEllipse( new SolidBrush( Color.FromArgb( 52, 152, 219 ) ), new Rectangle( _mouseRect.X - (_mouseRect.Width / 2), _mouseRect.Y - (_mouseRect.Height / 2), _mouseRect.Width, _mouseRect.Height ) );
                    g.DrawString("Box X :" + (_boxList[i].Area.X).ToString() + "\nBox Y :" + (_boxList[i].Area.Y).ToString() + "\nBox Texture : \n" + _boxList[i].Ground.ToString(), new Font("Arial", 10f), Brushes.Aqua, _boxList[i].RelativePosition);
                }
            }
        }


        public void MoveX(int centimeters) 
        {
            Offset( new Point( centimeters, 0 ) );
        }
        public void MoveY( int centimeters )
        {
            Offset( new Point( 0, centimeters ) );
        }

        /// <summary>
        /// Select the boxes with the targetedColor texture, and remplace them with the desired Color
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetColor">Texture you wish to change</param>
        /// <param name="Color">Remplacement color</param>
        public void FillBox(Box target, BoxGround targetColor, BoxGround Color)
        {

            if (target.Ground == targetColor && Color != target.Ground)
            {
                target.Ground = Color;
                if (target.Top != null)
                {
                    FillBox(target.Top, targetColor, Color);
                }
                if (target.Bottom != null)
                {
                    FillBox(target.Bottom, targetColor, Color);
                }
                if (target.Left != null)
                {
                    FillBox(target.Left, targetColor, Color);
                }
                if (target.Right != null)
                {
                    FillBox(target.Right, targetColor, Color);
                }
            }
        }

        public List<Box> SelectedBox
        {
            get { return _selectedBoxes; }
        }

        public void ChangeTexture(BoxGround SelectedTexture)
        {
            if (this.IsChangeTextureSelected)
            {
                foreach (Box box in this.SelectedBox)
                {
                    box.Ground = SelectedTexture;
                }

            }
            if (this.IsFillTextureSelected)
            {
                    foreach (Box box in this.SelectedBox)
                    {
                        this.FillBox(box, box.Ground, SelectedTexture);
                    }
            }
        }

        public bool IsChangeTextureSelected
        {
            get { return _changeTexture; }
            set { _changeTexture = value; _fillTexture = false; }
        }

        public bool IsFillTextureSelected
        {
            get { return _fillTexture; }
            set { _fillTexture = value; _changeTexture = false; }
        }

        public void DrawRectangleInViewPort( Graphics g,Rectangle source, Rectangle target, Rectangle viewPort, Rectangle targetMiniMap, Rectangle viewPortMiniMap )
        {
            int newSize = (int)(((double)source.Width / (double)viewPort.Width) * target.Width + 1);
            int newHeight = (int)(((double)source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.X / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width))) - (int)(viewPort.Y / (source.Width / (((double)source.Width / (double)viewPort.Width) * target.Width)));

            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.FillRectangle( Brushes.Black, new Rectangle( newXpos + target.X, newYpos + target.Y, newSize, newHeight ) );
            g.FillRectangle( Brushes.Black, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }

        public void DrawViewPortMiniMap( Graphics g, Rectangle source, Rectangle targetMiniMap, Rectangle viewPortMiniMap )
        {
            int newSizeMini = (int)(((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newHeightMini = (int)(((double)source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini = (int)(source.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.X / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini = (int)(source.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width))) - (int)(viewPortMiniMap.Y / (source.Width / (((double)source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            g.DrawRectangle( Pens.White, new Rectangle( newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini ) );
        }
       
    }

}
