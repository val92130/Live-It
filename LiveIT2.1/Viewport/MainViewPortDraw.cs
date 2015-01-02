using LiveIT2._1.Animals;
using LiveIT2._1.Enums;
using LiveIT2._1.Terrain;
using LiveIT2._1.Textures;
using LiveIT2._1.Vehicules;
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
        /// The draw.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void Draw(Graphics g)
        {
            this.MoveWithMouse();

            if (!this.followAnimal)
            {
                this.followedAnimal = null;
            }

            if (this.IsFollowingAnAnimal)
            {
                if (this.followedAnimal != null)
                {
                    this.AdjustViewPort(this.followedAnimal);                }
                else
                {
                    this.isFollowingAnAnimal = false;
                }
            }
            else
            {
                this.AdjustViewPort();
            }


            // Create the rain
            var t = new Random();
            if (t.Next(MinStartRain, MaxStartRain) == 30)
            {
                this.map.IsRaining = true;
            }

            if (t.Next(MinStopRain, MaxStopRain) == 40 && this.map.IsRaining)
            {
                this.map.IsRaining = false;
            }

            this.boxList = this.map.GetOverlappedBoxes(this.viewPort);
            this.boxListMini = this.map.GetOverlappedBoxes(this.miniMapViewPort);
            this.mouseRect.X = Cursor.Position.X - (this.mouseRect.Width / 2);
            this.mouseRect.Y = Cursor.Position.Y - (this.mouseRect.Height / 2);

            this.DrawBoxes(g);

            foreach (Rectangle r in this.map.BloodList)
            {
                this.DrawBloodInViewPort(g, r, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort);
            }

            this.DrawLandAnimals(g);

            this.PlayerBehavior(g);

            this.DrawCars(g);

            this.DrawVegetation(g);

            this.DrawFlyingAnimals(g);

            if (this.changeTexture)
            {
                this.DrawMouseSelector(g);
            }

            if (this.fillTexture)
            {
                this.FillMouseSelector(g);
            }

            if (this.putAnimal)
            {
                this.PutAnimalSelector(g);
            }

            if (this.putMapElement)
            {
                this.PutVegetationSelector(g);
            }

            this.AnimalFollowing(g);

            this.MakeRain(g);


            this.CheckIfPlayerHasEnteredACar();

            

            if( this.IsFollowingAnAnimal )
            {
                if( followedAnimal != null )
                {
                    g.DrawString( "Health : " + followedAnimal.Health.ToString(), new Font( "Arial", 10f ), Brushes.Black, new Point( followedAnimal.RelativePosition.X, followedAnimal.RelativePosition.Y - 60 ) );
                    g.DrawString( "Sex : " + followedAnimal.ESex.ToString(), new Font( "Arial", 10f ), Brushes.Black, new Point( followedAnimal.RelativePosition.X, followedAnimal.RelativePosition.Y - 40 ) );
                    g.DrawString( "Hunger : " + followedAnimal.Hunger.ToString(), new Font( "Arial", 10f ), Brushes.Black, new Point( followedAnimal.RelativePosition.X, followedAnimal.RelativePosition.Y - 20 ) );

                }
            }
            
            this.DrawMiniMapBoxes(g);
            DrawAnimalsInMiniMap(g);
            DrawVegetationInMiniMap(g);



            this.DrawViewPortMiniMap(g, this.viewPort, this.miniMap, this.miniMapViewPort);
            g.DrawRectangle(
    Pens.White,
    new Rectangle(this.miniMap.X, this.miniMap.Y, this.miniMap.Width, this.miniMap.Height + 20));

            foreach (Box b in map.Boxes)
            {
                Rectangle r = new Rectangle(new Point(this.MouseSelector.X, this.MouseSelector.Y - this.ScreenSize.Height + this.MiniMap.Height), this.MouseSelector.Size);
                if (r.IntersectsWith(b.RelativeMiniMapArea))
                {
                    if (this.HasClicked)
                    {
                        if (this.map.IsPlayer)
                        {
                            _pathFindTarget = b;
                            _playerPathFinder = new PathFinder(this.Player.OverlappedBox, _pathFindTarget, map);
                            while (!_playerPathFinder.FoundTarget)
                            {
                                _playerPathFinder.Update();
                                if (_playerPathFinder.IsStuck)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    
                }
            }

            if (_playerPathFinder != null && map.IsPlayer)
            {
                if (player.IsMoving)
                {
                    _playerPathFinder = new PathFinder(this.Player.OverlappedBox, _pathFindTarget, map);
                    while (!_playerPathFinder.FoundTarget)
                    {
                        _playerPathFinder.Update();
                        if (_playerPathFinder.IsStuck)
                        {
                            return;
                        }
                    }
                }
                if (!_playerPathFinder.IsStuck)
                {
                    foreach (Box b in _playerPathFinder.finalPath)
                    {
                        this.DrawRectangleInViewPort(g, b.CenteredArea, this.MiniMap, this.miniMapViewPort, Brushes.White);
                    }
                }
                else
                {
                    _playerPathFinder = null;
                }
            }

            if (map.IsPlayer)
            {
                if (_playerPathFinder != null)
                {
                    if (player.Area.IntersectsWith(_pathFindTarget.Area))
                    {
                        _playerPathFinder = null;
                    }
                }

                this.DrawRectangleInViewPort(g, player.Area, this.MiniMap, this.miniMapViewPort, Brushes.Black);
            }
            this.HasClicked = false;        
        }

        public void Update()
        {
            this.UpdateAnimals();
        }

        public void DrawVegetationInMiniMap(Graphics g)
        {
            for (int i = 0; i < this.map.Vegetation.Count; i++)
            {
                this.map.Vegetation[i].DrawInMiniMap(g,
                        this.screen,
                        this.viewPort,
                        this.miniMap,
                        this.miniMapViewPort,
                        this.texture);
            }
        }

        public void DrawAnimalsInMiniMap(Graphics g)
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                map.Animals[i].DrawMiniMap(g,
                        this.screen,
                        this.viewPort,
                        this.miniMap,
                        this.miniMapViewPort,
                        this.texture);
            }
        }

        /// <summary>
        /// The draw blood in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
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
        public void DrawBloodInViewPort(
            Graphics g,
            Rectangle source,
            Rectangle target,
            Rectangle viewPort,
            Rectangle targetMiniMap,
            Rectangle viewPortMiniMap)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage(
                this.texture.GetBlood(),
                new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawRectangle(
                Pens.Red,
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// Draw the selection rectangle to change the textures
        /// </summary>
        /// <param name="g">
        /// </param>
        public void DrawMouseSelector(Graphics g)
        {
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                if (!this.mouseRect.IntersectsWith(this.miniMap))
                {
                    if (
                        this.mouseRect.IntersectsWith(
                            new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)))
                    {
                        if (this.boxList[i].AnimalList.Count != 0)
                        {
                            g.DrawRectangle(
                                Pens.Red,
                                new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize));
                        }
                        else
                        {
                            g.DrawRectangle(
                                Pens.White,
                                new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize));
                            this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                        }

                        g.DrawString(
                            "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y
                            + "\nBox Texture : \n" + this.boxList[i].Ground,
                            new Font("Arial", 10f),
                            Brushes.Aqua,
                            this.boxList[i].RelativePosition);
                    }
                }
            }
        }

        /// <summary>
        /// The draw rectangle in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
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
        public void DrawRectangleInViewPort(
            Graphics g,
            Rectangle source,
            Rectangle target,
            Rectangle viewPort,
            Rectangle targetMiniMap,
            Rectangle viewPortMiniMap)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawRectangle(Pens.Blue, new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawRectangle(
                Pens.Blue,
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        public void DrawRectangleInViewPort(
            Graphics g,
            Rectangle source,
            Rectangle targetMiniMap,
            Rectangle viewPortMiniMap,
            Brush b)
        {

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.FillRectangle(
                b,
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// The draw rectangle in view port.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
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
        /// <param name="animal">
        /// The animal.
        /// </param>
        /// <param name="t">
        /// The t.
        /// </param>
        public void DrawRectangleInViewPort(
            Graphics g,
            Rectangle source,
            Rectangle target,
            Rectangle viewPort,
            Rectangle targetMiniMap,
            Rectangle viewPortMiniMap,
            Animal animal,
            Texture t)
        {
            var newSize = (int)((source.Width / (double)viewPort.Width) * target.Width + 1);
            var newHeight = (int)((source.Height / (double)viewPort.Width) * target.Width + 1);
            int newXpos = (int)(source.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.X / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));
            int newYpos = (int)(source.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)))
                          - (int)
                            (viewPort.Y / (source.Width / ((source.Width / (double)viewPort.Width) * target.Width)));

            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));

            g.DrawImage(
                t.LoadTexture(animal),
                new Rectangle(newXpos + target.X, newYpos + target.Y, newSize, newHeight));
            g.DrawImage(
                t.LoadTexture(animal),
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// The draw view port mini map.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="targetMiniMap">
        /// The target mini map.
        /// </param>
        /// <param name="viewPortMiniMap">
        /// The view port mini map.
        /// </param>
        public void DrawViewPortMiniMap(
            Graphics g,
            Rectangle source,
            Rectangle targetMiniMap,
            Rectangle viewPortMiniMap)
        {
            var newSizeMini = (int)((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            var newHeightMini = (int)((source.Height / (double)viewPortMiniMap.Width) * targetMiniMap.Width + 1);
            int newXposMini =
                (int)
                (source.X / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.X
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            int newYposMini =
                (int)
                (source.Y / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)))
                - (int)
                  (viewPortMiniMap.Y
                   / (source.Width / ((source.Width / (double)viewPortMiniMap.Width) * targetMiniMap.Width)));
            g.DrawRectangle(
                Pens.White,
                new Rectangle(newXposMini + targetMiniMap.X, newYposMini + targetMiniMap.Y, newSizeMini, newHeightMini));
        }

        /// <summary>
        /// Select the boxes with the targetedColor texture, and remplace them with the desired Color
        /// </summary>
        /// <param name="target">
        /// </param>
        /// <param name="targetColor">
        /// Texture you wish to change
        /// </param>
        /// <param name="Color">
        /// Remplacement color
        /// </param>
        public void FillBox(Box target, EBoxGround targetColor, EBoxGround Color)
        {
            if (target.Ground == targetColor && Color != target.Ground)
            {
                target.Ground = Color;
                if (target.Top != null)
                {
                    this.FillBox(target.Top, targetColor, Color);
                }

                if (target.Bottom != null)
                {
                    this.FillBox(target.Bottom, targetColor, Color);
                }

                if (target.Left != null)
                {
                    this.FillBox(target.Left, targetColor, Color);
                }

                if (target.Right != null)
                {
                    this.FillBox(target.Right, targetColor, Color);
                }
            }
        }

        /// <summary>
        /// Draw the selection rectangle to fill textures
        /// </summary>
        /// <param name="g">
        /// </param>
        public void FillMouseSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    g.FillEllipse(
                        new SolidBrush(Color.FromArgb(52, 152, 219)),
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground,
                        new Font("Arial", 10f),
                        Brushes.Aqua,
                        this.boxList[i].RelativePosition);
                }
            }
        }

        /// <summary>
        /// The put animal selector.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void PutAnimalSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    this.animalSelectorCursor.X = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.X;
                    this.animalSelectorCursor.Y = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.Y;
                    g.FillEllipse(
                        new SolidBrush(Color.Brown),
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground,
                        new Font("Arial", 10f),
                        Brushes.Aqua,
                        this.boxList[i].RelativePosition);
                }
            }
        }

        /// <summary>
        /// The put vegetation selector.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        public void PutVegetationSelector(Graphics g)
        {
            int count = 0;
            this.selectedBoxes.Clear();
            for (int i = 0; i < this.boxList.Count; i++)
            {
                this.mouseRect.Width = this.boxList[i].RelativeSize.Width / 4;
                this.mouseRect.Height = this.boxList[i].RelativeSize.Height / 4;
                if (
                    this.mouseRect.IntersectsWith(
                        new Rectangle(this.boxList[i].RelativePosition, this.boxList[i].RelativeSize)) && count != 1)
                {
                    count++;
                    this.selectedBoxes.Add(this.map[this.boxList[i].Line, this.boxList[i].Column]);
                    this.elementSelectorCursor.X = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.X;
                    this.elementSelectorCursor.Y = this.map[this.boxList[i].Line, this.boxList[i].Column].Area.Y;
                    g.FillEllipse(
                        new SolidBrush(Color.Brown),
                        new Rectangle(this.mouseRect.X, this.mouseRect.Y, this.mouseRect.Width, this.mouseRect.Height));
                    g.DrawString(
                        "Box X :" + this.boxList[i].Area.X + "\nBox Y :" + this.boxList[i].Area.Y + "\nBox Texture : \n"
                        + this.boxList[i].Ground,
                        new Font("Arial", 10f),
                        Brushes.Aqua,
                        this.boxList[i].RelativePosition);
                }
            }
        }

        /// <summary>
        /// The draw boxes.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawBoxes(Graphics g)
        {
            for (int i = 0; i < this.boxList.Count; i++)
            {
                for (int j = 0; j < this.map.Animals.Count(); j++)
                {
                    if (this.map.Animals[j].Area.IntersectsWith(this.boxList[i].Area))
                    {
                        this.boxList[i].AddAnimal(this.map.Animals[j]);
                    }
                }

                this.boxList[i].Draw(g, this.screen, this.texture, this.viewPort);
            }


        }

        private void DrawMiniMapBoxes(Graphics g)
        {
            for (int i = 0; i < this.boxListMini.Count; i++)
            {
                this.boxListMini[i].DrawMiniMap(g, this.miniMap, this.texture, this.miniMapViewPort);
            }
        }

        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < this.boxListMini.Count; i++)
            {
                this.boxListMini[i].DrawMiniMap(g, this.screen, this.texture, this.miniMapViewPort);
            }
        }

        /// <summary>
        /// The draw cars.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawCars(Graphics g)
        {
            foreach (Car car in this.carList)
            {
                car.Draw(g, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort, this.texture);
            }

            foreach (Tank tank in this.tankList)
            {
                tank.Draw(g, this.screen, this.viewPort, this.miniMap, this.miniMapViewPort, this.texture);
            }
        }

        /// <summary>
        /// The draw flying animals.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawFlyingAnimals(Graphics g)
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                if (this.map.Animals[i].Texture == EAnimalTexture.Eagle)
                {
                    this.map.Animals[i].Draw(
                        g,
                        this.screen,
                        this.viewPort,
                        this.miniMap,
                        this.miniMapViewPort,
                        this.texture);
                }
            }
        }

        /// <summary>
        /// The draw land animals.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawLandAnimals(Graphics g)
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                if (this.map.Animals[i].Texture != EAnimalTexture.Eagle)
                {
                    this.map.Animals[i].Draw(
                        g,
                        this.screen,
                        this.viewPort,
                        this.miniMap,
                        this.miniMapViewPort,
                        this.texture);
                }
            }
        }

        private void UpdateAnimals()
        {
            for (int i = 0; i < this.map.Animals.Count; i++)
            {
                this.map.Animals[i].Update();
            }
        }

        /// <summary>
        /// The draw vegetation.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void DrawVegetation(Graphics g)
        {
            for (int i = 0; i < this.map.Vegetation.Count; i++)
            {
                this.map.Vegetation[i].Draw(
                    g,
                    this.screen,
                    this.viewPort,
                    this.miniMap,
                    this.miniMapViewPort,
                    this.texture);
            }
        }

        /// <summary>
        /// The make rain.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        private void MakeRain(Graphics g)
        {
            if (this.map.IsRaining)
            {
                if (this.isRaining == false)
                {
                    this.Rain();
                    this.isRaining = true;
                }

                // g.DrawImage(_texture.GetThunder(), _screen);
                g.DrawImage(this.texture.GetRain(), this.screen);
            }
        }
    }
}
