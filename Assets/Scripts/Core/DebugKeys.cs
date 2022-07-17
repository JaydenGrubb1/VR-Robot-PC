using UnityEngine;

namespace Robot
{
	public class DebugKeys : MonoBehaviour
	{
		[SerializeField] private KeyCode resetPoseKey = KeyCode.R;

		private void Update()
		{
			if (Input.GetKeyDown(resetPoseKey))
			{
				VRManager.ResetPose();
			}
		}
	}
}