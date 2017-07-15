using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridTile : Assets.Scripts.AbstractBeatable {


    private GridMaster gridMaster;
    private int iXPosInGrid;
    private int iZPosInGrid;

    void Start()
    {
        iXPosInGrid = Mathf.FloorToInt(transform.position.x + 0.5f);
        iZPosInGrid = Mathf.FloorToInt(transform.position.z + 0.5f);

        Debug.Log("GridTile::Start (" + iXPosInGrid + "," + iZPosInGrid + ")");
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        gridMaster = gmGO.GetComponent<GridMaster>();

        gridMaster.RegisterTile(this, iXPosInGrid, iZPosInGrid);
    }

	public virtual void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    { /* empty default implementation */}

    public virtual void OnDiscoPeteLeaves(DiscoPeteBehaviour pete)
    { /* empty default implementation */}

    public virtual void OnDiscoPeteStays(DiscoPeteBehaviour pete)
    { /* empty default implementation */ }

    public void DestroyTile()
    {
        gridMaster.DeregisterTile(this, iXPosInGrid, iZPosInGrid);

        Destroy(gameObject);
    }
}
