//TODO Stack

using System.Collections.Generic;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class LDFS
    {
        private int MaxDepth { get; set; }
        private State _initialState;
        
        public bool Solve(State goal)
        {
            return true;
        }

        public Direction GetNextTurn()
        {
            return Direction.Down;
        }

        public State RecursiveDLS(State currentState, State goalState, out ResultIndicator resultIndicator)
        {
            bool cutoffOccured = false;
            
            if (currentState == goalState)
            {
                resultIndicator = ResultIndicator.Success;
                return currentState;
            }else if(currentState.Depth >= MaxDepth)
            {
                resultIndicator = ResultIndicator.Cutoff;
                return currentState;
            }
            else
            {
                foreach (var successor in FindExpand(currentState))
                {
                    State result = RecursiveDLS(successor, goalState, out ResultIndicator resultIndicator1);
                    if (resultIndicator1 == ResultIndicator.Cutoff)
                    {
                        cutoffOccured = true;
                    }else if (resultIndicator1 == ResultIndicator.Success)
                    {
                        resultIndicator = ResultIndicator.Success;
                        return result;
                    }
                }
            }
            if (cutoffOccured)
            {
                resultIndicator = ResultIndicator.Cutoff;
                return currentState;
            }
            else
            {
                resultIndicator = ResultIndicator.Failure;
                return currentState;
            }
        }

        private List<State> FindExpand(State currentState)
        {
            List<State> expand = new List<State>();
            if (currentState.EmptyTile.)
            {
                
            }
            
            
        }
    }
}