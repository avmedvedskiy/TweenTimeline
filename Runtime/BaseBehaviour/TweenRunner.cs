
#if UNITY_EDITOR
using DG.DOTweenEditor;
#endif
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine;

namespace Timeline.Move
{
    public abstract class TweenRunner<T> : PlayableTarget<T> where T : Component
    {
        private Tween _tweener;

        public override void OnGraphStop(Playable playable)
        {
            base.OnGraphStop(playable);
            _tweener?.Kill(true);
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            base.OnBehaviourPlay(playable,info);
            
            if(Target == null)
                return;

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

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
#if UNITY_EDITOR
            if (Application.isPlaying)
                return;

            if (!DOTweenEditorPreview.isPreviewing)
            {
                _tweener?.Goto((float)playable.GetTime());
            }
#endif
        }

        protected abstract Tween GenerateTween(float duration);
    }
}