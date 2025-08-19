using UnityEditor;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Drawing
{
	public class DrawableController : MonoBehaviour
	{
		[SerializeField]
		private Drawable[] _drawables;

		public void ClearDrawables()
		{
			foreach (var drawable in _drawables)
			{
				drawable.Clear();
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Fill Drawables")]
		private void FillDrawables()
		{
			_drawables = FindObjectsOfType<Drawable>();
			EditorUtility.SetDirty(this);
		}
#endif
	}
}