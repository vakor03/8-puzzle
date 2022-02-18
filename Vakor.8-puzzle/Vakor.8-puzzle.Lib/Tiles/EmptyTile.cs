using System;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.Coordinates;

namespace Vakor._8_puzzle.Lib.Tiles
{
    public class EmptyTile<T>:ITile<T>
    {
        public bool IsEmpty => true;

        public T Data
        {
            get => _data;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException();
                }
                _data = value;
            }
        }

        public Coordinate GoalCoordinates
        {
            get => _goalCoordinate;
            set
            {
                if (value.X >= Configuration.Dimension || value.Y >= Configuration.Dimension)
                {
                    throw new ArgumentException();
                }
                _goalCoordinate = value;
            }
        }

        private T _data;
        private Coordinate _goalCoordinate;

        public EmptyTile(T data, Coordinate goalCoordinate)
        {
            Data = data;
            GoalCoordinates = goalCoordinate;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}