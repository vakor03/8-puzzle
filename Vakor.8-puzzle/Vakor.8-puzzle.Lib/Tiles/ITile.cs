using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Lib.Tiles
{
    public interface ITile<T>
    {
        bool IsEmpty { get; }
        T Data { get; set; }
        Coordinate GoalCoordinates { get; set; }
    }
}