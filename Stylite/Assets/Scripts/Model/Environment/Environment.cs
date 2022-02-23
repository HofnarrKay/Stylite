using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentData
{
    public List<MissionOptionData> Missions;
    public MissionData CurrentMission;
    public FogData Fog;


    public EnvironmentData(Dictionary<string, MissionOption> missions, Mission currentMission, Fog fog)
    {
        Missions = new List<MissionOptionData>();
        foreach (MissionOption mission in missions.Values.ToList())
        {
            Missions.Add(mission.getData());
        }

        if(currentMission != null)
        {
            CurrentMission = currentMission.getData();
        }

        Fog = fog.GetData();
    }
}

public class Environment : GameComponent
{
    public Action<Resources> GatheredResources;

    private Dictionary<string, MissionOption> missions;

    private Mission currentMission;

    private Fog fog;

    public override void Setup()
    {
        missions = new Dictionary<string, MissionOption>();

        fog = new Fog(0, 0.0005f);
        Bind(fog);
        fog.Setup();
    }

    public override void Process(float deltaTime)
    {
        fog.Process(deltaTime);
    }

    public void AddMission(MissionOption missionOption)
    {
        missions.Add(missionOption.Key ,missionOption);
    }

    public void EmbarkOnMission(string key, ReconnaissanceParty reconnaissanceParty)
    {

        if(missions.ContainsKey(key))
        {
            currentMission = missions[key].ConstructMission(reconnaissanceParty);
            currentMission.GatheredResources += OnGatheredResources;
        }
        else
        {
            Debug.LogError("Key in Environment|EmbarkOnMission doesn't fit to any MissionOption");
        }
    }

    public void OnGatheredResources(Resources resources)
    {
        GatheredResources?.Invoke(resources);
    }

    public EnvironmentData getData() => new EnvironmentData(missions, currentMission, fog);
}
