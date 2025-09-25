using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Configurations
{
	public class ScriptableObjectAppConfigsProvider : AppConfigsProvider
	{
		[SerializeField]
		private AppConfigsScriptableObject _appConfigs;

		public override Task<AppConfigs> LoadConfigurations()
		{
			return Task.FromResult(_appConfigs.AppConfigs);
		}
	}
}
