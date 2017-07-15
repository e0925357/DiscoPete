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

    public void DeregisterTile(GridTile tile, int x, int z)
    {
        m_pGridTiles[x, z] = null;
    }

	public void OnDiscoPeteLanded(DiscoPeteBehaviour pete, int x, int z)
    {
        GridTile tile = ItlGetTileFromPos(x, z);
        if (tile != null)
            tile.OnDiscoPeteLanded(pete);
        else
            pete.Die();
    }

    public void OnDiscoPeteStaysOnTile(DiscoPeteBehaviour pete, int x, int z)
    {
        GridTile tile = ItlGetTileFromPos(x, z);
        if (tile != null)
            tile.OnDiscoPeteStays(pete);
        else
            pete.Die();
    }

    public void OnDiscoPeteLeavesTile(DiscoPeteBehaviour pete, int x, int z)
    {
        GridTile tile = ItlGetTileFromPos(x, z);
        if (tile != null)
            tile.OnDiscoPeteLeaves(pete);
        else
            pete.Die();
    }

    private GridTile ItlGetTileFromPos(int x, int z)
    {
        if (x < 0 || x >= m_pGridTiles.GetLength(0) || z < 0 || z >= m_pGridTiles.GetLength(1))
        {
            return null;
        }
        else 
        {
            return m_pGridTiles[x, z];
        }
    }
}
