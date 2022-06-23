using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public abstract class BaseTransformBehaviour : TweenRunnerBehaviourTarget<Transform>
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _startScale;

    public override void Resolve(PlayableGraph graph, GameObject go)
    {
        base.Resolve(graph,go);
        Target ??= go.transform;
    }

    protected override void OnSafeDefaultState()
    {
        if(Target == null)
            return;
        
        
        _startPosition = Target.position;
        _startRotation = Target.rotation;
        _startScale = Target.localScale;
    }

    protected override void OnRestoreFromDefaultState()
    {
        if(Target == null)
            return;
        
        Target.position = _startPosition;
        Target.rotation = _startRotation;
        Target.localScale = _startScale;
    }
}