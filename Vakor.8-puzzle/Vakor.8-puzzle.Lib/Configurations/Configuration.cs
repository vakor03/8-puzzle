namespace Vakor._8_puzzle.Lib.Configurations
{
    public static class Configuration
    {
        public static int Dimension { get; set; }
        public static int MixIterationCount { get; set; }

        static Configuration()
        {
            Dimension = 3;
            MixIterationCount = 100000;
        }
    }
}