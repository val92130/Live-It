using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Rock : Vegetation
    {
        public Rock(Map _map, Point StartPosition) 
            :base(_map, StartPosition)
        {
            Random r = new Random();
            int _random = r.Next( 50, 300 );
            List<VegetationTexture> RandomVegList = new List<VegetationTexture>() { VegetationTexture.Rock, VegetationTexture.Rock2, VegetationTexture.Rock3 };
            Random r2 = new Random();
            Texture = RandomVegList[r2.Next( 0, RandomVegList.Count )];
            this.Size = new Size( _random, _random );
        }
    }
}
