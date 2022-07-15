using System;
using UnityEngine;

namespace Robot.IK
{
	public class JointIK : MonoBehaviour
    {
		public bool useLimits = false;
		public Limits limits = new Limits(-60, 60);
		public Vector3 axis = Vector3.right;

		private float prevTheta = 0;
		public float currentTheta = 0;

		public void Rotate(float theta)
		{
			currentTheta += theta;

			if (useLimits)
			{
				currentTheta = Mathf.Clamp(currentTheta, limits.lower, limits.upper);
				theta = currentTheta - prevTheta;
			}

			transform.Rotate(axis, theta);
			prevTheta = currentTheta;
		}
	}

	[Serializable]
	public struct Limits
	{
		public float lower;
		public float upper;

		public Limits(float lower, float upper)
		{
			this.lower = lower;
			this.upper = upper;
		}
	}
}