using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData
{
    public string Key;
    public ReconnaissancePartyData ReconaissanceParty;

    public MissionData(string key, ReconnaissanceParty reconnaissanceParty)
    {
        Key = key;
        ReconaissanceParty = reconnaissanceParty.GetData();
    }
}

public class Mission : GameComponent
{
    public Action<Resources> GatheredResources;

    private string key;

    ReconnaissanceParty reconnaissanceParty;

    public Mission(string key, ReconnaissanceParty reconnaissanceParty)
    {
        this.key = key;
        this.reconnaissanceParty = reconnaissanceParty;
    }

    public void EmbarkOnMission(ReconnaissanceParty reconnaissanceParty)
    {
        this.reconnaissanceParty = reconnaissanceParty;
    }

    public MissionData getData() => new MissionData(key, reconnaissanceParty);

}
