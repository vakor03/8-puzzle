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

        public int Depth { get; set; }
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


        public State(ITile<T>[] tiles)
        {
            _tileLocations = new ITile<T>[Configuration.Dimension, Configuration.Dimension];
            PutTilesInDefaultLocations(tiles);
            LastDirection = Direction.Default;
        }

        public State(ITile<T>[,] tileLocations)
        {
            _tileLocations = tileLocations;
        }

        public void MixTiles()
        {
            Random random = new Random();

            for (int i = Configuration.Dimension - 1; i > 0; i--)
            {
                for (int j = Configuration.Dimension - 1; j > 0; j--)
                {
                    int m = random.Next(i + 1);
                    int n = random.Next(j + 1);

                    SwapTiles(new Coordinate(i, j), new Coordinate(m, n));
                }
            }
        }

        public void MoveEmptyTile(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    if (EmptyTileCoords.X == Configuration.Dimension - 1)
                    {
                        throw new ArgumentException();
                    }

                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X + 1, EmptyTileCoords.Y));
                    break;

                case Direction.Up:
                    if (EmptyTileCoords.X == 0)
                    {
                        throw new ArgumentException();
                    }

                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X - 1, EmptyTileCoords.Y));
                    break;

                case Direction.Left:
                    if (EmptyTileCoords.Y == 0)
                    {
                        throw new ArgumentException();
                    }

                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X, EmptyTileCoords.Y - 1));
                    break;

                case Direction.Right:
                    if (EmptyTileCoords.Y == Configuration.Dimension - 1)
                    {
                        throw new ArgumentException();
                    }

                    SwapTiles(EmptyTileCoords, new Coordinate(EmptyTileCoords.X, EmptyTileCoords.Y + 1));
                    break;
            }
        }

        public IState<T> MakeChild(Direction emptyTileDirection)
        {
            IState<T> stateClone = (IState<T>) Clone();
            stateClone.Depth  = Depth+1;
            stateClone.LastDirection = emptyTileDirection;
            stateClone.MoveEmptyTile(emptyTileDirection);
            return stateClone;
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
        
        private void PutTilesInDefaultLocations(ITile<T>[] tiles)
        {
            foreach (ITile<T> tile in tiles)
            {
                _tileLocations[tile.GoalCoordinates.X, tile.GoalCoordinates.Y] = tile;
            }
        }

        private void SwapTiles(Coordinate firstTileCoord, Coordinate secondTileCoord)
        {
            (this[firstTileCoord], this[secondTileCoord]) = (this[secondTileCoord], this[firstTileCoord]);
        }

        public object Clone()
        {
            IState<T> newState = new State<T>(_tileLocations.Clone() as ITile<T>[,]);
            return newState;
        }


        public int CompareTo(object? obj)
        {
            IState<T> secondState = (IState<T>) obj;
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    if (this[i,j].GoalCoordinates.CompareTo(secondState[i,j].GoalCoordinates)!=0)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }
    }
}