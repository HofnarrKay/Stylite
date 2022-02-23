using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [Header("Views")]
    [SerializeField] public CellView cellView;


    [Header("Prefabs")]
    [SerializeField] public GameObject DefaultCellPrefab;
    public Dictionary<string, GameObject> DefaultCellPrefabs;

    private Dictionary<HexCoordinate, CellView> createdCells;



    void Update()
    {
        
    }

    public void Visualize(BoardData board)
    {

    }

    public Vector3 ConvertHexCoordinatesToVector3(HexCoordinate hexCoordinate)
    {
        return new Vector3(0, 0, 0);
    }
}
