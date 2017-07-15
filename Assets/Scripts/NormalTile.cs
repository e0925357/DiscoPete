using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class NormalTile : GridTile {

	[SerializeField]
	private DiscoColorsProfile discoColors = null;
	private int m_iLifeOfTile = 3;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}

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

	void SetDiscoColor(Color color)
	{
		Material[] mats = GetComponentInChildren<Renderer>().materials;
		mats[Mathf.Min(1, mats.Length-1)].SetColor("_Color", color);
		mats[Mathf.Min(1, mats.Length - 1)].SetColor("_EmissionColor", color);
	}

	protected override void OnBeat()
	{
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}
}
