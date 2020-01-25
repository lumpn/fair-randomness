//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public static class BagExtensions
{
    public static T Take<T>(this Bag<T> bag, IRandom random)
    {
        return bag.Take(random.Next(bag.Count));
    }

    public static void Take<T>(this Bag<T> bag, int num, IRandom random)
    {
        for (int i = 0; i < num; i++)
        {
            bag.Take(random);
        }
    }

    public static T Sample<T>(this Bag<T> bag, IRandom random)
    {
        if (bag.Count < 1) bag.Reset;
        return bag.Take(random);
    }
}
