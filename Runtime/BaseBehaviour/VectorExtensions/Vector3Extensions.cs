using UnityEngine;

namespace Timeline.Move
{
    public static class Vector3Extensions
    {
        public static Vector3 InverseTransformPoint(this Vector3 position, Vector3 point) {
            Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
            Matrix4x4 inverse = matrix.inverse;
            return inverse.MultiplyPoint3x4(point);
        }
        
        public static Vector3 TransformPoint(this Vector3 position, Vector3 point) {
            Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
            //Matrix4x4 inverse = matrix.inverse;
            return matrix.MultiplyPoint3x4(point);
        }
    }
}