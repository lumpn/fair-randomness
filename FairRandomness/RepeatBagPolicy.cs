//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class RepeatBagPolicy : IPolicy
{
    private readonly int numElements;
    private readonly Bag<int> bag;

    public string Name { get { return string.Format("({0}x{1})-bag", bag.Capacity / numElements, numElements); } }

    public RepeatBagPolicy(int numRepeats, int[] elements)
    {
        numElements = elements.Length;
        var repeatedElements = new int[numElements * numRepeats];
        for (int i = 0; i < numRepeats; i++)
        {
            Array.Copy(elements, 0, repeatedElements, i * numElements, numElements);
        }

        bag = new Bag<int>(repeatedElements);
    }

    public int Sample(IRandom random)
    {
        return bag.Sample(random);
    }
}
