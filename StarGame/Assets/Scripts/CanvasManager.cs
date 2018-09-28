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
        constellationsPanel.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
    }
    public void setConstellationItem(ConstellationItem currentItem)
    {
        Debug.Log("Enter set image!");
        displayConstellationItem = currentItem;
        //constellationMatchImage.sprite = displayConstellationItem.image;
        
        //Debug.Log(displayConstellationItem.image.rect.width + " " + displayConstellationItem.image.rect.height);

        //Debug.Log(displayConstellationItem.image.rect.width + " " + displayConstellationItem.image.rect.height);


    }
    // Use this for initialization
    void Start () {
        setMenuStatus(false);
        //offset = constellationMatchImage.transform.position - playerCamera.transform.position;
        constellationsPanel.canvasManager = this;
        invokeMenuButton.onClick.AddListener(invokeMenu);
    }

    void invokeMenu()
    {
        setMenuStatus(true);
    }

    private void Update()
    {
        // transform.position = player.transform.position + offset;
        //constellationMatchImage.transform.position = playerCamera.transform.position;
        Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
        constellationMatch.transform.rotation = Quaternion.Euler(newRotation);
        //constellationsPanel.transform.localEulerAngles = playerCamera.transform.localEulerAngles;
        //float dist = Vector3.Distance(playerCamera.transform.position, constellationMatchImage.transform.position);
        //print("Distance to other: " + dist);
        //Debug.Log(playerCamera.transform.eulerAngles);
        //Debug.Log(playerCamera.transform.localEulerAngles);
        //Debug.Log(playerCamera.transform.localRotation);
        //constellationMatchImage.transform.position = playerCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane)) - offset;
        //Debug.Log(Screen.width / 2 + " " + Screen.height / 2);
        //Debug.Log(playerCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane)) - offset);
    }

}
