using DG.Tweening;
using Timeline.Move;
using UnityEditor;
using UnityEngine;

namespace TweenExtension.Editor
{
    [CustomPropertyDrawer(typeof(Easing))]
    public class EasingDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var easeProperty = property.FindPropertyRelative("_ease");
            var curveProperty = property.FindPropertyRelative("_curve");
            var scaleProperty = property.FindPropertyRelative("_scale");

            float offset = 5f;
            float halfWidth = position.width / 2f;
            Rect easeRect = new Rect(position.x, position.y, halfWidth, EditorGUIUtility.singleLineHeight);
            Rect curveRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            Rect scaleRect = new Rect(position.x + halfWidth + offset, position.y, halfWidth - offset,
                EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(easeRect, easeProperty, GUIContent.none);

            if (easeProperty.intValue == (int)Ease.INTERNAL_Custom)
            {
                EditorGUI.PropertyField(curveRect, curveProperty, GUIContent.none);
            }

            EditorGUI.PropertyField(scaleRect, scaleProperty, GUIContent.none);


            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            
            var easeProperty = property.FindPropertyRelative("_ease");
            float height = easeProperty.intValue == (int)Ease.INTERNAL_Custom ? 2f : 1f;
            return base.GetPropertyHeight(property, label) * height;
        }
    }
}