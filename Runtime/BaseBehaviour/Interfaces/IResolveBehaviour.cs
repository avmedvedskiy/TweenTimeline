using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Move
{
    public interface IResolveBehaviour
    {
        void Resolve(PlayableGraph graph, GameObject go);
    }
}