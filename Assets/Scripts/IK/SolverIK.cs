using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.IK
{
    public class SolverIK : MonoBehaviour
    {
        public Transform end;
        public Transform target;
        public float rate = 5;
        public float threshhold = 1;

        public List<JointIK> joints = new List<JointIK>();

        private float calculateSlope(JointIK joint)
		{
            float theta = 0.01f;

            float startDist = Vector3.Distance(target.position, end.position);
            joint.rotate(theta);

            float endDist = Vector3.Distance(target.position, end.position);
            joint.rotate(-theta);

            return (endDist - startDist) / theta;
		}

        private void Start()
        {
            joints.AddRange(GetComponentsInChildren<JointIK>());
        }

        private void Update()
        {
            if (Vector3.Distance(end.position, target.position) > threshhold)
            {
                foreach (JointIK joint in joints)
                {
                    float slope = calculateSlope(joint);
                    joint.rotate(-slope * rate);
                }
            }
        }
    }
}