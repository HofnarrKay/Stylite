using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] private HexCoordinates hexCoordinates;
    [SerializeField] private GlowHighlight highlight;

    [SerializeField] private HexType hexType;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public int GetCost()
        => hexType switch
        {
            HexType.Difficult => 3,
            HexType.Default => 2,
            HexType.Road => 1,
            _ => throw new Exception($"Hex of type {hexType} not supported")
        };

    public bool IsObstacle()
    {
        return this.hexType == HexType.Obstacle;
        
    }

    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
        highlight = GetComponent<GlowHighlight>();
    }

    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

    internal void ResetHighlight()
    {
        highlight.ResetGlowHighlight();
    }

    internal void HighlightPath()
    {
        highlight.HighlightValidPath();
    }
}

public enum HexType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstacle
}
