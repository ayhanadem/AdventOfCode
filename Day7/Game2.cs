namespace Day6;

public class Game2 : IComparable<Game2>
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


            int fivecounts = 0;
            int fourCounts = 0;
            int threeCount = 0;
            int twoCount = 0;
            int oneCount = 0;


            var jCounts = dict.ContainsKey(1) ? dict[1] : 0;


            
            foreach (var dictItem in dict)
            {
                if (dictItem.Key != 1)
                {
                    if (dictItem.Value == 5)
                    {
                        fivecounts++;
                    }
                    else if (dictItem.Value == 4)
                    {
                        fourCounts++;
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
            }

            if (jCounts == 5 || jCounts == 4)
                return 7;

            if (fivecounts == 1)
                return 7;
            else if (fourCounts == 1 && jCounts == 1)
                return 7;
            else if (fourCounts == 1 && jCounts == 0)
                return 6;
            
            
            else if (threeCount == 1 && jCounts == 2)
                return 7;
            else if (threeCount == 1 && jCounts == 1)
                return 6;
            else if (threeCount == 1 && twoCount == 1)
                return 5;
            else if (threeCount == 1)
                return 4;
            
            else if (twoCount == 2 && jCounts==1)
                return 5;
            else if (twoCount == 2)
                return 3;
            
            else if (twoCount == 1 && jCounts==3)
                return 7;
            else if (twoCount == 1 && jCounts==2)
                return 6;
            else if (twoCount == 1 && jCounts==1)
                return 4;
            else if (twoCount == 1)
                return 2;
            else if (oneCount == 2 && jCounts==3)
                return 6;
            else if (oneCount == 3 && jCounts==2)
                return 4;
            else if (oneCount == 4 && jCounts == 1)
                return 2;
            else
                return 1;

        }
    }

    public string Deck { get; set; }

    public long TotalResult =>
        Value * 10000000000 + Cards[0] * 100000000 + Cards[1] * 1000000 + Cards[2] * 10000 + Cards[3] * 100 +
        Cards[4] * 1;


    public int CompareTo(Game2? other)
    {
        if (TotalResult > other.TotalResult)
            return 1;
        else if (TotalResult < other.TotalResult)
            return -1;
        else return 0;
    }
}