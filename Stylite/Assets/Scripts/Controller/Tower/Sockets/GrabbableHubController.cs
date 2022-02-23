using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableHubController : GrabbableHub
{
    //Please simplify the socketSpawnerSpawner
    [SerializeField]
    private List<SocketSpawnerView> socketSpawners;

    [SerializeField]
    private List<SocketSpawnerSpawnerView> socketSpawnerSpawners;

    private SocketPositionInformation fromMovement;
    private SocketPositionInformation toMovement;

    protected override void OnStart()
    {
        fromMovement.Index = -1;
        toMovement.Index = -1;


        foreach (var socketSpawnerSpawner in socketSpawnerSpawners)
        {
            socketSpawnerSpawner.CreatedSocketSpawner += OnCreatedSocketSpawner;
        }

        foreach (var socketSpawner in socketSpawners)
        {
            socketSpawner.CreatedSocket += OnCreatedSocket;
        }
    }

    public void OnCreatedSocket(SocketView socket)
    {
        AddSocketToList(socket);
        socket.AddedGrabbable += OnGrabbableMovedTo;
        socket.RemovedGrabbable += OnGrabbableMovedFrom;
    }

    public void OnCreatedSocketSpawner(SocketSpawnerView socketSpawner)
    {
        socketSpawner.CreatedSocket += OnCreatedSocket;
    }


    public void OnGrabbableMovedFrom(SocketPositionInformation position)
    {
        fromMovement = position;
        CheckForCompleteAction();
    }

    public void OnGrabbableMovedTo(SocketPositionInformation position)
    {
        toMovement = position;
        CheckForCompleteAction();
    }

    private void CheckForCompleteAction()
    {
        if(fromMovement.Index != -1 && toMovement.Index != -1)
        {
            Model.Instance.CallAction(new MoveSocketContentActionArguments("MoveSocketContent", fromMovement, toMovement));
            toMovement.Index = -1;
            fromMovement.Index = -1;
        }
    }
}
