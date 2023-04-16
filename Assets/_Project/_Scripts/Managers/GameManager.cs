using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] int turnInSeconds = 3;
    [SerializeField] TerrainManager terrainManager;
    [SerializeField] CapitalManager capitalManager;
    [SerializeField] ProductionManager productionManager;
    [SerializeField] HealthManager healthManager;
    [SerializeField] EducationManager educationManager;
    [SerializeField] PopulationManager populationManager;
    [SerializeField] PollutionManager pollutionManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI result;
    IEnumerator turn;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        PollutionManager.OnMaxPollution += GameOver;
        ScoreManager.OnHDIGoal += GameOver;
    }
    
    void Start()
    {
        terrainManager.InitializeTilemap();
        terrainManager.UpdateTiles(populationManager.GetPopulation());
        turn = Turn();
        StartCoroutine(turn);
    }

    void OnDisable()
    {
        StopCoroutine(turn);
        PollutionManager.OnMaxPollution -= GameOver;
        ScoreManager.OnHDIGoal -= GameOver;
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(turnInSeconds);
        while (true)
        {
            Calculate();
            yield return new WaitForSeconds(turnInSeconds);
        }
    }

    void Calculate()
    {
        int population = populationManager.GetPopulation();
        UpdateCapital(population);
        UpdatePollution(population);
        UpdateScore(population);
        UpdatePopulation(population);
        UpdateCity(population);
    }

    void UpdateCapital(int population)
    {
        int profit = (int)productionManager.Profit(population);

        capitalManager.ChangeCapital(profit);
        capitalManager.ChangeProfit(profit);

        if ((int)(profit * .1f) > 100)
        {
            productionManager.ChangeInvestmentAmount((int)(profit * .1f));
            healthManager.ChangeInvestmentAmount((int)(profit * .1f));
            educationManager.ChangeInvestmentAmount((int)(profit * .1f));
        }
    }

    void UpdatePollution(int population)
    {
        pollutionManager.AdjustPollution(productionManager.TotalInvested(), 
            population, 
            educationManager.SupplyDemand(population));
    }

    void UpdateScore(int population)
    {
        scoreManager.CalculateHDI(
            capitalManager.GetCapital(), 
            population, 
            educationManager.SupplyDemand(population), 
            healthManager.SupplyDemand(population));
    }

    void UpdatePopulation(int population)
    {
        float populationGrowth = (productionManager.Weight(population) + 
            healthManager.Weight(population) + 
            educationManager.Weight(population) - 
            populationManager.GrowthThreshold()) * 
            populationManager.GrowthNormalize();
        populationManager.PopulationGrowth(populationGrowth);
    }

    void UpdateCity(int population)
    {
        terrainManager.UpdateTiles(population);
    }

    void GameOver(bool won)
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        if (won)
        {
            result.text = "You won. You reached the ideal HDI.";
        }
        else
        {
            result.text = "You lost. Pollution reached catastrophic levels.";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
