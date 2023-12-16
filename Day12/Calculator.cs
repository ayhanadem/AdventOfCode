using System.Diagnostics.CodeAnalysis;

namespace Day12;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);

        var sum = 0;
        foreach (var line in lines)
        {
            var item = new Instruction(line);
            var altCount = item.AlternativeCount;
            sum += altCount;
            Console.WriteLine(line + "      " + altCount);
        }

        Console.WriteLine(sum);
    }
}