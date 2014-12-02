using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LiveIT2._1
{
    [Serializable]
    public class Tree : Vegetation
    {
        public Tree(Map map, Point StartPosition) 
            :base(map, StartPosition)
        {
            Random r = new Random();
            int _random = r.Next( 400, 650 );
            List<VegetationTexture> RandomVegList = new List<VegetationTexture>() { VegetationTexture.Tree, VegetationTexture.Tree2, VegetationTexture.Tree3 };
            Random r2 = new Random();

            Texture = RandomVegList[r2.Next(0,RandomVegList.Count)];
            this.Size = new Size( _random, _random );
        }
    }
}
