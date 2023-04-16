using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : MonoBehaviour
{
    public static event Action<bool> OnMaxPollution;
    [SerializeField] PollutionSetupScriptableObject pollutionSO;
    [SerializeField] Slider pollutionSlider;
    [SerializeField] int maxPollution = 10000;
    float totalPollution;

    void Start()
    {
        UpdateSlider();
    }

    public void AdjustPollution(float productionInvested, float population, float education)
    {
        float productionPollution = productionInvested * pollutionSO.ProductionEffect;
        float populationPollution = population * pollutionSO.PopulationEffect;
        float educationEffect = education * pollutionSO.EducationEffect;
        float decay = totalPollution * pollutionSO.Decay;

        totalPollution += productionPollution + populationPollution - educationEffect - decay;
        if (totalPollution < 0) totalPollution = 0;
        UpdateSlider();
        if (totalPollution > maxPollution) OnMaxPollution.Invoke(false);
    }

    void UpdateSlider()
    {
        pollutionSlider.value = totalPollution / maxPollution;
    }
}
