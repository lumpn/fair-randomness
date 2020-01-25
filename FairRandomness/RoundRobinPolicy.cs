//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public sealed class RoundRobinPolicy : IPolicy
{
    private readonly int numBags;
    private readonly Bag<int>[] bags;
    private int index = 0;

    public RoundRobinPolicy(int numBags, int[] elements)
    {
        this.numBags = numBags;
        int len = elements.Length;
        bags = new Bag<int>[numBags];
        for (int i = 0; i < numBags; i++)
        {
            bags[i] = new Bag<int>(elements);
            int numRemove = len * i / numBags;
            bag.Take(numRemove);
        }
    }

    public int Sample(IRandom random)
    {
        var bag = bags[index];
        index = (index + 1) % numBags;
        return bag.Sample(random);
    }
}
