using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Configs
{
	[CreateAssetMenu(fileName = "AppConfigs", menuName = "Scriptable Objects/AppConfigs", order = 0)]
	public class AppConfigs : ScriptableObject
	{
		[SerializeField]
		private bool _showFps;

		public bool ShowFPS => _showFps;

		[SerializeField]
		private bool _useDeviceSimulator;

		public bool UseDeviceSimulator => _useDeviceSimulator;
	}
}
