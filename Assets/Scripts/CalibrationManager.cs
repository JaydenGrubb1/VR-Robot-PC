using Robot.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
	public class CalibrationManager : MonoBehaviour
	{
		public Transform headTracker;
		public Transform headOffset;

		private List<Vector3> samples = new List<Vector3>();
		private bool started = false;
		private bool done = false;
		private Vector3 average;

		// Update is called once per frame
		void Update()
		{
			if (Time.time > 3 && Time.time < 6)
			{
				if (!started)
					Debug.Log("Started calibration...");
				started = true;
				samples.Add(headTracker.position);
			}

			if (Time.time > 6 && !done)
			{
				done = true;
				Debug.Log("Finished calibrating...");
				Debug.Log("Calculating...");
				Calculate();
			}
		}

		private void Calculate()
		{
			average = Vector3.zero;
			int count = 0;
			int div = samples.Count / 4;

			for (int i = 0; i < div; i++)
			{
				Vector3? center = Mathx.CenterOfSphere(
					samples[i],
					samples[i + div],
					samples[i + (2 * div)],
					samples[i + (3 * div)]
					);

				if (center.HasValue)
				{
					average += center.Value;
					count++;
				}
			}

			if (count == 0)
				return;

			average /= count;
			headOffset.position = average;
		}
	}
}
