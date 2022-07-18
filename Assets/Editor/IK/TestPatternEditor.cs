using UnityEditor;
using UnityEngine;

namespace Robot.IK
{
	[CustomEditor(typeof(TestPattern))]
	public class TestPatternEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			TestPattern pattern = (TestPattern)target;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Previous"))
				pattern.Prev();

			if (pattern.paused)
			{
				if (GUILayout.Button("Resume"))
					pattern.paused = false;
			}
			else
			{
				if (GUILayout.Button("Pause"))
					pattern.paused = true;
			}

			if (GUILayout.Button("Next"))
				pattern.Next();

			GUILayout.EndHorizontal();
		}
	}
}