using LifeGameWithLINQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LifeGame.Tests
{
    
    
    /// <summary>
    ///LifegameTest のテスト クラスです。すべての
    ///LifegameTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class LifegameTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///現在のテストの実行についての情報および機能を
		///提供するテスト コンテキストを取得または設定します。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region 追加のテスト属性
		// 
		//テストを作成するときに、次の追加属性を使用することができます:
		//
		//クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//各テストを実行する前にコードを実行するには、TestInitialize を使用
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//各テストを実行した後にコードを実行するには、TestCleanup を使用
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		/// <summary>
		/// ブリンカーを用いてルールのテスト
		///</summary>
		[TestMethod()]
		public void UpdateTest_Blinker()
		{
			// 以下のような配置
			//□□□		□■□
			//■■■   =>	□■□
			//□□□		□■□
			int size = 3;
			Position[] livingCells = new Position[] { new Position(new [] { 1, 0 }), new Position(new [] { 1, 1 }), new Position(new [] { 1, 2 }) };
			Lifegame target = new Lifegame(size, livingCells);
			string expected = "\r\n□■□\r\n□■□\r\n□■□";

			string actual;
			target.Update();
			actual = target.ToString();
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// 過密死ルールのテスト
		///</summary>
		[TestMethod()]
		public void UpdateTest_過密死()
		{
			// 以下のような配置
			//■■■		■□■
			//■■□ =>	■□■
			//□□□		□□□
			int size = 3;
			Position[] livingCells = new Position[] { new Position(new [] { 0, 0 }), new Position(new [] { 0, 1 }), new Position(new [] { 0, 2 }),
													  new Position(new [] { 1, 0 }), new Position(new [] { 1, 1 })};
			Lifegame target = new Lifegame(size, livingCells);
			string expected = "\r\n■□■\r\n■□■\r\n□□□";

			string actual;
			target.Update();
			actual = target.ToString();
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///initCells のテスト
		///</summary>
		[TestMethod()]
		[DeploymentItem("LifeGameWithLINQ.exe")]
		public void initCellsTest()
		{
			int num = 5;
			Map_Accessor map = new Map_Accessor(new []{10, 10});
			Position_Accessor[] actual;
			actual = Lifegame_Accessor.initCells(num, map);

			CollectionAssert.AllItemsAreUnique(actual);
			Assert.IsTrue(
				actual.All(
					pos => (from index in Enumerable.Range(0, map.Size.Length) select index)
						.All(index => Enumerable.Range(0, map.Size[index]).Contains(pos.Coordinates[index]))));
		}
	}
}
