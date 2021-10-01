using System;

namespace Vakor._8_puzzle.Lib.Coordinates
{
    public struct Coordinate:IComparable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int CompareTo(Coordinate other)
        {
            if (X.CompareTo(other.X) == 0 && Y.CompareTo(other.Y)==0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}