using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplateActionData : ModelActionData
{
    public ResourcesData Costs;

    public UnitTemplateActionData(string key, Resources costs) : base(key)
    {
        Costs = costs.GetData();
    }
}

public class UnitTemplateAction : ModelAction
{
    const string unitKey = "Unit";

    public Resources costs;

    public int createdCharacterCount = 0;

    public UnitTemplateAction(string key, Resources costs) : base(key)
    {
        this.costs = costs;
    }

    public override void Act(ActionArguments actionArguments, Model model)
    {
        if(model.ContainsResources(costs))
        {
            model.ReduceResources(costs);
            model.AddNewUnitTemplate(new UnitTemplate(unitKey + createdCharacterCount.ToString()));
            createdCharacterCount++;
        }
    }

    public override ModelActionData GetData() => new UnitTemplateActionData(Key, costs);
}
