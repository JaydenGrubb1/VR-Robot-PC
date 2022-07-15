using UnityEngine;

public class TargetDebugger : MonoBehaviour
{
	public Color color = Color.white;
	public float radius = 1;

	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
