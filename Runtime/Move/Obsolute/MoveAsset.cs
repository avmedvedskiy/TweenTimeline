using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    [Serializable]
    public class MoveAsset : BasePlayableAsset<Move>
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            Resolve(graph,owner,owner);
            return base.CreatePlayable(graph, owner);
        }
    }
}