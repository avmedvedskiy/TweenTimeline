using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    public interface IResolve
    {
        void Resolve(PlayableGraph graph, GameObject owner, Object trackTarget);
    }
}