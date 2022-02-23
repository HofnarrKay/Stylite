using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInterfaceObserver : MonoBehaviour
{
    [SerializeField]
    private BuildingInterface buildingInterface;

    [SerializeField]
    private GameObject interfaceSocket;

    [SerializeField]
    private bool visibilityWhileBuilding = false;

    [SerializeField]
    private bool visibilityWhileNotBuilding = true;


    private void Start()
    {
        if(buildingInterface)
        {
            buildingInterface.StartedBuilding += OnStartedBuilding;
            buildingInterface.StoppedBuilding += OnStoppedBuilding;
        }
    }

    public void OnStartedBuilding()
    {
        interfaceSocket.SetActive(visibilityWhileBuilding);
    }

    public void OnStoppedBuilding()
    {
        interfaceSocket.SetActive(visibilityWhileNotBuilding);
    }
}
