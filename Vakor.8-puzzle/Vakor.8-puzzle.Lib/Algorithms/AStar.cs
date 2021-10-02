using Priority_Queue;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class AStar<T> : SolveAlgorithm<T>
    {
        public override int StatesInMemory => _statesInMemory;
        public override int IterationsCount => _iterationsCount;
        public override int SolutionDepth => _solutionDepth;

        private int _statesInMemory;
        private int _iterationsCount;
        private int _solutionDepth;

        public override ResultIndicator SolvePuzzle(IState<T> initialState)
        {
            RestoreCounters();
            FastPriorityQueue<State<T>> priorityQueue = new FastPriorityQueue<State<T>>(Configuration.MaxPossibleStates);
            
            priorityQueue.Enqueue((State<T>) initialState, FindHeuristic(initialState));
            _statesInMemory++;
            
            while (priorityQueue.Count != 0)
            {
                IState<T> currentState = priorityQueue.Dequeue();
                _iterationsCount++;
                if (currentState.AllTilesHasRightPlace)
                {
                    _solutionDepth = currentState.Depth;
                    return ResultIndicator.Success;
                }

                foreach (var childState in FindExpand(currentState))
                {
                    _statesInMemory++;
                    priorityQueue.Enqueue((State<T>) childState, FindHeuristic(childState));
                }
            }
            
            return ResultIndicator.Failure;
        }
        
        private int FindHeuristic(IState<T> currentState)
        {
            return currentState.Depth + currentState.PathCost;
        }

        private void RestoreCounters()
        {
            _iterationsCount = 0;
            _statesInMemory = 0;
            _solutionDepth = -1;
        }
    }
}