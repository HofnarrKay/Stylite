using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelActionData
{
    public string Key;

    public ModelActionData(string key)
    {
        Key = key;
    }
}

public class ModelAction 
{
    public string Key;
    
    public ModelAction(string key)
    {
        Key = key;
    }

    public virtual void Act(ActionArguments actionArguments, Model model)
    {
    }

    public virtual void Expand(ExpandActionArguments expandAction)
    {
    }

    public bool EqualsData(UnitTemplateActionData data)
    {
        return true;
    }

    public virtual ModelActionData GetData() => new ModelActionData(Key);
}
