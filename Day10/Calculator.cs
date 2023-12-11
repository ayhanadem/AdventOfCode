using System.IO.Pipes;
using System.Runtime.CompilerServices;

namespace Day10;

public class Calculator
{
    public static int rowCount = 5;
    public static int columnCount = 5;
    public static string[,] Surface = new string[rowCount, columnCount];
    public static int[,] History = new int[rowCount, columnCount];

    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "testInput.txt");
        var lines = File.ReadAllLines(path);


        var start = new StartPoint();
        var startPipe = new Pipe();


        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (lines[i][j] == 'S')
                {
                    start.Row = i;
                    start.Column = j;
                    startPipe.Row = i;
                    startPipe.Column = j;
                    startPipe.Value = "S";
                }

                Surface[i, j] = lines[i][j].ToString();
            }
        }

        var visitHistory = new int[rowCount, columnCount];


        if (Surface[start.Row, start.Column + 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row, start.Column + 1, Surface[start.Row, start.Column + 1], 1, startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row, start.Column - 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row, start.Column - 1, Surface[start.Row, start.Column - 1], 1, startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row - 1, start.Column] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row - 1, start.Column, Surface[start.Row - 1, start.Column], 1, startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row - 1, start.Column + 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row - 1, start.Column + 1, Surface[start.Row - 1, start.Column + 1], 1,
                startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row - 1, start.Column - 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row - 1, start.Column - 1, Surface[start.Row - 1, start.Column - 1], 1,
                startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row + 1, start.Column] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row + 1, start.Column, Surface[start.Row + 1, start.Column], 1,
                startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row + 1, start.Column + 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row + 1, start.Column + 1, Surface[start.Row + 1, start.Column + 1], 1,
                startPipe);
            start.ConnectedPipes.Add(pipe);
        }

        if (Surface[start.Row + 1, start.Column - 1] != ".")
        {
            History = new int[rowCount, columnCount];
            var pipe = CreatePipe(start.Row + 1, start.Column - 1, Surface[start.Row + 1, start.Column - 1], 1,
                startPipe);
            start.ConnectedPipes.Add(pipe);
        }


        foreach (var pipe in start.ConnectedPipes)
        {
            Console.WriteLine(pipe.GetMaxDegree);
        }
    }


    private static Pipe? CreatePipe(int row, int column, string value, int degree, Pipe previousPipe)
    {
        if (History[row, column] > 0)
        {
            return null;
        }

        var pipe = new Pipe
        {
            Row = row,
            Column = column,
            Value = value,
            Degree = degree,
            PreviousPipe = previousPipe
        };

        History[row, column] = 1;

        if (value.Equals("."))
        {
            degree = 0;
        }
        else if (value.Equals("|"))
        {
            if (previousPipe.Row < row)
            {
                if (row < rowCount - 1)
                {
                    pipe.NextPipe = CreatePipe(row + 1, column, Surface[row + 1, column], degree + 1, pipe);
                }
            }
            else
            {
                if (row > 0)
                {
                    pipe.NextPipe = CreatePipe(row - 1, column, Surface[row - 1, column], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("-"))
        {
            if (previousPipe.Column < column)
            {
                if (column < columnCount - 1)
                {
                    pipe.NextPipe = CreatePipe(row, column + 1, Surface[row, column + 1], degree + 1, pipe);
                }
            }
            else
            {
                if (column > 0)
                {
                    pipe.NextPipe = CreatePipe(row, column - 1, Surface[row, column - 1], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("L"))
        {
            if (previousPipe.Column == column)
            {
                if (row  > 0 )
                {
                    pipe.NextPipe = CreatePipe(row - 1, column, Surface[row - 1, column], degree + 1, pipe);
                }
            }
            else
            {
                if (column < columnCount -1)
                {
                    pipe.NextPipe = CreatePipe(row , column+1, Surface[row , column+1], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("7"))
        {
            if (previousPipe.Row == row)
            {
                if (row < rowCount - 1)
                {
                    pipe.NextPipe = CreatePipe(row + 1, column, Surface[row + 1, column], degree + 1, pipe);
                }
            }
            else
            {
                if (column > 0)
                {
                    pipe.NextPipe = CreatePipe(row, column - 1, Surface[row, column - 1], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("J"))
        {
            if (previousPipe.Row == row)
            {
                if (row > 0)
                {
                    pipe.NextPipe = CreatePipe(row - 1, column, Surface[row - 1, column], degree + 1, pipe);
                }
            }
            else
            {
                if (column > 0)
                {
                    pipe.NextPipe = CreatePipe(row, column - 1, Surface[row, column - 1], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("F"))
        {
            if (previousPipe.Row == row)
            {
                if (row < rowCount - 1)
                {
                    pipe.NextPipe = CreatePipe(row, column, Surface[row + 1, column], degree + 1, pipe);
                }
            }
            else
            {
                if (column < columnCount - 1)
                {
                    pipe.NextPipe = CreatePipe(row, column + 1, Surface[row, column + 1], degree + 1, pipe);
                }
            }
        }
        else if (value.Equals("S"))
            degree = 0;

        return pipe;
    }
}