using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoji_simulation
{
    public class Node
    {
        ///<summary>
        ///ノードの現在地
        /// </summary>
        public NodeKind NodeStatus { get; set; }

        /// <summary>
        /// ノードの中心のX座標
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// ノードの中心のY座標
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 移動候補先のノード
        /// </summary>
        public List<Node> NextNodes { get; set; } = new List<Node>();

        /// <summary>
        /// ノードの半径
        /// </summary>
        public double Radius { get; set; } = 10;

        ///<summary>
        ///このノードまでの最短距離のコスト
        /// </summary>
        public double DistanceCost { get; set; } = double.MaxValue;

        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Node(double x, double y, NodeKind kind = NodeKind.Unsearched)
        {
            X = x;
            Y = y;
            NodeStatus = kind;
        }

    }

    /// <summary>
    /// ノードの拡張クラス
    /// </summary>
    public static class NodeExpansion
    {
       /// <summary>
       /// ノード間の距離を計算
       /// </summary>
       /// <param name="node1">自身のノード</param>
       /// <param name="node2">ノード</param>
       /// <returns></returns>
        public static double DistanceFromNode(this Node node1, Node node2)
        {
            return Math.Sqrt(
                (node1.X - node2.X) * (node1.X - node2.X) + (node1.Y - node2.Y) * (node1.Y - node2.Y));

        }
    }


    /// <summary>
    /// ノードの種類
    /// </summary>
    public enum NodeKind
    {
        ///<summary>
        ///未探索
        /// </summary>
        Unsearched,

        ///<summary>
        ///確定
        /// </summary>
        Determind,

        ///<summary>
        ///スタート
        /// </summary>
        Start,

        ///<summary>
        ///ゴール
        /// </summary>
        Goal
    }

}
