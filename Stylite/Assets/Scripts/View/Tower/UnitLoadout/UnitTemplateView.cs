using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplateView : SocketSpawnerView
{
    [SerializeField]
    private GameObject spellSocketPrefab;
    
    [SerializeField]
    private Transform spellSpawnerSocket;

    private Dictionary<int, SocketView> sockets = new Dictionary<int, SocketView>();

    public void Visualize(UnitTemplateData unit)
    {
        foreach (SpellSocketData socket in unit.Spells)
        {
            if(!sockets.ContainsKey(socket.Index))
            {
                GameObject createdObject = Instantiate(spellSocketPrefab, spellSpawnerSocket);

                SocketView createdSocket = createdObject.GetComponent<SocketView>();
                
                if(createdSocket)
                {
                    createdSocket.Setup(new SocketPositionInformation(ContentType.Spells, unit.Key, socket.Index));
                    sockets.Add(socket.Index, createdSocket);
                    CreatedSocket(createdSocket);
                }
                else
                {
                    Debug.Log("SpeellSocketPrefab in " + gameObject.name + "/UnitTemplateView doesn't contain a SocketView");
                }
            }


            sockets[socket.Index].Visualize(socket);
        }
    }
}
