using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vakor._8_puzzle.Lib.Algorithms;
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
            IState<string> state = CreateDefaultState();

            Assert.AreEqual(true, state.AllTilesHasRightPlace);
            Assert.AreEqual(0, state.PathCost);
            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 1),
                state.EmptyTileCoords);
        }

        [TestMethod]
        public void MoveEmptyTileTest()
        {
            IState<string> state = CreateDefaultState();

            state.MoveEmptyTile(Direction.Left);
            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 2),
                state.EmptyTileCoords);
            Assert.ThrowsException<ArgumentException>((() => state.MoveEmptyTile(Direction.Down)));
            Assert.AreEqual(2, state.PathCost);
        }

        [TestMethod]
        public void MoveEmptyTileAllDimensionsTest()
        {
            IState<string> state = CreateDefaultState();

            state.MoveEmptyTile(Direction.Left);
            state.MoveEmptyTile(Direction.Right);
            state.MoveEmptyTile(Direction.Up);
            state.MoveEmptyTile(Direction.Down);
            
            Assert.AreEqual(new Coordinate(Configuration.Dimension - 1, Configuration.Dimension - 1),
                state.EmptyTileCoords);
        }
        
        private IState<string> CreateDefaultState()
        {
            ITile<string>[] field = new ITile<string>[Configuration.Dimension * Configuration.Dimension];
            for (int i = 0; i < Configuration.Dimension * Configuration.Dimension; i++)
            {
                if (i == Configuration.Dimension * Configuration.Dimension - 1)
                {
                    field[i] = new EmptyTile<string>(i.ToString(),
                        new Coordinate(i / Configuration.Dimension, i % Configuration.Dimension));
                }
                else
                {
                    field[i] = new UsualTile<string>(i.ToString(),
                        new Coordinate(i / Configuration.Dimension, i % Configuration.Dimension));
                }
            }

            IState<string> state = new State<string>(field);
            return state;
        }
    }
}