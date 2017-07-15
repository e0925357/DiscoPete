using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeTile : GridTile
{

	public Color tileColor = Color.white;

	protected override void Start()
	{
		base.Start();

		SetDiscoColor(tileColor);
	}

	void SetDiscoColor(Color color)
	{
		Material[] mats = GetComponentInChildren<Renderer>().materials;
		mats[Mathf.Min(1, mats.Length - 1)].SetColor("_Color", color);
		mats[Mathf.Min(1, mats.Length - 1)].SetColor("_EmissionColor", color);
	}

	protected override void OnBeat()
    {
        // TODO
    }
}
