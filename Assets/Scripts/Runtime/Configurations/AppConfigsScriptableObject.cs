using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Configurations
{
	[CreateAssetMenu(fileName = "AppConfigs", menuName = "Scriptable Objects/App Configurations", order = 0)]
	public class AppConfigsScriptableObject : ScriptableObject
	{
		[SerializeField]
		private AppConfigs _appConfigs;

		public AppConfigs AppConfigs => _appConfigs;
	}
}
