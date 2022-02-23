using System.Collections;
using System.Collections.Generic;


public enum ContentType
{
    Invalid = -1,
    Spells,
    Count
}

public class SocketData
{
    public int Index;

    public SocketData(int index)
    {
        Index = index;
    }
}

public class Socket 
{
    public int Index;

    public virtual void MoveContent(Socket socket)
    {

    }

    public virtual SpellComponent SetSpellContent(SpellComponent spell)
    {
        return spell;
    }
}
