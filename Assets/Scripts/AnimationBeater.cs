using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBeater : MonoBehaviour
{

	private static readonly int BEAT = Animator.StringToHash("Beat");
	private BeatMaster beatMaster;
	private Animator animator;

	// Use this for initialization
	void OnEnable()
	{
		GameObject bmGO = GameObject.FindWithTag("Music");
		beatMaster = bmGO.GetComponent<BeatMaster>();
		animator = GetComponent<Animator>();

		beatMaster.beatEvent += BeatMasterOnBeatEvent;
	}

	void OnDisable()
	{
		if (beatMaster != null)
			beatMaster.beatEvent -= BeatMasterOnBeatEvent;
	}

	private void BeatMasterOnBeatEvent()
	{
		animator.SetTrigger(BEAT);
	}
}
