using SFB;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Drawing
{
	public class DrawableImageSaver : MonoBehaviour
	{
		[SerializeField]
		private int _width;

		[SerializeField]
		private int _height;

		[SerializeField]
		private TextureFormat _textureFormat;

		private Drawable _drawable;

		public void Init(Drawable drawable)
		{
			_drawable = drawable;
		}

		public void SaveImage()
		{
			Texture2D image = _drawable.CopyToTexture2D(_width, _height, _textureFormat);

			StandaloneFileBrowser.SaveFilePanelAsync("Save Image", "", "Image", "png", async (path) =>
			{
				if (string.IsNullOrEmpty(path))
				{
					Debug.LogError("Invalid file path!");
					return;
				}

				byte[] data = image.EncodeToPNG();

				await File.WriteAllBytesAsync(path, data);
			});
		}

		public void LoadImage()
		{
			StandaloneFileBrowser.OpenFilePanelAsync("Load Image", "", "png", false, async (paths) =>
			{
				Texture2D image = new Texture2D(1, 1);

				string path = paths.FirstOrDefault();

				if (string.IsNullOrEmpty(path))
				{
					Debug.LogError("Invalid file path!");
					return;
				}

				byte[] data = await File.ReadAllBytesAsync(path);

				image.LoadImage(data);

				_drawable.SetTexture(image);
			});
		}
	}
}
