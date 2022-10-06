using System;
using Timeline.Move;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.SetPosition
{
    [Serializable]
    public class SetPositionByTarget : PlayableTarget<Transform>
    {
        [SerializeField] private ExposedReference<Transform> _endTransformReference = new() { exposedName = "End" };

        private Transform _endTransform;

        public override void Resolve(PlayableGraph graph, GameObject go, UnityEngine.Object trackTarget)
        {
            base.Resolve(graph, go, trackTarget);
            _endTransform = _endTransformReference.Resolve(graph.GetResolver());
        }
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            if(Target != null && _endTransform != null)
                Target.position = _endTransform.position;
        }
    }
}