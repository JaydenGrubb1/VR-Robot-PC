using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Valve.VR;
using UnityEngine.UI;

namespace Robot.UI
{
	public class RecenterPromt : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvas;
		[SerializeField] private float duration = 1f;
		[SerializeField] private Ease ease = Ease.InOutSine;
		[SerializeField] private Transform cameraRig;
		[SerializeField] private float threshold = 0.45f;
		[SerializeField] private float countdown = 2;
		[SerializeField] private Image progressBar;

		private bool doShow = false;
		private bool doCountdown = false;
		private bool doneCountdown = false;
		private float timeRemaining;

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

			bool btnA = SteamVR_Input.GetBooleanAction("ControllerA").GetState(SteamVR_Input_Sources.Any);
			bool btnB = SteamVR_Input.GetBooleanAction("ControllerB").GetState(SteamVR_Input_Sources.Any);

			if(btnA && btnB)
			{
				if (!doneCountdown)
				{
					if (doCountdown)
					{
						timeRemaining -= Time.deltaTime;
					}
					else
					{
						timeRemaining = countdown;
						doCountdown = true;
					}

					if (timeRemaining <= 0)
					{
						GameManager.ResetPoseVR();
						doneCountdown = true;
						doCountdown = false;
					}
				}
			}
			else
			{
				doCountdown = false;
				doneCountdown = false;
			}

			if (doCountdown)
				progressBar.fillAmount = 1.0f - (timeRemaining / countdown);
			else
				progressBar.fillAmount = 0.0f;
		}
	}
}