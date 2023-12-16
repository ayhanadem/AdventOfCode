namespace Day14;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");


        var lines = File.ReadAllLines(path);
        var rowCount = lines.Length;
        var columnCount = lines[0].Length;
        var newLines = new string[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                newLines[i, j] = lines[i][j].ToString();
            }
        }


        var dictResult = new Dictionary<int, int>();
        var resultList = RunCyle(newLines, rowCount, columnCount);

        var cycleCount = 20000;


        var cycleResults = new int[cycleCount];


        var solutionSet = new int[390];
        

        var dict = new Dictionary<int, List<int>>();
        for (int i = 1; i < cycleCount; i++)
        {
            resultList = RunCyle(resultList, rowCount, columnCount);
            var res = CalculateResult(resultList, rowCount, columnCount);

            if (dict.ContainsKey(res))
            {
                var list = dict[res];
                list.Add(i);
                dict[res] = list;

                if (i >= 16180 && i<16570)
                {
                    solutionSet[i - 16180] = res;
                }
            }
            else
            {
                dict[res] = new List<int>() { i };
            }
        }


        var resultIndex = (1000000000 - 16180) % 390;

        Console.WriteLine(solutionSet[resultIndex-1]);

    }


    public static int CalculateResult(string[,] newLines, int rowCount, int columnCount)
    {
        var sum = 0;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (newLines[i, j] == "O")
                {
                    sum += rowCount - i;
                }
            }
        }

        // Console.WriteLine(sum);

        return sum;
    }

    public static string[,] RunCyle(string[,] newLines, int rowCount, int columnCount)
    {
        var afterNorthTilt = TiltNorth(newLines, rowCount, columnCount);

        var afterWestTilt = TiltWest(afterNorthTilt, rowCount, columnCount);

        var afterSouthTilt = TiltSouth(afterWestTilt, rowCount, columnCount);

        var afterEastTilt = TiltEast(afterSouthTilt, rowCount, columnCount);

        //CalculateResult(afterEastTilt, rowCount, columnCount);
        return afterEastTilt;
    }


    public static string[,] TiltNorth(string[,] newLines, int rowCount, int columnCount)
    {
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                var value = newLines[j, i];
                if (value == "O")
                {
                    for (var k = j - 1; k >= 0; k--)
                    {
                        if (newLines[k, i] == ".")
                        {
                            //burada bir sorun var
                            newLines[k, i] = "O";
                            newLines[k + 1, i] = ".";
                            //PrintArray(newLines, rowCount, columnCount, j,i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return newLines;
    }

    public static string[,] TiltWest(string[,] newLines, int rowCount, int columnCount)
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                var value = newLines[i, j];
                if (value == "O")
                {
                    for (var k = j - 1; k >= 0; k--)
                    {
                        if (newLines[i, k] == ".")
                        {
                            newLines[i, k] = "O";
                            newLines[i, k + 1] = ".";
                            //PrintArray(newLines, rowCount, columnCount, j,i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return newLines;
    }


    public static string[,] TiltSouth(string[,] newLines, int rowCount, int columnCount)
    {
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = rowCount - 1; j >= 0; j--)
            {
                var value = newLines[j, i];
                if (value == "O")
                {
                    for (var k = j + 1; k < rowCount; k++)
                    {
                        if (newLines[k, i] == ".")
                        {
                            //burada bir sorun var
                            newLines[k, i] = "O";
                            newLines[k - 1, i] = ".";
                            // PrintArray(newLines, rowCount, columnCount, j,i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return newLines;
    }


    public static string[,] TiltEast(string[,] newLines, int rowCount, int columnCount)
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = columnCount - 1; j >= 0; j--)
            {
                var value = newLines[i, j];
                if (value == "O")
                {
                    for (var k = j + 1; k < rowCount; k++)
                    {
                        if (newLines[i, k] == ".")
                        {
                            //burada bir sorun var
                            newLines[i, k] = "O";
                            newLines[i, k - 1] = ".";
                            //PrintArray(newLines, rowCount, columnCount, j,i);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return newLines;
    }

    public static void PrintArray(string[,] array, int rowCount, int columnCount, int rowIndex = 0, int columnIndex = 0)
    {
        Console.WriteLine("************************************");
        for (int i = 0; i < rowCount; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < columnCount; j++)
            {
                if (i == rowIndex && j == columnIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(array[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(array[i, j] + " ");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("************************************");
    }
}