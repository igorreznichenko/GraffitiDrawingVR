using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Configurations
{
	public abstract class AppConfigsProvider : MonoBehaviour
	{
		public abstract Task<AppConfigs> LoadConfigurations();
	}
}
