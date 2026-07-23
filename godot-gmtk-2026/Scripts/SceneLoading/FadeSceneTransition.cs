using Godot;

namespace GodotGMTK2026.Scripts.SceneLoading;

public partial class FadeSceneTransition : SceneTransition
{
    public override async void StartTransition()
    {
        ColorRect rect = GetNode<ColorRect>("ColorRect");
        Color target = rect.Color;
        target.A = 1f;
        
        Tween t_in = CreateTween();
        t_in.SetEase(Tween.EaseType.Out);
        t_in.TweenProperty(rect, "color", target, 0.5f);
        await ToSignal(t_in, Tween.SignalName.Finished);

        LoadReady();

        target.A = 0f;
        Tween t_out = CreateTween();
        t_out.SetEase(Tween.EaseType.In);
        t_out.TweenProperty(rect, "color", target, 0.5f);
        await ToSignal(t_out, Tween.SignalName.Finished);
        
        TransitionFinished();
    }
}