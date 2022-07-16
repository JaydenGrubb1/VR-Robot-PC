using UnityEngine;

namespace Robot.IK
{
	public class SolverIK : MonoBehaviour
	{
		public TargetIK primaryTarget;
		public float rate = 0.5f;
		public float threshhold = 1f;
		public int maxSteps = 10;

		public JointIK[] joints;
		public TargetIK[] targets;

		private float CalculateSlope(JointIK joint)
		{
			float theta = 0.01f;
			float slope = 0;
			float totalWeight = 0;

			foreach (TargetIK target in targets)
				target.BeginGradient();

			joint.Rotate(theta);

			foreach (TargetIK target in targets)
			{
				totalWeight += target.weight;
				slope += target.EndGradient(theta) * target.weight;
			}

			joint.Rotate(-theta);

			return slope / totalWeight;
		}

		public void FindComponents()
		{
			joints = GetComponentsInChildren<JointIK>();
			targets = GetComponentsInChildren<TargetIK>();
		}

		public void ClearComponents()
		{
			joints = null;
			targets = null;
		}

		private void Update()
		{
			for (int i = 0; i < maxSteps; i++)
			{
				if (primaryTarget.GetDistance() > threshhold)
				{
					foreach (JointIK joint in joints)
					{
						float slope = CalculateSlope(joint);
						joint.Rotate(-slope * rate);
					}
				}
			}
		}
	}
}