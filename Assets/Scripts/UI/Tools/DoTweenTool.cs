using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class DoTweenTool
{
    public static Tweener DoFade(Text text,float end,float timer=0.6f)
    {
        return text.DOFade(end,timer);
    }
    public static Tweener DoFade(Image image, float end, float timer = 0.6f)
    {
        return image.DOFade(end, timer);
    }
    public static Tweener DoFade<T>(T t, float end, float timer = 0.6f) where T : Graphic
    {
        return t.DOFade(end, timer);
    }
    public static Tweener DoFadeOnComplete(Text text, float end, TweenCallback complete = null, float timer = 0.6f)
    {
        return text.DOFade(end,timer).OnComplete(complete);
    }
}
