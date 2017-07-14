using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaster : MonoBehaviour
{

	public delegate void BeatDelegate();

	public event BeatDelegate beatEvent;

	public AudioSource musicSource;
	public float beatOffset;
	public float bps;

	private int lastBeatIndex = -1;
	
	// Update is called once per frame
	void Update ()
	{
		int beat = getBeatIndex(musicSource.time - beatOffset);

		if (beat > lastBeatIndex)
		{
			lastBeatIndex = beat;
			if (beatEvent != null)
			{
				beatEvent();
			}
		}
	}

	private int getBeatIndex(float time)
	{
		return Mathf.FloorToInt(time * bps);
	}
}
