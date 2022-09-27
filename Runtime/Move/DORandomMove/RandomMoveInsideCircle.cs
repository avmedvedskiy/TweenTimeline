using System;
using DG.Tweening;
using Timeline.Move;
using TweenExtension;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Timeline.RandomMove
{
    [Serializable]
    public class RandomMoveInsideCircle : TweenRunner<Transform>
    {
        [SerializeField] private float _minRandom = 1f;
        [SerializeField] private float _maxRandom = 1f;
        [Tooltip("If u want get random on circle edges")]
        [SerializeField] private bool _normalized;
        [SerializeField] private Easing _easing;


        protected override Tween GenerateTween(float duration)
        {
            Vector3 endValue = Random.insideUnitCircle;
            if (_normalized)
                endValue.Normalize();
            endValue = endValue * Random.Range(_minRandom, _maxRandom) + Target.localPosition;
            return Target
                .DOLocalMove(endValue, duration)
                .SetEase(_easing);
        }
    }
}