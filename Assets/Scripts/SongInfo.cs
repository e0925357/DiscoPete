using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "SongInfo", menuName = "Song Information", order = 0)]
	public class SongInfo : ScriptableObject
	{
		public AudioClip song;
		public int bpm;
		public float offset;

		public float Bps { get { return bpm / 60f; } }
	}
}