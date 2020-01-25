//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public sealed class UniformPolicy : IPolicy
{
    private readonly int numElements;
    private readonly int[] elements;

    public string Name { get { return string.Format("{0}-uniform", numElements); } }

    public UniformPolicy(int[] elements)
    {
        this.numElements = elements.Length;
        this.elements = elements;
    }

    public int Sample(IRandom random)
    {
        return elements[random.Next(numElements)];
    }
}
