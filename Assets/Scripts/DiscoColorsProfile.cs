using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "DiscoColors", menuName = "DiscoColors", order = 0)]
	class DiscoColorsProfile : ScriptableObject
	{
		public Color[] DiscoColors = new Color[0];
	}
}
