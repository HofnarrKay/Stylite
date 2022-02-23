using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionOptionController : MonoBehaviour
{
    [SerializeField]
    private MissionOptionsView missions;

    private void Start()
    {
        missions.ChoseMission += OnChoseMission;
    }

    private void OnDestroy()
    {
        missions.ChoseMission -= OnChoseMission;
    }

    public void OnChoseMission(MissionOptionData missionOptionData)
    {
        Model.Instance.EmbarkOnMission(missionOptionData.Key);
    }
}
