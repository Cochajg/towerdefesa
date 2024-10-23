using UnityEngine;

[System.Serializable]  // Use este atributo para tornar a classe serializ�vel no inspetor
public class Tower
{
    public string name;
    public int cost;
    public GameObject prefab;

    // Construtor sem par�metros para permitir a serializa��o do Unity
    public Tower() { }

    // Construtor opcional para inicializar programaticamente
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
