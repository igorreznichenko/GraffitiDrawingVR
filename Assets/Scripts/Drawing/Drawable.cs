using System;
using UnityEngine;

namespace GraffitiDrawingVR.Drawing
{
	public class Drawable : MonoBehaviour
	{
		[SerializeField]
		private int _textureSize = 1024;

		[SerializeField]
		private RenderTexture _output;

		[SerializeField]
		private Color _initialColor = Color.gray;

		[SerializeField]
		private Material fillCrack;

		[SerializeField]
		private string TextureMaterialProperty = "_MainTex";

		[SerializeField]
		private RenderTextureFormat _rendrTextureFormat = RenderTextureFormat.ARGBHalf;

		private RenderTexture[] pingPongRts;
		private Mesh mesh;

		void Start()
		{
			_output = new RenderTexture(_textureSize, _textureSize, 0, _rendrTextureFormat);
			_output.Create();

			var r = GetComponent<Renderer>();
			r.material.SetTexture(TextureMaterialProperty, _output);

			pingPongRts = new RenderTexture[2];

			for (var i = 0; i < 2; i++)
			{
				var outputRt = new RenderTexture(_textureSize, _textureSize, 0, _rendrTextureFormat);
				outputRt.Create();
				RenderTexture.active = outputRt;
				GL.Clear(true, true, _initialColor);
				pingPongRts[i] = outputRt;
			}

			mesh = GetComponent<MeshFilter>().sharedMesh;

			Graphics.CopyTexture(pingPongRts[0], _output);
		}

		private void OnDestroy()
		{
			foreach (var rt in pingPongRts)
				rt.Release();
			_output.Release();
		}

		public void Draw(Material drawingMat)
		{
			drawingMat.SetTexture("_MainTex", pingPongRts[0]);

			var currentActive = RenderTexture.active;
			RenderTexture.active = pingPongRts[1];
			GL.Clear(true, true, Color.clear);

			drawingMat.SetPass(0);

			Graphics.DrawMeshNow(mesh, transform.localToWorldMatrix);

			RenderTexture.active = currentActive;

			Swap(pingPongRts);

			if (fillCrack != null)
			{
				Graphics.Blit(pingPongRts[0], pingPongRts[1], fillCrack);
				Swap(pingPongRts);
			}

			Graphics.CopyTexture(pingPongRts[0], _output);
		}

		void Swap<T>(T[] array)
		{
			var tmp = array[0];
			array[0] = array[1];
			array[1] = tmp;
		}
	}
}