using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Robot.UI
{
	public class RecenterPromt : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvas;
		[SerializeField] private float duration = 1f;
		[SerializeField] private Ease ease = Ease.InOutSine;
		[SerializeField] private Transform cameraRig;
		[SerializeField] private float threshold = 0.45f;

		private bool doShow = false;

		private void Start()
		{
			DOTween.To(x =>
			{
				if (doShow)
					canvas.alpha = x;
				else
					canvas.alpha = 0;
			}, 1, 0, duration)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(ease);
		}

		private void Update()
		{
			doShow = !(Mathf.Abs(1 - Vector3.Dot(cameraRig.forward, Vector3.forward)) < threshold);
		}
	}
}