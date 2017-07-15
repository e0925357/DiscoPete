using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BeatLight : AbstractBeatable
{
	public Light lightSource;
	public Color[] colors;
	private int colorIndex = 0;

	protected override void OnBeat()
	{
		lightSource.color = colors[colorIndex];

		if (++colorIndex >= colors.Length)
		{
			colorIndex = 0;
		}
	}
}
