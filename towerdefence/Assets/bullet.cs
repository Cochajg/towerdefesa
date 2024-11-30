using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour // Classe que representa um projétil que se move em direção ao alvo e causa dano
{
    [SerializeField] private Rigidbody2D rb; // Referência ao Rigidbody2D do projétil para controlar a física
    [SerializeField] private float bulletSpeed = 5f; // Velocidade do projétil
    [SerializeField] private int bulletDamage = 1; // Dano causado pelo projétil ao colidir com um inimigo
    private Transform target; // Referência ao alvo que o projétil deve atingir

    // Define o alvo que o projétil deve seguir
    public void SetTarget(Transform _target)
    {
        target = _target; // Atribui o alvo passado como parâmetro
    }

    // Método chamado a cada atualização de física
    private void FixedUpdate()
    {
        if (!target) { return; } // Se não houver alvo, não faz nada

        // Calcula a direção do projétil em relação ao alvo e normaliza para obter um vetor unitário
        Vector2 direction = (target.position - transform.position).normalized;

        // Define a velocidade do Rigidbody2D do projétil na direção do alvo
        rb.velocity = direction * bulletSpeed;
    }

    // Método chamado ao colidir com outro objeto
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Aplica dano ao inimigo ao colidir com ele
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);

        // Destrói o objeto do projétil após a colisão
        Destroy(gameObject);
    }
}
