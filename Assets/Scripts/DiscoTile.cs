using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoTile : MonoBehaviour {

	[SerializeField]
	private DiscoColorsProfile discoColors = null;

	// Use this for initialization
	void Start () {
		StartCoroutine(ChangeToRandomColor());
		int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
		SetDiscoColor(discoColors.DiscoColors[index]);
	}

	void SetDiscoColor(Color color)
	{
		GetComponentInChildren<Renderer>().materials[1].SetColor("_Color", color);
		GetComponentInChildren<Renderer>().materials[1].SetColor("_EmissionColor", color);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ChangeToRandomColor()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			int index = UnityEngine.Random.Range(0, discoColors.DiscoColors.Length - 1);
			SetDiscoColor(discoColors.DiscoColors[index]);
		}
	}
}
