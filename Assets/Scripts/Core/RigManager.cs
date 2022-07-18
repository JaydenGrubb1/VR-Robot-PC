using Robot.IK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
	public class RigManager : MonoBehaviour
	{
		public int profileIndex = 0;
		[SerializeField] private Transform leftShoulder;
		[SerializeField] private Transform rightShoulder;
		[SerializeField] private float unitScaleFactor = 0.001f;

		[SerializeField] private Transform robotRig;
		[SerializeField] private Transform target;
		[SerializeField] private SolverIK robotSolver;

		public Config.ConfigProfile CurrentProfile { get; private set; }

		private void Start()
		{
			UpdateRigTransforms();
		}

		public void UpdateRigTransforms()
		{
			CurrentProfile = Config.GetProfile(profileIndex);
			leftShoulder.transform.position = transform.position + CurrentProfile.leftShoulderOffset * unitScaleFactor;
			rightShoulder.transform.position = transform.position + CurrentProfile.rightShoulderOffset * unitScaleFactor;
		}

		private void Update()
		{
			transform.position = VRManager.HeadTracker.position;
			target.position = VRManager.LeftHandTracker.position;
		}
	}
}