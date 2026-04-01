// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DiamondCreatorBenchmark;

do
{
    Console.WriteLine(FormattableString.Invariant($"Starting Benchmark Diamonds creation at: {DateTime.Now:g}"));

    try
    {
        BenchmarkRunner.Run<BenchmarkDiamondCreator>();
    }
    catch (Exception e)
    {
        Console.WriteLine(FormattableString.Invariant($"Benchmark Diamonds creation failed.{Environment.NewLine}Error details: {e}"));
    }

    Console.WriteLine(FormattableString.Invariant($"Benchmark Diamonds successfully created at: {DateTime.Now:g}"));
} while (Console.ReadKey().Key != ConsoleKey.Escape);
