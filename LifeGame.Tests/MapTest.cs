using LifeGameWithLINQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame.Tests
{
    /// <summary>
    ///MapTest のテスト クラスです。すべての
    ///MapTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class MapTest
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
		///GetEnumerator のテスト 1次元
		///</summary>
		[TestMethod()]
		public void GetEnumeratorTest_1D()
		{
			int[] size = new int[] { 3 };
			Map target = new Map(size);
			IEnumerable<Position> expected = new Position[] { new Position(new int[] { 0 }), new Position(new int[] { 1 }), new Position(new int[] { 2 }) };
																
			IEnumerable<Position> actual;
			actual = target.GetAllEnumerablePositions();

			CollectionAssert.AreEqual(new List<Position>(expected), new List<Position>(actual));
		}

		/// <summary>
		///GetEnumerator のテスト 2次元
		///</summary>
		[TestMethod()]
		public void GetEnumeratorTest_2D()
		{
			int[] size = new int[] { 3, 3 };
			Map target = new Map(size);
			IEnumerable<Position> expected = new Position[] { new Position(new int[] { 0, 0 }), new Position(new int[] { 0, 1 }), new Position(new int[] { 0, 2 }),
																new Position(new int[] { 1, 0 }), new Position(new int[] { 1, 1 }), new Position(new int[] { 1, 2 }),
																new Position(new int[] { 2, 0 }), new Position(new int[] { 2, 1 }), new Position(new int[] { 2, 2 })};
			IEnumerable<Position> actual;
			actual = target.GetAllEnumerablePositions();
			CollectionAssert.AreEqual(new List<Position>(expected), new List<Position>(actual));
		}

		/// <summary>
		///GetEnumerator のテスト ３次元
		///</summary>
		[TestMethod()]
		public void GetEnumeratorTest_3D()
		{
			int[] size = new int[] {2, 2, 2}; 
			Map target = new Map(size);
			IEnumerable<Position> expected = new Position[] { new Position(new int[] { 0, 0, 0 }), new Position(new int[] { 0, 0, 1 }),
																new Position(new int[] { 0, 1, 0 }), new Position(new int[] { 0, 1, 1 }),
																new Position(new int[] { 1, 0, 0 }), new Position(new int[] { 1, 0, 1 }),
																new Position(new int[] { 1, 1, 0 }), new Position(new int[] { 1, 1, 1 })};
			IEnumerable<Position> actual;
			actual = target.GetAllEnumerablePositions();
			CollectionAssert.AreEqual(new List<Position>(expected), new List<Position>(actual));
		}

	}
}
