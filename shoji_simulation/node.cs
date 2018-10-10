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

        ///<summary>
        ///現在のノードの１つ前のノード
        /// </summary>
        public Node PreviousNode { get; set; }


        /// <summary>
        /// 移動できる候補のノード
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


        ///<summary>
        ///コピーコンストラクタ
        /// </summary>

        public Node(Node node)
        {
            NodeStatus = node.NodeStatus;

            X = node.X;
            Y = node.Y;

            Radius = node.Radius;

            DistanceCost = node.DistanceCost;

            PreviousNode = node.PreviousNode?.Clone();
        }

        ///<summary>
        ///ダイクストラ法を用いた探索
        /// </summary>
        ///
        ///<remarks>再起関数</remarks>

        public void DoDijikstra(List<Node> nodes, ref Node fixedNode)
        {
            //自分を含めると無限ループの可能性がある
            foreach (var node in nodes)
            {
                if (this == node)
                {
                    continue;
                }
                //確定ノードの場合はさらにその先まで探索する
                else if (node.NodeStatus == NodeKind.Determined)
                {
                    if (node.NextNodes.Count() == 0)
                    {
                        break;
                    }

                    //確定したルート通りでないと探索が終わらない
                    if (node.PreviousNode != this)
                    {
                        continue;
                    }

                    //
                    node.DoDijikstra(node.NextNodes, ref fixedNode);
                }
                //確定ノードではないときはコスト計算を行う
                else
                {
                    //コスト計算
                    //　TODO 距離以外も用いた計算の実装
                    var temporaryCost = CalculateCost(node);

                    if (fixedNode == null)
                    {
                        fixedNode = node;
                        node.DistanceCost = temporaryCost;
                        node.PreviousNode = this;
                    }
                    else if (fixedNode.DistanceCost > temporaryCost)
                    {
                        fixedNode = node;
                        node.DistanceCost = temporaryCost;
                        node.PreviousNode = this;
                    }
                    else if (node.DistanceCost > temporaryCost)
                    {
                        node.DistanceCost = temporaryCost;
                        node.PreviousNode = this;

                    }
                }
            }

        }


        ///<summary>
        ///コスト計算
        /// </summary>

        private double CalculateCost(Node node)
        {
            //100000に意味はなく大きければよし
            if (DistanceCost >= 100000)
            {
                return this.DistanceFromNode(node);
            }

            return DistanceCost + this.DistanceFromNode(node);

        }

        ///<summary>
        ///ディープコピー
        /// </summary>
        /// <return>コピーしたノード</return>
        public Node Clone()
        {
            return new Node(this);
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
        Determined,

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
