using GraffitiDrawingVR.Runtime.Drawing;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GraffitiDrawingVR.Runtime.Entry
{
	public class MainSceneEntryPoint : EntryPointBase
	{
		[SerializeField]
		private DrawableController _drawableController;

		[SerializeField]
		private XRBaseInteractable _resetButton;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_resetButton.selectEntered.AddListener(OnResetButtonClickEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_resetButton.selectEntered.RemoveListener(OnResetButtonClickEventHandler);
		}

		private void OnResetButtonClickEventHandler(SelectEnterEventArgs args)
		{
			_drawableController.ClearDrawables();
		}
	}
}
