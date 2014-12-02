using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveIT2._1
{
    [Serializable]
    public class Bush : Vegetation
    {
        public Bush(Map map, Point StartPosition) 
            :base(map, StartPosition)
        {
            this.Texture = VegetationTexture.Bush;
            this.Size = new Size( 150, 150 );
        }
    }
}
