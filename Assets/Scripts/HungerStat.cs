using UnityEngine;

public class HungerStat : Stat
{
    void Start()
    {
        InitializeStat();   
    }

    /// <summary>
    /// Increases max hunger
    /// </summary>
    /// <param name="newMaxStat">Value that max hunger has increased by</param>
    public void IncreaseMaxHunger(float newMaxStat)
    {
        maxStat += newMaxStat;
    }
}
