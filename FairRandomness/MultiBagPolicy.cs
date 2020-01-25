﻿//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class MultiBagPolicy : IPolicy
{
    private readonly int numBags;
    private readonly Bag<int>[] bags;

    public MultiBagPolicy(int numBags, int[] elements)
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
        var i = random.Next(numBags);
        var bag = bags[i];

        if (bag.Count < 1) bag.Reset();
        return bag.Take(random.Next(bag.Count));
    }
}