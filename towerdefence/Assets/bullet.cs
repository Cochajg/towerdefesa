using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour // Classe que representa um proj�til que se move em dire��o ao alvo e causa dano
{
    [SerializeField] private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do proj�til para controlar a f�sica
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do proj�til
    [SerializeField] private int bulletDamage = 1; // Dano causado pelo proj�til ao colidir com um inimigo
    private Transform target; // Refer�ncia ao alvo que o proj�til deve atingir

    // Define o alvo que o proj�til deve seguir
    public void SetTarget(Transform _target)
    {
        target = _target; // Atribui o alvo passado como par�metro
    }

    // M�todo chamado a cada atualiza��o de f�sica
    private void FixedUpdate()
    {
        if (!target) { return; } // Se n�o houver alvo, n�o faz nada

        // Calcula a dire��o do proj�til em rela��o ao alvo e normaliza para obter um vetor unit�rio
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade do Rigidbody2D do proj�til na dire��o do alvo
        rb.velocity = direction * bulletSpeed;
    }

    // M�todo chamado ao colidir com outro objeto
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Aplica dano ao inimigo ao colidir com ele
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);

        // Destr�i o objeto do proj�til ap�s a colis�o
        Destroy(gameObject);
    }
}
