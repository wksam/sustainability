using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapitalManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int totalCapital;
    int profit;

    void OnEnable()
    {
        EducationManager.changeCapital += ChangeCapital;
        HealthManager.changeCapital += ChangeCapital;
        ProductionManager.changeCapital += ChangeCapital;
    }

    void Start()
    {
        UpdateText();
    }

    public void ChangeCapital(int amount)
    {
        totalCapital += amount;
        UpdateText();
    }

    public void ChangeProfit(int amount)
    {
        profit = amount;
        UpdateText();
    }

    public int GetCapital()
    {
        return totalCapital;
    }

    public void UpdateText()
    {
        text.text = $"Capital: {Utils.FormatNumber(totalCapital)} / {Utils.FormatNumber(profit)}";
    }
}
