using System;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Enums;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.States
{
    public interface IState<T>:IComparable, ICloneable
    {
        ITile<T> this[int x, int y] { get; }
        bool AllTilesHasRightPlace { get; }
        int Depth { get; }
        int PathCost { get;}
        Coordinate EmptyTileCoords { get; }
        Direction LastDirection { get; set; }
        bool DirectionIsPossible(Direction direction);
        void MixTiles();
        void MoveEmptyTile(Direction direction);
    }
}