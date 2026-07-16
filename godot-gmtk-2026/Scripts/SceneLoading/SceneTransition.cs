using System;
using Godot;

namespace GodotGMTK2026.Scripts.SceneLoading;

public abstract partial class SceneTransition : Node
{
    public event Action OnLoadReady;
    public event Action OnTransitionFinish;
    public abstract void StartTransition();

    protected void LoadReady()
    {
        OnLoadReady?.Invoke();
    }

    protected void TransitionFinished()
    {
        OnTransitionFinish?.Invoke();
    }
}