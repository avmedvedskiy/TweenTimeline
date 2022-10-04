using UnityEngine;
using UnityEngine.Playables;

public class ChangeEndValue : MonoBehaviour
{
    [SerializeField] private PlayableDirector[] _directors;
    [SerializeField] private Transform _endValue;
    [SerializeField] private string _endRefName;

    public void OnEnable()
    {
        ChangeValue();
    }

    [ContextMenu(nameof(ChangeValue))]
    public void ChangeValue()
    {
        foreach (var director in _directors)
        {
            director.SetReferenceValue(new PropertyName(_endRefName), _endValue);
        }
    }

    private void OnValidate()
    {
        _directors = GetComponentsInChildren<PlayableDirector>();
    }
}