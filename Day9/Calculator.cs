namespace Day9;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);

        var total = 0;
        foreach (var line in lines)
        {
            total += CalculateLastNumber(line);
        }

        Console.WriteLine(total);
        
        
        var totalFirst = 0;
        foreach (var line in lines)
        {
            totalFirst += CalculateFirstNumber(line);
        }

        Console.WriteLine(totalFirst);
    }

    private static int CalculateLastNumber(string line)
    {
        var numberSplit = line.Split(' ');
        var numbers = new List<int>();
        foreach (var numberString in numberSplit)
        {
            numbers.Add(int.Parse(numberString));
        }

        var coordinates = new Coordinates(numbers);


        return coordinates.Calculate();
    }
    
    private static int CalculateFirstNumber(string line)
    {
        var numberSplit = line.Split(' ');
        var numbers = new List<int>();
        foreach (var numberString in numberSplit)
        {
            numbers.Add(int.Parse(numberString));
        }

        var coordinates = new Coordinates(numbers);


       // Console.WriteLine(coordinates);
        return coordinates.CalculateQ2();
    }
}