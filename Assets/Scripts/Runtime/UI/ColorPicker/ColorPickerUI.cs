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
		private GameObject _container;

		[SerializeField]
		private FlexibleColorPicker _colorPicker;

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

		public override Task Hide()
		{
			_container.SetActive(false);
			return Task.CompletedTask;
		}

		public void HideAndForget()
		{
			Hide().Except();
		}

		public override Task Show()
		{
			_container.SetActive(true);
			return Task.CompletedTask;
		}

		public void ShowAndForget()
		{
			Show().Except();
		}
	}
}
