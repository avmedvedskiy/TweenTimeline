using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class TransformMoveConstraintAsset : PlayableAsset
    {
        [SerializeField] private TransformMoveConstraintBehaviour _runnerBehaviour;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            _runnerBehaviour.Resolve(graph, go);
            return ScriptPlayable<TransformMoveConstraintBehaviour>.Create(graph, _runnerBehaviour);
        }
    }

}