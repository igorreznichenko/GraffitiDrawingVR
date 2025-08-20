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

		[SerializeField]
		private Button _loadImage;

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
			_loadImage.onClick.AddListener(OnLoadImageClickEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_resetButton.selectEntered.RemoveListener(OnResetButtonClickEventHandler);
			_saveImage.onClick.RemoveListener(OnSaveImageButtonClickEventHandler);
			_loadImage.onClick.RemoveListener(OnLoadImageClickEventHandler);
		}

		private void OnResetButtonClickEventHandler(SelectEnterEventArgs args)
		{
			_drawableController.ClearDrawables();
		}

		private void OnLoadImageClickEventHandler()
		{
			_imageSaver.LoadImage();
		}

		private void OnSaveImageButtonClickEventHandler()
		{
			_imageSaver.SaveImage();
		}
	}
}
