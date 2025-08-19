using GraffitiDrawingVR.Runtime.Drawing;
using UnityEditor;
using UnityEngine;

namespace GraffitiDrawingVR.EditorScripts
{
    [CustomEditor(typeof(DrawableController))]
    public class DrawableControllerEditor : Editor
    {
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			DrawableController drawableController = (DrawableController)target;

			if(GUILayout.Button("Fill Drawables"))
			{
				drawableController.FillDrawables();
			}
		}
    }
}
