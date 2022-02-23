using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogView : MonoBehaviour
{
    [SerializeField]
    private Transform modelTransform;

    [SerializeField]
    float districtSize = 90;

    private Vector3 startingPosition;

    private void Start()
    {
        if(modelTransform)
        {
            startingPosition = modelTransform.position;
        }
    }

    public void Visualize(FogData fog)
    {
        if(modelTransform)
        {
            modelTransform.localScale = new Vector3(modelTransform.localScale.x, fog.Height * districtSize, modelTransform.localScale.z);
            modelTransform.position = new Vector3(startingPosition.x,startingPosition.y + (modelTransform.localScale.y / 2) , startingPosition.z);
        }
    }
}
