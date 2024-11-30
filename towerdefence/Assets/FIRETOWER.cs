using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretFire : Turret // Classe especializada de torre que aplica dano de queimadura aos inimigos
{
    [SerializeField] private float burnDuration = 3f; // Duração do efeito de queimadura em segundos
    [SerializeField] private float burnDamagePerSecond = 1f; // Dano causado por segundo enquanto o inimigo está queimando

    // Método que ataca o inimigo aplicando o efeito de queimadura
    public override void Atacar()
    {
        if (target != null) // Verifica se há um alvo designado
        {
            Health enemyHealth = target.GetComponent<Health>(); // Obtém o componente de saúde do inimigo

            if (enemyHealth != null) // Verifica se o inimigo possui o componente de saúde
            {
                StartCoroutine(ApplyBurnDamage(enemyHealth)); // Inicia a aplicação do dano de queimadura
            }
        }
    }

    // Coroutine que aplica o dano de queimadura ao longo do tempo
    private IEnumerator ApplyBurnDamage(Health enemyHealth)
    {
        float elapsedTime = 0f; // Tempo decorrido desde o início do efeito de queimadura

        while (elapsedTime < burnDuration) // Continua aplicando dano até o fim da duração da queimadura
        {
            enemyHealth.TakeDamage(burnDamagePerSecond * Time.deltaTime); // Aplica dano ao inimigo a cada frame
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido
            yield return null; // Espera até o próximo frame para continuar
        }
    }

    // Método de disparo que cria e dispara o projétil e aplica o efeito de queimadura ao alvo
    protected override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Instancia um projétil na posição de disparo
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Obtém o script do projétil
        bulletScript.SetTarget(target); // Define o alvo do projétil
        Atacar(); // Aplica o ataque (efeito de queimadura) ao inimigo
    }
}
