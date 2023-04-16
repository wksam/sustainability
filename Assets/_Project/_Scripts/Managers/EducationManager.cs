using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EducationManager : MonoBehaviour
{
    public static event Action<int> changeCapital;
    [SerializeField] SectorSetupScriptableObject educationSO;
    [SerializeField] CapitalManager capitalManager;
    [SerializeField] PopulationManager populationManager;
    [SerializeField] TextMeshProUGUI text;
    int investmentAmount = 100;
    int totalInvestment = 0;

    void Start()
    {
        UpdateText();
    }

    public void IncreaseEducation()
    {
        if (capitalManager.GetCapital() >= investmentAmount) 
        {
            totalInvestment += investmentAmount;
            changeCapital.Invoke(-investmentAmount);
            UpdateText();
        }
    }

    public void DecreaseEducation()
    {
        if (totalInvestment >= investmentAmount)
        {
            totalInvestment -= investmentAmount;
            changeCapital.Invoke(investmentAmount);
            UpdateText();
        }
    }

    public float Weight(int population)
    {
        return SupplyDemand(population) * educationSO.PopulationEffect;
    }

    public float SupplyDemand(int population)
    {
        return Mathf.Min(Supply() / Demand(population), 1);
    }

    float Demand(int population)
    {
        return population * educationSO.DemandPerCapita;
    }

    float Supply()
    {
        return totalInvestment * educationSO.SupplyEfficiency;
    }

    public void ChangeInvestmentAmount(int amount)
    {
        investmentAmount = amount;
    }

    void UpdateText()
    {
        text.text = Utils.FormatNumber(totalInvestment);
    }
}
