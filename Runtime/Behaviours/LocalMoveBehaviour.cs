using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;


[Serializable]
public class LocalMoveBehaviour : BaseTransformBehaviour
{
    [SerializeField] private ExposedReference<Transform> _endTransformReference = new(){exposedName = "End"};

    [SerializeField] private Ease _ease;
    [SerializeField] private AxisConstraint _axisConstraint;
    [SerializeField] private float _curveScale = 1f;

    private Transform _endTransform;

    public override void Resolve(PlayableGraph graph, GameObject go)
    {
        _endTransform = _endTransformReference.Resolve(graph.GetResolver());
        base.Resolve(graph,go);
    }

    protected override Tweener GenerateTween(float duration)
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