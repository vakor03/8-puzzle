namespace Vakor._8_puzzle.Lib.Configurations
{
    public static class Configuration
    {
        public static int Dimension { get; set; }
        public static int MixIterationCount { get; set; }
        public static int MaxPossibleStates { get; set; }
        public static int LDFSMaxDepth { get; set; }

        static Configuration()
        {
            Dimension = 3;
            MixIterationCount = 100000;
            MaxPossibleStates = 181440;
            LDFSMaxDepth = 30;
        }
    }
}