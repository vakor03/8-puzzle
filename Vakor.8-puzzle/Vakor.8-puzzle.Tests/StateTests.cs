using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Tests
{
    [TestClass]
    public class StateTests
    {
        [TestMethod]
        public void RightPlaceTest()
        {
            var state = State<string>.CreateDefaultState();

            Assert.AreEqual(true, state.AllTilesHasRightPlace);
            Assert.AreEqual(0, state.PathCost);
            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 1),
                state.EmptyTileCoords);
        }

        [TestMethod]
        public void MoveEmptyTileTest()
        {
            var state = State<string>.CreateDefaultState();

            state.MoveEmptyTile(Direction.Left);
            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 2),
                state.EmptyTileCoords);
            Assert.ThrowsException<ArgumentException>(() => state.MoveEmptyTile(Direction.Down));
            Assert.AreEqual(2, state.PathCost);
        }

        [TestMethod]
        public void MoveEmptyTileAllDimensionsTest()
        {
            var state = State<string>.CreateDefaultState();

            state.MoveEmptyTile(Direction.Left);
            state.MoveEmptyTile(Direction.Right);
            state.MoveEmptyTile(Direction.Up);
            state.MoveEmptyTile(Direction.Down);

            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 1),
                state.EmptyTileCoords);
        }

        [TestMethod]
        public void CompareTest()
        {
            var state1 = State<string>.CreateDefaultState();
            var state2 = State<string>.CreateDefaultState();
            
            state1.MoveEmptyTile(Direction.Left);
            state2.MoveEmptyTile(Direction.Left);
            
            Assert.AreEqual(0, state1.CompareTo(state2));
        }
        [TestMethod]
        public void MixTilesTest()
        {
            IState<string> state = State<string>.CreateDefaultState();
            state.MixTiles();
            
            Assert.AreEqual(false, state.AllTilesHasRightPlace);
        }

        [TestMethod]
        public void CloneTest()
        {
            IState<string> state = State<string>.CreateDefaultState();

            IState<string> clonedState = state.Clone() as IState<string>;
            
            Assert.AreEqual(state.AllTilesHasRightPlace, clonedState.AllTilesHasRightPlace);
            Assert.AreNotEqual(state, clonedState);
        }

        [TestMethod]
        public void MakeChildTest()
        {
            IState<string> state = State<string>.CreateDefaultState();

            IState<string> changedState = State<string>.MakeChild(state, Direction.Left);
            
            Assert.AreEqual(Direction.Left, changedState.LastDirection);
            Assert.AreEqual(1, changedState.Depth);
            Assert.AreEqual(false,changedState.AllTilesHasRightPlace);
        }
    }
}