using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInterface : MonoBehaviour
{
    public Action StartedBuilding;
    public Action StoppedBuilding;

    [SerializeField]
    private BuildSpotView buildSpotView;

    [SerializeField]
    private Transform buildingButtonSockets;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private string actionkey = "BuildingAction";

    private bool currentlyBuilding = false;

    private bool chosePlaceToBuild = false;

    private Position chosenPlace;

    private List<GameObject> buttons = new List<GameObject>();

    private void Start()
    {
        buildSpotView.ChoseBuildSpot += OnChosenPosition;
    }

    private void OnDestroy()
    {
        buildSpotView.ChoseBuildSpot -= OnChosenPosition;
    }

    public void updateOptions(ActionsData actions)
    {
        if(currentlyBuilding && chosePlaceToBuild)
        {
            createButtons(actions.GetActions(actionkey));
        }
        else
        {
            foreach (var button in buttons.ToArray())
            {
                Destroy(button);
            }
            buttons.Clear();
        }
    }

    public void createButtons(List<ModelActionData> buildingActions)
    {

        for(int i = buttons.Count;  i < buildingActions.Count; i++)
        {
            GameObject createdButton = Instantiate(buttonPrefab, buildingButtonSockets);
            BuildingFloorOption buildingFloorOption = createdButton.GetComponent<BuildingFloorOption>();
            buildingFloorOption.Setup((BuildingActionData)buildingActions[i]);
            buildingFloorOption.BuildingCommand += Build;
            buttons.Add(createdButton);
        }
    }

    public void Build(BuildingActionData buildingFloorOption)
    {
        Model.Instance.CallAction(new BuildingActionArguments(actionkey, buildingFloorOption.Costs, buildingFloorOption.TowerFloor, chosenPlace));
        StopBuilding();
        StartBuilding();
    }

    public void StartBuilding()
    {
        currentlyBuilding = true;
        buildSpotView.ShowBuildSpots = true;
        StartedBuilding?.Invoke();
    }

    public void StopBuilding()
    {
        currentlyBuilding = false;
        chosePlaceToBuild = false;
        buildSpotView.ShowBuildSpots = false;
        StoppedBuilding?.Invoke();
    }

    public void OnChosenPosition(Position position)
    {
        chosePlaceToBuild = true;
        buildSpotView.ShowBuildSpots = false;
        chosenPlace = position;
    }
}
