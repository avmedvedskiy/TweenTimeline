
#if UNITY_EDITOR
using DG.DOTweenEditor;
#endif
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

[Serializable]
public abstract class TweenRunnerBehaviourTarget<T> : TweenRunnerBehaviour where T : Object
{
    [SerializeField] private ExposedReference<T> _target;
    protected T Target { get; set; }

    public override void Resolve(PlayableGraph graph, GameObject go)
    {
        Target = _target.Resolve(graph.GetResolver());
    }
}

public abstract class TweenRunnerBehaviour : PlayableBehaviour
{
    private Tweener _tweener;
    private bool _isInit;

    public abstract void Resolve(PlayableGraph graph, GameObject go);
    

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);
        SaveDefaultState();
    }

    public override void OnGraphStop(Playable playable)
    {
        base.OnGraphStop(playable);
        Reset();
        _isInit = false;
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        SaveDefaultState();
        
        float duration = (float)playable.GetDuration();
        _tweener ??= GenerateTween(duration);
        _tweener.Restart();

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DOTweenEditorPreview.PrepareTweenForPreview(_tweener, true, true, false);
            if (info.evaluationType == FrameData.EvaluationType.Playback)
                DOTweenEditorPreview.Start();
        }
#endif
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        _tweener?.Kill(true);

#if UNITY_EDITOR
        if (DOTweenEditorPreview.isPreviewing)
            DOTweenEditorPreview.Stop();
#endif
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
#if UNITY_EDITOR
        if (Application.isPlaying)
            return;

        if (!DOTweenEditorPreview.isPreviewing)
        {
            _tweener?.Goto((float)playable.GetTime());
        }
#endif
    }

    private void Reset()
    {
        _tweener?.Kill(true);

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            RestoreFromDefaultState();
        }
#endif
    }

    private void SaveDefaultState()
    {
        if (!_isInit)
        {
            OnSafeDefaultState();
            _isInit = true;
        }
    }

    private void RestoreFromDefaultState()
    {
        if (_isInit)
        {
            OnRestoreFromDefaultState();
        }
    }

    protected abstract Tweener GenerateTween(float duration);

    protected abstract void OnSafeDefaultState();

    protected abstract void OnRestoreFromDefaultState();
}