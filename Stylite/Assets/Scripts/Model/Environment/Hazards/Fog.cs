using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct FogData
{
    public float Speed;
    public float Height;

    public FogData(float speed, float height)
    {
        Speed = speed;
        Height = height;
    }
}

public class Fog : GameComponent
{
    float speed = 0.3f;
    float height = 1;

    public Fog(float startingHeight, float speed)
    {
        this.height = startingHeight;
        this.speed = speed;
    }

    public override void Process(float deltaTime)
    {
        height += deltaTime * speed;
    }

    public FogData GetData() => new FogData(speed, height);
}
