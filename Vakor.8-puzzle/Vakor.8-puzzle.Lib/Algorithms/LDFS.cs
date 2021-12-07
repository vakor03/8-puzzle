using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class LDFS<T>:SolveAlgorithm<T>
    {
        public LDFS()
        {
        }


        public override string AlgorithmName => "LDFS";
        public override int StatesInMemory => 0;

        public override int IterationsCount => _iterationsCount;

        public override int SolutionDepth => _solutionDepth;
        private readonly int _maxDepth = Configuration.LDFSMaxDepth;

        private int _iterationsCount;

        private int _solutionDepth;

        public override ResultIndicator SolvePuzzle(IState<T> initialState)
        {
            RestoreCounters();
            var resultState = RecursiveDLS(initialState, out ResultIndicator resultIndicator);
            _solutionDepth = resultState.Depth;
            return resultIndicator;
        }

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

            if (currentState.Depth >= _maxDepth)
            {
                resultIndicator = ResultIndicator.Cutoff;
                return currentState;
            }
            
            foreach (var successor in FindExpand(currentState))
            {
                IState<T> resultState = RecursiveDLS(successor, out ResultIndicator sucResultIndicator);
                if (sucResultIndicator == ResultIndicator.Cutoff)
                {
                    cutoffOccured = true;
                }
                else if (sucResultIndicator == ResultIndicator.Success)
                {
                    resultIndicator = ResultIndicator.Success;
                    return resultState;
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

        private void RestoreCounters()
        {
            _iterationsCount = 0;
            _solutionDepth = 0;
        }
    }
}