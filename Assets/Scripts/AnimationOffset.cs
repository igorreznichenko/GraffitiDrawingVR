using UnityEngine;

namespace GraffitiDrawingVR
{
	public class AnimationOffset : StateMachineBehaviour
	{
		[Range(0f, 1f)]
		[SerializeField]
		private float _minAnimationOffset;

		[Range(0f, 1f)]
		[SerializeField]
		private float _maxAnimationOffset;

		private bool _hasRandomized = false;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);

			if (_hasRandomized)
			{
				return;
			}

			float normalizedTime = Random.Range(_minAnimationOffset, _maxAnimationOffset);

			animator.Play(stateInfo.shortNameHash, layerIndex, normalizedTime);

			_hasRandomized = true;
		}
	}
}
