using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabbableSocket : MonoBehaviour
{
    public Action<GrabbableSocket, Grabbable> ClickedGrabbable;

    public Grabbable grabbable;

    private void Start()
    {
        if(grabbable != null)
        {
            ObserveGrabbable(grabbable);
        }
    }

    public void AddGrabbable(Grabbable grabbable)
    {
        this.grabbable = grabbable;
        ObserveGrabbable(grabbable);
        OnAddGrabbable(grabbable);
    }

    protected virtual void OnAddGrabbable(Grabbable grabbable)
    {

    }

    public Grabbable RemoveGrabbable()
    {
        Grabbable removedGrabbable = grabbable;
        if(grabbable) LoseGrabbable(removedGrabbable);
        grabbable = null;
        OnRemovedGrabbable(removedGrabbable);
        return removedGrabbable;
    }

    protected virtual void OnRemovedGrabbable(Grabbable removedGrabbable)
    {

    }

    public virtual bool CheckIfSwitchIsPossible(GrabbableSocket other)
    {
        return true;
    }

    protected void ObserveGrabbable(Grabbable grabbable)
    {
        if(grabbable)
        {
            grabbable.ClickedGrabbable += OnClickedGrabbable;
            grabbable.StoppedMoving += OnGrabbableStoppedMoving;
            grabbable.QueuedSwitchingSockets += OnGrabbableQueuedSwitchingSockets;
        }
    }

    protected void LoseGrabbable(Grabbable grabbable)
    {
        grabbable.ClickedGrabbable -= OnClickedGrabbable;
        grabbable.StoppedMoving -= OnGrabbableStoppedMoving;
        grabbable.QueuedSwitchingSockets -= OnGrabbableQueuedSwitchingSockets;
    }

    public bool PositionIsInsideHitbox(Vector2 position)
    {
        RectTransform rect = GetComponent<RectTransform>();
        if(position.x > rect.position.x - (rect.rect.width/2) && position.x < rect.position.x + (rect.rect.width / 2))
        {
            if (position.y > rect.position.y - (rect.rect.height / 2) && position.y < rect.position.y + (rect.rect.height / 2))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void OnGrabbableQueuedSwitchingSockets(Grabbable grabbable)
    {
    }

    public void OnGrabbableStoppedMoving(Grabbable grabbable)
    {
        grabbable.transform.SetParent(transform);
        grabbable.transform.position = transform.position;
    }

    public void OnClickedGrabbable(Grabbable grabbable) => ClickedGrabbable?.Invoke(this, grabbable);
}
