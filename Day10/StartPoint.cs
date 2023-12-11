namespace Day10;

public class StartPoint
{
    public int Row { get; set; }
    public int Column { get; set; }
    public List<Pipe> ConnectedPipes { get; set; } = new List<Pipe>();
}