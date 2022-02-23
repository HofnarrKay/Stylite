using System.Collections.Generic;
using UnityEngine;

public struct ActionsData
{
    public List<ModelActionData> Actions;

    public ActionsData(List<ModelAction> actions)
    {
        Actions = new List<ModelActionData>();

        foreach (var action in actions)
        {
            Actions.Add(action.GetData());
        }
    }

    public List<ModelActionData> GetActions(string key)
    {
        List<ModelActionData> retVal = new List<ModelActionData>();

        foreach (var action in Actions)
        {
            if (action.Key == key) retVal.Add(action);
        }

        return retVal;
    }
}

public class Actions
{
    private List<ModelAction> actions = new List<ModelAction>();

    public List<ModelAction> AvailableActions => actions;

    public void CallAction(ActionArguments arguments, Model model)
    {
        foreach(ModelAction action in actions)
        {
            if (action.Key == arguments.Key) action.Act(arguments, model);
        }
    }

    public List<ModelAction> GetActions(string key)
    {
        List<ModelAction> retVal = new List<ModelAction>();

        foreach (var action in actions)
        {
            if (action.Key == key) retVal.Add(action);
        }

        return retVal;
    }

    public void AddAction(ModelAction action)
    {
        actions.Add(action);
    }

    public void ExpandAction(ExpandActionArguments expandActionArguments)
    {
        foreach(ModelAction action in GetActions(expandActionArguments.Key))
        {
            action.Expand(expandActionArguments);
        }
    }

    public ActionsData getData() => new ActionsData(actions);
}
