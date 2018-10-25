using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        /// 移動できる狭い通路のノード候補
        /// </summary>
        public List<Node> NextNodesWithNarrow { get; set; } = new List<Node>();


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
        ///
        ///<param name="nodes">移動候補先すべてのノード</param>
        ///<param name="fixedNode"> 確定ノードの保存先</param>

        public void DoDijikstra(List<Node> nodes, ref Node fixedNode, 
            AgentBase agent, List<AgentBase> agents, bool narrow = false)
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

                    //ダイクストラで探索
                    node.DoDijikstra(node.NextNodes, ref fixedNode , agent, agents);
                    //狭い通路の探索
                    node.DoDijikstra(node.NextNodesWithNarrow, ref fixedNode, agent, agents, true);
                }
                //確定ノードではないときはコスト計算を行う
                else
                {
                    //コスト計算
                    //　TODO 距離以外も用いた計算の実装
                    double temporaryCost;

                    if(narrow)
                    {
                        temporaryCost = CalculateCostNarrow(node, agent);
                    }
                    else
                    {
                        temporaryCost = CalculateCost(node);
                        //ノード間の距離を水平に保つように回転させる
                        //Atan2では水平での角度を見ているため戻す場合はマイナスする
                        var theta = -Math.Atan2(this.Y - node.Y, this.X - node.X);

                        var Point = Rotate2D(node, this, theta);

                        Point.Y += 25;

                        var rect = new Rect(new Point(this.X, this.Y - 25), Point);

                        int count = 0;

                        foreach (var anotherAgent in agents)
                        {
                            if (anotherAgent == agent)
                            {
                                continue;
                            }

                            if (rect.Contains(Rotate2D(anotherAgent.Node, this, theta)))
                            {
                                count++;
                            }
                        }

                        if ((count * 100) / this.DistanceFromNode(node) > 2)
                        {
                            temporaryCost += count * 100;
                        }
                    }

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

        //---------------------------------------
        ///<summary>
        ///細い通路のコスト計算
        /// </summary>
        /// <param name="agent">エージェント</param>
        /// <param name="node">移動先のノード</param>
        //---------------------------------------
        private double CalculateCostNarrow(Node node , AgentBase agent)
        {
            if(DistanceCost >= 100000)
            {
                return ((agent.Speed / agent.SpeedNarrow)) * this.DistanceFromNode(node);
            }
            return DistanceCost + ((agent.Speed / agent.SpeedNarrow))+ this.DistanceFromNode(node);
        }




        //---------------------------------------
        ///<summary>
        ///座標を回転させる関数
        /// </summary>
        /// <param name="center">回転の中心となるノード</param>
        /// <param name="node">回転させたいノード</param>
        /// <param name="theta">回転角度</param>
        //----------------------------------------
        private Point Rotate2D(Node node, Node center, double theta)
        {
            var x = node.X;
            var y = node.Y;
            var centerX = center.X;
            var centerY = center.Y;

            //数学座標と同じにする
            y = -y;
            centerY = -centerY;

            var result = new Point
            {
                X = (x - centerX) * Math.Cos(theta) - (y - centerY) * Math.Sin(theta) + centerX,
                Y = -1.0 * ((x - centerX) * Math.Sin(theta) + (y - centerY) * Math.Cos(theta) + centerY)
            };

            return result;
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
        Goal,

        ///<summary>
        ///線が下にある階段の仮ゴール
        /// </summary>
        Temporary1,
    

        ///<summary>
        ///線が上にある階段の仮ゴール
        /// </summary>
        Temporary2

    }

}
