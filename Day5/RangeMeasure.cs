namespace Day5;

public class RangeMeasure
{
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }

    public RangeType Type { get; set; }


    public long GetDestinationValueFromSourceValue(long source)
    {
        if (source < SourceRangeStart)
            return -1;
        else if (source >= SourceRangeStart + RangeLength)
        {
            return -1;
        }
        else
        {
            return DestinationRangeStart + (source - SourceRangeStart);
        }
    }
    
    

    public override string ToString()
    {
        return Type.ToString() + " " + DestinationRangeStart + " " + SourceRangeStart + " " + RangeLength;
    }
}

public enum RangeType
{
    SEED_TO_SOIL,
    SOIL_TO_FERTILIZER,
    FERTILIZER_TO_WATER,
    WATER_TO_LIGHT,
    LIGHT_TO_TEMPARETURE,
    TEMPARETURE_TO_HUMIDITY,
    HUMIDITY_TO_LOCATION
}