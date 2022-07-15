using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.IK
{
    public class JointIK : MonoBehaviour
    {
		public float startAngle = 0;
		public bool useLimits = false;
		public Limits limits = new Limits(-60, 60);
		public Vector3 axis = Vector3.right;

		public float rotate(float theta)
		{
			transform.Rotate(axis, theta);
			return 0;
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