using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stat : MonoBehaviour
{
    [Tooltip("Set initial max stat here")]
    public float maxStat;
    [Tooltip("Set how many seconds it takes to lose 1 stat")]
    public float statRate;
    [Tooltip("Reference the stat slider here")]
    [SerializeField] Slider statSlider;

    [HideInInspector] [Tooltip("Current stat value")]
    public float currStat;
    
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
            currStat--;
            statSlider.value = currStat;
            yield return new WaitForSeconds(statRate);   
        }
    }

    /// <summary>
    /// Increase current stat by val
    /// </summary>
    /// <param name="val">Hunger satiated by food</param>
    public void IncreaseCurrStat(float val)
    {
        currStat += val;
    }

    /// <summary>
    /// Decrease stat decrement to 1 every statRate seconds
    /// </summary>
    /// <param name="newRate">Lose 1 current stat every newRate seconds</param>
    public void DecreaseStatRate(float newRate)
    {
        statRate = newRate;
    }
}
