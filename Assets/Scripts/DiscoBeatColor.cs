using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBeatColor : MonoBehaviour {

	[SerializeField]
	private DiscoColorsProfile discoColors = null;

	// Use this for initialization
	protected void Start () {
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}

	public void OnDiscoPeteLanded(DiscoPeteBehaviour pete)
	{
		//do nothig
	}

	void SetDiscoColor(Color color)
	{
		GetComponentInChildren<Renderer>().materials[1].SetColor("_Color", color);
		GetComponentInChildren<Renderer>().materials[1].SetColor("_EmissionColor", color);
	}

	protected void OnBeat()
	{
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}
}
