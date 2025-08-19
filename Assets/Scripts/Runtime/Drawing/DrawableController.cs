using System;
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
			Array.ForEach(_drawables, (drawable) => drawable.Clear());
		}

#if UNITY_EDITOR
		public void FillDrawables()
		{
			_drawables = FindObjectsOfType<Drawable>();
			EditorUtility.SetDirty(this);
		}
#endif
	}
}