using System;
using Priority_Queue;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.PriorityQueues;
using Vakor._8_puzzle.Lib.States;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    using System.Collections.Generic;

    public class AStar<T>
    {
        private IState<T> _initialState;

        public AStar(IState<T> initialState)
        {
            _initialState = initialState;
        }

        public bool Solve()
        {
            PriorityQueue<IState<T>> priorityQueue = new PriorityQueue<IState<T>>();
            List<IState<T>> visited = new List<IState<T>>();
            priorityQueue.AddElement(_initialState, _initialState.Depth+_initialState.PathCost);
            while (!priorityQueue.IsEmpty())
            {
                IState<T> currentState = priorityQueue.Dequeue();
                visited.Add(currentState);
                if (currentState.AllTilesHasRightPlace)
                {
                    Console.WriteLine(currentState.Depth);
                    return true;
                }

                foreach (var state in FindExpand(currentState))
                {
                    bool stateIsAlreadyVisited = false;
                    foreach (var st in visited)
                    {
                        if (st.CompareTo(state)==0)
                        {
                            stateIsAlreadyVisited = true;
                        }
                    }

                    if (!stateIsAlreadyVisited)
                    {
                        priorityQueue.AddElement((State<T>)state,state.Depth+state.PathCost);
                    }
                }
            }
            return false;
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