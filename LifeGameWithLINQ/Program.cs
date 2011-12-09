using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LifeGameWithLINQ
{
	class Program
	{
		static void Main(string[] args)
		{
			Lifegame lifegame = new Lifegame(20);

			Disp(lifegame, 0);
			var game = Enumerable.Range(1, 99).TakeWhile(t => {
																Thread.Sleep(500);
																lifegame.Update();
																Disp(lifegame, t);
												return !lifegame.IsEnd(); }).ToArray();
			Thread.Sleep(500);
			Console.WriteLine(string.Format("T={0}で終了", game.Length));
			Console.WriteLine("何かキーを押して終了...");
			Console.ReadKey();
		}
		
		/// <summary>
		/// 画面表示
		/// </summary>
		/// <param name="lifegame"></param>
		/// <param name="t"></param>
		static void Disp(Lifegame lifegame, int t)
		{
			Console.Clear();
			Console.Write("T" + t.ToString());
			Console.WriteLine(lifegame.ToString());
		}
	}

	/// <summary>
	/// ライフゲーム（2次元）
	/// </summary>
	class Lifegame
	{
		const int DIMENSION = 2;	// 次元数

		Map _map;
		Position[] _livingCells;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="size"></param>
		public Lifegame(int size)
		{
			_map = new Map((from s in Enumerable.Range(0, DIMENSION) select size).ToArray());	// 正方形で生成する
			_livingCells = initCells((int)Math.Pow(size * 2 / 3, DIMENSION), _map);	// 初期配置
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="size"></param>
		/// <param name="livingCells"></param>
		public Lifegame(int size, Position[] livingCells)
		{
			_map = new Map((from s in Enumerable.Range(0, DIMENSION) select size).ToArray());
			_livingCells = livingCells;
		}

		/// <summary>
		/// ランダム位置に細胞を生成
		/// </summary>
		/// <param name="num"></param>
		/// <param name="map"></param>
		/// <returns></returns>
		private static Position[] initCells(int num, Map map)
		{
			Random rnd = new Random();
			return (from count in Enumerable.Range(0, num)
					select new Position((from size in map.Size select rnd.Next(size)).ToArray())).Distinct().ToArray();
		}

		/// <summary>
		/// 状態更新
		/// </summary>
		public void Update()
		{
			_livingCells = (from pos in _map.GetAllEnumerablePositions()
								where isAlive(pos)
								select pos).ToArray();
		}
		
		/// <summary>
		/// 生存|生成条件判定
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		protected bool isAlive(Position pos)
		{
			return _livingCells.Contains(pos) ?
							// 周囲に2～3個ある座標は生存
								Enumerable.Range(2, 2).Contains(_livingCells.Count(cell => pos.IsStandBy(cell)))
							// 死んでいても、周囲に3つあれば生成
								: 3 == _livingCells.Count(cell => pos.IsStandBy(cell));
		}

		/// <summary>
		/// 表示・テスト用文字列に変換
		/// </summary>
		new public string ToString()
		{
			return string.Concat(
				_map.GetAllEnumerablePositions().Select<Position, string>((Position pos) =>
					(pos.Coordinates[1] == 0 ? System.Environment.NewLine : "")	// X=0なら改行
										+ (_livingCells.Contains(pos) ? "■" : "□")).ToArray());
		}

		/// <summary>
		/// 終了判定
		/// </summary>
		/// <returns></returns>
		public bool IsEnd()
		{
			// 全細胞死滅
			return _livingCells.Count() == 0;
		}
	}

	/// <summary>
	/// 拡張メソッド
	/// </summary>
	static class ListExtension
	{
		public static List<int> AddCurry(this List<int> self, int other)
		{
			self.Add(other);
			return self;
		}
	}

	/// <summary>
	/// 存在しうる全座標の集合を表す
	/// </summary>
	class Map
	{
		int[] _size;
		IEnumerable<Position> _allPositions;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="size"></param>
		public Map(int[] size)
		{
			_size = size;
		}

		/// <summary>
		/// 全座標の列挙（内部利用）
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Position> GetEnumerator()
		{
			return (from size in _size select size)
					.Aggregate<int, IEnumerable<Position>>(new Position[] { new Position(new int[] {}) },
					// １次元ずつ直行させる
					(curDim, dimSize) => (from curPos in curDim
											from exPos in Enumerable.Range(0, dimSize)
											select new Position(new List<int>(curPos.Coordinates).AddCurry(exPos).ToArray())));
		}

		/// <summary>
		/// 全座標の列挙
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Position> GetAllEnumerablePositions() 
		{
			// 高次元になるとさすがに重そうなので、計算は一回しかしないことにする
			return _allPositions == null ? _allPositions = GetEnumerator() : _allPositions; 
		}

		public int[] Size{get {return _size;}}
	}

	/// <summary>
	/// 位置を表現するクラス（n次元対応）
	/// intの配列でそれぞれの次元における座標を保持する
	/// </summary>
	class Position : IEquatable<Position>
	{
		int[] _coordinates;	// 配列でそれぞれの次元における座標を保持する

		protected Position()
		{
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="coordinates"></param>
		public Position(int[] coordinates)
		{
			_coordinates = coordinates;
		}

		/// <summary>
		/// 次元ごとの座標
		/// </summary>
		public int[] Coordinates{get{return _coordinates;}}

		/// <summary>
		/// 対象座標が隣接するか
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsStandBy(Position other)
		{
			int size = new int[] { this.Coordinates.Count(), other.Coordinates.Count() }.Min();	// 念のため、最少の軸数を取得

			return !this.Equals(other) &&
				Enumerable.Range(0, size).All(coordIndex => Math.Abs(this.Coordinates[coordIndex] - other.Coordinates[coordIndex]) <= 1);
		}

		/// <summary>
		/// 同値判定
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Position other)
		{
			return this.Coordinates.SequenceEqual(other.Coordinates);
		}
		/// <summary>
		/// 同値判定
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if (other == null) return base.Equals(other);

			if (!(other is Position))
				throw new InvalidCastException("比較対象をキャストできません");
			else
				return Equals(other as Position);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	}
}
