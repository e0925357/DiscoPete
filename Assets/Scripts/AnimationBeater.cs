using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AnimationBeater : AbstractBeatable
{

	private static readonly int BEAT = Animator.StringToHash("Beat");
	private static readonly int SPEED = Animator.StringToHash("Speed");
	private static readonly int RANDOM = Animator.StringToHash("Random");

	[SerializeField]
	private bool everyOtherBeat = true;
	[SerializeField]
	private bool hasRandomNumber = true;
	[SerializeField]
	private int maxRandomValue = 3;

	private Animator animator;
	private int lastnumber = 0;
	private int phase;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat(SPEED, beatMaster.songInfo.Bps);
		phase = Random.Range(0, 2);
	}

	protected override void OnBeat()
	{
		if (everyOtherBeat && beatMaster.NextBeat % 2 == phase) return;

		if (hasRandomNumber)
		{
			int rNumber = Random.Range(0, maxRandomValue + 1);
			if (rNumber == lastnumber)
			{
				rNumber = (rNumber + 1) % (maxRandomValue + 1);
			}

			lastnumber = rNumber;

			animator.SetInteger(RANDOM, lastnumber);
		}

		animator.SetTrigger(BEAT);
	}
}
