using DG.Tweening;

namespace ArcadianLab.DemoGame.Utilities.Generics
{
    public static class GenericMethods
    {
        public static void KillSequence(Sequence seq)
        {
            if (seq != null && (seq.IsActive() || seq.IsPlaying())) seq.Kill();
        }
        public static void KillTween(Tween tween)
        {
            if (tween != null) tween.Kill();
        }
    }
}
