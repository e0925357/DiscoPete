using UnityEngine;

namespace Assets.Scripts
{
	public class SpikeTrap : AbstractTrap
	{
		private static readonly int ACTIVE = Animator.StringToHash("Active");
		private static readonly int SPEED = Animator.StringToHash("Speed");

		private bool active = false;
		private Animator animator;

		protected override void Start()
		{
			base.Start();
			animator = GetComponent<Animator>();
            if(animator == null)
                animator = GetComponentInChildren<Animator>();

            animator.SetFloat(SPEED, beatMaster.songInfo.Bps);
		}

		protected override bool Active
		{
			get
			{
				return active;
			}
			set
			{
				active = value;
				animator.SetBool(ACTIVE, value);
			}
		}
	}
}