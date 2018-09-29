using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationsScrollViewButton : MonoBehaviour {
    ConstellationsMenuManager menuManager;
    public Button constellationButton;
    public Text descriptionLabel;
    public Text collectableLabel;
    public Image constellationImage;
    // Use this for initialization
    ConstellationItem source;
    void activateConstellation()
    {
        if (source.collectable == 1)
        {
            menuManager.activateConstellation(source);
        }
    }


    void Start () {
        constellationButton.onClick.AddListener(activateConstellation);
    }


    public void Setup(ConstellationItem currentItem,
        ConstellationsMenuManager _menuManager)
    {
        source = currentItem;
        menuManager = _menuManager;
        descriptionLabel.text = source.name;
        constellationImage.sprite = source.icon;
        if (source.collectable == 1)
            collectableLabel.text = "Avaiable";
        else
            collectableLabel.text = "Unavaiable";

    }
}
