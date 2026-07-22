namespace GodotGMTK2026.Scripts.Management;

public class PlayerStats
{
    public int OxygenCapacity { get; private set; }
    public int OxygenLevel { get; set; }
    public float OxygenEfficiency { get; private set; }
    public float ThrusterPower { get; private set; }
    public float ThrusterEfficiency { get; private set; }
    public bool HasDampingUpgrade { get; private set; }

    public PlayerStats(int oxygenCapacity, int oxygenLevel, float oxygenEfficiency, float thrusterPower, float thrusterEfficiency, bool hasDampingUpgrade)
    {
        OxygenCapacity = oxygenCapacity;
        OxygenLevel = oxygenLevel;
        OxygenEfficiency = oxygenEfficiency;
        ThrusterPower = thrusterPower;
        ThrusterEfficiency = thrusterEfficiency;
        HasDampingUpgrade = hasDampingUpgrade;
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

    public void SetOxygenLevel(int newLevel)
    {
        OxygenLevel = newLevel;
    }
    
    public void SetThrusterPower(float newPower)
    {
        ThrusterPower = newPower;
    }
    
    public void SetThrusterEfficiency(int newEfficiency)
    {
        ThrusterEfficiency = newEfficiency;
    }

    public void SetHasDampingUpgrade(bool hasUpgrade)
    {
        HasDampingUpgrade = hasUpgrade;
    }
}