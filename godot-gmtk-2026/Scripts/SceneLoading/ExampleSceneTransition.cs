using Godot;

namespace GodotGMTK2026.Scripts.SceneLoading;

public partial class ExampleSceneTransition : SceneTransition
{
    public override async void StartTransition()
    {
        Tween t_in = CreateTween();
        t_in.SetEase(Tween.EaseType.Out);
        t_in.TweenProperty(this, "position:y", 0f, 1f);
        await ToSignal(t_in, Tween.SignalName.Finished);

        LoadReady();
        
        Tween t_out = CreateTween();
        t_out.SetEase(Tween.EaseType.In);
        t_out.TweenProperty(this, "position:y", -650, 1f);
        await ToSignal(t_out, Tween.SignalName.Finished);
        
        TransitionFinished();
    }
}