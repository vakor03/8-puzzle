//TODO Stack

using System.Collections.Generic;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class LDFS<T>
    {
        public LDFS(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        private int MaxDepth { get; set; }
        private IState<object> _initialState;

        public bool Solve(IState<object> goal)
        {
            return true;
        }
        
        public IState<T> RecursiveDLS(IState<T> currentState,
            out ResultIndicator resultIndicator)
        {
            bool cutoffOccured = false;

            if (currentState.AllTilesHasRightPlace)
            {
                resultIndicator = ResultIndicator.Success;
                return currentState;
            }

            if (currentState.Depth >= MaxDepth)
            {
                resultIndicator = ResultIndicator.Cutoff;
                return currentState;
            }
            
            foreach (var successor in FindExpand(currentState))
            {
                IState<T> result = RecursiveDLS(successor, out ResultIndicator resultIndicator1);
                if (resultIndicator1 == ResultIndicator.Cutoff)
                {
                    cutoffOccured = true;
                }
                else if (resultIndicator1 == ResultIndicator.Success)
                {
                    resultIndicator = ResultIndicator.Success;
                    return result;
                }
            }

            if (cutoffOccured)
            {
                resultIndicator = ResultIndicator.Cutoff;
                return currentState;
            }

            resultIndicator = ResultIndicator.Failure;
            return currentState;
        }

        public List<IState<T>> FindExpand(IState<T> currentState)
        {
            List<IState<T>> expand = new();
            List<Direction> possibleDirections = new();
            
            Coordinate emptyTileCoords = currentState.EmptyTileCoords;
            
            if (emptyTileCoords.X > 0 && currentState.LastDirection!=Direction.Down)
            {
                possibleDirections.Add(Direction.Up);
            }

            if (emptyTileCoords.X < Configuration.Dimension - 1 && currentState.LastDirection!=Direction.Up)
            {
                possibleDirections.Add(Direction.Down);
            }

            if (emptyTileCoords.Y > 0 && currentState.LastDirection!=Direction.Right)
            {
                possibleDirections.Add(Direction.Left);
            }

            if (emptyTileCoords.Y < Configuration.Dimension - 1 && currentState.LastDirection!=Direction.Left)
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