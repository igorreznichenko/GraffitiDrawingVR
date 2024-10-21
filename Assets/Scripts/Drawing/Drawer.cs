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
		private Texture cookie;

		[SerializeField]
		private int shadowMapResolution = 1024;

		[SerializeField]
		private RenderTextureFormat _depthRenderTextureFormat = RenderTextureFormat.RFloat;

		[SerializeField]
		private Camera _camera;

		private Material _drawingMaterial;

		private Shader _depthRenderShader { get { return Shader.Find("Unlit/depthRender"); } }

		private Shader _spotDrawer { get { return Shader.Find("Hidden/SpotDrawer"); } }

		private RenderTexture depthOutput;

		private const float NEAR_CLIP_PLANE = 0.01f;

		private Camera Camera
		{
			get { return _camera; }
		}

		private void Start()
		{
			_drawingMaterial = new Material(_spotDrawer);

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
			Camera.nearClipPlane = NEAR_CLIP_PLANE;
			Camera.farClipPlane = _range;
			Camera.Render();
			RenderTexture.active = currentRt;

			var projMatrix = Camera.projectionMatrix;
			var worldToDrawerMatrix = transform.worldToLocalMatrix;

			_drawingMaterial.SetVector("_DrawerPos", transform.position);
			_drawingMaterial.SetFloat("_Emission", _intencity * Time.smoothDeltaTime);
			_drawingMaterial.SetColor("_Color", color);
			_drawingMaterial.SetMatrix("_WorldToDrawerMatrix", worldToDrawerMatrix);
			_drawingMaterial.SetMatrix("_ProjMatrix", projMatrix);
			_drawingMaterial.SetTexture("_Cookie", cookie);
			_drawingMaterial.SetTexture("_DrawerDepth", depthOutput);
		}

		public void Draw(Drawable drawable)
		{
			UpdateDrawingMat();
			drawable.Draw(_drawingMaterial);
		}
	}
}