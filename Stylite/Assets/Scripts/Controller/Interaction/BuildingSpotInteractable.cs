using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpotInteractable : Interactable
{
    private Position position;

    public void Setup(Position position)
    {
        this.position = position;
    }

    public void Shutdown()
    {

    }

    public override void BuildSpotInteraction(BuildSpotView buildSpotView)
    {
        buildSpotView.chooseBuildSpot(position);
    }
}
