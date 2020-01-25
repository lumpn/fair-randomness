//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public sealed class SequencePolicy : IPolicy
{
    private readonly Bag<int>[] bags;

    public SequencePolicy(int numBags, int[] elements)
    {
        this.lastBag = numBags - 1;
        var len = elements.Length;
        bags = new Bag<int>[numBags];
        for (int i = 0; i < numBags; i++)
        {
            var bag = new Bag<int>(elements);
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
                return bag.Take(index);
            }
            index -= count;
        }

        throw;
    }
}
