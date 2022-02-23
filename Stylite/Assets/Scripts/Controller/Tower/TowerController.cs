using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    BuildingInterface buildingInterface;

    [SerializeField]
    UnitLoadoutController unitLoadout;

    [SerializeField]
    ModelController unityGame;

    void Update()
    {
        ModelData gameData = Model.Instance.GetData();
        buildingInterface.updateOptions(gameData.Actions);
        unitLoadout.SetActions(gameData.Actions.Actions);
        //Resources resourceProduction = new Resources();
        //resourceProduction.Add(new Resource("Stone", 100));
        //resourceProduction.Add(new Resource("Arcane", 10));
        //Game.Instance.addTowerFloor(new Position(gameData.tower.floors.Count), new ResourceProductionFloor("MineralProductionFloor", resourceProduction));
    }

    public void changeGameSpeed(float newSpeed) => unityGame.GameSpeed = newSpeed;
}
