using System.Text;

namespace Day1_1;

public class Calculator
{
    public static int ReadFileAndCalculateSum()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "games.txt");
        string[] lines = File.ReadAllLines(path);

        int total = 0;
        foreach (string line in lines)
            total += GetNumberFromText(line);
        return total;
    }
    
    public static int ReadFileAndCalculatePower()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "games.txt");
        string[] lines = File.ReadAllLines(path);

        int total = 0;
        foreach (string line in lines)
            total += GetPowerFromText(line);
        return total;
    }
    
    
    


    public static int GetNumberFromText(string line)
    {
        var firstIterate = line.Split(':');
        var games = firstIterate[1];

        var gamesSplit = games.Split(';');

        foreach (var game in gamesSplit)
        {
            var red = 12;
            var green = 13;
            var blue = 14;

            var results = game.Split(',');
            foreach (var result in results)
            {
                var trimmedResult = result.Trim();
                if (trimmedResult.Contains("green"))
                {
                    var numberResult = trimmedResult.Replace("green", "").Trim();
                    var count = int.Parse(numberResult);

                    green = green - count;
                }

                if (trimmedResult.Contains("red"))
                {
                    var numberResult = trimmedResult.Replace("red", "").Trim();
                    var count = int.Parse(numberResult);
                    red = red - count;
                }

                if (trimmedResult.Contains("blue"))
                {
                    var numberResult = trimmedResult.Replace("blue", "").Trim();
                    var count = int.Parse(numberResult);
                    blue = blue - count;
                }

                if (green < 0 || red < 0 || blue < 0)
                {
                    Console.WriteLine("Failed " + line);
                    return 0;
                }
            }
        }


        var gameId = firstIterate[0].Split(' ')[1];
        return int.Parse(gameId);
    }


    public static int GetPowerFromText(string line)
    {
        var firstIterate = line.Split(':');
        var tours = firstIterate[1];

        var toursSplit = tours.Split(';');

        var redMax = -1;
        var blueMax = -1;
        var greenMax = -1;

        foreach (var tour in toursSplit)
        {
            var redTotal = 0;
            var greenTotal = 0;
            var blueTotal = 0;

            var values = tour.Split(',');
            foreach (var result in values)
            {
                var trimmedResult = result.Trim();
                if (trimmedResult.Contains("green"))
                {
                    var numberResult = trimmedResult.Replace("green", "").Trim();
                    var count = int.Parse(numberResult);

                    greenTotal = greenTotal + count;
                }

                if (trimmedResult.Contains("red"))
                {
                    var numberResult = trimmedResult.Replace("red", "").Trim();
                    var count = int.Parse(numberResult);
                    redTotal = redTotal + count;
                }

                if (trimmedResult.Contains("blue"))
                {
                    var numberResult = trimmedResult.Replace("blue", "").Trim();
                    var count = int.Parse(numberResult);
                    blueTotal = blueTotal + count;
                }
            }


            if (redTotal > redMax)
            {
                redMax = redTotal;
            }

            if (greenTotal > greenMax)
            {
                greenMax = greenTotal;
            }

            if (blueTotal > blueMax)
            {
                blueMax = blueTotal;
            }
        }


        if (redMax <=0) redMax = 1;
        if (blueMax <=0) blueMax = 1;
        if (greenMax <=0) greenMax = 1;
        return redMax * blueMax * greenMax;
    }
}