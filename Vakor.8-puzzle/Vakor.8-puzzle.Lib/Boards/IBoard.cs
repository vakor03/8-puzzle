using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Boards
{
    public interface IBoard
    {
        ITile[,] GameField { get; }
        ITile this[int x, int y] { get; }
        const int Dimension = 3;
        
        void Init();
        void MixTiles();
    }
}