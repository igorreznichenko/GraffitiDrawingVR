using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Runtime.Extensions
{
	public static class TaskExtensions
	{
		public static void Except(this Task task)
		{
			task.ContinueWith(t =>
			{
				if (t.Exception != null)
				{
					UnityEngine.Debug.Log(t.Exception);
				}
			}, TaskContinuationOptions.NotOnCanceled);
		}
	}
}
