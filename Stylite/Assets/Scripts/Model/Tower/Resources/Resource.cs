using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourceData
{
    public string key;
    public float amount;

    public ResourceData(string key, float amount)
    {
        this.key = key;
        this.amount = amount;
    }
}

public class Resource : GameComponent
{
    private string key;
    private float amount;

    public string Key => key;
    public float Amount => amount;

    public Resource(string key, float amount)
    {
        this.key = key;
        this.amount = amount;
    }

    public void Add(float amount) => this.amount += amount;

    public Resource Duplicate() => new Resource(key, amount);

    public ResourceData GetData() => new ResourceData(key, amount);
}
