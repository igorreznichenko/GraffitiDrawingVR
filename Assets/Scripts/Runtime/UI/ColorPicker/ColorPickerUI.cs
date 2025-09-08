using GraffitiDrawingVR.Extensions;
using GraffitiDrawingVR.Runtime.Extensions;
using GraffitiDrawingVR.Runtime.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace GraffitiDrawingVR
{
	public class ColorPickerUI : UIBase
	{
		[SerializeField]
		private FlexibleColorPicker _colorPicker;

		[SerializeField]
		private CanvasGroup _canvasGroup;

		public Color ActiveColor
		{
			get => _colorPicker.color;
			set
			{
				if (_colorPicker.color != value)
				{
					_colorPicker.color = value;
				}
			}
		}

		public event UnityAction<Color> ColorUpdatedEvent
		{
			add => _colorPicker.onColorChange.AddListener(value);
			remove => _colorPicker.onColorChange.RemoveListener(value);
		}

		public override Task Show()
		{
			_canvasGroup.ShowImmediately();
			return Task.CompletedTask;
		}

		public void ShowAndForget()
		{
			Show().Except();
		}

		public override Task Hide()
		{
			_canvasGroup.HideImmediately();
			return Task.CompletedTask;
		}

		public void HideAndForget()
		{
			Hide().Except();
		}
	}
}
