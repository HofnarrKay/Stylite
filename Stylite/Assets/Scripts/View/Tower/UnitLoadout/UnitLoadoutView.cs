using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoadoutView : SocketSpawnerSpawnerView
{
    [SerializeField]
    Transform socket;

    [SerializeField]
    GameObject unitTemplatePrefab;

    Dictionary<string, UnitTemplateView> instantiatedUnitViews = new Dictionary<string, UnitTemplateView>();

    public void Visualize(UnitLoadoutData unitLoadout)
    {
        foreach (var unit in unitLoadout.Units)
        {
            if(!instantiatedUnitViews.ContainsKey(unit.Key))
            {
                GameObject instantiatedObjects = Instantiate(unitTemplatePrefab, socket);

                UnitTemplateView unitTemplateView = instantiatedObjects.GetComponent<UnitTemplateView>();
                if (unitTemplateView)
                {
                    instantiatedUnitViews.Add(unit.Key, unitTemplateView);

                    CreatedSocketSpawner?.Invoke(unitTemplateView);
                }
                else
                {
                    Destroy(instantiatedObjects);
                }
            }

            instantiatedUnitViews[unit.Key].Visualize(unit);
        }
    }
}
