using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellContainerData
{
    public List<SpellSocketData> Spells;
    public List<SpellSocketData> Modifiers;

    public SpellContainerData(List<SpellSocket> spells, List<SpellSocket> modifiers)
    {
        Spells = new List<SpellSocketData>();
        foreach(SpellSocket socket in spells)
        {
            Spells.Add(socket.GetData());
        }

        Modifiers = new List<SpellSocketData>();
        foreach (SpellSocket socket in modifiers)
        {
            Spells.Add(socket.GetData());
        }
    }
}

public class SpellContainer : GameComponent
{
    private List<SpellSocket> spells;

    private List<SpellSocket> modifiers;

    public override void Setup()
    {
        spells = new List<SpellSocket>();
        modifiers = new List<SpellSocket>();

        CreatedAction?.Invoke(new MoveSocketContentAction("MoveSocketContent", ContentType.Spells, "SpellContainer", new List<Socket>()));
    }

    public void AddSockets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddSocket();
        }
    }

    public void AddSocket()
    {
        int currentIndex = 0;

        if(spells.Count != 0)
        {
            SpellSocket highestSocket = spells.Aggregate((l, r) => l.Index > r.Index ? l : r);
            currentIndex = highestSocket.Index + 1;
        }

        SpellSocket newSocket = new SpellSocket(currentIndex);

        spells.Add(newSocket);

        ExpandActionRequest?.Invoke(new ExpandMoveSocketActionArguments("MoveSocketContent", ContentType.Spells, "SpellContainer", newSocket));
    }

    public SpellComponent AddSpell(SpellComponent spell, int index)
    {
        foreach (var socket in spells)
        {
            if(socket.Index == index)
            {
                SpellComponent previousSpell = socket.Spell;
                socket.Spell = spell;
                return previousSpell;
            }
        }
        Debug.LogError("No Socket with the index: " + index + " in the Spellcontainer was found.");
        return null;
    }

    public SpellContainerData GetData() => new SpellContainerData(spells, modifiers);
}
