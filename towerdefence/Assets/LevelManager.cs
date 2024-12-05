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
    public void GameOver()
    {
        if (isGameOver) return; // Evita que o Game Over seja chamado várias vezes

        isGameOver = true; // Marca que o jogo terminou
        Debug.Log("Game Over! Um inimigo alcançou o ponto final.");

        // Exibe o painel de Game Over
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        if (!AdManager.instance.isGamePausedByAd)
        {
            Time.timeScale = 0; // Apenas pausa o jogo se não estiver pausado por um anúncio
        }
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
