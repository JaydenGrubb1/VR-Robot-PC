using UnityEngine;

namespace Robot.IK
{
	public class SolverIK : MonoBehaviour
    {
        public Transform end;
        public Transform target;
        public float rate = 5;
        public float threshhold = 1;

        private JointIK[] joints;

        private float CalculateSlope(JointIK joint)
		{
            float theta = 0.01f;

            float startDist = Vector3.Distance(target.position, end.position);
            joint.Rotate(theta);

            float endDist = Vector3.Distance(target.position, end.position);
            joint.Rotate(-theta);

            return (endDist - startDist) / theta;
		}

        private void Start()
        {
            joints = GetComponentsInChildren<JointIK>();
        }

        private void Update()
        {
            if (Vector3.Distance(end.position, target.position) > threshhold)
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