using System;
using Timeline.Move.Tools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public abstract class BasePlayableAsset<T> : PlayableAsset, IResolve where T : class, IPlayableBehaviour, new()
    {
        [NoFoldOut] [SerializeField] private T _runnerBehaviour;

        protected T Behaviour => _runnerBehaviour;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
#if UNITY_EDITOR
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
#endif
            return ScriptPlayable<T>.Create(graph, _runnerBehaviour);
        }

#if UNITY_EDITOR
        private void OnDestroy()
        {
#if UNITY_EDITOR
            SceneView.duringSceneGui -= OnSceneGUI;
#endif
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (_runnerBehaviour is ISceneViewHandler handler)
                handler.OnSceneGUI();
        }
#endif
        public void Resolve(PlayableGraph graph, GameObject owner, UnityEngine.Object trackTarget)
        {
            if (_runnerBehaviour is IResolve resolveBehaviour)
                resolveBehaviour.Resolve(graph, owner, trackTarget);
        }
    }
}