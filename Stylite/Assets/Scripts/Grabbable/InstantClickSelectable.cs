using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstantClickSelectable : Selectable
{
    public Action Clicked;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
