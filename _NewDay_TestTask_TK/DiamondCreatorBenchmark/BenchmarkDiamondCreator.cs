using BenchmarkDotNet.Attributes;
using DiamondCreatorLib;

namespace DiamondCreatorBenchmark
{
    [MemoryDiagnoser()]
    public class BenchmarkDiamondCreator
    {
        private static readonly string EnglishAlphabetUppercase = DiamondCreatorLib.Properties.Resources.ENGLISH_ALPHABET_UPPERCASE;
        private static readonly char LetterA = EnglishAlphabetUppercase.First();
        private static readonly char LetterZ = EnglishAlphabetUppercase.Last();
        private static readonly char LetterL = EnglishAlphabetUppercase[11];

        private DiamondCreatorService? diamondCreatorService;

        public BenchmarkDiamondCreator()
        {
            InitializeDiamondCreator();

            DefineAlphabetFromString();
            DefineAlphabetFromFirstAndLastSymbols();

            CreateDiamondBenchmark_A();

            CreateDiamondBenchmark_L();

            CreateDiamondBenchmark_Z();
        }

        [Benchmark]
        public void InitializeDiamondCreator()
        {
            diamondCreatorService = new DiamondCreatorService();
        }

        [Benchmark]
        public void DefineAlphabetFromString()
        {
            diamondCreatorService?.DefineAlphabet(EnglishAlphabetUppercase);
        }

        [Benchmark]
        public void DefineAlphabetFromFirstAndLastSymbols()
        {
            diamondCreatorService?.DefineAlphabet(EnglishAlphabetUppercase.First(), EnglishAlphabetUppercase.Last());
        }

        [Benchmark]
        public string? CreateDiamondBenchmark_A()
        {
            return diamondCreatorService?.CreateDiamond(LetterA);
        }

        [Benchmark]
        public string? CreateDiamondBenchmark_L()
        {
            return diamondCreatorService?.CreateDiamond(LetterL);
        }

        [Benchmark]
        public string? CreateDiamondBenchmark_Z()
        {
            return diamondCreatorService?.CreateDiamond(LetterZ);
        }
    }
}
