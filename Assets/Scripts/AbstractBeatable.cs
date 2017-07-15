using UnityEngine;

namespace Assets.Scripts
{
	public abstract class AbstractBeatable : MonoBehaviour
	{
		private BeatMaster beatMaster;

		protected virtual void OnEnable()
		{
			GameObject bmGO = GameObject.FindWithTag("Music");
			beatMaster = bmGO.GetComponent<BeatMaster>();

			beatMaster.beatEvent += OnBeat;
		}

		protected virtual void OnDisable()
		{
			if (beatMaster != null)
				beatMaster.beatEvent -= OnBeat;
		}

		protected abstract void OnBeat();
	}
}