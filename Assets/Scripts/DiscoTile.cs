using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTile : GridTile {

	[SerializeField]
	private DiscoColorsProfile discoColors = null;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}

	public override void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
	{
		//do nothig
	}

	void SetDiscoColor(Color color)
	{
		GetComponentInChildren<Renderer>().materials[1].SetColor("_Color", color);
		GetComponentInChildren<Renderer>().materials[1].SetColor("_EmissionColor", color);
	}

	protected override void OnBeat()
	{
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}
}
