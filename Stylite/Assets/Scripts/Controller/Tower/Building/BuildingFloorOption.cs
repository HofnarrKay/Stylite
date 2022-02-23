using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingFloorOption : MonoBehaviour
{
    public Action<BuildingActionData> BuildingCommand;

    [SerializeField]
    private Text text;

    private BuildingActionData buildingActionData;

    public void Setup(BuildingActionData buildingActionData)
    {
        this.buildingActionData = buildingActionData;
        text.text = buildingActionData.TowerFloor.Key;
    }

    public void Build()
    {
        BuildingCommand?.Invoke(buildingActionData);
    }
}
