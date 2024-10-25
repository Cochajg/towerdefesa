using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour // Classe que gerencia os pontos de vida e destruição de um inimigo
{
    [SerializeField] private float hitpoints = 2; // Pontos de vida do inimigo, configurados no editor
    private bool isDestroyed = false; // Controle interno para evitar múltiplas destruições
    [SerializeField] private int currencyWorth = 50; // Moeda dada ao jogador ao destruir este inimigo

    // Método para aplicar dano ao inimigo
    public void TakeDamage(float dmg)
    {
        hitpoints -= dmg; // Subtrai o dano recebido dos pontos de vida

        // Se os pontos de vida chegarem a zero ou menos e o inimigo ainda não estiver destruído
        if (hitpoints <= 0 && !isDestroyed)
        {
            LevelManager.instance.IncreaseCurrency(currencyWorth); // Aumenta a moeda do jogador ao destruir o inimigo
            isDestroyed = true; // Marca o inimigo como destruído
            EnemySpawner.onEnemyDestroy.Invoke(); // Notifica outros componentes do jogo sobre a destruição do inimigo
            Destroy(gameObject); // Remove o inimigo do jogo
        }
    }
}
