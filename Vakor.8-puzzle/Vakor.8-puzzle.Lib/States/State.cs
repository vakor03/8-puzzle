using System;
using Priority_Queue;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.States
{
    public class State<T> : FastPriorityQueueNode, IState<T>
    {
        public ITile<T> this[int x, int y] => _tileLocations[x, y];

        private ITile<T> this[Coordinate coordinate]
        {
            get => _tileLocations[coordinate.X, coordinate.Y];
            set => _tileLocations[coordinate.X, coordinate.Y] = value;
        }

        public bool AllTilesHasRightPlace
        {
            get
            {
                for (int i = 0; i < Configuration.Dimension; i++)
                {
                    for (int j = 0; j < Configuration.Dimension; j++)
                    {
                        if (this[i, j].GoalCoordinates.CompareTo(new Coordinate(i, j)) != 0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public int Depth => _depth;
        
        private int _depth;
        private ITile<T>[,] _tileLocations;

        public int PathCost => FindPathCost();

        public Coordinate EmptyTileCoords
        {
            get
            {
                for (int i = 0; i < Configuration.Dimension; i++)
                {
                    for (int j = 0; j < Configuration.Dimension; j++)
                    {
                        if (this[i, j].IsEmpty)
                        {
                            return new Coordinate(i, j);
                        }
                    }
                }

                throw new ArgumentException();
            }
        }

        public Direction LastDirection { get; set; }


        // public State(ITile<T>[] tiles)
        // {
        //     _tileLocations = new ITile<T>[Configuration.Dimension, Configuration.Dimension];
        //     PutTilesInDefaultLocations(tiles);
        //     LastDirection = Direction.Default;
        // }

        public State(ITile<T>[,] tileLocations, int depth = 0)
        {
            _tileLocations = tileLocations;
            _depth = 0;
        }

        public void MixTiles()
        {
            Random random = new Random();

            for (int k = 0; k < Configuration.MixIterationCount; k++)
            {
                Direction direction = (Direction) random.Next(4);
                if (DirectionIsPossible(direction))
                {
                    MoveEmptyTile(direction);
                }
                else
                {
                    k++;
                }
            }
        }

        public void MoveEmptyTile(Direction direction)
        {
            if (!DirectionIsPossible(direction))
            {
                throw new ArgumentException();
            }

            switch (direction)
            {
                case Direction.Down:
                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X + 1, EmptyTileCoords.Y));
                    break;

                case Direction.Up:
                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X - 1, EmptyTileCoords.Y));
                    break;

                case Direction.Left:
                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X, EmptyTileCoords.Y - 1));
                    break;

                case Direction.Right:
                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X, EmptyTileCoords.Y + 1));
                    break;
            }
        }

        public static IState<T> MakeChild(IState<T> parentState,Direction emptyTileDirection)
        {
            State<T> stateClone = (State<T>) parentState.Clone();
            stateClone._depth= parentState.Depth + 1;
            stateClone.LastDirection = emptyTileDirection;
            stateClone.MoveEmptyTile(emptyTileDirection);
            return stateClone;
        }
        
        public object Clone()
        {
            IState<T> newState = new State<T>(_tileLocations.Clone() as ITile<T>[,]);
            return newState;
        }

        private int FindPathCost()
        {
            int result = 0;
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    if (_tileLocations[i, j].GoalCoordinates.CompareTo(new Coordinate(i, j)) != 0)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        // private void PutTilesInDefaultLocations(ITile<T>[] tiles)
        // {
        //     foreach (ITile<T> tile in tiles)
        //     {
        //         _tileLocations[tile.GoalCoordinates.X, tile.GoalCoordinates.Y] = tile;
        //     }
        // }

        private void SwapTiles(Coordinate firstTileCoord, Coordinate secondTileCoord)
        {
            (this[firstTileCoord], this[secondTileCoord]) = (this[secondTileCoord], this[firstTileCoord]);
        }

        public int CompareTo(object? obj)
        {
            IState<T> secondState = (IState<T>) obj;
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    if (this[i, j].GoalCoordinates.CompareTo(secondState[i, j].GoalCoordinates) != 0)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        public bool DirectionIsPossible(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (EmptyTileCoords.X > 0 && LastDirection != Direction.Down)
                    {
                        return true;
                    }

                    break;
                case Direction.Down:
                    if (EmptyTileCoords.X < Configuration.Dimension - 1 && LastDirection != Direction.Up)
                    {
                        return true;
                    }

                    break;
                case Direction.Left:
                    if (EmptyTileCoords.Y > 0 && LastDirection != Direction.Right)
                    {
                        return true;
                    }

                    break;
                case Direction.Right:
                    if (EmptyTileCoords.Y < Configuration.Dimension - 1 && LastDirection != Direction.Left)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        public static IState<T> CreateDefaultState()
        {
            if (typeof(T) == typeof(string))
            {
                return CreateDefaultStringState() as IState<T>;
            }

            if (typeof(T) == typeof(int))
            {
                return CreateDefaultIntegerState() as IState<T>;
            }

            throw new ArgumentException();
        }

        private static State<string> CreateDefaultStringState()
        {
            ITile<string>[,] tileLocations =
                new ITile<T>[Configuration.Dimension, Configuration.Dimension] as ITile<string>[,];
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    if (i == Configuration.Dimension - 1 && j == Configuration.Dimension - 1)
                    {
                        tileLocations[i, j] = new EmptyTile<string>("0", new Coordinate(i, j));
                    }
                    else
                    {
                        tileLocations[i, j] = new UsualTile<string>((i * Configuration.Dimension + j + 1).ToString(),
                            new Coordinate(i, j));
                    }
                }
            }

            return new State<string>(tileLocations){LastDirection = Direction.Default};
    }

        private static State<int> CreateDefaultIntegerState()
        {
            ITile<int>[,] tileLocations =
                new ITile<T>[Configuration.Dimension, Configuration.Dimension] as ITile<int>[,];
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    if (i == Configuration.Dimension - 1 && j == Configuration.Dimension - 1)
                    {
                        tileLocations[i, j] = new EmptyTile<int>(0, new Coordinate(i, j));
                    }
                    else
                    {
                        tileLocations[i, j] = new UsualTile<int>((i * Configuration.Dimension + j + 1),
                            new Coordinate(i, j));
                    }
                }
            }

            return new State<int>(tileLocations){LastDirection = Direction.Default};
        }
    }
}