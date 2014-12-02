using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LiveIT2._1;
using System.Drawing;

namespace LiveIT2._1
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test_Map()
        {
            Map map = new Map(200,20);
            Assert.That( map.MapSize == 200 );
            Assert.That( map.BoxSize == 20 * 100 );
        }

        [Test]
        public void Get_Box_Point()
        {
            Map map = new Map( 100, 40 );
            Rectangle r = new Rectangle( 0, 0, 1000, 1000 );
            List<Box> boxes = (List<Box>)map.GetOverlappedBoxes(r);
            int i = 0;
            foreach( Box box in boxes )
            {
                i++;
            }
            Assert.AreEqual( i, 2 );
        }
    }
}
