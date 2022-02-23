using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellData
{
    public HexCoordinate hexCoordinate;
    public List<CellAttributeData> cellAttributes;
    public string Key;
}
public struct MovementCostArguments
{
    public float cost;
}
public class Cell 
{
    #region
    //TODO Actions
    #endregion

    #region
    private HexCoordinate hexCoordinate;
    private List<CellAttribute> cellAttributes;
    #endregion

    #region
    public bool Collide()
    {
        return true;
    }

    public float GetMovementCost()
    {
        return 0f;
    }

    public void Destory()
    {

    }
    public void AddAttribute(CellAttribute cellAttribute)
    {
        cellAttribute.Connect(this);
        cellAttributes.Add(cellAttribute);
    }
    //todo remove attribute
    #endregion
}
