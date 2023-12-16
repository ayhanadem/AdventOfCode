using System.Text.RegularExpressions;

namespace Day12;

public class Instruction
{
    public Instruction(string line)
    {
        var codeSplit = line.Split(' ');
        this.Code = codeSplit[0];
        Result = new List<int>();
        var numberSplit = codeSplit[1].Split(',');

        foreach (var ns in numberSplit)
        {
            Result.Add(int.Parse(ns));
        }


        foreach (var rs in Result)
        {
            ResultString = ResultString + rs.ToString();
        }

        PossiblesWithUnknown.Enqueue(Code);
        CreateListOfPossibleCode();
    }

    private void CreateListOfPossibleCode()
    {
        while (PossiblesWithUnknown.Count > 0)
        {
            var code = PossiblesWithUnknown.Dequeue();
            if (code.Contains('?'))
            {
                var regex = new Regex(Regex.Escape("?"));
                var first = regex.Replace(code, ".", 1);
                var second = regex.Replace(code, "#", 1);

                PossiblesWithUnknown.Enqueue(first);
                PossiblesWithUnknown.Enqueue(second);
            }
            else
            {
                var result = GetResultFromCode(code);
                if (result.Count == Result.Count)
                {
                    var resStr = "";
                    foreach (var rs in result)
                    {
                        resStr = resStr + rs;
                    }

                    PossibleResults.Add(resStr);
                }
            }
        }
    }

    public List<int> GetResultFromCode(string code)
    {
        var counter = 0;
        var result = new List<int>();
        foreach (var ch in code)
        {
            if (ch.Equals('#'))
            {
                counter++;
            }
            else
            {
                if (counter > 0)
                {
                    result.Add(counter);
                }

                counter = 0;
            }
        }

        if (counter > 0)
        {
            result.Add(counter);
        }

        return result;
    }

    public string Code { get; set; }
    public List<int> Result { get; set; }

    public string ResultString { get; set; }
    public List<string> PossibleResults { get; set; } = new List<string>();

    public Queue<string> PossiblesWithUnknown { get; set; } = new Queue<string>();


    public int AlternativeCount
    {
        get
        {
            var counter = 0;
            foreach (var result in PossibleResults)
            {
                if (result.Equals(ResultString))
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}