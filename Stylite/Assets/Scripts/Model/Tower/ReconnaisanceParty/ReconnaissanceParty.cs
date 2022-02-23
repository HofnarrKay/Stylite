using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconnaissancePartyData
{
    public int MaxSize;
    public List<UnitData> Units;

    public ReconnaissancePartyData(int maxSize, List<Unit> units)
    {
        MaxSize = maxSize;

        Units = new List<UnitData>();
        foreach (var unit in units)
        {
            Units.Add(unit.GetData());
        }
    }
}

public class ReconnaissanceParty : GameComponent
{
    public int maxSize;

    private List<Unit> units;

    public List<Unit> Units => units;

    public ReconnaissanceParty(int maxSize)
    {
        this.maxSize = maxSize;
    }

    public override void Setup()
    {
        units = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        //worker.LeftJob += OnWorkerLeftJob;
        units.Add(unit);
    }

    public void OnWorkerLeftJob(Unit unit)
    {
        //worker.LeftJob -= OnWorkerLeftJob;
        units.Remove(unit);
    }

    public void RemoveWorker(Unit unit)
    {
        units.Remove(unit);
    }

    public ReconnaissancePartyData GetData() => new ReconnaissancePartyData(maxSize, units);
}
