using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBeatColor : AbstractBeatable {

	[SerializeField]
	private DiscoColorsProfile discoColors = null;
	public bool controlAllMaterials = false;

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

	void SetRandomDiscoColors()
	{
		Renderer[] componentRenderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentRenderers)
		{
			foreach (Material material in renderer.materials)
			{
				int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
				Color color = discoColors.DiscoColors[index];
				material.SetColor("_Color", color);
				material.SetColor("_EmissionColor", color);
			}
		}
	}

	protected override void OnBeat()
	{
		if (!controlAllMaterials)
		{
			int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
			SetDiscoColor(discoColors.DiscoColors[index]);
		}
		else
		{
			SetRandomDiscoColors();
		}
	}
}
