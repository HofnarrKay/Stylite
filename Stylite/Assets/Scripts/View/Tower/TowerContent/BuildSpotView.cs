using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpotView : MonoBehaviour
{
    public Action<Position> ChoseBuildSpot;

    [SerializeField]
    private GameObject buildPreviewPrefab;

    [SerializeField]
    private Transform socket;

    private List<GameObject> instancedBuildPreviews = new List<GameObject>();

    public bool ShowBuildSpots = false;

    public void Visualize(TowerData tower, float heightPerFloor)
    {
        if(ShowBuildSpots)
        {
            UpdateBuildSpots(tower, heightPerFloor);
        }
        else
        {
            foreach (var buildPreview in instancedBuildPreviews.ToArray())
            {
                Destroy(buildPreview);
                BuildingSpotInteractable interactable = buildPreview.GetComponent<BuildingSpotInteractable>();

                if (interactable)
                {
                    interactable.Interaction -= OnInteraction;
                    interactable.Shutdown() ;
                }
            }
            instancedBuildPreviews.Clear();
        }
    }


    private void UpdateBuildSpots(TowerData tower, float heightPerFloor)
    {
        for(int i = instancedBuildPreviews.Count; i < tower.AvailableBuildingSpots.Count; i++) 
        {
            GameObject createdSpot = Instantiate(buildPreviewPrefab, socket); 
            createdSpot.transform.position = new Vector3(transform.position.x, (tower.AvailableBuildingSpots[i].Height * heightPerFloor) + (heightPerFloor / 2f) + transform.position.y, transform.position.z);
            instancedBuildPreviews.Add(createdSpot);

            BuildingSpotInteractable interactable = createdSpot.GetComponent<BuildingSpotInteractable>();
            if(interactable)
            {
                interactable.Interaction += OnInteraction;
                interactable.Setup(tower.AvailableBuildingSpots[i]);
            }
        }
    }

    public void OnInteraction(Interactable interactable)
    {
        interactable.BuildSpotInteraction(this);
    }

    public void chooseBuildSpot(Position position)
    {
        ChoseBuildSpot?.Invoke(position);
    }
}
