using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour // Classe responsável pelo movimento dos inimigos
{
    [SerializeField] private Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo para controlar a física
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento do inimigo
    private Transform target; // O próximo ponto de destino que o inimigo deve alcançar
    [SerializeField] private int pathIndex = 0; // Índice atual do caminho que o inimigo está seguindo
    private float baseSpeed; // Velocidade base do inimigo para poder ser restaurada

    private void Start()
    {
        baseSpeed = moveSpeed; // Armazena a velocidade inicial
        target = LevelManager.instance.path[pathIndex]; // Define o primeiro ponto do caminho como destino
    }

    private void Update()
    {
        // Verifica se o inimigo chegou ao ponto de destino
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Avança para o próximo ponto do caminho

            // Verifica se o inimigo atingiu o final do caminho
            if (pathIndex == LevelManager.instance.path.Length || pathIndex == 14)
            {
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica que um inimigo foi destruído
                Destroy(gameObject); // Remove o inimigo do jogo
                return; // Encerra a execução deste método
            }
            else
            {
                // Define o próximo ponto de destino
                target = LevelManager.instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Calcula a direção para o próximo ponto de destino e normaliza
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade do Rigidbody2D na direção do destino
        rb.velocity = direction * moveSpeed;
    }

    // Método para atualizar a velocidade do inimigo
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed; // Atualiza a velocidade do inimigo
    }

    // Método para restaurar a velocidade do inimigo ao seu valor base
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed; // Restaura a velocidade base
    }
}
