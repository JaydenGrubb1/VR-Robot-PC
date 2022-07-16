using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.IK
{
    public class TargetIK : MonoBehaviour
    {
		public Transform target;

		[Range(0f, 1f)]
		public float weight = 0.5f;
		public bool useRotation;

		private float initDist;
		private bool isCalculating = false;

		public float GetDistance()
		{
			if (useRotation)
				return Vector3.Distance(transform.eulerAngles, target.eulerAngles);
			else
				return Vector3.Distance(transform.position, target.position);
		}

		public void BeginGradient()
		{
			//Check if already calculating
			if (isCalculating)
				return;

			initDist = GetDistance();
			isCalculating = true;
		}

		public float EndGradient(float theta)
		{
			//Check if not calculating
			if(!isCalculating)
				return 0f;

			float gradient = (GetDistance() - initDist) / theta;
			isCalculating = false;
			return gradient;
		}
	}
}