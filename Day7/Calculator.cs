using Day6;

namespace Day7;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var games = new List<Game>();
        foreach (var line in lines)
        {
            var lineParts = line.Split(' ');

            var game = new Game();

            game.Bid = int.Parse(lineParts[1].ToString());
            game.Cards = new List<int>();


            foreach (var ch in lineParts[0])
            {
                if (ch.Equals('A'))
                {
                    game.Cards.Add(14);
                }
                else if (ch.Equals('K'))
                {
                    game.Cards.Add(13);
                }
                else if (ch.Equals('Q'))
                {
                    game.Cards.Add(12);
                }
                else if (ch.Equals('J'))
                {
                    game.Cards.Add(11);
                }
                else if (ch.Equals('T'))
                {
                    game.Cards.Add(10);
                }
                else
                {
                    try
                    {
                        game.Cards.Add(int.Parse(ch.ToString()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                }
            }

            games.Add(game);
        }


        games.Sort();
        long total = 0;

        int counter = 1;
    
        foreach (var game in games)
        {
            total += (counter * game.Bid);
            counter++;
        }

        Console.WriteLine(total);
    }
    
    
    public static void ReadFileAndCalculateQ2()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var games = new List<Game2>();
        foreach (var line in lines)
        {
            var lineParts = line.Split(' ');

            var game = new Game2();

            game.Bid = int.Parse(lineParts[1].ToString());
            game.Deck = lineParts[0];
            game.Cards = new List<int>();


            foreach (var ch in lineParts[0])
            {
                if (ch.Equals('A'))
                {
                    game.Cards.Add(14);
                }
                else if (ch.Equals('K'))
                {
                    game.Cards.Add(13);
                }
                else if (ch.Equals('Q'))
                {
                    game.Cards.Add(12);
                }
                else if (ch.Equals('J'))
                {
                    game.Cards.Add(1);
                }
                else if (ch.Equals('T'))
                {
                    game.Cards.Add(10);
                }
                else
                {
                    try
                    {
                        game.Cards.Add(int.Parse(ch.ToString()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                }
            }

            games.Add(game);
        }


        games.Sort();
        long total = 0;

        int counter = 1;
    
        foreach (var game in games)
        {
            total += (counter * game.Bid);
            counter++;
        }

        Console.WriteLine(total);
    }
}