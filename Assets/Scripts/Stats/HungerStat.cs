using UnityEngine;
using UnityEngine.UI;

public class HungerStat : Stat
{
    void Start()
    {
        statSlider = GameObject.FindWithTag("HungerSlider").GetComponent<Slider>();
        InitializeStat();   
    }
}
