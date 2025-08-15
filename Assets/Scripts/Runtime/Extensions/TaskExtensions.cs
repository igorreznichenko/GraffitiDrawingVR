using System.Threading.Tasks;
using UnityEngine;

namespace GraffitiDrawingVR.Extensions
{
	public static class TaskExtensions
	{
		public static void Except(this Task task)
		{
			task.ContinueWith(t =>
			{
				if (t.Exception != null)
				{
					Debug.Log(t.Exception);
				}
			}, TaskContinuationOptions.NotOnCanceled);
		}
	}
}
