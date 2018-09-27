using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public Button invokeMenuButton;
    public Image constellationMatchImage;
    public ConstellationsMenuManager constellationsPanel;
    public ConstellationItem displayConstellationItem;
    public void setMenuStatus(bool status)
    {
        
        invokeMenuButton.gameObject.SetActive(!status);
        constellationMatchImage.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
    }
    public void setConstellationItem(ConstellationItem currentItem)
    {
        Debug.Log("Enter set image!");
        displayConstellationItem = currentItem;
        constellationMatchImage.sprite = displayConstellationItem.image;
    }
    // Use this for initialization
    void Start () {
        setMenuStatus(false);
        constellationsPanel.canvasManager = this;
        invokeMenuButton.onClick.AddListener(invokeMenu);
    }

    void invokeMenu()
    {
        setMenuStatus(true);
    }

}
