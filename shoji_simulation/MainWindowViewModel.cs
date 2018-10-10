using Newtonsoft.Json;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace shoji_simulation
{
    class MainWindowViewModel
    {
        /// <summary>
        /// 駅のレイアウト
        /// </summary>
        private StationLayoutParam station_;

        ///<summary>
        ///ノードのリスト
        ///</summary>
        private List<Node> nodes_;

        ///<summary>
        ///Xamlにある画像のコントロール
        ///todo:これがなくてもサイズを変更できるようになる
        /// </summary>
        private Image image_;

        ///<summary>
        ///エージェントのリスト
        /// </summary>
        private List<AgentBase> agents_;

        ///<summary>
        ///表示する画像のビットマップ
        ///</summary>
        public RenderTargetBitmap Bitmap { get; set; }

        ///<summary>
        ///レンタリング用のビジュアル
        ///</summary>
        public DrawingVisual DrawVisual { get; set; }

        /// <summary>
        /// ビジュアルコンテンツ
        /// </summary>
        public DrawingContext DrawContext { get; set; }

        ///<summary>
        ///レイアウトを読み込むコマンド
        /// </summary>
        public ReactiveCommand LoadLayoutCommand { get; set; }


        /// <summary>
        /// Xamlデザイナー用のコンストラクタ
        /// </summary>
        public MainWindowViewModel()
            : this(new Image())
        {
        }

        ///<summary>
        ///コンストラクタ
        ///<param name="Image">xamlにあるImageコントロール</param>
        /// </summary>
        public MainWindowViewModel(Image image)
        {
            station_ = new StationLayoutParam();

            nodes_ = new List<Node>();

            image_ = image;

            agents_ = new List<AgentBase>();

            Bitmap = new RenderTargetBitmap(
                1000,
                1000,
                96,
                96,
                PixelFormats.Default);

            DrawVisual = new DrawingVisual();

            LoadLayoutCommand = new ReactiveCommand();

            LoadLayoutCommand.Subscribe(_ => OpenFile());

        }

        /// <summary>
        /// Jsonファイルからレイアウト情報を読み込む関数
        /// </summary>
        public void OpenFile()
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Json File(.json)|*.json";
                fileDialog.Title = "開くファイルを選択してください";
                fileDialog.Multiselect = false;

                //ダイアログを表示する
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var streamReader = new StreamReader(fileDialog.FileName))
                    {
                        var json = streamReader.ReadToEnd();

                        station_ = JsonConvert.DeserializeObject<StationLayoutParam>(json);

                        nodes_.Clear();

                        agents_.Clear();

                        //ノードの設定  
                        //改札
                        foreach (var kaisatu in station_.Kaisatus)
                        {
                            double distance = 25;

                            SetUpNodes(new Node(
                                kaisatu.PositionX - distance,
                                kaisatu.PositionY - distance));

                            SetUpNodes(new Node(
                                kaisatu.PositionX - distance,
                                kaisatu.PositionY + kaisatu.Height + distance));


                        }

                        //駅員室やエレベーター
                        foreach (var room in station_.Rooms)
                        {
                            double distance = 15;

                            //左上
                            SetUpNodes(new Node(
                                room.PositionX - distance,
                                room.PositionY - distance));

                            //右上
                            SetUpNodes(new Node(
                                room.PositionX + room.Width + distance,
                                room.PositionY - distance));

                            //左下
                            SetUpNodes(new Node(
                                room.PositionX - distance,
                                room.PositionY + room.Height + distance));

                            //左上
                            SetUpNodes(new Node(
                                room.PositionX + room.Width + distance,
                                room.PositionY + room.Height + distance));
                        }

                        //上に線がある階段
                        foreach (var stairsUp in station_.StairsUp)
                        {
                            double distance = 15;


                            SetUpNodes(new Node(
                                stairsUp.PositionX - distance,
                                stairsUp.PositionY - distance));

                            SetUpNodes(new Node(
                                stairsUp.PositionX + stairsUp.Width + distance,
                                stairsUp.PositionY - distance));

                            SetUpNodes(new Node(
                                stairsUp.PositionX - distance,
                                  stairsUp.PositionY + stairsUp.Height + distance));

                            SetUpNodes(new Node(
                                stairsUp.PositionX + stairsUp.Width + distance,
                                stairsUp.PositionY + stairsUp.Height + distance));

                            //真ん中上
                            SetUpNodes(new Node(
                                stairsUp.PositionX + stairsUp.Width / 2,
                                stairsUp.PositionY + stairsUp.Height / 4));

                            //真ん中下
                            SetUpNodes(new Node(
                                stairsUp.PositionX + stairsUp.Width / 2,
                                stairsUp.PositionY + stairsUp.Height + distance));
                        }

                        //下に線がある階段
                        foreach (var stairsDown in station_.StairsDown)
                        {
                            double distance = 15;


                            SetUpNodes(new Node(
                                stairsDown.PositionX - distance,
                                stairsDown.PositionY - distance));

                            SetUpNodes(new Node(
                                stairsDown.PositionX + stairsDown.Width + distance,
                                stairsDown.PositionY - distance));

                            SetUpNodes(new Node(
                                stairsDown.PositionX - distance,
                                  stairsDown.PositionY + stairsDown.Height + distance));

                            SetUpNodes(new Node(
                                stairsDown.PositionX + stairsDown.Width + distance,
                                stairsDown.PositionY + stairsDown.Height + distance));

                            //真ん中上
                            SetUpNodes(new Node(
                                stairsDown.PositionX + stairsDown.Width / 2,
                                stairsDown.PositionY - distance));

                            //真ん中上
                            SetUpNodes(new Node(
                                stairsDown.PositionX + stairsDown.Width / 2,
                                stairsDown.PositionY + stairsDown.Height - distance));
                        }

                        //出口の設定
                        foreach (var goal in station_.Goals)
                        {
                            nodes_.Add(new Node(goal.PositionX, goal.PositionY, NodeKind.Goal));
                        }

                        //エージェントの設定(座標)
                        agents_.Add(new AgentBase(500, 500));

                        //全てのエージェントで移動可能かどうかを調べる
                        foreach (var agent in agents_)
                        {
                            //最初にエージェントの現在地から移動可能な候補を探す
                            foreach(var node in nodes_)
                                {
                                if (Djikstra.IsColidedSomething(agent.Node, node, 25, station_))
                                {
                                    agent.Node.NextNodes.Add(node);
                                }
                            }

                            Node goalNode = null;

                            while (true)
                            {
                                Node determinedNode = null;

                                //ダイクストラ法で探索
                                agent.Node.DoDijikstra(agent.Node.NextNodes, ref determinedNode);

                                //ゴールにたどり着けないエージェントがいるとき
                                //エージェントの初期位置がしっかりしていれば起きない
                                if (determinedNode == null)
                                {
                                    throw new Exception("ゴールにいけません");
                                }

                                //ゴールなら終了
                                if (determinedNode.NodeStatus == NodeKind.Goal)
                                {
                                    goalNode = determinedNode.Clone();
                                    break;
                                }

                                //ノードを確定ノードに
                                determinedNode.NodeStatus = NodeKind.Determined;

                                //確定ノードから移動可能なノードを全てNextNodesに保存
                                foreach (var node in nodes_)
                                {
                                    if (determinedNode == node || node.NodeStatus == NodeKind.Determined)
                                    {
                                        continue;
                                    }

                                    if (Djikstra.IsColidedSomething(determinedNode, node, 10, station_))
                                    {
                                        determinedNode.NextNodes.Add(node);
                                    }

                                }
                            }

                            ///出口までの経路をエージェントに保持させる
                            //この後レイアウト内のノードは全て初期化されるのでディープコピーしたものを渡す
                            //ゴールを見つけた時にゴールノードとゴールまでに通るノードをディープコピーしてあるためここでCloneしなくてよい
                            while (true)
                            {
                                agent.RouteNode.Add(goalNode);

                                goalNode = goalNode.PreviousNode;

                                if (goalNode.NodeStatus == NodeKind.Start)
                                {
                                    agent.RouteNode.Reverse();
                                    break;
                                }
                            }

                            //ノードの初期化
                            foreach (var node in nodes_)
                            {
                                node.NextNodes.Clear();

                                if (node.NodeStatus != NodeKind.Goal)
                                {
                                    node.NodeStatus = NodeKind.Unsearched;
                                }

                                node.PreviousNode = null;
                                node.DistanceCost = double.MaxValue;
                            }
                        }


                        DrawLayout();

                    }
                }

            }
        }

        ///<summary>
        ///ノードが他のノードの近くにあった時統合する
        /// </summary>
        public void SetUpNodes(Node node)
        {
            foreach (var nodes in nodes_)
            {
                if (nodes.DistanceFromNode(node) < 25)
                {
                    nodes.X = (nodes.X + nodes.X) / 2.0;
                    nodes.Y = (nodes.Y + nodes.Y) / 2.0;
                    return;
                }
            }
            nodes_.Add(node);
        }

        /// <summary>
        /// レイアウトを描画する関数
        /// </summary>
        /// <param name="layout">レイアウトの情報</param>
        public void DrawLayout()
        {

            double radius = 5;

            Bitmap = new RenderTargetBitmap(
                station_.Width,
                station_.Height,
                96,
                96,
                PixelFormats.Default);

            //これをしないと画像が更新されない
            image_.Source = Bitmap;

            DrawContext = DrawVisual.RenderOpen();

            //描画するオブジェクトの作成
            DrawContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, station_.Width, station_.Height));

            //改札の描画
            foreach (var kaisatu in station_.Kaisatus)
            {
                DrawContext.DrawRectangle(
                    Brushes.Yellow,
                    new Pen(Brushes.Black, 1),
                    new Rect(kaisatu.PositionX, kaisatu.PositionY, kaisatu.Width, kaisatu.Height));
            }


            //駅員室の描画
            foreach (var room in station_.Rooms)
            {
                DrawContext.DrawRectangle(
                    Brushes.Black,
                    new Pen(Brushes.Black, 1),
                    new Rect(room.PositionX, room.PositionY, room.Width, room.Height));
            }

            //上に線がある階段の描画
            foreach (var stairsUp in station_.StairsUp)
            {
                ///<summary>
                ///左の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsUp.PositionX, stairsUp.PositionY),
                    new Point(stairsUp.PositionX, stairsUp.PositionY + stairsUp.Height));

                ///<summary>
                ///右の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsUp.PositionX + stairsUp.Width, stairsUp.PositionY),
                    new Point(stairsUp.PositionX + stairsUp.Width, stairsUp.PositionY + stairsUp.Height));

                ///<summary>
                ///上の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsUp.PositionX, stairsUp.PositionY),
                    new Point(stairsUp.PositionX + stairsUp.Width, stairsUp.PositionY));
            }


            //下に線がある階段の描画
            foreach (var stairsDown in station_.StairsDown)
            {
                ///<summary>
                ///左の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsDown.PositionX, stairsDown.PositionY),
                    new Point(stairsDown.PositionX, stairsDown.PositionY + stairsDown.Height));

                ///<summary>
                ///右の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsDown.PositionX + stairsDown.Width, stairsDown.PositionY),
                    new Point(stairsDown.PositionX + stairsDown.Width, stairsDown.PositionY + stairsDown.Height));

                ///<summary>
                ///下の線
                /// </summary>
                DrawContext.DrawLine(
                    new Pen(Brushes.Black, 1),
                    new Point(stairsDown.PositionX, stairsDown.PositionY + stairsDown.Height),
                    new Point(stairsDown.PositionX + stairsDown.Width, stairsDown.PositionY + stairsDown.Height));
            }

            //出口の描画
            foreach (var goal in station_.Goals)
            {
                DrawContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(goal.PositionX, goal.PositionY, goal.Width, goal.Height));
            }

            //ベンチの描画
            foreach (var bench in station_.Benchs)
            {
                DrawContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(bench.PositionX, bench.PositionY, bench.Width, bench.Height));
            }

            //柱の描画
            foreach (var pillar in station_.Pillars)
            {
                DrawContext.DrawRectangle(
                    Brushes.Blue,
                    null,
                    new Rect(pillar.PositionX, pillar.PositionY, pillar.Width, pillar.Height));
            }


            //ノードの描画
            foreach (var node in nodes_)
            {
                DrawContext.DrawEllipse(
                    Brushes.Blue,
                    null,
                    new Point(node.X, node.Y), radius, radius);
            }

            //エージェントの描画
            foreach (var agent in agents_)
            {
                DrawContext.DrawEllipse(
                    null,
                    new Pen(Brushes.Green, 1),
                    new Point(agent.Node.X, agent.Node.Y), agent.Radius, agent.Radius);

                //経路の線
                foreach(var node in agent.RouteNode)
                {
                    DrawContext.DrawLine(
                        new Pen(Brushes.Red, 10),
                        new Point(node.PreviousNode.X, node.PreviousNode.Y),
                        new Point(node.X, node.Y));
                }
            }



            DrawContext.Close();

            //表示する画像を更新 
            Bitmap.Render(DrawVisual);
        }

    }

}
