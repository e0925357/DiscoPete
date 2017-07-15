using UnityEngine;

namespace Assets.Scripts
{
	public class SpikeTrap : AbstractTrap
	{
		private static readonly int ACTIVE = Animator.StringToHash("Active");

		private bool active = false;
		private Animator animator;

		protected override void Start()
		{
			base.Start();
			animator = GetComponent<Animator>();
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