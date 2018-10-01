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
    class AgentBase
    {
        ///<summary>
        ///エージェントの半径
        /// </summary>
        public double Radius{get; set;} = 25.0;

        ///<summary>
        ///エージェントの速度
        /// </summary>
        public double Speed { get; set; } = 15.0;

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

    }
}
