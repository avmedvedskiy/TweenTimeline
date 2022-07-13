using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [System.Serializable]
    public class MoveAsset : PlayableAsset
    {
        [SerializeField] private MoveBehaviour _runnerBehaviour;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            _runnerBehaviour.Resolve(graph,go);
            return ScriptPlayable<MoveBehaviour>.Create(graph, _runnerBehaviour);
        }
    }
}