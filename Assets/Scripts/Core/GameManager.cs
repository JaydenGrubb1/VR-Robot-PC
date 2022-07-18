using UnityEngine;

namespace Robot
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;

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

		public static void Quit()
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
}