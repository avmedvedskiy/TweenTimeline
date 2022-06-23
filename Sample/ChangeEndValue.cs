using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChangeEndValue : MonoBehaviour
{
    public PlayableDirector director;
    public Transform endValue;
    public string endRefName;

    public void OnEnable()
    {
        //director.stopped += _ => Debug.Log("stopped");
        ChangeValue();
    }

    [ContextMenu(nameof(ChangeValue))]
    public void ChangeValue()
    {
        director.SetReferenceValue(new PropertyName(endRefName), endValue);
        director.Play();
    }

    public void OnFlyComplete()
    {
        Debug.Log("Done");
    }
}