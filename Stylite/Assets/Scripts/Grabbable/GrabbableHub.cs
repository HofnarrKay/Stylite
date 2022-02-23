using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableHub : MonoBehaviour
{
    [SerializeField]
    private List<GrabbableSocket> initSockets;

    protected List<GrabbableSocket> sockets;

    protected Grabbable grabbable;
    protected GrabbableSocket currentSocket;

    void Start()
    {
        sockets = new List<GrabbableSocket>();

        foreach(GrabbableSocket socket in initSockets)
        {
            AddSocketToList(socket);
        }

        OnStart();
    }

    protected virtual void OnStart() { }

    private void Update()
    {
        if(grabbable)
        {
            if(Input.GetMouseButton(0))
            {
                grabbable.transform.position = Input.mousePosition;
            }
            else
            {
                GrabbableSocket overlappingSocket = null;
                foreach (GrabbableSocket socket in sockets)
                {
                    if(socket.PositionIsInsideHitbox(Input.mousePosition))
                    {
                        overlappingSocket = socket;
                        break;
                    }
                }

                if(overlappingSocket)
                {
                    grabbable.QueuedSwitchingSockets?.Invoke(grabbable);
                    Grabbable grabbableOfOverlappingSocket = overlappingSocket.RemoveGrabbable();
                    Grabbable currentGrabbable = currentSocket.RemoveGrabbable();

                    if(grabbableOfOverlappingSocket)
                    {
                        currentSocket.AddGrabbable(grabbableOfOverlappingSocket);
                        grabbableOfOverlappingSocket.StoppedMoving(grabbableOfOverlappingSocket);
                    }

                    overlappingSocket.AddGrabbable(currentGrabbable);
                }

                grabbable.StoppedMoving?.Invoke(grabbable);

                grabbable = null;
                currentSocket = null;
            }
        }
    }

    public void AddSocketToList(GrabbableSocket grabbableSocket)
    {
        sockets.Add(grabbableSocket);
        grabbableSocket.ClickedGrabbable += OnClickedGrabbable;
    }

    public void RemoveSocketFromList(GrabbableSocket grabbableSocket)
    {
        sockets.Add(grabbableSocket);
        grabbableSocket.ClickedGrabbable -= OnClickedGrabbable;
    }

    public void OnClickedGrabbable(GrabbableSocket socket, Grabbable grabbable)
    {
        this.grabbable = grabbable;
        this.currentSocket = socket;
        grabbable.gameObject.transform.SetParent(transform);
    }
}
