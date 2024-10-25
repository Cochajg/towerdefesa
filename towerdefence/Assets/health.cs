using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour // Classe que gerencia os pontos de vida e destrui��o de um inimigo
{
    [SerializeField] private float hitpoints = 2; // Pontos de vida do inimigo, configurados no editor
    private bool isDestroyed = false; // Controle interno para evitar m�ltiplas destrui��es
    [SerializeField] private int currencyWorth = 50; // Moeda dada ao jogador ao destruir este inimigo

    // M�todo para aplicar dano ao inimigo
    public void TakeDamage(float dmg)
    {
        hitpoints -= dmg; // Subtrai o dano recebido dos pontos de vida

        // Se os pontos de vida chegarem a zero ou menos e o inimigo ainda n�o estiver destru�do
        if (hitpoints <= 0 && !isDestroyed)
        {
            LevelManager.instance.IncreaseCurrency(currencyWorth); // Aumenta a moeda do jogador ao destruir o inimigo
            isDestroyed = true; // Marca o inimigo como destru�do
            EnemySpawner.onEnemyDestroy.Invoke(); // Notifica outros componentes do jogo sobre a destrui��o do inimigo
            Destroy(gameObject); // Remove o inimigo do jogo
        }
    }
}
