using GraffitiDrawingVR.Runtime.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Drawing
{
	public class Drawable : MonoBehaviour
	{
		[SerializeField]
		private int _textureSize = 1024;

		[SerializeField]
		private RenderTexture _output;

		private RenderTexture _temp1;

		private RenderTexture _temp2;

		private List<RenderTexture> _renderTextures = new List<RenderTexture>();

		[SerializeField]
		private Color _initialColor = Color.gray;

		[SerializeField]
		private Material fillCrack;

		[SerializeField]
		private string TextureMaterialProperty = "_MainTex";

		[SerializeField]
		private RenderTextureFormat _renderTextureFormat = RenderTextureFormat.ARGBHalf;

		private Renderer _renderer;

		private MeshFilter _meshFilter;

		private const string DRAWER_SHADER_MAIN_TEX_KEYWORD = "_MainTex";

		private Texture _originalTexture;

		void Start()
		{
			_renderer = GetComponent<Renderer>();

			_meshFilter = GetComponent<MeshFilter>();

			_originalTexture = _renderer.material.GetTexture(TextureMaterialProperty);

			if (_originalTexture == null)
			{
				_output = new RenderTexture(_textureSize, _textureSize, 0, _renderTextureFormat);
				_output.Create();
				_output.Fill(_initialColor);
			}
			else
			{
				_output = new RenderTexture(_originalTexture.width, _originalTexture.height, 0, _renderTextureFormat);
				_output.Create();
				Graphics.Blit(_originalTexture, _output);
			}

			_renderer.material.SetTexture(TextureMaterialProperty, _output);

			_temp1 = new RenderTexture(_output);
			_temp2 = new RenderTexture(_output);

			_temp1.Create();
			_temp2.Create();

			Graphics.Blit(_output, _temp1);

			_renderTextures.Add(_temp1);
			_renderTextures.Add(_temp2);
		}

		public void Draw(Material drawingMat)
		{
			drawingMat.SetTexture(DRAWER_SHADER_MAIN_TEX_KEYWORD, _renderTextures[0]);

			RenderTexture previousTexture = RenderTexture.active;

			RenderTexture.active = _renderTextures[1];

			GL.Clear(true, true, Color.clear);

			drawingMat.SetPass(0);

			Graphics.DrawMeshNow(_meshFilter.sharedMesh, _meshFilter.transform.localToWorldMatrix);

			RenderTexture.active = previousTexture;

			Graphics.Blit(_renderTextures[1], _output);

			_renderTextures.Reverse();
		}

		public void Clear()
		{
			if (_originalTexture != null)
			{
				Graphics.Blit(_originalTexture, _output);
			}
			else
			{
				_output.Fill(_initialColor);
			}

			Graphics.Blit(_output, _renderTextures[0]);
		}

		public Texture2D CopyToTexture2D(int width, int height, TextureFormat textureFormat)
		{
			RenderTexture renderTexture = new RenderTexture(width, height, 0);
			renderTexture.Create();

			Graphics.Blit(_output, renderTexture);

			RenderTexture currentActive = RenderTexture.active;

			Texture2D result = new Texture2D(width, height, textureFormat, false);

			result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			result.Apply();

			RenderTexture.active = currentActive;

			renderTexture.Release();

			return result;
		}

		public void SetTexture(Texture2D image)
		{
			Graphics.Blit(image, _renderTextures[0]);
			Graphics.Blit(image, _output);
		}

		private void OnDestroy()
		{
			_output.Release();
			_temp1.Release();
			_temp2.Release();
		}
	}
}