using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
    public Button invokeMenuButton;
    public GameObject constellationMatch;
    public ConstellationsMenuManager constellationsPanel;
    public ConstellationItem displayConstellationItem;
    public GameObject playerFocus;
    private Vector3 offset;

    public void setMenuStatus(bool status)
    {
        
        invokeMenuButton.gameObject.SetActive(!status);
        constellationMatch.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
    }
    public void setConstellationItem(ConstellationItem currentItem)
    {
        Debug.Log("Enter set image!");
        displayConstellationItem = currentItem;

        constellationMatch.transform.GetComponentInChildren<Renderer>().material = currentItem.matchMaterial;
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

    private void Update()
    {
        Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
        constellationMatch.transform.rotation = Quaternion.Euler(newRotation);
    }

}
