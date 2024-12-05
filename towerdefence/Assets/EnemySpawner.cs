using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour // Classe responsável por gerar inimigos no jogo
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>(); // Array de prefabs de inimigos que podem ser gerados
    [SerializeField] private int baseEnemies = 8; // Número base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f; // Taxa de inimigos a serem gerados por segundo
    [SerializeField] private float timeBetweenWaves = 5f; // Tempo entre ondas de inimigos
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escalonamento de dificuldade
    [SerializeField] private float enemiesPerSecondCap = 15f; // Limite máximo de inimigos por segundo

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento que é chamado quando um inimigo é destruído

    private int currentWave = 1; // Onda atual de inimigos
    private float timeSinceLastSpawn; // Tempo desde o último inimigo gerado
    private int enemiesAlive; // Contador de inimigos ainda vivos
    private int enemiesLeftToSpawn; // Contador de inimigos restantes a serem gerados
    private float eps; // Número de inimigos por segundo para a onda atual
    private bool isSpawning = false; // Indica se os inimigos estão sendo gerados

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); // Adiciona o método EnemyDestroyed ao evento onEnemyDestroy
    }

    private void Start()
    {
        StartCoroutine(StartWave()); // Inicia a geração da primeira onda de inimigos
    }

    private void Update()
    {
        if (!isSpawning) return; // Se não estiver gerando inimigos, retorna

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o último spawn

        // Verifica se é hora de gerar um novo inimigo
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy(); // Gera um novo inimigo
            enemiesLeftToSpawn--; // Diminui o contador de inimigos restantes
            enemiesAlive++; // Aumenta o contador de inimigos vivos
            timeSinceLastSpawn = 0f; // Reseta o timer de spawn
        }

        // Verifica se todos os inimigos da onda foram destruídos
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave(); // Encerra a onda atual
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--; // Diminui o contador de inimigos vivos quando um inimigo é destruído
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Espera o tempo definido entre ondas
        isSpawning = true; // Começa a geração de inimigos
        enemiesLeftToSpawn = EnemiesPerWave(); // Define quantos inimigos serão gerados na nova onda
        eps = EnemiesPerSecond(); // Calcula quantos inimigos devem ser gerados por segundo
    }

    private void EndWave()
    {
        isSpawning = false; // Para a geração de inimigos
        timeSinceLastSpawn = 0f; // Reseta o tempo desde o último spawn
        currentWave++; // Aumenta a contagem da onda atual
        StartCoroutine(StartWave()); // Inicia a próxima onda
    }

    private void SpawnEnemy()
    {
        // Verifica se a lista de prefabs não está vazia
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("A lista de prefabs de inimigos está vazia!"); // Log de aviso
            return; // Sai do método se não houver prefabs
        }

        // Seleciona um prefab aleatório da lista
        GameObject prefabParaSpawnar = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        // Instancia o inimigo na posição de início definida pelo LevelManager
        _ = Instantiate(prefabParaSpawnar, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    // Calcula o número de inimigos a serem gerados na onda atual com base na dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); // Retorna um número inteiro baseado na dificuldade
    }

    // Calcula quantos inimigos podem ser gerados por segundo com base na dificuldade   
    private float EnemiesPerSecond()
    {
        // Retorna a quantidade de inimigos por segundo limitada pelo cap
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
