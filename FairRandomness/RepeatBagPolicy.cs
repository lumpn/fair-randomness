//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class RepeatBagPolicy : IPolicy
{
    private readonly Bag<int> bag;

    public RepeatBagPolicy(int numRepeats, int[] elements)
    {
        int len = elements.Length;
        var repeatedElements = new int[len * numRepeats];
        for (int i = 0; i < numRepeats; i++)
        {
            Array.Copy(elements, 0, repeatedElements, len * i, len);
        }

        bag = new Bag<int>(repeatedElements);
    }

    public int Sample(IRandom random)
    {
        if (bag.Count < 1) bag.Reset();
        return bag.Take(random.Next(bag.Count));
    }
}
