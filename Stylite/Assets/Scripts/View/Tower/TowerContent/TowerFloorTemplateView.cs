using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFloorTemplateView : MonoBehaviour
{
    [SerializeField]
    private string key = "Floor";

    public string Key => key;

    [SerializeField]
    public GameObject Prefab;
}
