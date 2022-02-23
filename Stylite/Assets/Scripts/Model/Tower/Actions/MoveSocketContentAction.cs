using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SocketPositionInformation
{
    public ContentType ContentType;
    public string ContainerIdentifier;
    public int Index;

    public SocketPositionInformation(ContentType contentType, string containerIdentifier, int index)
    {
        ContentType = contentType;
        ContainerIdentifier = containerIdentifier;
        Index = index;
    }
}


public class MoveSocketContentAction : ModelAction
{
    public Dictionary<ContentType, Dictionary<string, Dictionary<int, Socket>>> sockets;

    public MoveSocketContentAction(string key, ContentType contentType, string containerIdentifier, List<Socket> sockets) : base(key)
    {
        this.sockets = new Dictionary<ContentType, Dictionary<string, Dictionary<int, Socket>>>();
        AddSockets(contentType, containerIdentifier, sockets);
    }

    public override void Act(ActionArguments actionArguments, Model model)
    {
        MoveSocketContentActionArguments arguments = (MoveSocketContentActionArguments)actionArguments;

        bool possibleArguments = true;
        if(sockets.ContainsKey(arguments.ContentOwner.ContentType))
        {
            if(sockets[arguments.ContentOwner.ContentType].ContainsKey(arguments.ContentOwner.ContainerIdentifier))
            {
                if(!sockets[arguments.ContentOwner.ContentType][arguments.ContentOwner.ContainerIdentifier].ContainsKey(arguments.ContentOwner.Index))
                {
                    Debug.LogError("Index of ContentOwner in MoveSocketContentAction is Wrong: \nIndex: " + arguments.ContentOwner.Index.ToString());
                    possibleArguments = false;
                }
            }
            else
            {
                Debug.LogError("ContainerIdentifier of ContentOwner in MoveSocketContentAction is Wrong: \nContainerIdentifier: " + arguments.ContentOwner.ContainerIdentifier.ToString());
                possibleArguments = false;
            }
        }
        else
        {
            Debug.LogError("ContentType of ContentOwner in MoveSocketContentAction is Wrong: \nContentType: " + arguments.ContentOwner.ContentType.ToString());
            possibleArguments = false;
        }

        if (sockets.ContainsKey(arguments.ContentDestination.ContentType))
        {
            if (sockets[arguments.ContentDestination.ContentType].ContainsKey(arguments.ContentDestination.ContainerIdentifier))
            {
                if (!sockets[arguments.ContentDestination.ContentType][arguments.ContentDestination.ContainerIdentifier].ContainsKey(arguments.ContentDestination.Index))
                {
                    Debug.LogError("Index of ContentDestination in MoveSocketContentAction is Wrong: \nIndex: " + arguments.ContentOwner.Index.ToString());
                    possibleArguments = false;
                }
            }
            else
            {
                Debug.LogError("ContainerIdentifier of ContentDestination in MoveSocketContentAction is Wrong: \nContainerIdentifier: " + arguments.ContentDestination.ContainerIdentifier.ToString());
                possibleArguments = false;
            }
        }
        else
        {
            Debug.LogError("ContentType of ContentDestination in MoveSocketContentAction is Wrong: \nContentType: " + arguments.ContentDestination.ContentType.ToString());
            possibleArguments = false;
        }

        if (possibleArguments) sockets[arguments.ContentOwner.ContentType][arguments.ContentOwner.ContainerIdentifier][arguments.ContentOwner.Index].MoveContent(sockets[arguments.ContentDestination.ContentType][arguments.ContentDestination.ContainerIdentifier][arguments.ContentDestination.Index]);
    }

    public override void Expand(ExpandActionArguments expandAction)
    {
        ExpandMoveSocketActionArguments arguments = (ExpandMoveSocketActionArguments)expandAction;

        AddSocket(arguments.ContentType, arguments.ContainerIdentifier, arguments.Socket);
    }

    //Let us ignore this function
    private void AddSockets(ContentType contentType, string containerIdentifier, List<Socket> sockets)
    {
        if(!this.sockets.ContainsKey(contentType))
            this.sockets.Add(contentType, new Dictionary<string, Dictionary<int, Socket>>());

        if(!this.sockets[contentType].ContainsKey(containerIdentifier))
            this.sockets[contentType].Add(containerIdentifier, new Dictionary<int, Socket>());


        foreach (Socket socket in sockets)
        {
            this.sockets[contentType][containerIdentifier].Add(socket.Index, socket);
        }
    }

    private void AddSocket(ContentType contentType, string containerIdentifier, Socket socket)
    {
        if (!this.sockets.ContainsKey(contentType))
            this.sockets.Add(contentType, new Dictionary<string, Dictionary<int, Socket>>());

        if (!this.sockets[contentType].ContainsKey(containerIdentifier))
            this.sockets[contentType].Add(containerIdentifier, new Dictionary<int, Socket>());


        this.sockets[contentType][containerIdentifier].Add(socket.Index, socket);
    }
}
