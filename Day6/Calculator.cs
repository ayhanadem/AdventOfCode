namespace Day6;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var races = new List<Race>();

        races.Add(new Race() { Time = 35, Distance = 213 });
        races.Add(new Race() { Time = 69, Distance = 1168 });
        races.Add(new Race() { Time = 68, Distance = 1086 });
        races.Add(new Race() { Time = 87, Distance = 1248 });


        long total = 1;
        foreach (var race in races)
        {
            total *= race.PossibleBeatCounts;
        }

        Console.WriteLine(total);

        var raceLast = new Race() { Time = 35696887, Distance = 213116810861248 };

        Console.WriteLine(raceLast.PossibleBeatCounts);

    }
}

public class Race
{
    public long Time { get; set; }
    public long Distance { get; set; }

    public long PossibleBeatCounts
    {
        get
        {
            int count = 0;

            for (int i = 1; i <= Time; i++)
            {
                if ((Time - i) * i > Distance)
                    count++;
            }

            return count;
        }
    }
}