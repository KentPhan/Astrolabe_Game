using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public Button invokeMenuButton;
    public Image constellationMatchImage;
    public ConstellationsMenuManager constellationsPanel;
    
    public void setMenuStatus(bool status)
    {
        
        invokeMenuButton.gameObject.SetActive(!status);
        constellationMatchImage.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
    }

	// Use this for initialization
	void Start () {
        setMenuStatus(false);
        constellationsPanel.canvasManager = this;
        invokeMenuButton.onClick.AddListener(invokeMenu);
    }

    void invokeMenu()
    {
        Debug.Log("You have clicked the button!");
        setMenuStatus(true);
    }

}
