using System;
using DG.Tweening;
using Timeline.Move;
using TweenExtension;
using UnityEngine;

namespace Timeline.Scale
{
    [Serializable]
    public class ScaleAnimate : TweenRunner<Transform>
    {
        [Tooltip("if negative value - will be used current")] [SerializeField]
        private float _fromValue = -1f;

        [SerializeField] private float _value;
        [SerializeField] private bool _relative;
        [SerializeField] private Easing _easing;

        protected override Tween GenerateTween(float duration)
        {
            Vector3 finishScale = _relative ? Target.localScale * _value : Vector3.one * _value;

            if (_fromValue >= 0f)
                Target.localScale = Vector3.one * _fromValue;
            return Target
                .DOScale(finishScale, duration)
                .SetEase(_easing);
        }
    }
}