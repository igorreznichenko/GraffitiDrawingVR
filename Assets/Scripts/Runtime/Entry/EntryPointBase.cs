using GraffitiDrawingVR.Runtime.Configurations;
using GraffitiDrawingVR.Runtime.DebugScripts;
using GraffitiDrawingVR.Runtime.Extensions;
using GraffitiDrawingVR.Runtime.VFX;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

namespace GraffitiDrawingVR.Runtime.Entry
{
	public abstract class EntryPointBase : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		[SerializeField]
		private AppConfigsProvider _appConfigsProvider;

		[SerializeField]
		private XRDeviceSimulator _xrDeviceSimulator;

		[SerializeField]
		private FPSCounter[] _fpsCounters;

		private void Start()
		{
			Initialize().Except();
		}

		protected virtual async Task Initialize()
		{
			AppConfigs appConfigs = await _appConfigsProvider.LoadConfigurations();

			SetupConfigs(appConfigs);
		}

		private void SetupConfigs(AppConfigs appConfigs)
		{
			_xrDeviceSimulator.gameObject.SetActive(appConfigs.UseXRDeviceSimulator);

			foreach (var fpsCounter in _fpsCounters)
			{
				if (appConfigs.ShowFPSCounters)
				{
					fpsCounter.Show();
				}
				else
				{
					fpsCounter.Hide();
				}
			}
		}
	}
}
