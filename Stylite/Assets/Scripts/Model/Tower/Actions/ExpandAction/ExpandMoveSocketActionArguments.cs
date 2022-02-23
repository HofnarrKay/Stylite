using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandMoveSocketActionArguments : ExpandActionArguments
{
    public ContentType ContentType;
    public string ContainerIdentifier;
    public Socket Socket;

    public ExpandMoveSocketActionArguments(string key, ContentType contentType, string containerIdentifier, Socket socket) : base(key)
    {
        ContentType = contentType;
        ContainerIdentifier = containerIdentifier;
        Socket = socket;
    }

}
