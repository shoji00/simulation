using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoji_simulation
{
    /// <summary>
    /// 改札の情報クラス
    /// </summary>
    public class StationKaisatuParam
    {
        /// <summary>
        /// 改札の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 改札の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 改札のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 改札のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">改札の縦幅</param>
        /// <param name="height">改札の横幅</param>
        /// <param name="positionX">改札のX座標</param>
        /// <param name="positionY">改札のY座標</param>
        public StationKaisatuParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// 駅員室の情報クラス
    /// </summary>
    public class StationRoomParam
    {
        /// <summary>
        /// 駅員室の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 駅員室の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 駅員室のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 駅員室のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">駅員室の縦幅</param>
        /// <param name="height">駅員室の横幅</param>
        /// <param name="positionX">駅員室のX座標</param>
        /// <param name="positionY">駅員室のY座標</param>
        public StationRoomParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// 階段上に線の情報クラス
    /// </summary>
    public class StationStairsUpParam
    {
        /// <summary>
        /// 階段の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 階段の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 階段のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 階段のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">階段の縦幅</param>
        /// <param name="height">階段の横幅</param>
        /// <param name="positionX">階段のX座標</param>
        /// <param name="positionY">階段のY座標</param>
        public StationStairsUpParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// 階段上に線の情報クラス
    /// </summary>
    public class StationStairsDownParam
    {
        /// <summary>
        /// 階段の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 階段の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 階段のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 階段のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">階段の縦幅</param>
        /// <param name="height">階段の横幅</param>
        /// <param name="positionX">階段のX座標</param>
        /// <param name="positionY">階段のY座標</param>
        public StationStairsDownParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// 出口の情報クラス
    /// </summary>
    public class StationGoalParam
    {
        /// <summary>
        /// 出口の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 出口の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 出口のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 出口のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">出口の縦幅</param>
        /// <param name="height">出口の横幅</param>
        /// <param name="positionX">出口のX座標</param>
        /// <param name="positionY">出口のY座標</param>
        public StationGoalParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// ベンチの情報クラス
    /// </summary>
    public class StationBenchParam
    {
        /// <summary>
        /// ベンチの横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// ベンチの縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// ベンチのX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// ベンチのY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">ベンチの縦幅</param>
        /// <param name="height">ベンチの横幅</param>
        /// <param name="positionX">ベンチのX座標</param>
        /// <param name="positionY">ベンチのY座標</param>
        public StationBenchParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }

    /// <summary>
    /// 柱の情報クラス
    /// </summary>
    public class StationPillarParam
    {
        /// <summary>
        /// 柱の横幅
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 柱の縦幅
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 柱のX座標
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// 柱のY座標
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width">柱の縦幅</param>
        /// <param name="height">柱の横幅</param>
        /// <param name="positionX">柱のX座標</param>
        /// <param name="positionY">柱のY座標</param>
        public StationPillarParam(double width, double height, double positionX, double positionY)
        {
            Height = height;
            Width = width;
            PositionX = positionX;
            PositionY = positionY;
        }

    }


    /// <summary>
    /// 駅のレイアウトクラス
    /// </summary>
    public class StationLayoutParam
    {

        /// <summary>
        /// レイアウトの横幅
        /// </summary>
        public int Width { get; set; } = 1000;

        /// <summary>
        /// レイアウトの縦幅
        /// </summary>
        public int Height { get; set; } = 1000;

        /// <summary>
        /// 改札のリスト）
        /// </summary>
        public List<List<StationKaisatuParam>> Kaisatus { get; set; } = new List<List<StationKaisatuParam>>();

        /// <summary>
        /// 駅員室のリスト）
        /// </summary>
        public List<StationRoomParam> Rooms { get; set; } = new List<StationRoomParam>();

        /// <summary>
        /// 階段のリスト
        /// </summary>
        public List<StationStairsUpParam> StairsUp { get; set; } = new List<StationStairsUpParam>();

        /// <summary>
        /// 階段のリスト
        /// </summary>
        public List<StationStairsDownParam> StairsDown { get; set; } = new List<StationStairsDownParam>();

        /// <summary>
        /// 出口のリスト
        /// </summary>
        public List<StationGoalParam> Goals { get; set; } = new List<StationGoalParam>();

        /// <summary>
        /// ベンチのリスト）
        /// </summary>
        public List<StationBenchParam> Benchs { get; set; } = new List<StationBenchParam>();

        /// <summary>
        /// 柱のリスト
        /// </summary>
        public List<StationPillarParam> Pillars { get; set; } = new List<StationPillarParam>();

    }

}
