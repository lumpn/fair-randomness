//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

public class FairRandomness
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: fair <numElements> <numSamples> [seed]");
            Environment.ExitCode = 1;
            return;
        }

        int numElements;
        if (!int.TryParse(args[0], out numElements))
        {
            Console.Error.WriteLine("Could not parse <numElements>");
            Environment.ExitCode = 2;
            return;
        }

        int numSamples;
        if (!int.TryParse(args[1], out numSamples))
        {
            Console.Error.WriteLine("Could not parse <numSamples>");
            Environment.ExitCode = 3;
            return;
        }

        var now = DateTime.Now;
        int seed = now.Hour * 3600 + now.Minute * 60 + now.Second;
        if (args.Length > 2 && !int.TryParse(args[2], out seed))
        {
            Console.Error.WriteLine("Could not parse [seed]");
            Environment.ExitCode = 4;
            return;
        }

        var elements = new int[numElements];
        for (int i = 0; i < numElements; i++)
        {
            elements[i] = i + 1;
        }

        var random = new SystemRandom(seed);
        var policy = new SingleBagPolicy(elements);

        var samples = new List<int>(numSamples);
        for (int run = 0; run < numSamples; run++)
        {
            var value = policy.Sample(random);
            samples.Add(value);
        }

        var numA = samples.Count(p => p == 1);
        var numB = samples.Count(p => p == 2);
        var numC = samples.Count(p => p == 3);
        var pA = (float)numA / numSamples;
        var pB = (float)numB / numSamples;
        var pC = (float)numC / numSamples;

        Console.WriteLine("A, B, C: {0:P}, {1:P}, {2:P}", pA, pB, pC);
    }
}
