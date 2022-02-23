using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActionArguments : ActionArguments
{
    public ResourcesData Costs;
    public TowerFloorData TowerFloor;
    public Position Position;

    public BuildingActionArguments(string key, ResourcesData costs, TowerFloorData towerFloor, Position position) : base(key)
    {
        Costs = costs;
        TowerFloor = towerFloor;
        Position = position;
    }
}
