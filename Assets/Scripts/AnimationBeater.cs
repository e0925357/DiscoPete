using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AnimationBeater : AbstractBeatable
{

	private static readonly int BEAT = Animator.StringToHash("Beat");
	private Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	protected override void OnBeat()
	{
		animator.SetTrigger(BEAT);
	}
}
