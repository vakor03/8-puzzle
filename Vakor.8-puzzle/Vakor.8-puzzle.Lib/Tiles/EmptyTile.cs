using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Lib.Tiles
{
    public class EmptyTile:ITile
    {
        public int Data => _defaultData;
        public bool IsEmpty => true;
        public Coordinate CurrentCoordinates { get; set; }
        public Coordinate GoalCoordinates { get; set; }
        public bool HasRightPlace { get; set; }

        private int _defaultData;
        public EmptyTile()
        {
            _defaultData = 0;
            HasRightPlace = false;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}