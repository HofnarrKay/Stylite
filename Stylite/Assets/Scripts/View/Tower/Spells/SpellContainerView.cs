using System.Collections.Generic;
using UnityEngine;

public class SpellContainerView : SocketSpawnerView
{
    [SerializeField]
    private GameObject spellSocketPrefab;

    [SerializeField]
    private Transform objectSocket;

    [SerializeField]
    private ContentType contentType = ContentType.Spells;

    [SerializeField]
    private string containerIdentifier = "SpellContainer";

    Dictionary<int, SocketView> spellSockets = new Dictionary<int, SocketView>();

    public void Visualize(SpellContainerData spellContainer)
    {
        if(spellContainer.Spells.Count > spellSockets.Count)
        {
            CreateSpellSockets(spellContainer);
        }

        foreach(SpellSocketData socket in spellContainer.Spells)
        {
            spellSockets[socket.Index].Visualize(socket);
        }
    }

    public void CreateSpellSockets(SpellContainerData spellContainer)
    {
        foreach (var socket in spellContainer.Spells)
        {
            if(!spellSockets.ContainsKey(socket.Index))
            {
                GameObject createdObject = Instantiate(spellSocketPrefab, objectSocket);

                SocketView socketView = createdObject.GetComponent<SocketView>();
                if(socketView)
                {
                    spellSockets.Add(socket.Index, socketView);
                    socketView.Setup(new SocketPositionInformation(contentType, containerIdentifier, socket.Index));
                    CreatedSocket(socketView);
                }
                else
                {
                    Destroy(createdObject);
                }
            }
        }
    }
}
