using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketView : GrabbableSocket
{
    public Action<SocketPositionInformation> AddedGrabbable;
    public Action<SocketPositionInformation> RemovedGrabbable;

    [SerializeField]
    GameObject grabbablePrefab;

    private SocketPositionInformation position;


    public void Setup(SocketPositionInformation socketPositionInformation)
    {
        position = socketPositionInformation;
    }

    public virtual void Visualize(SpellSocketData socketData)
    {
        if(socketData.Spell != null)
        {
            if (grabbable == null)
            {
                GameObject createdObject = Instantiate(grabbablePrefab, transform);
                Grabbable createdGrabbable = createdObject.GetComponent<Grabbable>();

                if(createdGrabbable)
                {
                    this.grabbable = createdGrabbable;
                    ObserveGrabbable(createdGrabbable);
                }
                else
                {
                    Debug.LogError("Prefab in " + gameObject.name + "/SocketView doesn't contain a Grabbable Script");
                }
            }

        }
        else
        {
            if(grabbable != null)
            {
                Destroy(grabbable.gameObject);
                grabbable = null;
            }
        }
    }

    protected override void OnAddGrabbable(Grabbable grabbable)
    {
        AddedGrabbable?.Invoke(position);
    }

    protected override void OnRemovedGrabbable(Grabbable removedGrabbable)
    {
        if(removedGrabbable)
        {
            Destroy(removedGrabbable.gameObject);
        }
        RemovedGrabbable?.Invoke(position);
    }

}
