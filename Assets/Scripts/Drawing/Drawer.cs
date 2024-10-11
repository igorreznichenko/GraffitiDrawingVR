using UnityEngine;

namespace GraffitiDrawingVR.Drawing
{
	public class Drawer : MonoBehaviour
	{
		[SerializeField]
		private float _intencity = 1f;

		public float Intencity
		{
			get { return _intencity; }
			set { _intencity = value; }
		}

		[SerializeField]
		private Color color = Color.white;

		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		[Range(0.01f, 90f)]
		[SerializeField]
		private float _angle = 30f;

		public float Angle
		{
			get { return _angle; }
			set { _angle = value; }
		}

		[SerializeField]
		private float _range = 10f;

		public float Range
		{
			get { return _range; }
			set { _range = value; }
		}

		[SerializeField]
		private Material _drawingMat;

		[SerializeField]
		private Texture cookie;

		[SerializeField]
		private int shadowMapResolution = 1024;

		[SerializeField]
		private RenderTextureFormat _depthRenderTextureFormat = RenderTextureFormat.RFloat;

		[SerializeField]
		private Camera _camera;

		private Shader _depthRenderShader { get { return Shader.Find("Unlit/depthRender"); } }

		private RenderTexture depthOutput;

		private Camera Camera
		{
			get { return _camera; }
		}

		private void Start()
		{
			InitCamera(Camera);
		}

		private void InitCamera(Camera camera)
		{
			depthOutput = new RenderTexture(shadowMapResolution, shadowMapResolution, 16, _depthRenderTextureFormat);

			depthOutput.wrapMode = TextureWrapMode.Clamp;

			depthOutput.Create();

			camera.targetTexture = depthOutput;
			camera.SetReplacementShader(_depthRenderShader, "RenderType");
			camera.clearFlags = CameraClearFlags.Nothing;
			camera.nearClipPlane = 0.01f;
			camera.enabled = false;
		}

		public void UpdateDrawingMat()
		{
			var currentRt = RenderTexture.active;
			RenderTexture.active = depthOutput;
			GL.Clear(true, true, Color.white * Camera.farClipPlane);
			Camera.fieldOfView = _angle;
			Camera.nearClipPlane = 0.01f;
			Camera.farClipPlane = _range;
			Camera.Render();
			RenderTexture.active = currentRt;

			var projMatrix = Camera.projectionMatrix;
			var worldToDrawerMatrix = transform.worldToLocalMatrix;

			_drawingMat.SetVector("_DrawerPos", transform.position);
			_drawingMat.SetFloat("_Emission", _intencity * Time.smoothDeltaTime);
			_drawingMat.SetColor("_Color", color);
			_drawingMat.SetMatrix("_WorldToDrawerMatrix", worldToDrawerMatrix);
			_drawingMat.SetMatrix("_ProjMatrix", projMatrix);
			_drawingMat.SetTexture("_Cookie", cookie);
			_drawingMat.SetTexture("_DrawerDepth", depthOutput);
		}

		public void Draw(Drawable drawable)
		{
			UpdateDrawingMat();
			drawable.Draw(_drawingMat);
		}
	}
}