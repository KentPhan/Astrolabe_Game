using UnityEngine;

[System.Serializable]
public class Constellation
{
    public string name;
    public int collectable = 0;
    public Vector3 eulerAngles;
    public Vector3 scale;
    public Sprite icon;
    public Sprite finishedIcon;
    public Material matchMaterial;
    public Material displayMaterial;
    public Material congratulationMaterial;
    public Material finishedMaterial;
    public GameObject displayInMap;
    public Texture[] animation;

}

