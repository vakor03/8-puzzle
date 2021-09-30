using Vakor._8_puzzle.Lib.States;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Boards
{
    public class Board<T> : IBoard<T>
    {
        public ITile<T> this[int x, int y] => _currentState[x,y];
        
        private IState<T> _currentState;
        private ITile<T>[] _allTiles;

        public Board(ITile<T>[] allTiles)
        {
            _allTiles = allTiles;
        }

        public Board(IState<T> currentState)
        {
            _currentState = currentState;
        }

        public void GenerateNewField()
        {
            _currentState = new State<T>(_allTiles);
        }

        public void MixTiles()
        {
           _currentState.MixTiles();
        }
    }
}