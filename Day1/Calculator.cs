using System.Text;

namespace Day1_1;

public class Calculator
{
    public static int ReadFileAndCalculateSum()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "codes.txt");
        string[] lines = File.ReadAllLines(path);

        int total = 0;
        foreach (string line in lines)
            total += GetNumberFromText(GetLineFromLine(line));
        return total;
    }


    public static string GetLineFromLine(string line)
    {
        return line.Replace("one", "o1ne").Replace("two", "t2wo")
            .Replace("three", "th3ree").Replace("four", "4").Replace("five", "5five")
            .Replace("six", "6").Replace("seven", "7seven").Replace("eight", "eigh8t")
            .Replace("nine", "9nine");
    }
    public static int GetNumberFromText(string line)
    {
        int first = -1;
        int second = -1;
        StringBuilder sb = new StringBuilder();
        foreach (var ch in line)
        {
            if (Char.IsNumber(ch))
            {
                if (first >= 0)
                    second = int.Parse(ch.ToString());
                else
                {
                    first = int.Parse(ch.ToString());
                }
            }
        }

        if (first > 0 && second >= 0)
        {
            return first * 10 + second;
        }
        else if (first >= 0)
        {
            return first * 10 + first;
        }
        else
        {
            return 0;
        }
    }
}