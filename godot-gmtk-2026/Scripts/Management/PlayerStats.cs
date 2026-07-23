namespace GodotGMTK2026.Scripts.Management;

public class PlayerStats
{
    public int OxygenCapacity { get; private set; }
    public float OxygenLevel { get; set; }
    public float OxygenEfficiency { get; private set; }
    public float ThrusterPower { get; private set; }
    public float ThrusterEfficiency { get; private set; }
    public float DampingFactor { get; private set; }

    public PlayerStats(int oxygenCapacity, float oxygenEfficiency, float thrusterPower, float thrusterEfficiency, float dampingFactor)
    {
        OxygenCapacity = oxygenCapacity;
        OxygenEfficiency = oxygenEfficiency;
        ThrusterPower = thrusterPower;
        ThrusterEfficiency = thrusterEfficiency;
        DampingFactor = dampingFactor;

        OxygenLevel = oxygenCapacity; //Oxygen level always starts at full capacity
    }

    public void SetOxygenCapacity(int newCapacity)
    {
        OxygenCapacity = newCapacity;
    }
    
    public void SetOxygenEfficiency(float newEfficiency)
    {
        OxygenEfficiency = newEfficiency;
    }

    public void RefillOxygen()
    {
        OxygenLevel = OxygenCapacity;
    }

    public void SetOxygenLevel(float newLevel)
    {
        OxygenLevel = newLevel;
    }
    
    public void SetThrusterPower(float newPower)
    {
        ThrusterPower = newPower;
    }
    
    public void SetThrusterEfficiency(float newEfficiency)
    {
        ThrusterEfficiency = newEfficiency;
    }

    public void SetDampingFactor(float newDampingFactor)
    {
        DampingFactor = newDampingFactor;
    }
}