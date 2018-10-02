using UnityEngine;

[System.Serializable]
public class Constellation
{
    public string name;
    public int collectable = 0;
    public Vector3 eulerAngles;
    public Vector3 scale;
    public Sprite icon;
    public Material matchMaterial;
    public Material displayMaterial;
    public GameObject displayInMap;
}

