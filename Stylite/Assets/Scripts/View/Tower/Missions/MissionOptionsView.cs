using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionOptionsView : MonoBehaviour
{
    public Action<MissionOptionData> ChoseMission;

    [SerializeField]
    private GameObject missionPrefab;

    [SerializeField]
    private Transform missionSocket;

    [SerializeField]
    private SceneSwitch sceneSwitch;

    private Dictionary<string, MissionOptionView> createdMissionViews = new Dictionary<string, MissionOptionView>();
    private MissionOptionData currentlySelectedMission;


    public void Visualize(List<MissionOptionData> missions)
    {
        foreach(MissionOptionData missionOption in missions)
        {
            if(!createdMissionViews.ContainsKey(missionOption.Key))
            {
                GameObject createdMission = Instantiate(missionPrefab, missionSocket);
                MissionOptionView createdMissionOption = createdMission.GetComponent<MissionOptionView>();
                if(createdMissionOption)
                {
                    createdMissionViews.Add(missionOption.Key, createdMissionOption);
                    createdMissionOption.ChoseMission += SelectMission;
                }
            }
        }

        foreach (var keyValuePair in createdMissionViews.ToList())
        {
            bool missionsContainsKey = false;

            foreach (MissionOptionData missionOption in missions)
            {
                if (keyValuePair.Key == missionOption.Key)
                {
                    keyValuePair.Value.Visualize(missionOption);
                    missionsContainsKey = true;
                }
            }

            if(!missionsContainsKey)
            {
                keyValuePair.Value.ChoseMission -= SelectMission;
                keyValuePair.Value.Shutdown();
                createdMissionViews.Remove(keyValuePair.Key);
            }
        }


    }

    public void Shutdown()
    {

    }

    public void OnChoseMission()
    {
        if (currentlySelectedMission == null) return;
        ChoseMission?.Invoke(currentlySelectedMission);
        sceneSwitch.ChangeScene(1);
    }


    public void SelectMission(MissionOptionView missionOptionView, MissionOptionData mission)
    {
        currentlySelectedMission = mission;
    }
}
