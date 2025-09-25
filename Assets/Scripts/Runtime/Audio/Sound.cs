using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Audio
{
	[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable Objects/Sound", order = 0)]
	public class Sound : ScriptableObject
	{
		[SerializeField]
		private AudioClip _audioClip;

		public AudioClip AudioClip => _audioClip;

		[SerializeField]
		private float _volume;

		public float Volume => _volume;

		[SerializeField]
		private bool _loop;

		public bool Loop => _loop;
	}
}
