using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour
{
    Text text;

    private void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    public void Visualize(ResourcesData resourcesData)
    {
        if(text)
        {
            text.text = "";
            foreach (var resource in resourcesData.resources.Values.ToList())
            {
                text.text += resource.key + ": " + ((int)resource.amount).ToString() + "  ";
            }
        }
    }
}
