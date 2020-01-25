//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public sealed class Bag<T>
{
    private T[] elements;
    private int length;

    public int Capacity { get { return elements.Length; } }
    public int Count { get { return length; } }

    public Bag(T[] initialElements)
    {
        int len = initialElements.Length;
        elements = new T[len];
        Array.Copy(initialElements, elements, len);
        length = len;
    }

    public T TakeAt(int index)
    {
        int last = length - 1;
        var value = elements[index];
        elements[index] = elements[last];
        elements[last] = value;
        length = last;
        return value;
    }

    public void Reset()
    {
        length = elements.Length;
    }
}
