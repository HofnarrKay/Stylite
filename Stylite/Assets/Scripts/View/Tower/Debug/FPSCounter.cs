using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text text;

    void Update()
    {
        if(text)
        {
            text.text = (1 / Time.deltaTime).ToString();
        }
    }
}
