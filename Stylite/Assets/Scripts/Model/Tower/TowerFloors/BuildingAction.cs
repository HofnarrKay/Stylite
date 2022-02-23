using UnityEngine;

public class BuildingActionData : ModelActionData
{
    public ResourcesData Costs;
    public TowerFloorData TowerFloor;

    public BuildingActionData(string key, Resources costs, TowerFloor towerFloor) : base(key)
    {
        this.Costs = costs.GetData();
        this.TowerFloor = towerFloor.GetData();
    }
}

public class BuildingAction : ModelAction
{
    Resources costs;
    TowerFloor towerFloor;
    Tower towerReference;

    public Resources Costs => costs;

    public BuildingAction(string key, Resources costs, TowerFloor towerFloor, Tower towerReference) : base(key)
    {
        this.costs = costs;
        this.towerFloor = towerFloor;
        this.towerReference = towerReference;
    }

    public override void Act(ActionArguments actionArguments, Model model)
    {
        BuildingActionArguments buildingActionArguments = (BuildingActionArguments)actionArguments;
        
        if(EqualsData(buildingActionArguments.Costs, buildingActionArguments.TowerFloor))
        {
            if(towerReference.Contains(costs) && towerReference.CheckForBuildingSpots(buildingActionArguments.Position))
            {
                towerReference.Subtract(costs);
                towerReference.AddFloor(buildingActionArguments.Position, towerFloor.Duplicate());
            }
        }
    }

    public bool EqualsData(ResourcesData costs, TowerFloorData towerFloor)
    {
        if (!this.costs.EqualsData(costs)) return false;
        if (!this.towerFloor.EqualsData(towerFloor)) return false;
        return true;
    }

    public override ModelActionData GetData() => new BuildingActionData(Key, costs, towerFloor);
}
