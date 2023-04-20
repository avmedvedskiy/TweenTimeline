using System;
using DG.Tweening;
using Timeline.Move;
using TweenExtension;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

namespace Timeline.Fade
{
    [Serializable]
    public class CanvasGroupFade : TweenRunner<CanvasGroup>
    {
        [SerializeField] private float _toValue;
        [SerializeField] private Easing _ease;


        protected override Tween GenerateTween(float duration)
        {
            return Target.DOFade(_toValue, duration)
                .SetEase(_ease);
        }
    }
}