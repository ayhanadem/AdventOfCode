using System.Collections;

namespace Day4;

public class Calculator
{
    public static int ReadFileAndCalculateSum()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);

        return lines.Sum(GetValueFromLine);
    }

    private static int GetValueFromLine(string line)
    {
        var lineParts = line.Split('|');

        var winningPart = lineParts[0].Split(':')[1].Trim();
        var numbersPart = lineParts[1].Trim();


        var winningNumbers = new Dictionary<int, int>();

        var winningArray = winningPart.Split(" ");
        foreach (var numberString in winningArray)
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                var number = int.Parse(numberString);
                winningNumbers[number] = number;
            }
        }

        var subTotal = 0;
        foreach (var numberString in numbersPart.Split(' '))
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                var number = int.Parse(numberString);
                if (winningNumbers.ContainsKey(number))
                {
                    if (subTotal == 0)
                    {
                        subTotal = 1;
                    }
                    else
                    {
                        subTotal = subTotal * 2;
                    }
                }
            }
        }

        return subTotal;
    }


    public static long ReadFileAndCalculateCount()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var numberWinningArray = new int[lines.Length];
        for (var i = 0; i < lines.Length; i++)
        {
            numberWinningArray[i] = GetCountValueFromLine(lines[i]);
        }

        var numberQueue = new Queue();
        for (var i = 0; i < numberWinningArray.Length; i++)
        {
            var number = numberWinningArray[i];
            for (var j = 1; j <= number; j++)
            {
                numberQueue.Enqueue(i + j);
            }
        }


        var count = numberWinningArray.Length;
        while (numberQueue.Count > 0)
        {

            var numberIndex = (int)numberQueue.Dequeue()!;
            count++;

            if (numberWinningArray[numberIndex] <= 0) continue;
            for (var j = 1; j <= numberWinningArray[numberIndex]; j++)
            {
                numberQueue.Enqueue(numberIndex + j);
            }

        }

        return count;
    }

    private static int GetCountValueFromLine(string line)
    {
        var lineParts = line.Split('|');

        var winningPart = lineParts[0].Split(':')[1].Trim();
        var numbersPart = lineParts[1].Trim();


        var winningNumbers = new Dictionary<int, int>();

        var winningArray = winningPart.Split(" ");
        foreach (var numberString in winningArray)
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                var number = int.Parse(numberString);
                winningNumbers[number] = number;
            }
        }

        var subTotal = 0;
        foreach (var numberString in numbersPart.Split(' '))
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                var number = int.Parse(numberString);
                if (winningNumbers.ContainsKey(number))
                {
                    subTotal++;
                }
            }
        }

        return subTotal;
    }
}