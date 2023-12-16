using System.Text;

namespace Day15;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");


        var line = File.ReadAllText(path);

        CalculateLineValueQ1(line);
        CalculateLineValueQ2(line);
    }

    private static void CalculateLineValueQ1(string line)
    {
        var words = line.Split(',');

        int sum = 0;
        foreach (var word in words)
        {
            int wordValue = GetWordValue(word);
            sum += wordValue;
        }

        Console.WriteLine(sum);
    }

    private static int GetWordValue(string word)
    {
        byte[] asciiBytes = Encoding.ASCII.GetBytes(word);

        int currentValue = 0;
        foreach (var asciiByte in asciiBytes)
        {
            currentValue += asciiByte;

            currentValue = (currentValue * 17) % 256;
        }

        return currentValue;
    }

    private static void CalculateLineValueQ2(string line)
    {
        var words = line.Split(',');

        var dict = new Dictionary<int, List<KeyValuePair<string, int>>>();
        foreach (var word in words)
        {
            int multiplier = 0;
            bool equals = false;
            var newWord = "";

            if (word.Contains("="))
            {
                var wordSplit = word.Split('=');
                newWord = wordSplit[0];
                equals = true;
                multiplier = int.Parse(wordSplit[1]);
            }
            else
            {
                newWord = word.Replace("-", "");
            }

            int value = GetWordValue(newWord);
            if (dict.ContainsKey(value))
            {
                var list = dict[value];

                var newList = new List<KeyValuePair<string, int>>();

                bool isExist = false;
                foreach (var pair in list)
                {
                    if (equals)
                    {
                        if (pair.Key.Equals(newWord))
                        {
                            var newPair = new KeyValuePair<string, int>(newWord, multiplier);
                            newList.Add(newPair);
                            isExist = true;
                        }
                        else
                        {
                            newList.Add(pair);
                        }
                    }
                    else
                    {
                        if (pair.Key.Equals(newWord))
                        {
                            continue;
                        }
                        else
                        {
                            newList.Add(pair);
                        }
                    }
                }

                if (equals && !isExist)
                {
                    var newPair = new KeyValuePair<string, int>(newWord, multiplier);
                    newList.Add(newPair);
                }
                
                dict[value] = newList;
            }
            else
            {
                if (!equals) continue;
                var newList = new List<KeyValuePair<string, int>>();
                var newPair = new KeyValuePair<string, int>(newWord, multiplier);
                newList.Add(newPair);

                dict[value] = newList;
            }
        }


        int sum = 0;
        foreach (var item in dict)
        {

            var list = item.Value;

            int counter = 1;
            foreach (var pair in list)
            {
                sum += counter * pair.Value * (item.Key+1);
                counter++;
            }
            
        }

        Console.WriteLine(sum);
    }
}