using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLight : MonoBehaviour
{
	public Light lightSource;
	public Color[] colors;
	private BeatMaster beatMaster;
	private int colorIndex = 0;

	// Use this for initialization
	void OnEnable ()
	{
		GameObject bmGO = GameObject.FindWithTag("Music");
		beatMaster = bmGO.GetComponent<BeatMaster>();

		beatMaster.beatEvent += BeatMasterOnBeatEvent;
	}

	void OnDisable()
	{
		if(beatMaster != null)
			beatMaster.beatEvent -= BeatMasterOnBeatEvent;
	}

	private void BeatMasterOnBeatEvent()
	{
		lightSource.color = colors[colorIndex];

		if (++colorIndex >= colors.Length)
		{
			colorIndex = 0;
		}
	}
}
