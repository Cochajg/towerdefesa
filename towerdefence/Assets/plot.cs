using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject tower;
    private Color starColor;

    void Start()
    {
        if (sr != null)
        {
            starColor = sr.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer não atribuído!");
        }
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = starColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;
        Tower towerToBuild = buding.instance.GetSelectedTower();

        if (towerToBuild.cost> LevelManager.instance.currency)
        {
            Debug.Log("oi");
            return; 
        }
        
        LevelManager.instance.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

    }

    void Update()
    {
    }
}
