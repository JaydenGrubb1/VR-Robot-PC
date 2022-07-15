using System.Collections.Generic;
using UnityEngine;

public class TestPattern : MonoBehaviour
{
    public Transform targetHandle;
    public float switchDelay = 3f;

    private List<Transform> targets = new List<Transform>();
    private float timeCounter = 0;
    private int targetIndex = 0;

    private void Start()
    {
        targets.AddRange(GetComponentsInChildren<Transform>());
        targets.Remove(transform);
    }

    private void Update()
    {
        if(timeCounter <= 0)
		{
            timeCounter = switchDelay;
            
            targetIndex++;
            if(targetIndex >= targets.Count)
                targetIndex = 0;

            targetHandle.position = targets[targetIndex].position;
		}

        timeCounter -= Time.deltaTime;
    }
}
