using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public abstract class BasePlayableAsset<T> : PlayableAsset where T : class, IPlayableBehaviour, new()
    {
        [SerializeField] private T _runnerBehaviour;

        protected T Behaviour => _runnerBehaviour;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
#if UNITY_EDITOR
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
#endif
            if(_runnerBehaviour is IResolveBehaviour resolveBehaviour)
                resolveBehaviour.Resolve(graph,owner);
            
            return ScriptPlayable<T>.Create(graph, _runnerBehaviour);
        }

#if UNITY_EDITOR
        private void OnDestroy()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if(_runnerBehaviour is ISceneViewHandler handler)
                handler.OnSceneGUI();
        }
#endif
    }
}