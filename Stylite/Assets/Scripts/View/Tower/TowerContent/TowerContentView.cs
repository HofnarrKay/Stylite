using System;
using System.Collections.Generic;
using UnityEngine;



public class TowerContentView : MonoBehaviour
{
    public Action<FloorView> CreatedFloor;
    public Action<FloorView> DestroyedFloor;

    [SerializeField]
    private BuildSpotView buildSpotView;

    [SerializeField]
    private List<TowerFloorTemplateView> towerFloorTemplates;

    [SerializeField]
    private Transform floorSocket;

    [SerializeField]
    private float heightPerFloor = 1;

    private Dictionary<string, TowerFloorTemplateView> sortedFloorTemplates = new Dictionary<string, TowerFloorTemplateView>();
    private Dictionary<Position, FloorView> floors = new Dictionary<Position, FloorView>();

    private void Start()
    {
        foreach (var towerFloorTemplate in towerFloorTemplates)
        {
            AddTemplate(towerFloorTemplate);
        }
    }

    private void AddTemplate(TowerFloorTemplateView templateView)
    {
        if (!sortedFloorTemplates.ContainsKey(templateView.Key)) sortedFloorTemplates.Add(templateView.Key, templateView);
    }

    public void Visualize(TowerData tower)
    {

        foreach (KeyValuePair<Position, TowerFloorData> towerFloor in tower.Floors)
        {
            if (floors.ContainsKey(towerFloor.Key))
            {
                if(floors[towerFloor.Key].Key == towerFloor.Value.Key) continue;

                DestroyedFloor?.Invoke(floors[towerFloor.Key]);
                floors[towerFloor.Key].Destroy();
                floors.Remove(towerFloor.Key);
            }

            if(sortedFloorTemplates.ContainsKey(towerFloor.Value.Key))
            {
                GameObject createdObject = Instantiate(sortedFloorTemplates[towerFloor.Value.Key].Prefab, floorSocket);

                FloorView towerView = createdObject.GetComponent<FloorView>();
                if (!towerView)
                {
                    Destroy(createdObject);
                    continue;
                }
                CreatedFloor?.Invoke(towerView);

                towerView.Setup(towerFloor.Value.Key, towerFloor.Key);
                floors.Add(towerFloor.Key, towerView);

                createdObject.transform.position = TransformPosition(towerFloor.Key);
            }
            else
            {
                Debug.LogError("TowerFloor Key: " + towerFloor.Value.Key + " was not present in TowerContentView.cs/Visualize -> sortedFloorTemplates");
            }
        }

        buildSpotView.Visualize(tower, heightPerFloor);
    }

    Vector3 TransformPosition(Position position)
    {
        Vector3 retVal = new Vector3(transform.position.x, (position.Height * heightPerFloor) + (heightPerFloor / 2f) + transform.position.y, transform.position.z);

        return retVal;

    }
}
