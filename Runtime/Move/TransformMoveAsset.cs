using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class TransformMoveAsset : PlayableAsset
    {
        [SerializeField] private TransformMoveBehaviour _runnerBehaviour;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            _runnerBehaviour.Resolve(graph, go);
            return ScriptPlayable<TransformMoveBehaviour>.Create(graph, _runnerBehaviour);
        }
    }

}