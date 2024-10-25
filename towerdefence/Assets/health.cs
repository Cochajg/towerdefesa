using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float hitponints = 2;
    private bool isDestroyed = false;
    [SerializeField] private int currencyWorth = 50;

    public void takedamaga(float dmg)
    {
        hitponints-=dmg;
        if (hitponints <= 0&& !isDestroyed)
        {
            LevelManager.instance.IncreaseCurrency(currencyWorth);
               
            isDestroyed = true;
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
 
