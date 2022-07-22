using System;
using DG.Tweening;
using TweenExtension;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move.Bezier
{
    [Serializable]
    public class ScaleAnimate : TweenRunner<Transform>
    {
        [Tooltip("if negative value - will be used current")] [SerializeField]
        private float _fromValue = -1f;

        [SerializeField] private float _value;
        [SerializeField] private Easing _easing;

        protected override Tween GenerateTween(float duration)
        {
            if (_fromValue >= 0f)
                Target.localScale = Vector3.one * _fromValue;

            return Target
                .DOScale(_value, duration)
                .SetEase(_easing);
        }
    }
}