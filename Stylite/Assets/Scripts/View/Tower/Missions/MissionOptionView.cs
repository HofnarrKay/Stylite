using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionOptionView : MonoBehaviour
{
    public Action<MissionOptionView, MissionOptionData> ChoseMission;

    [SerializeField]
    Text text;

    private string key;

    MissionOptionData data;

    public void Visualize(MissionOptionData missionOption)
    {
        text.text = missionOption.Key;
        key = missionOption.Key;
        data = missionOption;
    }


    public void Shutdown()
    {
        Destroy(gameObject);
    }

    public void OnChoseMission() => ChoseMission?.Invoke(this, data);
}
