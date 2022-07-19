using DG.Tweening;

namespace TweenExtension
{
    public static class EasingTweenExtension
    {
        public static T SetEase<T>(this T t, Easing easing) where T : Tween
        {
            return easing.SetEase(t);
        }
    }
}