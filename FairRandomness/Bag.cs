//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;

public class Bag<T>
{
    private T[] elements;
    private int length;

    public int Count { get { return lenght; } }

    public Bag(T[] initialElements)
    {
        int len = initialElements.Length;
        elements = new T[len];
        Array.Copy(initialElements, elements, len);
        lenght = len;
    }

    public T Take(int index)
    {
        int last = length - 1;
        var value = elements[i];
        elements[i] = elements[last];
        elements[last] = value;
        length = last;
        return value;
    }

    public void Reset()
    {
        length = elements.Length;
    }
}
