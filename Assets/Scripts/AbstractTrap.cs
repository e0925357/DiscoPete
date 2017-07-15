using UnityEngine;

namespace Assets.Scripts
{
	public abstract class AbstractTrap : AbstractBeatable
	{
		public int activeTime = 2;
		public int inactiveTime = 3;
		public int startOffset = 0;
		public bool startActive;

		private int beatCount = 0;

		protected virtual void Start()
		{
			beatCount = startActive ? 0 : activeTime;
		}

		protected override void OnBeat()
		{
			if (startOffset > 0)
			{
				--startOffset;
				return;
			}

			++beatCount;

			if (beatCount >= activeTime + inactiveTime - 1)
			{
				beatCount = 0;
			}

			if (beatCount < activeTime != Active)
			{
				Active = beatCount < activeTime;
			}
		}

		protected abstract bool Active { get; set; }
	}
}