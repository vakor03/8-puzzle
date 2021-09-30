using System.Collections.Generic;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    //TODO RESULT INDEX, states in memory, iterations count
    //30 MIN, 512 MB
    public abstract class SolveAlgorithm<T>
    {
        public abstract int StatesInMemory { get; }
        public abstract int IterationsCount { get; }
        public abstract int SolutionDepth { get; }

        public abstract ResultIndicator SolvePuzzle(IState<T> initialState);
        protected List<IState<T>> FindExpand(IState<T> currentState)
        {
            List<IState<T>> expand = new();
            List<Direction> possibleDirections = new();

            Coordinate emptyTileCoords = currentState.EmptyTileCoords;

            if (emptyTileCoords.X > 0 && currentState.LastDirection != Direction.Down)
            {
                possibleDirections.Add(Direction.Up);
            }

            if (emptyTileCoords.X < Configuration.Dimension - 1 && currentState.LastDirection != Direction.Up)
            {
                possibleDirections.Add(Direction.Down);
            }

            if (emptyTileCoords.Y > 0 && currentState.LastDirection != Direction.Right)
            {
                possibleDirections.Add(Direction.Left);
            }

            if (emptyTileCoords.Y < Configuration.Dimension - 1 && currentState.LastDirection != Direction.Left)
            {
                possibleDirections.Add(Direction.Right);
            }


            foreach (var direction in possibleDirections)
            {
                expand.Add(currentState.MakeChild(direction));
            }


            return expand;
        }
    }
}