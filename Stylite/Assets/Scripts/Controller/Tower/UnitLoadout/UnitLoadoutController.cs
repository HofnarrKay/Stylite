using System.Collections.Generic;
using UnityEngine;

public class UnitLoadoutController : MonoBehaviour
{
    public void SetActions(List<ModelActionData> templateActions)
    {

    }

    public void CallAction(string key)
    {
        Model.Instance.CallAction(new ActionArguments(key));
    }
}
