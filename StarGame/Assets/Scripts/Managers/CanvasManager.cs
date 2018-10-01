using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance = null;

    public static CanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CanvasManager();
            }
            return _instance;
        }
    }


    public GameObject playerFocus;
    //public GameObject constellationMatchDisplayInFocus;


    public ConstellationsMenuManager constellationsPanel;

    private Vector3 offset;

    // Buttons
    public Button startGameButton;
    public Button openConsellationMenuButton;

    // Panels
    public GameObject startPanel;


    // matching constellation
    public ConstellationManager constellationManager;
    public ConstellationItem constellationMatchItem;
    private int constellationMatchItemId = -1;

    private CanvasManager()
    {

    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        setMenuStatus(false);
        constellationsPanel.canvasManager = this;

        // Event listeners
        startGameButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GoToFreeRoam();
        });
        openConsellationMenuButton.onClick.AddListener(invokeMenu);
    }

    void invokeMenu()
    {
        setMenuStatus(true);
    }

    public void ShowFreeRoam()
    {
        Debug.Log("You clicked the button!");
        setMenuStatus(false);
        startPanel.SetActive(false);
    }


    public void setMenuStatus(bool status)
    {
        openConsellationMenuButton.gameObject.SetActive(!status);
        constellationsPanel.transform.gameObject.SetActive(status);
        MusicManager.Instance.ChangeChannel("menu");
        //constellationMatchDisplayInFocus.SetActive(false);
        if (status)
            constellationsPanel.RefreshDisplay();
        else if (constellationMatchItemId >= 0)
        {
            MusicManager.Instance.ChangeChannel("find");
            //constellationMatchDisplayInFocus.SetActive(true);
        }
    }

    public void setConstellationMatch(int idInCostellationItemList)
    {

        //constellationMatchDisplayInFocus.SetActive(true);
        MusicManager.Instance.ChangeChannel("find");
        Debug.Log("Enter set image! id:" + idInCostellationItemList);
        constellationMatchItemId = idInCostellationItemList;
        constellationMatchItem = constellationManager.constellationItemList[idInCostellationItemList];
        //constellationMatchDisplayInFocus.transform.GetComponentInChildren<Renderer>().material = constellationMatchItem.matchMaterial;
    }


    void matchConstellation()
    {
        Debug.Log("Matching!");
        // TODO: animation display

        // change status of constellation
        // set collectable to 0
        constellationManager.constellationItemList[constellationMatchItemId].collectable = 0;

        // disable matching constellation in camera and set status to unavailable
        //constellationMatchDisplayInFocus.SetActive(false);
        MusicManager.Instance.ChangeChannel("background");
        constellationMatchItemId = -1;


    }

    private void Update()
    {
        //if (constellationMatchDisplayInFocus.activeInHierarchy)
        //{
        //    Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
        //    constellationMatchDisplayInFocus.transform.rotation = Quaternion.Euler(newRotation);
        //    MusicManager.Instance.UpdateFindingDistanceMusic(constellationMatchItem, constellationMatchDisplayInFocus);
        //    if (ConstellationManager.IsMatchConstellation(constellationMatchItem, constellationMatchDisplayInFocus))
        //        matchConstellation();
        //}
    }

}
