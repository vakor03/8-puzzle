using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Lib.Tiles
{
    public interface ITile
    {
        int Data { get; }
        bool IsEmpty { get; }
        bool HasRightPlace { get; set; }
        public Coordinate CurrentCoordinates { get; set; }
    }
}