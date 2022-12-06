using GAConsoleApp;
using System.Diagnostics;
Stopwatch stopwatch = Stopwatch.StartNew();

var target = "It was the best of times, it was blurst of times (It was the best of times, it was worst of times) .";

WordMatchGeneticAlgorithm ga = new(10000, target, 10000 / 3);
for (int i = 0; i < 10000; i++)
{
    var best = ga.Best();
    if (best == target)
    {
        Console.WriteLine($"{i}\t{best}\t成功");
        break;
    }
    Console.WriteLine($"{i}\t{best}");
    ga.Evolve();
}
stopwatch.Stop();
Console.WriteLine($"耗时:{stopwatch.ElapsedMilliseconds / 1000.0}秒");