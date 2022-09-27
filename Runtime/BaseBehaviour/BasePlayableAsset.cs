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


        [NonSerialized] private PlayableGraph _graph;
        [NonSerialized] private GameObject _owner;
        [NonSerialized] private UnityEngine.Object _trackTarget;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
#if UNITY_EDITOR
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
#endif
            var playable = ScriptPlayable<T>.Create(graph, _runnerBehaviour);
            if (playable.GetBehaviour() is IResolve resolve)
            {
                resolve.Resolve(_graph, _owner, _trackTarget);
            }

            return playable;
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
            _graph = graph;
            _owner = owner;
            _trackTarget = trackTarget;
        }
    }
}