using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeatMaster : MonoBehaviour
{

	public delegate void BeatDelegate();

	public event BeatDelegate beatEvent;

	private AudioSource musicSource;
	public SongInfo songInfo;

    public float maxBeatDiff = 0.1f;

	private int lastBeatIndex = -1;

	void Awake()
	{
		musicSource = GetComponent<AudioSource>();
		musicSource.clip = songInfo.song;
		musicSource.Play();
	}
	
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
        float fCurrentTimeBeat = getCurrentTime() * songInfo.Bps;
        float fNearestBeat = Mathf.Floor(fCurrentTimeBeat + 0.5f);

        float fDiff = Mathf.Abs(fCurrentTimeBeat - fNearestBeat);
        fDiff /= songInfo.Bps;

		//Debug.Log(string.Format("Beat diff: {0}", fDiff));

        return fDiff < maxBeatDiff;
    }


	private int getBeatIndex(float time)
	{
		return Mathf.FloorToInt(time * songInfo.Bps);
	}

    private float getCurrentTime()
    {
        return musicSource.time - songInfo.offset;
    }

	public int NearestBeat
	{
		get { return Mathf.RoundToInt(getCurrentTime() * songInfo.Bps); }
	}

	public int LastBeat { get { return lastBeatIndex; } }

	public int NextBeat { get { return lastBeatIndex + 1; } }
}
