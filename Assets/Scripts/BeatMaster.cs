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

    public float maxBeatDiff = 0.2f;

	private int lastBeatIndex = -1;
	
	// Update is called once per frame
	void Update ()
	{
		int beat = getBeatIndex(getCurrentTime());

		if (beat > lastBeatIndex)
		{
			lastBeatIndex = beat;
			if (beatEvent != null)
			{
				beatEvent();
			}
		}
	}

    public bool allowsJump()
    {
        float fCurrentTimeBeat = getCurrentTime() * bps;
        float fNearestBeat = Mathf.Floor(fCurrentTimeBeat + 0.5f);

        float fDiff = Mathf.Abs(fCurrentTimeBeat - fNearestBeat);

		//Debug.Log(string.Format("Beat diff: {0}", fDiff));

        return fDiff < maxBeatDiff;
    }


	private int getBeatIndex(float time)
	{
		return Mathf.FloorToInt(time * bps);
	}

    private float getCurrentTime()
    {
        return musicSource.time - beatOffset;
    }

	public int NearestBeat
	{
		get { return Mathf.RoundToInt(getCurrentTime() * bps); }
	}

	public int LastBeat { get { return lastBeatIndex; } }

	public int NextBeat { get { return lastBeatIndex + 1; } }
}
