using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.Utilities
{
	public static class Mathx
	{
		public static bool LogWarning { get; set; } = false;

		public static bool Approximately(Vector3 a, Vector3 b)
		{
			return Mathf.Approximately(a.x, b.x) &&
				Mathf.Approximately(a.y, b.y) &&
				Mathf.Approximately(a.z, b.z);
		}

		public static Vector3? IntersectionOfLines(Vector3 p1, Vector3 d1, Vector3 p2, Vector3 d2)
		{
			if (Mathf.Abs(Vector3.Dot(d1, d2)) == 1)
			{
				if(LogWarning)
					Debug.LogWarning("Lines do not intercept");

				return null;
			}

			Vector3 d3 = p2 - p1;
			Vector3 normal = Vector3.Cross(d1, d3);


			if (p2 + d2 != p2 + Vector3.ProjectOnPlane(d2, normal))
			{
				if(LogWarning)
					Debug.LogWarning("Lines do not intercept");

				return null;
			}

			float theta = Vector3.SignedAngle(d1, d3, normal);
			float phi = -Vector3.SignedAngle(-d1, -d2, normal);

			float dX = (d3.magnitude / Mathf.Sin(phi * Mathf.Deg2Rad))
				* Mathf.Sin(theta * Mathf.Deg2Rad);

			return p2 + (d2.normalized * dX);
		}

		public static Vector3? CenterOfSphere(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			if(a == b || a == c || a == d || b == c || b == d || c == d)
			{
				if (LogWarning)
					Debug.LogWarning("One or more points are coincident");

				return null;
			}

			Vector3 ab = b - a;
			Vector3 ac = c - a;

			if(Mathf.Abs(Vector3.Dot(ab, ac)) == 1)
			{
				if (LogWarning)
					Debug.LogWarning("Points A, B and C are collinear");

				return null;
			}


			Vector3 circleNormal = Vector3.Cross(ab, ac);
			Vector3 abMid = Vector3.Lerp(a, b, 0.5f);
			Vector3 acMid = Vector3.Lerp(a, c, 0.5f);

			Vector3? _centerLine = IntersectionOfLines(
				abMid, Vector3.Cross(ab, circleNormal),
				acMid, Vector3.Cross(ac, circleNormal));

			if (!_centerLine.HasValue)
				return null;

			Vector3 centerLine = _centerLine.Value;

			if(d == Vector3.ProjectOnPlane(d, centerLine))
			{
				if (LogWarning)
					Debug.LogWarning("Point D is coplanar with plane ABC");

				return null;
			}

			Vector3 dCirc = Vector3.ProjectOnPlane(d, circleNormal) + Vector3.Project(centerLine, circleNormal);
			Vector3 edge = ((dCirc - centerLine).normalized * (a - centerLine).magnitude) + centerLine;

			Vector3 edgeMid = Vector3.Lerp(edge, d, 0.5f);

			Vector3 edgeRight = Vector3.Cross(edgeMid - edge, edge - centerLine);
			Vector3 edgeIn = Vector3.Cross(edgeRight, edgeMid - edge);

			Vector3? center = IntersectionOfLines(centerLine, circleNormal, edgeMid, edgeIn);

			return center;
		}
	}
}