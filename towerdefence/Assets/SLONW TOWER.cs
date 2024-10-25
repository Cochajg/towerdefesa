using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlomo : Turret // Classe especializada de torre que desacelera os inimigos em seu alcance
{
    [SerializeField] private float aps = 4f; // Ataques por segundo (aps) da torre
    [SerializeField] private float FreezeTime = 1f; // Tempo de duração do efeito de desaceleração nos inimigos

   
    private void Update()
    {
        timeUntilFire += Time.deltaTime; // Incrementa o tempo até o próximo ataque
        if (timeUntilFire >= 1f / aps) // Se o tempo acumulado é suficiente para um ataque
        {
            FreezeEnemies(); // Aplica o efeito de desaceleração aos inimigos
            timeUntilFire = 0f; // Reseta o tempo até o próximo ataque
        }
    }

    // Aplica o efeito de desaceleração nos inimigos dentro do alcance da torre
    private void FreezeEnemies()
    {
        // Realiza um CircleCast para detectar todos os inimigos no alcance da torre
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++) // Percorre todos os inimigos detectados
            {
                RaycastHit2D hit = hits[i];
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>(); // Obtém o script de movimento do inimigo
                em.UpdateSpeed(0.5f); // Reduz a velocidade do inimigo pela metade
                StartCoroutine(ResetEnemySpeed(em)); // Inicia a rotina para restaurar a velocidade normal após o tempo de congelamento
            }
        }
    }

    // Coroutine que restaura a velocidade normal do inimigo após o tempo de desaceleração
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(FreezeTime); // Espera pelo tempo de congelamento
        em.ResetSpeed(); // Restaura a velocidade original do inimigo
    }

    void Start()
    {
      
    }
}
