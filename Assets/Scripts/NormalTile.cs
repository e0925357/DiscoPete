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

    public override void Reset()
    {
        base.Reset();
        m_iLifeOfTile = 3;
    }

    public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
    {
        pete.Say("Landed on tile! Tile state: " + m_iLifeOfTile);
    }

    public override void OnDiscoPeteLeaves(DiscoPeteBehaviour pete)
    {
        //Debug.Log("Disco Pete leaves tile!");
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
        //Debug.Log("Disco Pete stays on tile!");
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
