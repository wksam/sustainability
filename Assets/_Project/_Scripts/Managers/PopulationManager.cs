using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationManager : MonoBehaviour
{
    [SerializeField] PopulationSetupScriptableObject populationSO;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int totalPopulation = 100;

    void Start()
    {
        UpdateText();
    }

    public int GetPopulation()
    {
        return totalPopulation;
    }

    public float GrowthThreshold()
    {
        return populationSO.PopulationGrowthThreshold;
    }

    public float GrowthNormalize()
    {
        return populationSO.PopulationGrowthNormalize;
    }

    public void PopulationGrowth(float growth)
    {
        totalPopulation += (int)(totalPopulation * growth);
        UpdateText();
    }

    void UpdateText()
    {
        text.text = $"Population: {Utils.FormatNumber(totalPopulation)}";
    }
}
