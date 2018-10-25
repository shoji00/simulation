using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoji_simulation
{
    ///<summary>
    ///エージェントのBaseクラス
    /// </summary>
    public class AgentBase
    {
        ///<summary>
        ///エージェントの半径
        /// </summary>
        public double Radius{get; set;} = 25;

        ///<summary>
        ///普通のエージェントの速度[m/s]
        /// </summary>
        public double Speed { get; set; } = 1.5;

        ///<summary>
        ///遅いエージェントの速度[m/s]
        /// </summary>
        public double DownSpeed { get; set; } = 1.2;

        ///<summary>
        ///狭い通路での移動速度[m/s]
        /// </summary>
        public double SpeedNarrow { get; set; } = 1.0;

        ///<summary>
        ///距離のみのコスト
        /// </summary>
        public double DistanceCost { get; set; } = 0.0;

        ///<summary>
        ///エージェントの現在ノード
        /// </summary>
        public Node Node { get; set; }

        ///<summary>
        ///実際に避難する経路を順番に並べる
        /// </summary>
        public List<Node> RouteNode { get; set; } = new List<Node>();

        ///<summary>
        ///自分を含めたエージェントのリスト
        /// </summary>
        public List<AgentBase> Agents { get; set; } = new List<AgentBase>();

        //
        ///<summary>
        ///コンストラクタ
        ///</summary>
        ///<param name="x">x座標</param>
        ///<param name="y">y座標</param>
        
        public AgentBase(double x,double y)
        {
            Node = new Node(x, y, NodeKind.Start);
        }


        ///<summary>
        ///移動
        /// </summary>
        ///<param name="node">移動先のノード</param>
        ///<returns>
        ///true:ノードに到着した
        ///false:ノードに到着してない
        /// </returns>
        
        public bool MoveTo(Node node)
        {
            ///<param name="distance">距離</param>
            var distance = this.Node.DistanceFromNode(node);
            //1ステップは0.5秒なので2で割る
            ///<param name="movableDistance">移動可能な距離</param>
            var movableDistance = this.Speed * 100 / 2;
            var theta = Math.Atan2(node.Y - this.Node.Y, node.X - this.Node.X);

            var PositionX = this.Node.X;
            var PositionY = this.Node.Y;

            if(movableDistance > distance)
            {
                this.Node.X = node.X;
                this.Node.Y = node.Y;
            }
            else
            {
                var magnification = movableDistance / distance;

                this.Node.X += magnification * distance * Math.Cos(theta);
                this.Node.Y += magnification * distance * Math.Sin(theta);
            }

            foreach(var agent in Agents)
            {
                if(agent == this)
                {
                    continue;
                }

                if(this.Node.DistanceFromNode(agent.Node) < this.Radius + agent.Radius)
                {
                    this.Node.X = PositionX;
                    this.Node.Y = PositionY;
                }
            }

            if(this.Node.DistanceFromNode(node) < this.Radius / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
