using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform[] path;
    public bool isGameOver = false;
    public GameObject gameOverPanel;
    public Transform startPoint;

    public int currency;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Método para adicionar 100 moedas como recompensa
    public void RewardCurrency()
    {
        IncreaseCurrency(100);
    }
    public void Reiniciar()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isGameOver=false;

    }
}
