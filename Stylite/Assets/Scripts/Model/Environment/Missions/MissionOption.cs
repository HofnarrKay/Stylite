using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionOptionData
{
    public string Key;

    public MissionOptionData(string key)
    {
        Key = key;
    }

}

public class MissionOption
{
    private string key;

    public string Key
    {
        get => key;
    }
    public MissionOption(string key)
    {
        this.key = key;
    }

    public Mission ConstructMission(ReconnaissanceParty reconnaissanceParty) => new Mission(key, reconnaissanceParty);

    public MissionOptionData getData() => new MissionOptionData(key);
}
