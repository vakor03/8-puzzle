using System;
using Vakor._8_puzzle.Lib.Algorithms;
using Vakor._8_puzzle.Lib.Configurations;
using Vakor._8_puzzle.Lib.States;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IState<string> state = State<string>.CreateDefaultState();
            state.MixTiles();
            ShowState(state);
            SolveAlgorithm<string> algorithm = new AStar<string>();
            
            Console.WriteLine(algorithm.SolvePuzzle(state));
            Console.WriteLine(algorithm.IterationsCount);

            SolveAlgorithm<string> algorithm1 = new LDFS<string>(25);

            Console.WriteLine(algorithm1.SolvePuzzle(state));
            Console.WriteLine(algorithm1.IterationsCount);

        }

        private static void ShowState(IState<string> state)
        {
            string separator = " ";
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    Console.Write($"{state[i,j].Data}{separator}");
                }
            }

            Console.WriteLine();
        }
    }
}