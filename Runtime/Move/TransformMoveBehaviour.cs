using System;
using DG.Tweening;
using TweenExtension;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class TransformMoveBehaviour : TweenRunnerBehaviour<Transform>
    {
        [SerializeField] private ExposedReference<Transform> _endTransformReference = new(){exposedName = "End"};

        [SerializeField] private Easing _ease;
        [SerializeField] private AxisConstraint _axisConstraint;

        private Transform _endTransform;

        public override void Resolve(PlayableGraph graph, GameObject go)
        {
            base.Resolve(graph,go);
            _endTransform = _endTransformReference.Resolve(graph.GetResolver());
            Target ??= go.transform;
        }

        protected override Tween GenerateTween(float duration)
        {
            if (_endTransform == null)
                return null;
        
            var tweener = Target.DOMove(_endTransform.position, duration)
                .SetOptions(_axisConstraint)
                .SetEase(_ease);
            return tweener;
        }
    }
}