using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Timeline.Move
{
    public abstract class BaseTrack : TrackAsset
    {
        private Object Binding => outputs.FirstOrDefault().sourceObject;
        private Object _target;

        protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
        {
            var director = gameObject.GetComponent<PlayableDirector>();
            _target = director.GetGenericBinding(Binding);
            if(clip.asset is IResolve resolve)
                resolve.Resolve(graph,gameObject,_target);
            return base.CreatePlayable(graph, gameObject, clip);
        }

        //public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        //{
        //    _target = director.GetGenericBinding(Binding);
        //    base.GatherProperties(director, driver);
        //}
    }
}