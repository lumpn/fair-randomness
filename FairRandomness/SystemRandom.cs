//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public class SystemRandom : IRandom
{
    private readonly Random random;

    public SystemRandom(int seed)
    {
        random = new Random(seed);
    }

    public int Next(int maxValue)
    {
        return random.Next(maxValue);
    }
}
