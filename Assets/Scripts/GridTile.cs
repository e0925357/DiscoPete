using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridTile : Assets.Scripts.AbstractBeatable {


    private GridMaster gridMaster;
    private int m_iXPosInGrid;
    private int m_iZPosInGrid;

    private bool m_bDestroyed = false;

    protected virtual void Start()
    {
        m_iXPosInGrid = Mathf.FloorToInt(transform.position.x + 0.5f);
        m_iZPosInGrid = Mathf.FloorToInt(transform.position.z + 0.5f);

        Debug.Log("GridTile::Start (" + m_iXPosInGrid + "," + m_iZPosInGrid + ")");
        GameObject gmGO = GameObject.FindWithTag("GridMaster");
        gridMaster = gmGO.GetComponent<GridMaster>();

        gridMaster.RegisterTile(this, m_iXPosInGrid, m_iZPosInGrid);
    }

    protected virtual void Update()
    {
        if(m_bDestroyed && gameObject.transform.position.y > -5.0f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3.0f);
        }
    }

    public virtual void Reset()
    {
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        m_bDestroyed = false;
    }

    public bool IsDestroyed()
    { return m_bDestroyed;  }

    public virtual void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    { /* empty default implementation */}

    public virtual void OnDiscoPeteLeaves(DiscoPeteBehaviour pete)
    { /* empty default implementation */}

    public virtual void OnDiscoPeteStays(DiscoPeteBehaviour pete)
    { /* empty default implementation */ }

    public void DestroyTile()
    {
        m_bDestroyed = true;
    }
}
