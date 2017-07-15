using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : GridTile {

    private int m_iLifeOfTile = 3;

	public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    {
        m_iLifeOfTile--;
        pete.Say("Landed on tile! Tile state: " + m_iLifeOfTile);

        transform.localScale += new Vector3(0.0f, -0.33f, 0.0f);
    }

    protected override void OnBeat()
    {

    }
}
