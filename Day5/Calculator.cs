using System.Collections;

namespace Day5;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");
        var lines = File.ReadAllLines(path);


        var seeds = new List<long>();
        var rangeMeasures = new List<RangeMeasure>();
        RangeType type = RangeType.SEED_TO_SOIL;
        foreach (var line in lines)
        {
            if (line.StartsWith("seeds:"))
            {
                seeds = GetNumbersFromLine(line.Replace("seeds: ", ""));
            }
            else if (line.StartsWith("seed-to-soil map:"))
            {
                type = RangeType.SEED_TO_SOIL;
            }
            else if (line.StartsWith("soil-to-fertilizer map:"))
            {
                type = RangeType.SOIL_TO_FERTILIZER;
            }
            else if (line.StartsWith("fertilizer-to-water map:"))
            {
                type = RangeType.FERTILIZER_TO_WATER;
            }
            else if (line.StartsWith("water-to-light map:"))
            {
                type = RangeType.WATER_TO_LIGHT;
            }
            else if (line.StartsWith("light-to-temperature map:"))
            {
                type = RangeType.LIGHT_TO_TEMPARETURE;
            }
            else if (line.StartsWith("temperature-to-humidity map:"))
            {
                type = RangeType.TEMPARETURE_TO_HUMIDITY;
            }
            else if (line.StartsWith("humidity-to-location map:"))
            {
                type = RangeType.HUMIDITY_TO_LOCATION;
            }
            else if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                var numbers = GetNumbersFromLine(line).ToArray();
                var range = new RangeMeasure()
                {
                    Type = type,
                    SourceRangeStart = numbers[1],
                    RangeLength = numbers[2],
                    DestinationRangeStart = numbers[0]
                };
                rangeMeasures.Add(range);
            }
        }


        Console.WriteLine(GetMinimumDestination(seeds, rangeMeasures));


        var secondSeeds = GetSeedsFromSeeds(seeds);
        
        Console.WriteLine(GetMinimumDestination(secondSeeds, rangeMeasures));

    }

    private static List<long> GetSeedsFromSeeds(List<long> seeds)
    {
        List<long> list = new List<long>();
        
        for (int i = 0; i < seeds.Count; i++)
        {
            if (i % 2 == 1)
            {
                for (long j = 0; i < seeds[i]; j++)
                {
                    list.Add(seeds[i-1] +j);
                }
            }
        }

        return list;
    }


    private static List<long> GetSoilListWithRange(List<RangeMeasure> rangeMeasures, long seedFirst, long seedSecond)
    {
        var soils = new List<long>();

        var measures = rangeMeasures.Where(
            m => m.Type.Equals(RangeType.SEED_TO_SOIL));

    

        measures = measures.Where(
            m => 
                (seedFirst >= m.DestinationRangeStart && seedFirst < m.DestinationRangeStart+ m.RangeLength)
            || (seedFirst + seedSecond >= m.DestinationRangeStart && seedFirst +seedSecond < m.DestinationRangeStart+ m.RangeLength));

        foreach (var measure in measures)
        {
            soils.Add(measure.DestinationRangeStart);
        }

        return soils;
    }


    private static long GetMinimumDestination(List<long> seeds, List<RangeMeasure> rangeMeasures)
    {
        var minDestination = long.MaxValue;
        foreach (var seed in seeds)
        {
            var soil = GetNextValue(rangeMeasures, seed, RangeType.SEED_TO_SOIL);
            var fertilizier = GetNextValue(rangeMeasures, soil, RangeType.SOIL_TO_FERTILIZER);
            var water = GetNextValue(rangeMeasures, fertilizier, RangeType.FERTILIZER_TO_WATER);
            var light = GetNextValue(rangeMeasures, water, RangeType.WATER_TO_LIGHT);
            var tempareture = GetNextValue(rangeMeasures, light, RangeType.LIGHT_TO_TEMPARETURE);
            var humidity = GetNextValue(rangeMeasures, tempareture, RangeType.TEMPARETURE_TO_HUMIDITY);
            var destination = GetNextValue(rangeMeasures, humidity, RangeType.HUMIDITY_TO_LOCATION);

            if (destination < minDestination)
            {
                minDestination = destination;
            }
        }


        return minDestination;
    }


    private static long GetMinimumDestinationForSoils(List<long> soils, List<RangeMeasure> rangeMeasures)
    {
        var minDestination = long.MaxValue;
        foreach (var soil in soils)
        {
            var fertilizier = GetNextValue(rangeMeasures, soil, RangeType.SOIL_TO_FERTILIZER);
            var water = GetNextValue(rangeMeasures, fertilizier, RangeType.FERTILIZER_TO_WATER);
            var light = GetNextValue(rangeMeasures, water, RangeType.WATER_TO_LIGHT);
            var tempareture = GetNextValue(rangeMeasures, light, RangeType.LIGHT_TO_TEMPARETURE);
            var humidity = GetNextValue(rangeMeasures, tempareture, RangeType.TEMPARETURE_TO_HUMIDITY);
            var destination = GetNextValue(rangeMeasures, humidity, RangeType.HUMIDITY_TO_LOCATION);

            if (destination < minDestination)
            {
                minDestination = destination;
            }
        }


        return minDestination;
    }

    private static long GetNextValue(List<RangeMeasure> measures, long seed, RangeType type)
    {
        var destination = measures
            .FirstOrDefault(m => m.Type.Equals(type) && m.GetDestinationValueFromSourceValue(seed) > -1);
        if (destination != null)
        {
            return destination.GetDestinationValueFromSourceValue(seed);
        }
        else
        {
            return seed;
        }
    }

    private static List<long> GetNumbersFromLine(string line)
    {
        var numbers = line.Split(' ');
        return numbers.Select(long.Parse).ToList();
    }
}