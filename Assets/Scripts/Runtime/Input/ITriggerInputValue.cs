namespace GraffitiDrawingVR.Runtime.Input
{
	public interface ITriggerInputValue
	{
		public float MaxTriggerValue
		{
			get;
			set;
		}

		public float GetTriggerValue();
	}
}