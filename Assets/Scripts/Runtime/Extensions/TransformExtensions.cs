using GraffitiDrawingVR.Runtime.Core;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Extensions
{
	public static class TransformExtensions
	{
		public static Vector3 GetDirectionByAxis(this Transform transform, Axis axis)
		{
			switch (axis)
			{
				case Axis.X:
					return transform.right;
				case Axis.NegX:
					return -transform.right;
				case Axis.Y:
					return transform.up;
				case Axis.NegY:
					return -transform.up;
				case Axis.Z:
					return transform.forward;
				case Axis.NegZ:
					return -transform.forward;
			}

			throw new System.Exception("Invalid axis value!");
		}
	}
}