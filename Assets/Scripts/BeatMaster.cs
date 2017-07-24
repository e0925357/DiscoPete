using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

struct GameObjectPriorityPair
{
	public GameObject go;
	public int priority;
}

enum BeatDiffType
{
	Relative, Absolute
}

[RequireComponent(typeof(AudioSource))]
public class BeatMaster : MonoBehaviour
{

	public delegate void BeatDelegate();

	public event BeatDelegate beatEvent;
    public event BeatDelegate onJumpChancePassedEvent;

	private AudioSource musicSource;
	public SongInfo songInfo;
	public AudioSource deactivateLightsSound;
	[SerializeField]
	private bool shouldHandleSongEnd = true;

	[SerializeField]
	private float maxBeatDiff = 0.2f;
	[SerializeField]
	private BeatDiffType beatDiffType = BeatDiffType.Relative;

	private int lastBeatIndex = -1;
	private int lastJumpMissIndex = -1;
	
	void Awake()
	{
		musicSource = GetComponent<AudioSource>();
		musicSource.clip = songInfo.song;
		StartCoroutine(StartSong(1f));

		if(shouldHandleSongEnd)
			StartCoroutine(handleSongEnd());
	}

	IEnumerator StartSong(float delay)
	{
		yield return new WaitForSeconds(delay);

		musicSource.Play();
	}

	IEnumerator handleSongEnd()
	{
		yield return new WaitForSeconds(musicSource.clip.length + 2f);

		GameObject[] emissiveObjects = GameObject.FindGameObjectsWithTag("Emissive");
		GameObjectPriorityPair[] goPairs = new GameObjectPriorityPair[emissiveObjects.Length];

		int numGroups = Mathf.Max(2, emissiveObjects.Length / 10);

		for (int i = 0; i < emissiveObjects.Length; ++i)
		{
			goPairs[i].go = emissiveObjects[i];
			goPairs[i].priority = Random.Range(0, numGroups);
		}

		goPairs = goPairs.OrderBy(x => x.priority).ToArray();

		int lastIndex = 0;

		for (int p = 0; p < numGroups; ++p)
		{
			for (int i = lastIndex; i < goPairs.Length; ++i)
			{
				lastIndex = i;
				if (goPairs[i].priority > p) break;

				Renderer[] renderers = goPairs[i].go.GetComponentsInChildren<Renderer>();
				bool changeColor = !"SpotlightCone".Equals(goPairs[i].go.name);

				for (int r = 0; r < renderers.Length; ++r)
				{
					if (changeColor)
					{
						Material[] materials = renderers[r].materials;
						int matIndex = Mathf.Min(1, materials.Length - 1);

						materials[matIndex].SetColor("_Color", Color.black);
						materials[matIndex].SetColor("_EmissionColor", Color.black);
					}
					else
					{
						renderers[r].enabled = false;
					}
				}
			}

			if (deactivateLightsSound)
			{
				deactivateLightsSound.Play();
			}

			if (p + 1 < numGroups)
			{
				yield return new WaitForSeconds(0.8f);
			}
			else
			{
				GameObject[] lightGos = GameObject.FindGameObjectsWithTag("Light");

				for (int l = 0; l < lightGos.Length; ++l)
				{
					Light[] lights = lightGos[l].GetComponentsInChildren<Light>();

					for (int ls = 0; ls < lights.Length; ++ls)
					{
						lights[ls].enabled = false;
					}
				}
			}
		}

		GameObject guiGO = GameObject.FindWithTag("LevelAndPointMaster");
		LevelAndPointBehaviour levelMaster = guiGO.GetComponent<LevelAndPointBehaviour>();
		levelMaster.DisplaySongOverMessage();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float time = getCurrentTime();
		int beat = getBeatIndex(time);

		if (beat > lastBeatIndex)
		{
			lastBeatIndex = beat;
			if (beatEvent != null)
			{
				beatEvent();
			}
		}

		int missIndex = getBeatIndex(time - (beatDiffType == BeatDiffType.Relative ? maxBeatDiff / songInfo.Bps : maxBeatDiff));


		if (missIndex > lastJumpMissIndex)
		{
			lastJumpMissIndex = missIndex;

			if (onJumpChancePassedEvent != null)
            {
                onJumpChancePassedEvent();
            }
        }
	}

    public bool allowsJump()
    {
        float fCurrentTimeBeat = convertToBeats(getCurrentTime());
        float fNearestBeat = Mathf.Round(fCurrentTimeBeat);

        float fDiff = Mathf.Abs(fCurrentTimeBeat - fNearestBeat);

		if(beatDiffType == BeatDiffType.Absolute)
			fDiff /= songInfo.Bps;

		Debug.Log(string.Format("fDiff: {0}", fDiff));

        return fDiff < maxBeatDiff;
    }

    public float getDiscoPeteSpeedDependingOnMusic()
    {
        return 2.0f * songInfo.Bps;
    }

	private int getBeatIndex(float time)
	{
		return Mathf.FloorToInt(convertToBeats(time));
	}

	private float convertToBeats(float time)
	{
		return time * songInfo.Bps + 0.5f;
	}

    public float getMusicLength()
    {
        return musicSource.clip.length;
    }

    private float getCurrentTime()
    {
        return musicSource.time - songInfo.offset;
    }

	public int NearestBeat
	{
		get { return Mathf.RoundToInt(convertToBeats(getCurrentTime())); }
	}

	public int LastBeat { get { return lastBeatIndex; } }

	public int NextBeat { get { return lastBeatIndex + 1; } }
}
