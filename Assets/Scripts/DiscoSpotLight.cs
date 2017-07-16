using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSpotLight : AbstractBeatable
{
	[SerializeField]
	private DiscoColorsProfile discoColors = null;

	[SerializeField]
	private Renderer lightEngineRenderer = null;
	[SerializeField]
	private Renderer lightConeRenderer = null;

	// Use this for initialization
	void Start () {
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetLightColor(discoColors.DiscoColors[index]);
	}

	void SetLightColor(Color color)
	{
		SetDiscoColorOnMaterial(lightEngineRenderer.materials[1], color);
		SetDiscoColorOnMaterial(lightConeRenderer.materials[0], color, true);

	}

	private void SetDiscoColorOnMaterial(Material material, Color color, bool setColorOnly = false)
	{
		material.SetColor("_Color", color);

		if (!setColorOnly)
		{
			material.SetColor("_EmissionColor", color);
		}
	}

	protected override void OnBeat()
	{
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetLightColor(discoColors.DiscoColors[index]);
	}
}
