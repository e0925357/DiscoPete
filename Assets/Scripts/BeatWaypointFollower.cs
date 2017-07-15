using System;
using UnityEngine;

namespace Assets.Scripts
{
	public enum WaypointMovementMode
	{
		JumpToPoint, Incremental
	}

	public enum WaypointWarppingMode
	{
		Loop, Bounce
	}

	public class BeatWaypointFollower : AbstractBeatable
	{
		private static readonly int SPEED = Animator.StringToHash("Speed");
		private static readonly int BEAT = Animator.StringToHash("Beat");

		private const float epsilon = 0.1f;
		public WaypointMovementMode movementMode;
		public WaypointWarppingMode wrappingMode;
		public AnimationCurve movementEasing;
		public Transform waypointsParent;
		public Transform objectTransform;

		private int waypointIndex = 1;
		private Vector3 startPos;
		private Vector3 endPos;
		private float timer = float.MaxValue;
		private int nextWaypointDirection = 1;
		private float movementTime = 0.2f;
		private Animator animator;

		protected override void OnEnable()
		{
			base.OnEnable();
			movementTime = 1f/beatMaster.songInfo.Bps;
			animator = GetComponent<Animator>();

			if (animator != null)
			{
				animator.SetFloat(SPEED, beatMaster.songInfo.Bps);
			}
		}

		void Update()
		{
			if (timer >= movementTime) return;

			timer = Mathf.Clamp(timer + Time.deltaTime, 0, movementTime);

			float t = movementEasing.Evaluate(timer/movementTime);

			objectTransform.position = Vector3.LerpUnclamped(startPos, endPos, t);
		}

		protected override void OnBeat()
		{
			if (animator != null)
			{
				animator.SetTrigger(BEAT);
			}

			int lastIndex;

			switch (movementMode)
			{
				case WaypointMovementMode.JumpToPoint:
					lastIndex = nextWaypoint();

					startPos = waypointsParent.GetChild(lastIndex).position;
					endPos = waypointsParent.GetChild(waypointIndex).position;
					break;
				case WaypointMovementMode.Incremental:
					float diff2End = (objectTransform.position - waypointsParent.GetChild(waypointIndex).position).sqrMagnitude;

					if (diff2End <= epsilon)
					{
						lastIndex = nextWaypoint();

						startPos = waypointsParent.GetChild(lastIndex).position;
						endPos = waypointsParent.GetChild(waypointIndex).position;

						float length = (startPos - endPos).magnitude;

						if (length > 1 + epsilon)
						{
							endPos = startPos + (endPos - startPos)/length;
						}
					}
					else if (diff2End <= 1 + epsilon)
					{
						startPos = objectTransform.position;
						endPos = waypointsParent.GetChild(waypointIndex).position;
					}
					else
					{
						startPos = objectTransform.position;
						endPos = startPos + (waypointsParent.GetChild(waypointIndex).position - startPos).normalized;
					}

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			timer = 0;
		}

		private int nextWaypoint()
		{
			int lastIndex = waypointIndex;
			waypointIndex += nextWaypointDirection;

			if (waypointIndex >= waypointsParent.childCount)
			{
				switch (wrappingMode)
				{
					case WaypointWarppingMode.Loop:
						waypointIndex = 0;
						break;
					case WaypointWarppingMode.Bounce:
						waypointIndex -= 2;
						nextWaypointDirection = -1;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else if (waypointIndex < 0)
			{
				switch (wrappingMode)
				{
					case WaypointWarppingMode.Loop:
						waypointIndex = waypointsParent.childCount - 1;
						break;
					case WaypointWarppingMode.Bounce:
						waypointIndex += 2;
						nextWaypointDirection = 1;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return lastIndex;
		}

		void OnDrawGizmos()
		{
			if (waypointsParent == null) return;

			Gizmos.color = Color.blue;

			for (int i = 1; i < waypointsParent.childCount; ++i)
			{
				Gizmos.DrawLine(waypointsParent.GetChild(i-1).position, waypointsParent.GetChild(i).position);
			}
		}
	}
}