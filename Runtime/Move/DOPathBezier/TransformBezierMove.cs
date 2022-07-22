using System;
using DG.Tweening;
using TweenExtension;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move.Bezier
{
    [Serializable]
    public class TransformBezierMove : TweenRunner<Transform>, ISceneViewHandler
    {
        [SerializeField] private ExposedReference<Transform> _endTransformReference = new() { exposedName = "End" };

        public Vector3 startTangent;
        public Vector3 endTangent;

        [SerializeField] private Easing _easing;

        private Transform _endTransform;
        private Vector3 _initialPosition;


        public override void Resolve(PlayableGraph graph, GameObject go, UnityEngine.Object target)
        {
            base.Resolve(graph, go, target);
            _endTransform = _endTransformReference.Resolve(graph.GetResolver());
        }

        protected override void OnSetTarget()
        {
            base.OnSetTarget();
            _initialPosition = Target.position;
        }

        protected override Tween GenerateTween(float duration)
        {
            if (_endTransform == null)
                return null;

            return Target
                .DOPath(
                    new[]
                    {
                        _endTransform.position,
                        Target.TransformPoint(startTangent),
                        _endTransform.TransformPoint(endTangent)
                    },
                    duration, PathType.CubicBezier)
                .SetEase(_easing);
        }

        public void OnSceneGUI()
        {
#if UNITY_EDITOR
            if(_endTransform == null)
                return;
            
            _endTransform.position = Handles.PositionHandle(_endTransform.position, _endTransform.rotation);

            startTangent =
                _initialPosition.InverseTransformPoint(Handles.PositionHandle(_initialPosition.TransformPoint(startTangent),
                    Quaternion.identity));
            
            endTangent =
                _endTransform.InverseTransformPoint(Handles.PositionHandle(_endTransform.TransformPoint(endTangent),
                    Quaternion.identity));

            Handles.DrawBezier(
                _initialPosition,
                _endTransform.position,
                _initialPosition.TransformPoint(startTangent),
                _endTransform.TransformPoint(endTangent),
                Color.red,
                null,
                2f);
#endif
        }
    }
}