using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Action<Interactable> Interaction;

    public void interact()
    {
        Interaction?.Invoke(this);
    }

    virtual public void BuildSpotInteraction(BuildSpotView buildSpotView)
    {

    }

    virtual public void ViewFloorContentInteraction(FloorContentView floorContentView)
    {

    }
}
