using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductionManager : MonoBehaviour
{
    public static event Action<int> changeCapital;
    [SerializeField] SectorSetupScriptableObject productionSO;
    [SerializeField] CapitalManager capitalManager;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float pricePerProduct = 1;
    int investmentAmount = 100;
    int totalInvestment = 0;

    void Start()
    {
        UpdateText();
    }

    public void IncreaseProduction()
    {
        if (capitalManager.GetCapital() >= investmentAmount) 
        {
            totalInvestment += investmentAmount;
            changeCapital.Invoke(-investmentAmount);
            UpdateText();
        }
    }

    public void DecreaseProduction()
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
        return SupplyDemand(population) * productionSO.PopulationEffect;
    }

    float Demand(int population)
    {
        return population * productionSO.DemandPerCapita;
    }

    float Supply()
    {
        return totalInvestment * productionSO.SupplyEfficiency;
    }

    public float SupplyDemand(int population)
    {
        return Mathf.Min(Supply() / Demand(population), 1);
    }

    public float Profit(int population)
    {
        return Mathf.Min(Demand(population), Supply()) * pricePerProduct;
    }

    public int TotalInvested()
    {
        return totalInvestment;
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
