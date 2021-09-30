using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Boards
{
    public interface IBoard<T>
    {
        const int Dimension = 3;
        ITile<T> this[int x, int y] { get; }
        void GenerateNewField();
        void MixTiles();
    }
}