using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoji_simulation
{
    class Djikstra
    {
        private static bool CheckCollisionSide(Point r1, Point r2, Point p1, Point p2)
        {
            double t1, t2;

            //衝突判定
            t1 = (r1.X - r2.X) * (p1.Y - r1.Y) + (r1.Y - r2.Y) * (r1.X - p1.X);
            t2 = (r1.X - r2.X) * (p2.Y - r1.Y) + (r1.Y - r2.Y) * (r1.X - p2.X);

            if(t1 * t2 < 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
