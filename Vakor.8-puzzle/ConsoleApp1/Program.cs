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
            SolveAlgorithm<string> algorithmAStar = new AStar<string>();
            SolveAlgorithm<string> algorithmLDFS = new LDFS<string>();
            ShowState(state);
            algorithmLDFS.SolvePuzzle(state);
            ShowStatistics(algorithmLDFS);

            algorithmAStar.SolvePuzzle(state);
            ShowStatistics(algorithmAStar);





            // Console.WriteLine();
            // ShowStatistics(algorithmLDFS);
        }

        private static void ShowState(IState<string> state)
        {
            string separator = " ";
            for (int i = 0; i < Configuration.Dimension; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < Configuration.Dimension; j++)
                {
                    Console.Write($"{state[i, j].Data}{separator}");
                }
            }

            Console.WriteLine();
        }

        private static void ShowStatistics(SolveAlgorithm<string> algorithm)
        {
            Console.WriteLine($"Algorithm {algorithm.AlgorithmName}");
            Console.WriteLine($"Iterations: {algorithm.IterationsCount}");
            Console.WriteLine($"States in memory: {algorithm.StatesInMemory}");
            Console.WriteLine($"Depth of the solution node: {algorithm.SolutionDepth}");
        }
    }
}