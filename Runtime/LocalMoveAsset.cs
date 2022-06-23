using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class LocalMoveAsset : PlayableAsset
{
    [SerializeField] private LocalMoveBehaviour _runnerBehaviour;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        _runnerBehaviour.Resolve(graph,go);
        return ScriptPlayable<LocalMoveBehaviour>.Create(graph, _runnerBehaviour);
    }
}

