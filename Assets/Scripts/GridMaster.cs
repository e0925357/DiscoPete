using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaster : MonoBehaviour {

    public int gridSizeX;
    public int gridSizeZ;

    private GridTile[,] m_pGridTiles;

    void Awake()
    {
        Debug.Log("GridMaster::Awake (" + gridSizeX + "," + gridSizeZ + ")");
        m_pGridTiles = new GridTile[gridSizeX, gridSizeZ];
    }

    public void RegisterTile(GridTile tile, int x, int z)
    {
        Debug.Log("Registering tile " + tile);
        m_pGridTiles[x, z] = tile;
    }

	public void OnDiscoPeteLanded(DiscoPeteBehaviour pete, int x, int z)
    {
        if (x < 0 || x >= m_pGridTiles.GetLength(0))
            Debug.Log("DISCO PETE IS OUTSIDE");
        else if (z < 0 || z >= m_pGridTiles.GetLength(1))
            Debug.Log("DISCO PETE IS OUTSIDE");
        else
        {
            if (m_pGridTiles[x, z] != null)
                m_pGridTiles[x, z].OnDiscoPeteLanded(pete);
        }
    }
}
