using Godot;
using GodotGMTK2026.Scripts.Items;
using GodotGMTK2026.Scripts.Items.Inventory;

namespace GodotGMTK2026.Scripts.Machines.RefineryMachine;

public partial class Refinery : Node3D
{
    [Export] private RefineryRecipe[] _recipes;
    [Export] private float _processingTime;
    public Inventory Inventory { get; private set; }
    private Timer _processingTimer;
    public override void _Ready()
    {
        Inventory = new Inventory(10); //Virtually Infinite
        _processingTimer = new Timer();
        AddChild(_processingTimer);

        _processingTimer.WaitTime = _processingTime;
        _processingTimer.OneShot = true;
        _processingTimer.Timeout += ProcessingTimerFinished;
    }

    public override void _ExitTree()
    {
        if (_processingTimer == null)
            return;
        _processingTimer.Timeout -= ProcessingTimerFinished;
    }

    public override void _Process(double delta)
    {
        //Start a timer when the refinery inventory is not empty!
        if (Inventory.Count() > 0 && _processingTimer.IsStopped())
            _processingTimer.Start();
    }

    private void ProcessingTimerFinished()
    {
        //Whenever the timer finishes, process one item from the refinery inventory
        Item item = Inventory.GetFirst();
        if (item == null)
            return;
        
        foreach (RefineryRecipe recipe in _recipes)
            if (recipe.Input == item.Type)
            {
                //TODO: Add the recipe output somewhere!
            }
        Inventory.RemoveFirst();
    }
    
}