//TODO Stack

using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class LDFS<T>:SolveAlgorithm<T>
    {
        public LDFS(int maxDepth)
        {
            MaxDepth = maxDepth;
        }

        private int MaxDepth { get; set; }

        private IState<T> RecursiveDLS(IState<T> currentState,
            out ResultIndicator resultIndicator)
        {
            _iterationsCount++;
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

        public override int StatesInMemory => 0;
        public override int IterationsCount => _iterationsCount;
        public override int SolutionDepth => _solutionDepth;

        private int _iterationsCount;
        private int _solutionDepth;

        private void RestoreCounters()
        {
            _iterationsCount = 0;
            _solutionDepth = 0;
        }

        public override ResultIndicator SolvePuzzle(IState<T> initialState)
        {
            RestoreCounters();
            RecursiveDLS(initialState, out ResultIndicator resultIndicator);
            return resultIndicator;
        }
    }
}