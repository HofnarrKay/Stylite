using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CellAttributeData
{
    public string Key;
}
public class CellAttribute
{
    public virtual void Connect(Cell cell)
    {

    }
    public virtual void Disconnect(Cell cell)
    {

    }
}
