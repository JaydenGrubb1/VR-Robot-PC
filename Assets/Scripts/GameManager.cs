using UnityEngine;
using Valve.VR;

namespace Robot
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;

		[SerializeField] private KeyCode resetPoseKey = KeyCode.R;

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

			Config.Load();
		}

		private void Update()
		{
			if (Input.GetKeyDown(resetPoseKey))
			{
				ResetPoseVR();
			}
		}

		public static void ResetPoseVR()
		{
			OpenVR.Chaperone.ResetZeroPose(ETrackingUniverseOrigin.TrackingUniverseStanding);
		}

		public static void Quit()
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
}