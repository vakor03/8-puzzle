using System;
using System.Collections;
using System.Collections.Generic;
using Vakor._8_puzzle.Lib.Coordinates;
using Vakor._8_puzzle.Lib.Tiles;

namespace Vakor._8_puzzle.Lib.Boards
{
    public class Board : IBoard
    {
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
            Random random = new Random();

            for (int i = IBoard.Dimension - 1; i > 0; i--)
            {
                for (int j = IBoard.Dimension - 1; j > 0; j--)
                {
                    Coordinate sourceCoords = new Coordinate {X = i, Y = j};
                    Coordinate destCoords = new Coordinate {X = random.Next(i + 1), Y = random.Next(j + 1)};

                    SwapTiles(sourceCoords, destCoords);
                }
            }
        }

        private void SwapTiles(Coordinate sourceCoords, Coordinate destCoords)
        {
            (_gameField[sourceCoords.X, sourceCoords.Y], _gameField[destCoords.X, destCoords.Y]) = (_gameField[destCoords.X, destCoords.Y], _gameField[sourceCoords.X, sourceCoords.Y]);
        }

        private void PutTilesOnRightPlaces()
        {
            for (int i = 0; i < IBoard.Dimension * IBoard.Dimension - 1; i++)
            {
                _gameField[i / IBoard.Dimension, i % IBoard.Dimension] = new UsualTile(i + 1) {HasRightPlace = true};
            }

            _gameField[IBoard.Dimension - 1, IBoard.Dimension - 1] = new EmptyTile {HasRightPlace = true};
        }
    }
}