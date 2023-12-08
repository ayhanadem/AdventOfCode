namespace Day8;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var instructions = lines[0];


        var dict = new Dictionary<string, KeyValuePair<string, string>>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
            {
                //TNX = (BBN, MXH)
                var value = lines[i].Substring(0, 3);
                var left = lines[i].Substring(7, 3);
                var right = lines[i].Substring(12, 3);

                dict.Add(value, new KeyValuePair<string, string>(left, right));
            }
        }

        var isfound = false;
        var counter = 1;
        var next = "AAA";

        while (!isfound)
        {
            foreach (var instruction in instructions)
            {
                var value = dict[next];
                if (instruction == 'L')
                {
                    next = value.Key;
                }
                else
                {
                    next = value.Value;
                }

                if (next == "ZZZ")
                {
                    isfound = true;
                    break;
                }
                else
                {
                    counter++;
                }
            }
        }

        Console.WriteLine(counter);
    }


    public static void ReadFileAndCalculateQ2()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var instructions = lines[0];


        var dict = new Dictionary<string, KeyValuePair<string, string>>();

        var Alist = new List<string>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
            {
                //TNX = (BBN, MXH)
                var value = lines[i].Substring(0, 3);
                var left = lines[i].Substring(7, 3);
                var right = lines[i].Substring(12, 3);

                if (value.EndsWith("A"))
                {
                    Alist.Add(value);
                }

                dict.Add(value, new KeyValuePair<string, string>(left, right));
            }
        }


        var counterList = new List<int>();

        foreach (var aStart in Alist)
        {
            var isfound = false;
            var counter = 1;
            var next = aStart;

            while (!isfound)
            {
                foreach (var instruction in instructions)
                {
                    var value = dict[next];
                    if (instruction == 'L')
                    {
                        next = value.Key;
                    }
                    else
                    {
                        next = value.Value;
                    }

                    if (next.EndsWith("Z"))
                    {
                        isfound = true;
                        break;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }

            counterList.Add(counter);
        }

        var array = new long[counterList.Count];

        for (int i = 0; i < counterList.Count; i++)
        {
            array[i] = (long)counterList[i];
        }

        Console.WriteLine(LCMFinder.FindLcm(array, (long)counterList.Count));
    }
}