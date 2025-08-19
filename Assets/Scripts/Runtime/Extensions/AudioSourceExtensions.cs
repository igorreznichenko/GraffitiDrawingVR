using GraffitiDrawingVR.Runtime.Audio;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Extensions
{
	public static class AudioSourceExtensions
	{
		public static void SetSound(this AudioSource audioSource, Sound sound)
		{
			audioSource.clip = sound.AudioClip;
			audioSource.volume = sound.Volume;
			audioSource.loop = sound.Loop;
		}
	}
}
