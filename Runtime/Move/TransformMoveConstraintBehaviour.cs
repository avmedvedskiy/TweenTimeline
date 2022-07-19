using System;
using System.Collections.Generic;
using DG.Tweening;
using TweenExtension;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class TransformMoveConstraintBehaviour : TweenRunnerBehaviour<Transform>
    {
        [SerializeField] private ExposedReference<Transform> _endTransformReference = new() { exposedName = "End" };

        [SerializeField] private Easing _x;
        [SerializeField] private Easing _y;
        [SerializeField] private Easing _z;

        private Transform _endTransform;

        public override void Resolve(PlayableGraph graph, GameObject go)
        {
            base.Resolve(graph, go);
            _endTransform = _endTransformReference.Resolve(graph.GetResolver());
            Target ??= go.transform;
        }

        protected override Tween GenerateTween(float duration)
        {
            if (_endTransform == null)
                return null;

            var sequence = DOTween.Sequence();
            var position = _endTransform.position;
            sequence
                .Join(Target
                    .DOMoveX(position.x, duration)
                    .SetEase(_x))
                .Join(Target
                    .DOMoveY(position.y, duration)
                    .SetEase(_y))
                .Join(Target
                    .DOMoveZ(position.z, duration)
                    .SetEase(_z));
            return sequence;
        }
    }
}