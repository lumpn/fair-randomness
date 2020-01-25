//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public sealed class SingleBagPolicy : IPolicy
{
    private readonly Bag<int> bag;

    public string Name { get { return string.Format("{0}-bag", bag.Capacity); } }

    public SingleBagPolicy(int[] elements)
    {
        bag = new Bag<int>(elements);
    }

    public int Sample(IRandom random)
    {
        return bag.Sample(random);
    }
}
