using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : GridTile {

    private int m_iLifeOfTile = 3;

	public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    {
        pete.Say("Landed on tile! Tile state: " + m_iLifeOfTile);
    }

    public override void OnDiscoPeteLeaves(DiscoPeteBehaviour pete)
    {
        Debug.Log("Disco Pete leaves tile!");
        m_iLifeOfTile--;

        if (m_iLifeOfTile > 0)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        else
        {
            DestroyTile();
        }
    }

    public override void OnDiscoPeteStays(DiscoPeteBehaviour pete)
    {
        Debug.Log("Disco Pete stays on tile!");
        m_iLifeOfTile--;


        if(m_iLifeOfTile > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        }
        else if (m_iLifeOfTile == 0)
        {
            DestroyTile();
            pete.Die();
        }
    }

    protected override void OnBeat()
    {

    }
}
