using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buding : MonoBehaviour
{

    public static buding instance;
    [SerializeField] private Tower[] towers;
  


    private int SelectedTower=0;
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }
    public void SetSelectedTower(int _SelectedTower)
    {
        SelectedTower = _SelectedTower;
    }
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
