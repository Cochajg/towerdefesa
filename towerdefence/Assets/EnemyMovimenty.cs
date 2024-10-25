using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour // Classe respons�vel pelo movimento dos inimigos
{
    [SerializeField] private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do inimigo para controlar a f�sica
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento do inimigo
    private Transform target; // O pr�ximo ponto de destino que o inimigo deve alcan�ar
    [SerializeField] private int pathIndex = 0; // �ndice atual do caminho que o inimigo est� seguindo
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
            pathIndex++; // Avan�a para o pr�ximo ponto do caminho

            // Verifica se o inimigo atingiu o final do caminho
            if (pathIndex == LevelManager.instance.path.Length || pathIndex == 14)
            {
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica que um inimigo foi destru�do
                Destroy(gameObject); // Remove o inimigo do jogo
                return; // Encerra a execu��o deste m�todo
            }
            else
            {
                // Define o pr�ximo ponto de destino
                target = LevelManager.instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Calcula a dire��o para o pr�ximo ponto de destino e normaliza
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade do Rigidbody2D na dire��o do destino
        rb.velocity = direction * moveSpeed;
    }

    // M�todo para atualizar a velocidade do inimigo
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed; // Atualiza a velocidade do inimigo
    }

    // M�todo para restaurar a velocidade do inimigo ao seu valor base
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed; // Restaura a velocidade base
    }
}
