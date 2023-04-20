using System;
using DG.Tweening;
using Timeline.Move;
using TweenExtension;
using UnityEngine;
using UnityEngine.UI;

namespace Timeline.Fade
{
    [Serializable]
    public class ImageFade : TweenRunner<Image>
    {
        [SerializeField] private float _value;
        [SerializeField] private Easing _ease;
        protected override Tween GenerateTween(float duration)
        {
            return Target.DOFade(_value, duration)
                .SetEase(_ease);
        }
    }
}