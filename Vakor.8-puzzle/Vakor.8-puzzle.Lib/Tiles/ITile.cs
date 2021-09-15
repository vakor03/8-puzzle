namespace Vakor._8_puzzle.Lib.Tiles
{
    public interface ITile
    {
        int Data { get; }
        bool IsEmpty { get; }
        bool HasRightPlace { get; set; }
    }
}