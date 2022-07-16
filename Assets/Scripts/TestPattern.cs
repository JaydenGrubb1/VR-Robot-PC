using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
	public class TestPattern : MonoBehaviour
	{
		public Transform targetHandle;
		[Range(0f, 10f)]
		public float switchDelay = 3f;

		private List<Transform> targets = new List<Transform>();
		private float timeCounter = 0;
		private int targetIndex = 0;

		[HideInInspector]
		public bool paused = false;

		private void Start()
		{
			targets.AddRange(GetComponentsInChildren<Transform>());
			targets.Remove(transform);
		}

		private void Increment(int amount)
		{
			timeCounter = switchDelay;

			targetIndex += amount;
			if (targetIndex >= targets.Count)
				targetIndex = 0;
			if (targetIndex < 0)
				targetIndex = targets.Count - 1;

			targetHandle.position = targets[targetIndex].position;
		}

		private void Update()
		{
			if (paused)
				return;

			if (timeCounter <= 0)
			{
				Increment(1);
			}

			timeCounter -= Time.deltaTime;
		}

		public void Prev()
		{
			Increment(-1);
		}

		public void Next()
		{
			Increment(1);
		}
	}
}