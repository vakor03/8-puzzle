using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Lib.Tiles
{
    public class UsualTile:ITile
    {
        public int Data { get; }
        public bool IsEmpty => false;
        public bool HasRightPlace { get; set; }
        public Coordinate CurrentCoordinates { get; set; }

        public UsualTile(int data)
        {
            Data = data;
            HasRightPlace = false;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}