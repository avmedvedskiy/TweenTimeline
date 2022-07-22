using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class Move : TweenRunner<Transform>
    {
        [SerializeField] private ExposedReference<Transform> _endTransformReference = new(){exposedName = "End"};

        [SerializeField] private Ease _ease;
        [SerializeField] private AxisConstraint _axisConstraint;
        [SerializeField] private float _curveScale = 1f;

        private Transform _endTransform;

        public override void Resolve(PlayableGraph graph, GameObject owner, UnityEngine.Object trackTarget)
        {
            base.Resolve(graph,owner,trackTarget);
            _endTransform = _endTransformReference.Resolve(graph.GetResolver());
            Target ??= owner.transform;
        }

        protected override Tween GenerateTween(float duration)
        {
            if (_endTransform == null)
                return null;
        
            var tweener = Target.DOMove(_endTransform.position, duration)
                .SetOptions(_axisConstraint)
                .SetEase(_ease);
            tweener.easeOvershootOrAmplitude *= _curveScale;
            return tweener;
        }
    }
}