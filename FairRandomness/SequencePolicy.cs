﻿//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class SequencePolicy : IPolicy
{
    private readonly Bag<int>[] bags;

    public string Name { get { return string.Format("{0}-bag {1}-sequence", bags[0].Capacity, bags.Length); } }

    public SequencePolicy(int numBags, int[] elements)
    {
        var len = elements.Length;
        bags = new Bag<int>[numBags];
        for (int i = 0; i < numBags; i++)
        {
            bags[i] = new Bag<int>(elements);
        }
    }

    public int Sample(IRandom random)
    {
        var total = 0;
        foreach (var bag in bags)
        {
            total += bag.Count;
        }
        var index = random.Next(total);

        foreach (var bag in bags)
        {
            var count = bag.Count;
            if (index < count)
            {
                var value = bag.TakeAt(index);
                if (bag.Count < 1) bag.Reset();
                return value;
            }
            index -= count;
        }

        throw new InvalidOperationException(string.Format("index {0} exceeded total {1}", index, total));
    }
}
