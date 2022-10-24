using UnityEngine;

namespace Timeline.Tools
{
    public class ResetChildPositionOnEnable : MonoBehaviour
    {
        private void OnEnable()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = Vector3.zero;
            }
        }
    }
}