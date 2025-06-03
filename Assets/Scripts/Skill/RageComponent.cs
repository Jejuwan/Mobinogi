using UnityEngine;

public class RageComponent : MonoBehaviour
{
    public int currentRage;
    public int maxRage = 50;

    public bool IsMax => currentRage >= maxRage;

    public void GainRage(int amount)
    {
        currentRage = Mathf.Min(maxRage, currentRage + amount);
    }

    public void ConsumeRage(int amount)
    {
        currentRage = Mathf.Max(0, currentRage - amount);
    }
}