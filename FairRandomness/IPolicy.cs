//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------

public interface IPolicy
{
    string Name { get; }
    int Sample(IRandom random);
}
