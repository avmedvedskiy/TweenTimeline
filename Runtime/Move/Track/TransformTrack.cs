using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Timeline.Move
{    
    [TrackColor(1.0f, 0.0f, 0.0f)]
    [TrackBindingType(typeof(Transform))]
    [TrackClipType(typeof(ISupportTweenTrack))]
    public class TransformTrack : BaseTrack
    {
        
    }
}