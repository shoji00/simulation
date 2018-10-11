using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace shoji_simulation
{
    public static class Djikstra
    {
        public static bool IsColidedSomething(Node node1, Node node2, double tolerance, StationLayoutParam station)
        {
            ///<summary>
            ///ノード間を中心線とする短径とその辺と全ての席との衝突判定をチェックする関数
            /// </summary>
            /// 
            ///<remarks>
            /// 
            /// ノード1○-----------○ノード2
            /// 
            /// </remarks>
            /// 
            ///<param name="node1">ノード1</param>
            ///<param name="node2">ノード2</param>
            ///<param name="tolerance">許容範囲</param>
            ///<param name="station">駅のレイアウト</param>
            ///<returns>
            ///true:衝突している
            ///false:衝突していない
            /// </returns>


            var x1 = node1.X;
            var x2 = node2.X;
            var y1 = node1.Y;
            var y2 = node2.Y;

            double theta = Math.Atan2(y2 - y1, x2 - x1);

            //改札上辺
            if (CheckCollisionDeterminationWithAllkaisatu(
                new Point(x1 - tolerance * Math.Sin(theta), y1 + tolerance * Math.Cos(theta)),
                new Point(x2 - tolerance * Math.Sin(theta), y2 + tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            //右辺
            if (CheckCollisionDeterminationWithAllkaisatu(
                new Point(x2 - tolerance * Math.Sin(theta), y2 + tolerance * Math.Cos(theta)),
                new Point(x2 + tolerance * Math.Sin(theta), y2 - tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            //下辺
            if (CheckCollisionDeterminationWithAllkaisatu(
                new Point(x2 + tolerance * Math.Sin(theta), y2 - tolerance * Math.Cos(theta)),
                new Point(x1 + tolerance * Math.Sin(theta), y1 - tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            //左辺
            if (CheckCollisionDeterminationWithAllkaisatu(
                new Point(x1 + tolerance * Math.Sin(theta), y1 - tolerance * Math.Cos(theta)),
                new Point(x1 - tolerance * Math.Sin(theta), y1 + tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }



            //駅員室上辺
            if (CheckCollisionDeterminationWithAllroom(
                new Point(x1 - tolerance * Math.Sin(theta), y1 + tolerance * Math.Cos(theta)),
                new Point(x2 - tolerance * Math.Sin(theta), y2 + tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            //右辺
            if (CheckCollisionDeterminationWithAllroom(
                new Point(x2 - tolerance * Math.Sin(theta), y2 + tolerance * Math.Cos(theta)),
                new Point(x2 + tolerance * Math.Sin(theta), y2 - tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            //下辺
            if (CheckCollisionDeterminationWithAllroom(
                new Point(x2 + tolerance * Math.Sin(theta), y2 - tolerance * Math.Cos(theta)),
                new Point(x1 + tolerance * Math.Sin(theta), y1 - tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }


            //左辺
            if (CheckCollisionDeterminationWithAllroom(
                new Point(x1 + tolerance * Math.Sin(theta), y1 - tolerance * Math.Cos(theta)),
                new Point(x1 - tolerance * Math.Sin(theta), y1 + tolerance * Math.Cos(theta)),
                station))
            {
                return false;
            }

            return true;
        }

        ///<summary>
        ///辺と席との衝突判定
        /// </summary>
        /// <param name="r1">辺の座標1</param>
        /// <param name="r2">辺の座標2</param>
        /// <returns>
        /// true:衝突してる
        /// false:衝突してない
        /// </returns>
        /// 改札
        private static bool CheckCollisionDeterminationWithAllkaisatu(Point r1, Point r2, StationLayoutParam station)
        {
            bool t1, t2;

            foreach(var kaisatu in station.Kaisatus)
            {
                    //改札の上辺
                    var p1 = new Point(kaisatu.PositionX - (kaisatu.Width / 2), kaisatu.PositionY -(kaisatu.Height / 2));
                    var p2 = new Point(kaisatu.PositionX + (kaisatu.Width / 2), kaisatu.PositionY - (kaisatu.Height / 2));

                    t1 = CheckCollisionSide(r1, r2, p1, p2);
                    t2 = CheckCollisionSide(p1, p2, r1, r2);

                    if(t1 && t2)
                    {
                        return true;
                    }

                    //改札の右辺
                     p1 = new Point(kaisatu.PositionX + (kaisatu.Width / 2), kaisatu.PositionY - (kaisatu.Height / 2));
                     p2 = new Point(kaisatu.PositionX + (kaisatu.Width / 2), kaisatu.PositionY + (kaisatu.Height / 2));

                    t1 = CheckCollisionSide(r1, r2, p1, p2);
                    t2 = CheckCollisionSide(p1, p2, r1, r2);

                    if (t1 && t2)
                    {
                        return true;
                    }

                    //改札の下辺
                     p1 = new Point(kaisatu.PositionX + (kaisatu.Width / 2), kaisatu.PositionY + (kaisatu.Height / 2));
                     p2 = new Point(kaisatu.PositionX - (kaisatu.Width / 2), kaisatu.PositionY + (kaisatu.Height / 2));

                    t1 = CheckCollisionSide(r1, r2, p1, p2);
                    t2 = CheckCollisionSide(p1, p2, r1, r2);

                    if (t1 && t2)
                    {
                        return true;
                    }

                    //改札の左辺
                     p1 = new Point(kaisatu.PositionX - (kaisatu.Width / 2), kaisatu.PositionY + (kaisatu.Height / 2));
                     p2 = new Point(kaisatu.PositionX - (kaisatu.Width / 2), kaisatu.PositionY - (kaisatu.Height / 2));

                    t1 = CheckCollisionSide(r1, r2, p1, p2);
                    t2 = CheckCollisionSide(p1, p2, r1, r2);

                    if (t1 && t2)
                    {
                        return true;
                    }

                
            }

            return false;
        }


        //駅員室
        private static bool CheckCollisionDeterminationWithAllroom(Point r1, Point r2, StationLayoutParam station)
        {
            bool t1, t2;

            foreach (var room in station.Rooms)
            {
                //駅員室の上辺
                var p1 = new Point(room.PositionX - (room.Width), room.PositionY - (room.Height / 2));
                var p2 = new Point(room.PositionX + (room.Width), room.PositionY - (room.Height / 2));

                t1 = CheckCollisionSide(r1, r2, p1, p2);
                t2 = CheckCollisionSide(p1, p2, r1, r2);

                if (t1 && t2)
                {
                    return true;
                }

                //駅員室の右辺
                p1 = new Point(room.PositionX + (room.Width), room.PositionY - (room.Height / 2));
                p2 = new Point(room.PositionX + (room.Width), room.PositionY + (room.Height / 2));

                t1 = CheckCollisionSide(r1, r2, p1, p2);
                t2 = CheckCollisionSide(p1, p2, r1, r2);

                if (t1 && t2)
                {
                    return true;
                }

                //駅員室の下辺
                p1 = new Point(room.PositionX + (room.Width), room.PositionY + (room.Height / 2));
                p2 = new Point(room.PositionX - (room.Width), room.PositionY + (room.Height / 2));

                t1 = CheckCollisionSide(r1, r2, p1, p2);
                t2 = CheckCollisionSide(p1, p2, r1, r2);

                if (t1 && t2)
                {
                    return true;
                }

                //駅員室の左辺
                p1 = new Point(room.PositionX - (room.Width), room.PositionY + (room.Height / 2));
                p2 = new Point(room.PositionX - (room.Width), room.PositionY - (room.Height / 2));

                t1 = CheckCollisionSide(r1, r2, p1, p2);
                t2 = CheckCollisionSide(p1, p2, r1, r2);

                if (t1 && t2)
                {
                    return true;
                }


            }

            return false;
        }






        /// <summary>
        /// 辺と辺の衝突判定
        /// </summary>
        /// <param name="r1">辺1の座標1</param>
        /// <param name="r2">辺1の座標2</param>
        /// <param name="p1">辺2の座標1</param>
        /// <param name="p2">辺2の座標2</param>
        /// <returns>
        /// true:衝突してる
        /// false:衝突してない
        /// </returns>
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
