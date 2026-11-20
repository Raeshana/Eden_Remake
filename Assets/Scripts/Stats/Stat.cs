using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stat : MonoBehaviour
{
    [Tooltip("Set initial max stat here")]
    public float maxStat;
    [Tooltip("Set how much of stat is lost every second")]
    public float statDecrease;
    [Tooltip("Reference the stat slider here")]
    [HideInInspector]
    public Slider statSlider;

    [HideInInspector] [Tooltip("Current stat value")]
    private float currStat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitializeStat()
    {
        // Initialize max hunger
        statSlider.maxValue = maxStat;
        // Set current hunger to max hunger
        currStat = maxStat;
        // Start decreasing hunger by hunger rate
        StartCoroutine(StatCoroutine());
    }

    /// <summary>
    /// Decrease current stat by 1 every statRate seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator StatCoroutine()
    {
        while (true)
        {
            currStat -= statDecrease;
            statSlider.value = currStat;
            yield return new WaitForSeconds(1f);   
        }
    }

    /// <summary>
    /// Increase current stat by val
    /// </summary>
    /// <param name="val">Value to increase current stat by</param>
    public void IncreaseCurrStat(float val)
    {
        if (val == 0)
        {
            Debug.Log("Current stat is being increased by 0");
            return;
        }
        currStat += val;
        currStat = (currStat > maxStat) ? maxStat : currStat;
        statSlider.value = currStat;
    }

    /// <summary>
    /// Change stat decrement
    /// </summary>
    /// <param name="newDecrease">Lose new decrease stat every second</param>
    public void DecreaseStatDepletion(float newDecrease)
    {
        if (newDecrease <= statDecrease)
        {
            Debug.Log("You are not decreasing the stat depletion!");
            return;
        }
        statDecrease = newDecrease;
    }

    /// <summary>
    /// Increases max stat
    /// </summary>
    /// <param name="newMaxStat">Value that max stat has increased by</param>
    public void IncreaseMaxStat(float newMaxStat)
    {
        maxStat += newMaxStat;
    }
}
