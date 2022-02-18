using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Tests
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void CompareTest()
        {
            Coordinate firstCoord = new Coordinate(1, 1);
            Coordinate secondCoord = new Coordinate(1, 2);
            
            Assert.AreEqual(-1, firstCoord.CompareTo(secondCoord));
            Assert.AreEqual(0,firstCoord.CompareTo(firstCoord));
        }
        
    }
}