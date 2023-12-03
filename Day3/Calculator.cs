using System.Collections;

namespace Day3;

public class Calculator
{
    public static int ReadFileAndCalculateSum()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "schema.txt");
        string[] lines = File.ReadAllLines(path);

        var rowCount = lines.Length;
        var columnCount = lines[0].Length;
        var schemaArray = new char[rowCount, columnCount];
        var symbolArray = new int[rowCount, columnCount];

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            for (int j = 0; j < line.Length; j++)
            {
                if (Char.IsNumber(line[j]))
                {
                    schemaArray[i, j] = line[j];
                }
                else if (line[j] == '.')
                {
                    schemaArray[i, j] = '.';
                }
                else
                {
                    schemaArray[i, j] = '.';

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j + 1] = 1;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j - 1] = 1;
                        }
                        else
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j - 1] = 1;
                            symbolArray[i + 1, j + 1] = 1;
                        }
                    }
                    else if (i == rowCount - 1)
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j + 1] = 1;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j - 1] = 1;
                        }
                        else
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j - 1] = 1;
                            symbolArray[i - 1, j + 1] = 1;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j + 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j + 1] = 1;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j - 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j - 1] = 1;
                        }
                        else
                        {
                            symbolArray[i, j] = 1;
                            symbolArray[i, j - 1] = 1;
                            symbolArray[i, j + 1] = 1;
                            symbolArray[i - 1, j] = 1;
                            symbolArray[i - 1, j - 1] = 1;
                            symbolArray[i - 1, j + 1] = 1;
                            symbolArray[i + 1, j] = 1;
                            symbolArray[i + 1, j - 1] = 1;
                            symbolArray[i + 1, j + 1] = 1;
                        }
                    }
                }
            }
        }


        var total = 0;
        var number = 0;
        var isValid = false;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (Char.IsNumber(schemaArray[i, j]))
                {
                    number = number * 10 + int.Parse(schemaArray[i, j].ToString());
                    if (symbolArray[i, j] == 1)
                    {
                        isValid = true;
                    }
                }
                else
                {
                    if (isValid)
                    {
                        total = total + number;
                        number = 0;
                        isValid = false;
                    }
                    else
                    {
                        number = 0;
                        isValid = false;
                    }
                }
            }


            if (isValid)
            {
                total = total + number;
                number = 0;
                isValid = false;
            }
            else
            {
                number = 0;
                isValid = false;
            }
        }


        return total;
    }


    public static int ReadFileAndCalculateMultiple()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "schema.txt");
        string[] lines = File.ReadAllLines(path);

        var rowCount = lines.Length;
        var columnCount = lines[0].Length;
        var schemaArray = new char[rowCount, columnCount];
        var symbolArray = new int[rowCount, columnCount];


        int starIndex = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            for (int j = 0; j < line.Length; j++)
            {
                if (Char.IsNumber(line[j]))
                {
                    schemaArray[i, j] = line[j];
                }
                else if (line[j] == '.')
                {
                    schemaArray[i, j] = '.';
                }
                else if (line[j] == '*')
                {
                    schemaArray[i, j] = '.';

                    starIndex++;
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j + 1] = starIndex;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j - 1] = starIndex;
                        }
                        else
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j - 1] = starIndex;
                            symbolArray[i + 1, j + 1] = starIndex;
                        }
                    }
                    else if (i == rowCount - 1)
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j + 1] = starIndex;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j - 1] = starIndex;
                        }
                        else
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j - 1] = starIndex;
                            symbolArray[i - 1, j + 1] = starIndex;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j + 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j + 1] = starIndex;
                        }
                        else if (j == columnCount - 1)
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j - 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j - 1] = starIndex;
                        }
                        else
                        {
                            symbolArray[i, j] = starIndex;
                            symbolArray[i, j - 1] = starIndex;
                            symbolArray[i, j + 1] = starIndex;
                            symbolArray[i - 1, j] = starIndex;
                            symbolArray[i - 1, j - 1] = starIndex;
                            symbolArray[i - 1, j + 1] = starIndex;
                            symbolArray[i + 1, j] = starIndex;
                            symbolArray[i + 1, j - 1] = starIndex;
                            symbolArray[i + 1, j + 1] = starIndex;
                        }
                    }
                }
                else
                {
                    schemaArray[i, j] = '.';
                }
            }
        }


        var total = 0;
        var number = 0;
        var numberIndex = 0;

        var numberDict = new Dictionary<int, int>();
        var multiplierDict = new Dictionary<int, ArrayList>();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (Char.IsNumber(schemaArray[i, j]))
                {
                    number = number * 10 + int.Parse(schemaArray[i, j].ToString());

                    numberDict[numberIndex] = number;

                    if (symbolArray[i, j] > 0)
                    {
                        var index = symbolArray[i, j];
                        if (multiplierDict.ContainsKey(index))
                        {
                            var arrayList = multiplierDict[index];
                            if (!arrayList.Contains(numberIndex))
                            {
                                arrayList.Add(numberIndex);
                            }
                            multiplierDict[index] = arrayList;
                        }
                        else
                        {
                            multiplierDict[index] = new ArrayList { numberIndex };
                        }
                    }
                }
                else
                {
                        number = 0;
                        numberIndex++;

                }
            }


            number = 0;
            numberIndex++;
        }




        foreach (var dict in multiplierDict)
        {
            if (dict.Value.Count > 1)
            {
                var subMultiple = 1;
                foreach (int ind in dict.Value)
                {
                    subMultiple = subMultiple * numberDict[ind];
                }
                total = total + subMultiple;
            }
        }
        
        
        return total;
    }
}