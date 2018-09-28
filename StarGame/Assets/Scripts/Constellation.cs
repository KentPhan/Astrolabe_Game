using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ConstellationItem
{
    public string name;
    public int collectable = 0;
    public Vector3 rotation;
    public Sprite icon;
    public Material matchMaterial;
    public Material displayMaterial;
    public GameObject displayInMap;
}

