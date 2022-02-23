using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Action<Grabbable> ClickedGrabbable;
    public Action<Grabbable> StoppedMoving;
    public Action<Grabbable> QueuedSwitchingSockets;

    [SerializeField]
    InstantClickSelectable selectable;

    private void Start()
    {
        selectable.Clicked += OnClicked;
    }

        

    public void OnClicked() => ClickedGrabbable?.Invoke(this);

    public void OnStoppedMoving() => StoppedMoving?.Invoke(this);
}
