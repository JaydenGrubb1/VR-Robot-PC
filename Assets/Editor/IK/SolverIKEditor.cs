using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Robot.IK
{
    [CustomEditor(typeof(SolverIK))]
    public class SolverIKEditor : Editor
    {
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			SolverIK solver = (SolverIK)target;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Find Components"))
				solver.FindComponents();

			if (GUILayout.Button("Clear"))
				solver.ClearComponents();

			GUILayout.EndHorizontal();
		}
	}
}