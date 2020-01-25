//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public interface IRandom
{
    /// returns a random value between 0 (inclusive) and maxValue (exclusive)
    int Next(int maxValue);
}
