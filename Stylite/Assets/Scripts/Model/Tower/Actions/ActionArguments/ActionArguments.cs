using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionArguments
{
    private string key;

    public string Key => key;

    public ActionArguments(string key)
    {
        this.key = key;
    }

}
