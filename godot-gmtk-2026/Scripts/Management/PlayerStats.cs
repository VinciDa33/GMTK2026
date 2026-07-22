namespace GodotGMTK2026.Scripts.Management;

public class PlayerStats
{
    public int OxygenCapacity { get; private set; }
    public float OxygenEfficiency { get; private set; }
    public float ThrusterPower { get; private set; }
    public float ThrusterEfficiency { get; private set; }

    public PlayerStats(int oxygenCapacity, float oxygenEfficiency, float thrusterPower, float thrusterEfficiency)
    {
        OxygenCapacity = oxygenCapacity;
        OxygenEfficiency = oxygenEfficiency;
        ThrusterPower = thrusterPower;
        ThrusterEfficiency = thrusterEfficiency;
    }

    public void SetOxygenCapacity(int newCapacity)
    {
        OxygenCapacity = newCapacity;
    }
    
    public void SetOxygenEfficiency(float newEfficiency)
    {
        OxygenEfficiency = newEfficiency;
    }
    
    public void SetThrusterPower(float newPower)
    {
        ThrusterPower = newPower;
    }
    
    public void SetThrusterEfficiency(int newEfficiency)
    {
        ThrusterEfficiency = newEfficiency;
    }
}