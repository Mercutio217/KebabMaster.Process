﻿namespace KebabMaster.Process.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; }
    
    private Role() { }

    public Role(string name)
    {
        Name = name;
    }
}