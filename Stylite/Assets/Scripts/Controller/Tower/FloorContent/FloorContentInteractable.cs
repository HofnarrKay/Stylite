using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorContentInteractable : Interactable
{
    public override void ViewFloorContentInteraction(FloorContentView floorContentView)
    {
        FloorView floorView = GetComponent<FloorView>();
        if(floorView)
        {
            floorContentView.VisualizeJobsInFloor(floorView.Position);
        }
    }
}
