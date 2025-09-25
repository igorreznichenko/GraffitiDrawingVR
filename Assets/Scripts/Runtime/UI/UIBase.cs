using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.UI
{
	public abstract class UIBase : MonoBehaviour
	{
		public abstract Task Show();

		public abstract Task Hide();
	}
}
