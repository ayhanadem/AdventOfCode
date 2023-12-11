namespace Day11;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "testInput.txt");
        var lines = File.ReadAllLines(path);


        var universe = new List<List<int>>();
        foreach (var line in lines)
        {
            var row = new List<int>();
            foreach (var ch in line)
            {
                if (ch.Equals('#'))
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(0);
                }
            }

            universe.Add(row);
            if (row.Sum() == 0)
            {
                universe.Add(row);
            }
        }

        var replacedColumns = new List<int>();

        for (int i = 0; i < universe[0].Count; i++)
        {
            var isEmpty = true;
            for (int j = 0; j < universe.Count; j++)
            {
                if (universe[j][i] == 1)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                replacedColumns.Add(i);
            }
        }


        var counter = 1;
        foreach (var index in replacedColumns)
        {
            foreach (var row in universe)
            {
                row.Insert(index + counter, 0);
            }

            counter++;
        }


        var points = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < universe.Count; i++)
        {
            for (int j = 0; j < universe[i].Count; j++)
            {
                if (universe[i][j] == 1)
                {
                    points.Add(new KeyValuePair<int, int>(i, j));
                }
            }
        }

        int sum = 0;

        foreach (var point in points)
        {
            Console.Write("("+point.Key +"," + point.Value+")  ");
        }

        Console.WriteLine();
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                sum = sum + Math.Abs((points[i].Key - points[j].Key)) + Math.Abs((points[i].Value - points[j].Value));
            }
        }

        Console.WriteLine(sum);
    }


    public static void ReadFileAndCalculateQ2Test()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "testInput.txt");
        var lines = File.ReadAllLines(path);
        var expendNumber = 9;

        var universe = new List<List<int>>();
        foreach (var line in lines)
        {
            var row = new List<int>();
            foreach (var ch in line)
            {
                if (ch.Equals('#'))
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(0);
                }
            }

            universe.Add(row);
            if (row.Sum() == 0)
            {
                for (int i = 0; i < expendNumber; i++)
                {
                    universe.Add(row);
                }
               
            }
        }

        var replacedColumns = new List<int>();

        for (int i = 0; i < universe[0].Count; i++)
        {
            var isEmpty = true;
            for (int j = 0; j < universe.Count; j++)
            {
                if (universe[j][i] == 1)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                replacedColumns.Add(i);
            }
        }


        var counter = 1;
        foreach (var index in replacedColumns)
        {
            foreach (var row in universe)
            {
                for (int i = 0; i < expendNumber; i++)
                {
                    row.Insert(index + counter + i, 0);
                }
            }

            counter = counter + expendNumber;
        }


        var points = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < universe.Count; i++)
        {
            for (int j = 0; j < universe[i].Count; j++)
            {
                if (universe[i][j] == 1)
                {
                    points.Add(new KeyValuePair<int, int>(i, j));
                }
            }
        }

        
        foreach (var point in points)
        {
            Console.Write("("+point.Key +"," + point.Value+")  ");
        }

        Console.WriteLine();
        int sum = 0;

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                sum = sum + Math.Abs((points[i].Key - points[j].Key)) + Math.Abs((points[i].Value - points[j].Value));
            }
        }

        Console.WriteLine(sum);
    }

    public static void ReadFileAndCalculateQ2()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var universe = new List<List<int>>();
        var replacedRows = new List<int>();


        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var row = new List<int>();
            foreach (var ch in line)
            {
                if (ch.Equals('#'))
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(0);
                }
            }

            if (row.Sum() == 0)
            {
                replacedRows.Add(i);
            }

            universe.Add(row);
        }

        var replacedColumns = new List<int>();

        for (int i = 0; i < universe[0].Count; i++)
        {
            var isEmpty = true;
            for (int j = 0; j < universe.Count; j++)
            {
                if (universe[j][i] == 1)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                replacedColumns.Add(i);
            }
        }

        var points = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < universe.Count; i++)
        {
            for (int j = 0; j < universe[i].Count; j++)
            {
                if (universe[i][j] == 1)
                {
                    points.Add(new KeyValuePair<int, int>(i, j));
                }
            }
        }


        var expendNumber = 999999;
        var expendedPoints = new List<KeyValuePair<int, int>>();
        foreach (var point in points)
        {
            int rowIndex = point.Key;
            int columnIndex = point.Value;

            var newRowIndex = rowIndex;
            var newColumnIndex = columnIndex;


            foreach (var row in replacedRows)
            {
                if (row < rowIndex)
                {
                    newRowIndex = newRowIndex + expendNumber;
                }
            }

            foreach (var column in replacedColumns)
            {
                if (column < columnIndex)
                {
                    newColumnIndex = newColumnIndex + expendNumber;
                }
            }

            expendedPoints.Add(new KeyValuePair<int, int>(newRowIndex, newColumnIndex));
        }


        long sum = 0;

        foreach (var point in expendedPoints)
        {
            Console.Write("("+point.Key +"," + point.Value+")  ");
        }

        Console.WriteLine();
        for (int i = 0; i < expendedPoints.Count; i++)
        {
            for (int j = i + 1; j < expendedPoints.Count; j++)
            {
                sum = sum + Math.Abs((expendedPoints[i].Key - expendedPoints[j].Key)) +
                      Math.Abs((expendedPoints[i].Value - expendedPoints[j].Value));
            }
        }

        Console.WriteLine(sum);
    }
}