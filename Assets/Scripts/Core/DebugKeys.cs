using UnityEngine;
using UnityEngine.InputSystem;

namespace Robot
{
	public class DebugKeys : MonoBehaviour
    {
		[SerializeField] private Key resetPoseKey;

        private void Update()
        {
			if (Keyboard.current[resetPoseKey].wasPressedThisFrame)
			{
				VRManager.ResetPose();
			}
        }
    }
}
