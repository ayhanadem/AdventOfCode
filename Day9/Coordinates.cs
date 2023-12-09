using System.Text;

namespace Day9;

public class Coordinates
{
    public List<int> NumberList { get; set; }

    public int LastNumber
    {
        get { return NumberList.Last(); }
    }

    public int FirstNumber
    {
        get { return NumberList.First(); }
    }

    public Coordinates? SubCoordinates { get; set; }


    private void CreateSubCoordinate()
    {
        var isAll = true;
        foreach (var number in NumberList)
        {
            if (number != 0)
            {
                isAll = false;
                break;
            }
        }


        if (!isAll)
        {
            var subNumberList = new List<int>();

            if (NumberList.Count > 1)
            {
                for (int i = 1; i < NumberList.Count; i++)
                {
                    subNumberList.Add(NumberList[i] - NumberList[i - 1]);
                }
            }

            SubCoordinates = new Coordinates(subNumberList);
        }
    }


    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var number in NumberList)
        {
            sb.Append(number).Append(" ");
        }

        sb.AppendLine();

        if (SubCoordinates != null)
        {
            sb.Append(SubCoordinates.ToString());
        }

        return sb.ToString();
    }

    public Coordinates(List<int> numberList)
    {
        NumberList = numberList;
        CreateSubCoordinate();
    }

    public int Calculate()
    {
        if (this.SubCoordinates != null)
        {
            return this.LastNumber + this.SubCoordinates.Calculate();
        }
        else
        {
            return this.LastNumber;
        }
    }


    public int CalculateQ2()
    {
        if (this.SubCoordinates != null)
        {
            return this.FirstNumber - this.SubCoordinates.CalculateQ2();
        }
        else
        {
            return this.FirstNumber;
        }
    }
}