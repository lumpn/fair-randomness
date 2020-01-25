//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
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

        var random = new SystemRandom(seed);
        var policy = new Bag<string>(elements);

        var samples = new List<string>(numSamples);
        for (int run = 0; run < numSamples; run++)
        {
            var value = policy.Sample(random);
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
