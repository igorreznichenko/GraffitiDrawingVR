using GraffitiDrawingVR.Runtime.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace GraffitiDrawingVR.Runtime.Entry
{
	public class MainSceneEntryPoint : EntryPointBase
	{
		[SerializeField]
		private DrawableController _drawableController;

		[SerializeField]
		private XRBaseInteractable _resetButton;

		[Header("Image Saving")]
		[SerializeField]
		private DrawableImageSaver _imageSaver;

		[SerializeField]
		private Drawing.Drawable _mainDrawable;

		[SerializeField]
		private Button _saveImage;

		protected override async Task Initialize()
		{
			await base.Initialize();

			_imageSaver.Init(_mainDrawable);
		}

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
			_saveImage.onClick.AddListener(OnSaveImageButtonClickEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_resetButton.selectEntered.RemoveListener(OnResetButtonClickEventHandler);
			_saveImage.onClick.RemoveListener(OnSaveImageButtonClickEventHandler);
		}

		private void OnResetButtonClickEventHandler(SelectEnterEventArgs args)
		{
			_drawableController.ClearDrawables();
		}

		private void OnSaveImageButtonClickEventHandler()
		{
			_imageSaver.SaveImage();
		}
	}
}
