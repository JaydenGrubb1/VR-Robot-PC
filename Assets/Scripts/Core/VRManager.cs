using UnityEngine;
using Valve.VR;

namespace Robot
{
	public class VRManager : MonoBehaviour
	{
		private static VRManager instance;

		[SerializeField] private Transform headTracker;
		[SerializeField] private Transform leftHandTracker;
		[SerializeField] private Transform rightHandTracker;

		public static Transform HeadTracker { get { return instance.headTracker; } }
		public static Transform LeftHandTracker { get { return instance.leftHandTracker; } }
		public static Transform RightHandTracker { get { return instance.rightHandTracker; } }

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

		public static void ResetPose()
		{
			OpenVR.Chaperone.ResetZeroPose(ETrackingUniverseOrigin.TrackingUniverseStanding);
		}
	}
}