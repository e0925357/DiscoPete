using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridTile : MonoBehaviour {

    public int xPosInGrid;
    public int zPosInGrid;

    private GridMaster gridMaster;

    void Start()
    {
        Debug.Log("GridTile::Start (" + xPosInGrid + "," + zPosInGrid + ")");
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        gridMaster = gmGO.GetComponent<GridMaster>();

        gridMaster.RegisterTile(this, xPosInGrid, zPosInGrid);
    }

	public abstract void OnDiscoPeteLanded(DiscoPeteBehaviour pete);
}
