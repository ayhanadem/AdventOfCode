namespace Day6;

public class Game : IComparable<Game>
{
    public List<int> Cards { get; set; }

    public int Bid { get; set; }

    public int Value
    {
        get
        {
            var dict = new Dictionary<int, int>();

            foreach (var card in Cards)
            {
                if (dict.ContainsKey(card))
                {
                    dict[card] = dict[card] + 1;
                }
                else
                {
                    dict[card] = 1;
                }
            }


            int threeCount = 0;
            int twoCount = 0;
            int oneCount = 0;

            foreach (var dictItem in dict)
            {
                if (dictItem.Value == 5)
                {
                    return 7;
                }
                else if (dictItem.Value == 4)
                {
                    return 6;
                }
                else if (dictItem.Value == 3)
                {
                    threeCount++;
                }
                else if (dictItem.Value == 2)
                {
                    twoCount++;
                }
                else
                {
                    oneCount++;
                }
            }

            if (threeCount == 1 && twoCount == 1)
            {
                return 5;
            }

            if (threeCount == 1)
            {
                return 4;
            }
            else if (twoCount == 2)
            {
                return 3;
            }
            else if (twoCount == 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }


    public long TotalResult =>
        Value * 10000000000 + Cards[0] * 100000000 + Cards[1] * 1000000 + Cards[2] * 10000 + Cards[3] * 100 +
        Cards[4] * 1;


    public int CompareTo(Game? other)
    {
        if (TotalResult > other.TotalResult)
            return 1;
        else if (TotalResult < other.TotalResult)
            return -1;
        else return 0;
    }
}