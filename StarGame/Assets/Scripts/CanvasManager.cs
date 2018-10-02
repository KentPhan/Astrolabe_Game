using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public GameObject playerFocus;
    public GameObject constellationMatchDisplayInFocus;

    public Button invokeMenuButton;
    public ConstellationsMenuManager constellationsPanel;
    public MusicManager musicManager;

    private Vector3 offset;

    public ConstellationManager constellationManager;
    // matching constellation
    public ConstellationItem constellationMatchItem;
    private int constellationMatchItemId = -1;

    public void setMenuStatus(bool status)
    {   
        invokeMenuButton.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
        musicManager.ChangeChannel("menu");
        constellationMatchDisplayInFocus.SetActive(false);
        if (status)
            constellationsPanel.RefreshDisplay();
        else if (constellationMatchItemId >= 0)
        {
            musicManager.ChangeChannel("find");
            constellationMatchDisplayInFocus.SetActive(true);
        }
    }
    
    public void setConstellationMatch(int idInCostellationItemList)
    {

        constellationMatchDisplayInFocus.SetActive(true);
        musicManager.ChangeChannel("find");
        Debug.Log("Enter set image! id:"+ idInCostellationItemList);
        constellationMatchItemId = idInCostellationItemList;
        constellationMatchItem = constellationManager.constellationItemList[idInCostellationItemList];
        constellationMatchDisplayInFocus.transform.GetComponentInChildren<Renderer>().material = constellationMatchItem.matchMaterial;
        constellationMatchDisplayInFocus.transform.localScale = constellationMatchItem.scale;
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

    void matchConstellation()
    {
        Debug.Log("Matching!");
        // TODO: animation display

        // change status of constellation
        // set collectable to 0
        constellationManager.constellationItemList[constellationMatchItemId].collectable = 0;
        
        // disable matching constellation in camera and set status to unavailable
        constellationMatchDisplayInFocus.SetActive(false);
        musicManager.ChangeChannel("background");
        constellationMatchItemId = -1;


    }

    private void Update()
    {
        if (constellationMatchDisplayInFocus.activeInHierarchy)
        {
            Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
            constellationMatchDisplayInFocus.transform.rotation = Quaternion.Euler(newRotation);
            musicManager.UpdateFindingDistanceMusic(constellationMatchItem, constellationMatchDisplayInFocus);
            if (ConstellationManager.IsMatchConstellation(constellationMatchItem, constellationMatchDisplayInFocus)) 
                matchConstellation();
        }
    }

}
