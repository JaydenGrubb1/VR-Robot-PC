using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Robot.IK
{
    [CustomEditor(typeof(JointIK))]
    public class JointIKEditor : Editor
    {
		ArcHandle lowerArc = new ArcHandle();
		ArcHandle upperArc = new ArcHandle();
		ArcHandle startArc = new ArcHandle();

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			JointIK joint = (JointIK)target;

			Vector3 newAxis = joint.axis;
			EditorGUI.BeginChangeCheck();
			GUILayout.BeginHorizontal();

			if (GUILayout.Button("Align X"))
				newAxis = Vector3.right;

			if (GUILayout.Button("Align Y"))
				newAxis = Vector3.up;

			if (GUILayout.Button("Align Z"))
				newAxis = Vector3.forward;

			if (GUILayout.Button("Flip"))
				newAxis *= -1;

			GUILayout.EndHorizontal();
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(joint, "Change Joint Axis");
				joint.axis = newAxis;
			}
		}

		private void OnSceneGUI()
		{
			JointIK joint = (JointIK)target;

			Vector3 cross = Vector3.Cross(joint.axis, Vector3.up);
			if (cross.sqrMagnitude == 0)
				cross = Vector3.forward;

			Handles.color = Handles.secondaryColor;
			Handles.ArrowHandleCap(0, joint.transform.position, Quaternion.LookRotation(cross), 5, EventType.Repaint);

			Handles.color = Handles.selectedColor;
			Handles.ArrowHandleCap(0, joint.transform.position, Quaternion.LookRotation(joint.axis), 10, EventType.Repaint);

			if (joint.useLimits)
			{

				//if (joint.limits.lower > 0)
				//	joint.limits.lower = 0;
				//if (joint.limits.lower < -(360 - joint.limits.upper))
				//	joint.limits.lower = -(360 - joint.limits.upper);
				//if (joint.limits.lower < -360)
				//	joint.limits.lower = -360;

				lowerArc.angle = joint.limits.lower;
				Vector3 norm1 = joint.axis;
				Matrix4x4 mat1 = Matrix4x4.TRS(joint.transform.position, Quaternion.LookRotation(cross, norm1), Vector3.one * 10);

				using (new Handles.DrawingScope(mat1))
				{
					EditorGUI.BeginChangeCheck();
					lowerArc.DrawHandle();
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(joint, "Change Joint Limits");
						joint.limits.lower = lowerArc.angle;
					}
				}

				//if (joint.limits.upper < 0)
				//	joint.limits.upper = 0;
				//if (joint.limits.upper > 360 + joint.limits.lower)
				//	joint.limits.upper = 360 + joint.limits.lower;

				upperArc.angle = joint.limits.upper;
				Vector3 norm2 = joint.axis;
				Matrix4x4 mat2 = Matrix4x4.TRS(joint.transform.position, Quaternion.LookRotation(cross, norm2), Vector3.one * 10);

				using (new Handles.DrawingScope(mat2))
				{
					EditorGUI.BeginChangeCheck();
					upperArc.DrawHandle();
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(joint, "Change Joint Limits");
						joint.limits.upper = upperArc.angle;
					}
				}
			}

			if (joint.startAngle < -180)
				joint.startAngle = -180;
			if (joint.startAngle > 180)
				joint.startAngle = 180;

			Handles.color = Color.black;

			startArc.angle = joint.startAngle;
			Vector3 norm3 = joint.axis;
			Matrix4x4 mat3 = Matrix4x4.TRS(joint.transform.position, Quaternion.LookRotation(cross, norm3), Vector3.one * 7);

			using (new Handles.DrawingScope(mat3))
			{
				EditorGUI.BeginChangeCheck();
				startArc.DrawHandle();
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(joint, "Change Start Angle");
					joint.startAngle = startArc.angle;
				}
			}

			SceneView.RepaintAll();
		}
	}
}