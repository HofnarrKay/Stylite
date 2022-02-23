using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorView : MonoBehaviour
{
    string key;
    Position position;

    public string Key => key;
    public Position Position => position;

    public void Setup(string key, Position position)
    {
        this.key = key;
        this.position = position;
    }

    public void Destroy()
    {
        Object.Destroy(gameObject);
    }
}

