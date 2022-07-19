using System;
using DG.Tweening;
using UnityEngine;

namespace TweenExtension
{
    [Serializable]
    public class Easing
    {
        [SerializeField] private Ease _ease = Ease.OutQuad;

        [SerializeField]
        private AnimationCurve _curve = new(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        [SerializeField] private float _scale = 1f;

        public T SetEase<T>(T tween) where T : Tween
        {
            if (_ease == Ease.INTERNAL_Custom)
                tween.SetEase(_curve);
            else
                tween.SetEase(_ease);

            tween.easeOvershootOrAmplitude *= _scale;
            return tween;
        }
    }
}