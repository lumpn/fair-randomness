﻿//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class MultiBagPolicy : IPolicy
{
    private readonly int numBags;
    private readonly Bag<int>[] bags;

    public string Name { get { return string.Format("{0}x {1}-bag", numBags, bags[0].Capacity); } }

    public MultiBagPolicy(int numBags, int[] elements, IRandom random)
    {
        this.numBags = numBags;
        var len = elements.Length;
        bags = new Bag<int>[numBags];
        for (int i = 0; i < numBags; i++)
        {
            var bag = new Bag<int>(elements);
            int numRemove = len * i / numBags;
            bag.Take(numRemove, random);
            bags[i] = bag;
        }
    }

    public int Sample(IRandom random)
    {
        var i = random.Next(numBags);
        var bag = bags[i];
        return bag.Sample(random);
    }
}
