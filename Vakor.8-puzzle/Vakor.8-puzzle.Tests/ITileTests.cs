using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Tests
{
    [TestClass]
    public class ITileTests
    {
        [TestMethod]
        public void UsualTileTest()
        {
            int tileData = new Random().Next(20);
            Coordinate tileCoordinate =
                new Coordinate(new Random().Next(Configuration.Dimension), new Random().Next(Configuration.Dimension));
            ITile<int> usualTile = new UsualTile<int>(tileData, tileCoordinate);
            
            Assert.AreEqual(false, usualTile.IsEmpty);
            Assert.AreEqual(tileData, usualTile.Data);
            Assert.AreEqual(tileCoordinate,usualTile.GoalCoordinates);
        }
        
        [TestMethod]
        public void EmptyTileTest()
        {
            int tileData = new Random().Next(20);
            Coordinate tileCoordinate =
                new Coordinate(new Random().Next(Configuration.Dimension), new Random().Next(Configuration.Dimension));
            ITile<int> emptyTile = new EmptyTile<int>(tileData, tileCoordinate);
            
            Assert.AreEqual(true, emptyTile.IsEmpty);
            Assert.AreEqual(tileData, emptyTile.Data);
            Assert.AreEqual(tileCoordinate,emptyTile.GoalCoordinates);
        }

        [TestMethod]
        public void TileNullDataTest()
        {
            string tileData = "new Random().Next(20)";
            Coordinate tileCoordinate =
                new Coordinate(new Random().Next(Configuration.Dimension), new Random().Next(Configuration.Dimension));
            ITile<string> emptyTile = new UsualTile<string>(tileData, tileCoordinate);
            
            Assert.ThrowsException<ArgumentNullException>((() => emptyTile.Data = null));
        }
        
        [TestMethod]
        public void TileIncorrectCoordinatesTest()
        {
            int tileData = new Random().Next(20);
            Coordinate tileCoordinate =
                new Coordinate(new Random().Next(Configuration.Dimension), new Random().Next(Configuration.Dimension));
            ITile<int> emptyTile = new UsualTile<int>(tileData, tileCoordinate);
            
            Assert.ThrowsException<ArgumentException>(() => emptyTile.GoalCoordinates = new Coordinate(Configuration.Dimension, Configuration.Dimension));

        }
    }
}