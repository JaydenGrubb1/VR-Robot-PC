using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.Utilities
{
	public static class Mathx
	{
		public static Vector3 IntersectionOfLines(Vector3 p1, Vector3 d1, Vector3 p2, Vector3 d2)
		{
			if (Mathf.Abs(Vector3.Dot(d1, d2)) == 1)
				throw new ArithmeticException("Lines do not intersect");

			Vector3 d3 = p2 - p1;
			Vector3 normal = Vector3.Cross(d1, d3);

			if (p2 + d2 != p2 + Vector3.ProjectOnPlane(d2, normal))
				throw new ArithmeticException("Lines do not intersect");

			float theta = Vector3.SignedAngle(d1, d3, normal);
			float phi = -Vector3.SignedAngle(-d1, -d2, normal);
			float dX = (d3.magnitude / Mathf.Sin(phi * Mathf.Deg2Rad))
				* Mathf.Sin(theta * Mathf.Deg2Rad);

			return p2 + (d2.normalized * dX);
		}

		public static Vector3 CenterOfSphere(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{


			return Vector3.zero;
		}
	}
}
