//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class RoundRobinPolicy : IPolicy
{
    private readonly int numBags;
    private readonly Bag<int>[] bags;
    private int index = 0;

    public RoundRobinPolicy(int numBags, int[] elements)
    {
        this.numBags = numBags;
        bags = new Bag<int>[numBags];
        for (int i = 0; i < numBags; i++)
        {
            bags[i] = new Bag<int>(elements);
        }
    }

    public int Sample(IRandom random)
    {
        var bag = bags[index];
        index = (index + 1) % numBags;

        if (bag.Count < 1) bag.Reset();
        return bag.Take(random.Next(bag.Count));
    }
}
