using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{

    public float GameSpeed = 1;

    void Update()
    {
        Model.Instance.Process(Time.deltaTime * GameSpeed);
    }
}
