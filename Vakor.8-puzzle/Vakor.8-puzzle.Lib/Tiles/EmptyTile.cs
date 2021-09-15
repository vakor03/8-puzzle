namespace Vakor._8_puzzle.Lib.Tiles
{
    public class EmptyTile:ITile
    {
        public int Data => _defaultData;
        public bool IsEmpty => true;
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