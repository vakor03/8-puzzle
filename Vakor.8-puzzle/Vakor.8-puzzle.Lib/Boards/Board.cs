using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Boards
{
    public class Board : IBoard
    {
        public ITile[,] GameField => _gameField;

        public ITile this[int x, int y] => _gameField[x, y];

        private readonly ITile[,] _gameField;


        public Board()
        {
            _gameField = new ITile[IBoard.Dimension, IBoard.Dimension];
        }

        public void Init()
        {
            PutTilesOnRightPlaces();
        }


        public void MixTiles()
        {
            throw new System.NotImplementedException();
        }

        private void PutTilesOnRightPlaces()
        {
            for (int i = 0; i < IBoard.Dimension * IBoard.Dimension - 1; i++)
            {
                _gameField[i / IBoard.Dimension, i % IBoard.Dimension] = new UsualTile(i + 1) {HasRightPlace = true};
            }

            _gameField[IBoard.Dimension, IBoard.Dimension] = new EmptyTile {HasRightPlace = true};
        }
    }
}