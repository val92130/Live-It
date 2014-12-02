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
            Texture = VegetationTexture.Tree;
            this.Size = new Size( 500, 500 );
        }
    }
}
