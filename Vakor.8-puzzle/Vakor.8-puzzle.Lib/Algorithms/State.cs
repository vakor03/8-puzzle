using System;
using Vakor._8_puzzle.Lib.Boards;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Algorithms
{
    public class State
    {
        public Direction LastAction { get; set; }

        public ITile EmptyTile
        {
            get
            {
                foreach (var tile in _tileLocations)
                {
                    if (tile.IsEmpty)
                    {
                        return tile;
                    }
                }

                throw new ArgumentException();
            }
        }

        public int Depth { get; set; }
        public int PathCost { get; set; }
        
        public ITile this[int x, int y]{
            get
            {
                if (x > IBoard.Dimension && y > IBoard.Dimension)
                {
                    throw new ArgumentException();
                }
                return _tileLocations[x, y];
            }
        }

        private ITile[,] _tileLocations;

        public State(ITile[,] tileLocations, Direction lastAction, int depth)
        {
            _tileLocations = tileLocations;
            LastAction = lastAction;
            Depth = depth;
        }
    }
}

