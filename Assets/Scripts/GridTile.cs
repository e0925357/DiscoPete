using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridTile : Assets.Scripts.AbstractBeatable {


    private GridMaster gridMaster;

    protected virtual void Start()
    {
        int iXPosInGrid = Mathf.FloorToInt(transform.position.x + 0.5f);
        int iZPosInGrid = Mathf.FloorToInt(transform.position.z + 0.5f);

        Debug.Log("GridTile::Start (" + iXPosInGrid + "," + iZPosInGrid + ")");
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        gridMaster = gmGO.GetComponent<GridMaster>();

        gridMaster.RegisterTile(this, iXPosInGrid, iZPosInGrid);
    }

	public abstract void OnDiscoPeteLanded(DiscoPeteBehaviour pete);
}
