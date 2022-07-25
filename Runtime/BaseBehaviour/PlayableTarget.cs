using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public abstract class PlayableTarget<T> : PlayableBehaviour, IResolve where T : Component
    {
        protected T Target { get; set; }

        public virtual void Resolve(PlayableGraph graph, GameObject go, UnityEngine.Object trackTarget)
        {
            Target = trackTarget as T;
        }

        //this methods only for previwing in editor mode
#if UNITY_EDITOR
        [NonSerialized] private bool _isInit;

        public override void OnGraphStart(Playable playable)
        {
            if (!Application.isPlaying && Target != null)
                SaveDefaultState();
        }

        public override void OnGraphStop(Playable playable)
        {
            if (!Application.isPlaying && Target != null)
                RestoreFromDefaultState();
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!Application.isPlaying && Target != null)
                SaveDefaultState();
        }

        private void SaveDefaultState()
        {
            //save component
            if (!_isInit)
            {
                CacheUtils.Save(Target);
                _isInit = true;
            }
        }

        private void RestoreFromDefaultState()
        {
            //Restore component
            if (_isInit)
            {
                CacheUtils.Restore(Target);
                CacheUtils.Remove(Target);
                _isInit = false;
            }
        }
#endif
    }
}