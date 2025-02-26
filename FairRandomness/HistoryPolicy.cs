//----------------------------------------
// MIT License
// Copyright(c) 2024 Jonas Boetel
//----------------------------------------

using System.Collections.Generic;

public sealed class HistoryPolicy : IPolicy
{
    private readonly int numElements;
    private readonly int[] elements;
    private readonly int numRetries;

    private readonly Queue<int> history = new Queue<int>();

    public string Name { get { return string.Format("({0}, {1}, {2}) history", numElements, numRetries, history.Count); } }

    public HistoryPolicy(int[] elements, int numRetries, int historyLength)
    {
        this.numElements = elements.Length;
        this.elements = elements;
        this.numRetries = numRetries;
        for (int i = 0; i < historyLength; i++)
        {
            history.Enqueue(1);
        }
    }

    public int Sample(IRandom random)
    {
        int element = 0;
        for (int i = 0; i <= numRetries; i++)
        {
            element = elements[random.Next(numElements)];
            if (!history.Contains(element)) break;
        }

        history.Dequeue();
        history.Enqueue(element);
        return element;
    }
}
