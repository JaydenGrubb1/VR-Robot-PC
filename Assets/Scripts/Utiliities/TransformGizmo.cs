using UnityEngine;

namespace Robot.Utilities
{
	public class TransformGizmo : MonoBehaviour
	{
		public bool gizmoEnabled = true;
		public Color color = Color.white;
		public float radius = 1;

		private void OnDrawGizmos()
		{
			if(!gizmoEnabled)
				return;

			Gizmos.color = color;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}