using System;
using System.Collections.Generic;
using System.Linq;

public class FairRandomness
{
    private static readonly string[] elements = { "A", "B", "C" };
    private const int seed = 42;

    public static void Main(string[] args)
    {
        int numSamples;
        if (args.Length < 1 || !int.TryParse(args[0], out numSamples))
        {
            numSamples = 100;
        }

        var samples = new List<string>(numSamples);

        var random = new Random();

        var bag1 = new List<string>(elements);
        var bag2 = new List<string>(elements);
        var bagLength1 = bag1.Count;
        var bagLength2 = bag2.Count;

        for (int run = 0; run < numSamples; run++)
        {
            var b = random.Next(2);
            var bag = (b == 0) ? bag1 : bag2;
            var length = (b == 0) ? bagLength1 : bagLength2;

            var i = random.Next(length);
            var last = length - 1;
            var value = bag[i];
            bag[i] = bag[last];
            bag[last] = value;

            if (b == 0)
            {
                bagLength1--;
                if (bagLength1 <= 0) bagLength1 = bag1.Count;
            }
            else
            {
                bagLength2--;
                if (bagLength2 <= 0) bagLength2 = bag2.Count;
            }

            samples.Add(value);
        }

        var numA = samples.Count(p => p == "A");
        var numB = samples.Count(p => p == "B");
        var numC = samples.Count(p => p == "C");
        var pA = (float)numA / numSamples;
        var pB = (float)numB / numSamples;
        var pC = (float)numC / numSamples;

        Console.WriteLine("A, B, C: {0:P}, {1:P}, {2:P}", pA, pB, pC);
    }
}
