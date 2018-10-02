using UnityEngine;
using UnityEngine.UI;

public class ConstellationsScrollViewButton : MonoBehaviour
{
    ConstellationMenu menuManager;
    public Button constellationButton;
    public Text descriptionLabel;
    public Text collectableLabel;
    public Image constellationImage;
    int id;
    // Use this for initialization
    ConstellationItem source;
    void activateConstellation()
    {
        if (source.collectable == 1)
        {
            menuManager.activateConstellationMatch(id);
        }
    }


    void Start()
    {
        constellationButton.onClick.AddListener(activateConstellation);
    }


    public void Setup(ConstellationItem currentItem,
        ConstellationMenu _menuManager, int idInContellationList)
    {
        id = idInContellationList;
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
