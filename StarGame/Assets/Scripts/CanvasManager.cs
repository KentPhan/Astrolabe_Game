using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public GameObject playerFocus;
    public GameObject constellationMatchDisplayInFocus;

    public Button invokeMenuButton;
    public ConstellationsMenuManager constellationsPanel;
    
    
    private Vector3 offset;

    public ConstellationManager constellationManager;
    // matching constellation
    public ConstellationItem constellationMatchItem;
    private int constellationMatchItemId;

    public void setMenuStatus(bool status)
    {   
        invokeMenuButton.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
        if (status) 
            constellationsPanel.RefreshDisplay();
    }
    public void setConstellationMatch(int idInCostellationItemList)
    {
        constellationMatchDisplayInFocus.SetActive(true);
        Debug.Log("Enter set image! id:"+ idInCostellationItemList);
        constellationMatchItemId = idInCostellationItemList;
        constellationMatchItem = constellationManager.constellationItemList[idInCostellationItemList];
        constellationMatchDisplayInFocus.transform.GetComponentInChildren<Renderer>().material = constellationMatchItem.matchMaterial;
    }
    // Use this for initialization
    void Start () {
        constellationMatchDisplayInFocus.SetActive(false);
        setMenuStatus(false);
        constellationsPanel.canvasManager = this;
        invokeMenuButton.onClick.AddListener(invokeMenu);
    }

    void invokeMenu()
    {
        setMenuStatus(true);
    }

    void matchConstellation()
    {
        Debug.Log("Matching!");
        // TODO: animation display

        // disable matching constellation in camera 
        constellationMatchDisplayInFocus.SetActive(false);


        // change status of constellation
        // set collectable to 0
        constellationManager.constellationItemList[constellationMatchItemId].collectable = 0;
     
    }

    private void Update()
    {
        if (constellationMatchDisplayInFocus.activeInHierarchy)
        {
            Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
            constellationMatchDisplayInFocus.transform.rotation = Quaternion.Euler(newRotation);
            if (ConstellationManager.IsMatchConstellation(constellationMatchItem, constellationMatchDisplayInFocus)) 
                matchConstellation();
        }
    }

}
