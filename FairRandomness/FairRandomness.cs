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

        var initRandom = new SystemRandom(seed);
        var policies = new IPolicy[]{
            new UniformPolicy(elements),
            new SingleBagPolicy(elements),
            new RepeatBagPolicy(1, elements),
            new RepeatBagPolicy(2, elements),
            new RepeatBagPolicy(3, elements),
            new RepeatBagPolicy(4, elements),
            new RepeatBagPolicy(5, elements),
            new RepeatBagPolicy(6, elements),
            new SequencePolicy(1, elements),
            new SequencePolicy(2, elements),
            new SequencePolicy(3, elements),
            new SequencePolicy(4, elements),
            new SequencePolicy(5, elements),
            new SequencePolicy(6, elements),
            new MultiBagPolicy(1, elements, initRandom),
            new MultiBagPolicy(2, elements, initRandom),
            new MultiBagPolicy(3, elements, initRandom),
            new MultiBagPolicy(4, elements, initRandom),
            new MultiBagPolicy(5, elements, initRandom),
            new MultiBagPolicy(6, elements, initRandom),
            new RoundRobinPolicy(1, elements,initRandom),
            new RoundRobinPolicy(2, elements,initRandom),
            new RoundRobinPolicy(3, elements,initRandom),
            new RoundRobinPolicy(4, elements,initRandom),
            new RoundRobinPolicy(5, elements,initRandom),
            new RoundRobinPolicy(6, elements,initRandom),
        };

        int numPolicies = policies.Length;
        int numCols = numPolicies * 2;
        int numRows = 100;
        int floodCol = 0;
        var csv = new int[numCols, numRows];
        foreach (var policy in policies)
        {
            var random = new SystemRandom(seed);
            RunExperiment(numSamples, policy, random, csv, floodCol, floodCol + numPolicies);
            floodCol++;
        }

        using (var file = System.IO.File.CreateText("distribution.csv"))
        {
            file.Write("Length; ");
            foreach (var policy in policies) file.Write("{0} flood; ", policy.Name);
            foreach (var policy in policies) file.Write("{0} drought; ", policy.Name);
            file.WriteLine();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    file.Write(csv[col, row]);
                    file.Write("; ");
                }
                file.WriteLine();
            }
        }
    }

    private static void RunExperiment(int numSamples, IPolicy policy, IRandom random, int[,] csv, int floodCol, int droughtCol)
    {
        var samples = new List<int>(numSamples);
        for (int run = 0; run < numSamples; run++)
        {
            var value = policy.Sample(random);
            samples.Add(value);
        }

        // count hits
        var numHits = samples.Count(p => p == 1);
        var probability = (float)numHits / numSamples;

        Console.WriteLine("Policy; Samples; Hits; Probability;");
        Console.WriteLine("{0}; {1}; {2}; {3};", policy.Name, numSamples, numHits, probability);
        Console.WriteLine();

        // drought stats
        var distances = new Dictionary<int, int>();
        int distance = 0;
        foreach (var value in samples)
        {
            distance++;
            if (value == 1)
            {
                Increment(distances, distance);
                distance = 0;
            }
        }

        Console.WriteLine("Distance; Occurence;");
        int maxDistance = distances.Keys.Max();
        for (int i = 0; i < maxDistance; i++)
        {
            var value = GetOrFallback(distances, i, 0);
            Console.WriteLine("{0}; {1};", i, value);
            csv[droughtCol, i] = value;
        }
        Console.WriteLine();

        // flood stats
        var streaks = new Dictionary<int, int>();
        int previousValue = -1;
        int streakLength = 0;
        foreach (var value in samples)
        {
            streakLength++;
            if (value != previousValue)
            {
                Increment(streaks, streakLength);
                previousValue = value;
                streakLength = 0;
            }
        }

        Console.WriteLine("Streak; Occurence;");
        int maxStreak = streaks.Keys.Max();
        for (int i = 0; i < maxStreak; i++)
        {
            var value = GetOrFallback(streaks, i, 0);
            Console.WriteLine("{0}; {1};", i, value);
            csv[floodCol, i] = value;
        }
        Console.WriteLine();
    }

    private static int GetOrFallback(IDictionary<int, int> dict, int key, int fallbackValue)
    {
        int value;
        return dict.TryGetValue(key, out value) ? value : fallbackValue;
    }

    private static void Increment(IDictionary<int, int> dict, int key)
    {
        var value = GetOrFallback(dict, key, 0);
        dict[key] = value + 1;
    }
}
