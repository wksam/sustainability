using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static event Action<bool> OnHDIGoal;
    [SerializeField] ScoreSetupScriptableObject scoreSO;
    [SerializeField] Slider hdiSlider;
    [SerializeField] Slider capitalSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider educationSlider;
    [SerializeField] float hdiGoal = .8f;

    void Start()
    {
        UpdateSliders(0f, 0f, 0f, 0f);
    }

    public void CalculateHDI(int capital, int population, float educationSupplyDemand, float healthSupplyDemand)
    {
        double capitalIndex = CapitalIndex(capital, population);
        double healthIndex = HealthIndex(healthSupplyDemand);
        double educationIndex = EducationIndex(educationSupplyDemand);
        double hdi = Math.Cbrt(capitalIndex * educationIndex * healthIndex);
        UpdateSliders((float)hdi, (float)capitalIndex, (float)healthIndex, (float)educationIndex);
        if (hdi > hdiGoal) OnHDIGoal.Invoke(true);
    }

    double CapitalIndex(int capital, int population)
    {
        double perCapita = capital / population;
        double capitalIndex = (Math.Log(perCapita, Math.E) - Math.Log(scoreSO.CapitalIndexMin, Math.E)) / 
            (Math.Log(scoreSO.CapitalIndexMax, Math.E) - Math.Log(scoreSO.CapitalIndexMin, Math.E));
        return capitalIndex;
    }

    double HealthIndex(float supplyDemand)
    {
        double healthIndex = (Mathf.Lerp(scoreSO.HealthIndexMin, scoreSO.HealthIndexMax, supplyDemand) - scoreSO.HealthIndexMin) / 
            (scoreSO.HealthIndexMax - scoreSO.HealthIndexMin);
        return healthIndex;
    }

    double EducationIndex(float supplyDemand)
    {
        double educationIndex = Mathf.Lerp(scoreSO.EducationIndexMin, scoreSO.EducationIndexMax, supplyDemand);
        return educationIndex;  
    }

    void UpdateSliders(float hdi, float capitalIndex, float healthIndex, float educationIndex)
    {
        hdiSlider.value = hdi;
        capitalSlider.value = capitalIndex;
        healthSlider.value = healthIndex;
        educationSlider.value = educationIndex;
    }
}
