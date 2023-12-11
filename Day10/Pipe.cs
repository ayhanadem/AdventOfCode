using Microsoft.VisualBasic.CompilerServices;

namespace Day10;

public class Pipe
{
    public string Value { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public Pipe? PreviousPipe { get; set; }
    public Pipe? NextPipe { get; set; }
    public int Degree { get; set; }


    public int GetMaxDegree
    {
        get
        {
            if (NextPipe != null)
            {
                return int.Max(Degree, NextPipe.GetMaxDegree);
            }
            else
            {
                return Degree;
            }
        }
    }
}