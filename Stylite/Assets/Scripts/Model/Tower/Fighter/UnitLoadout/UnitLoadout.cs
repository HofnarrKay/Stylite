using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLoadoutData
{
    public List<UnitTemplateData> Units;

    public UnitLoadoutData(List<UnitTemplate> units)
    {
        Units = new List<UnitTemplateData>();

        foreach (var unit in units)
        {
            Units.Add(unit.GetData());
        }
    }
}

public class UnitLoadout : GameComponent
{
    private List<UnitTemplate> units;

    public override void Setup()
    {
        units = new List<UnitTemplate>();
    }


    public void AddUnit(UnitTemplate unit)
    {
        units.Add(unit);
        Bind(unit);
        unit.Setup();
    }

    public UnitLoadoutData GetData() => new UnitLoadoutData(units);
}
