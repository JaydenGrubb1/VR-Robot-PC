using Robot.IK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
	public class LockTransforms : MonoBehaviour
	{
		[Tooltip("Leave null to lock this transform to target")]
		public Transform origin;
		public Transform target;
		public UpdateMethod updateMethod;

		private void Update()
		{
			if(updateMethod == UpdateMethod.Normal)
				DoTransformation();
		}

		private void FixedUpdate()
		{
			if (updateMethod == UpdateMethod.Fixed)
				DoTransformation();
		}

		private void LateUpdate()
		{
			if (updateMethod == UpdateMethod.Late)
				DoTransformation();
		}

		private void DoTransformation()
		{
			if (origin == null)
				transform.position = target.position;
			else
				origin.position = target.position;
		}
	}

	public enum UpdateMethod
	{
		Normal,
		Late,
		Fixed
	}
}