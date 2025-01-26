using System;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.VFX
{
	public abstract class Fader : MonoBehaviour
	{
		public abstract void FadeIn(Action callback);

		public abstract void FadeOut(Action callback);
	}
}
