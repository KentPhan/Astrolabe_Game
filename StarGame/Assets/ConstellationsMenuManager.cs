using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationsMenuManager : MonoBehaviour
{
    public Button CloseButton;
    public CanvasManager canvasManager;
    // Use this for initialization

    void CloseMenu()
    {
        Debug.Log(canvasManager);
        canvasManager.setMenuStatus(false);
    }

    void Start()
    {
        
        CloseButton.onClick.AddListener(CloseMenu);        
    }

}
