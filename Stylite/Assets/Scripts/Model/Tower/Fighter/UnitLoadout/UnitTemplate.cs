using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplateData
{
    public string Key;
    public SpellSocketData[] Spells;

    public UnitTemplateData(string key, SpellSocket[] spells)
    {
        Key = key;
        Spells = new SpellSocketData[spells.Length];
        for (int i = 0; i < spells.Length; i++)
        {
            if(spells[i] != null)
            {
                Spells[i] = spells[i].GetData();
            }
        }
    }
}

public class UnitTemplate : GameComponent
{
    private string key;

    private SpellSocket[] spells;

    private const int spellAmount = 3;

    public UnitTemplate(string key)
    {
        spells = new SpellSocket[spellAmount];
        this.key = key;
    }

    public override void Setup()
    {
        for (int i = 0; i < spellAmount; i++)
        {
            spells[i] = new SpellSocket(i);
            ExpandActionRequest?.Invoke(new ExpandMoveSocketActionArguments("MoveSocketContent", ContentType.Spells, key, spells[i]));
        }
    }

    public UnitTemplateData GetData() => new UnitTemplateData(key, spells);
}
