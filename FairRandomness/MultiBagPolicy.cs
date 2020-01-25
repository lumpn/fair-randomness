using System;

public class MultiBagRandomizer<T>
{
    private readonly Bag<T>[] bags;
    private readonly int length;

    public int BagCount { get { return bags.Length; } }
    public int Count { get { return length; } }

    public MultiBagRandomizer(int numBags, T[] elements)
    {
        bags = new Bag<T>[numBags];
        for(int i = 0; i < numBags; i++)
        {
            bags[i] = new Bag<T>(elements);
        }
    }
}
