using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField]
    private FogView fog;

    [SerializeField]
    private ResourceView resourceView;

    [SerializeField]
    private TowerContentView towerView;

    [SerializeField]
    private FloorContentView floorContentView;

    [SerializeField]
    private MissionOptionsView missions;

    [SerializeField]
    private UnitLoadoutView loadout;

    [SerializeField]
    private SpellContainerView spellContainerView;

    void Update()
    {
        ModelData gameData = Model.Instance.GetData();
        Visualize(gameData);
    }


    void Visualize(ModelData gameData)
    {
        fog?.Visualize(gameData.Environment.Fog);
        resourceView?.Visualize(gameData.Tower.Resources);
        towerView?.Visualize(gameData.Tower);
        floorContentView?.Visualize(gameData.Tower);
        missions?.Visualize(gameData.Environment.Missions);
        loadout?.Visualize(gameData.Tower.unitLoadoutData);
        spellContainerView?.Visualize(gameData.Tower.SpellContainer);
    }
}
