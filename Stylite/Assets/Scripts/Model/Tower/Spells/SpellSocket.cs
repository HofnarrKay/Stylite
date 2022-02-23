using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSocketData : SocketData
{
    public SpellComponentData Spell;


    public SpellSocketData(SpellComponent spell, int index) : base(index)
    {
        if(spell != null) Spell = spell.GetData();
        Index = index;
    }
}

public class SpellSocket : Socket
{
    public SpellComponent Spell;

    public SpellSocket(int index)
    {
        Index = index;
    }

    public override void MoveContent(Socket socket)
    {
        Spell = socket.SetSpellContent(Spell);
    }

    public override SpellComponent SetSpellContent(SpellComponent spell)
    {
        SpellComponent removedSpell = Spell;
        Spell = spell;
        return base.SetSpellContent(removedSpell);
    }

    public bool IsEmpty() => Spell == null;

    public SpellSocketData GetData() => new SpellSocketData(Spell, Index);
}
