using SFB;
using System.IO;
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
					return;
				}

				byte[] data = image.EncodeToPNG();

				await File.WriteAllBytesAsync(path, data);
			});
		}
	}
}
