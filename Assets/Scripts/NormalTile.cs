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
        m_iLifeOfTile--;

        if (m_iLifeOfTile > 0)
            transform.localScale += new Vector3(0.0f, -0.25f, 0.0f);
        else
        {
            DestroyTile();
        }
    }

    public override void OnDiscoPeteStays(DiscoPeteBehaviour pete)
    {
        m_iLifeOfTile--;

        transform.localScale += new Vector3(0.0f, -0.33f, 0.0f);

        if (m_iLifeOfTile == 0)
        {
            DestroyTile();
            pete.Die();
        }
    }

    protected override void OnBeat()
    {

    }
}
