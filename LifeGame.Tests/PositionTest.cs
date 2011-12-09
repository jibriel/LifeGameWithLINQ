using LifeGameWithLINQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LifeGame.Tests
{
    
    
    /// <summary>
    ///PositionTest のテスト クラスです。すべての
    ///PositionTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class PositionTest
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
		///IsStandBy のテスト 一致
		///</summary>
		[TestMethod()]
		public void IsStandByTest_一致()
		{
			int[] coordinates = new int[] {1, 1 };
			int[] coordinatesOther = new int[] { 2, 2 }; 

			Position target = new Position(coordinates);
			Position other = new Position(coordinatesOther);

			bool expected = true; 
			bool actual;
			actual = target.IsStandBy(other);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///IsStandBy のテスト 不一致
		///</summary>
		[TestMethod()]
		public void IsStandByTest_不一致()
		{
			int[] coordinates = new int[] { 1,1 };
			int[] coordinatesOther = new int[] { 2,3 };

			Position target = new Position(coordinates);
			Position other = new Position(coordinatesOther);

			bool expected = false;
			bool actual;
			actual = target.IsStandBy(other);
			Assert.AreEqual(expected, actual);
		}
	}
}
