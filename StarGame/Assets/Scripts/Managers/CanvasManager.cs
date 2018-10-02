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
    public GameObject constellationMatchDisplayInFocus;




    private Vector3 offset;

    // Buttons
    public Button startGameButton;
    public Button openConsellationMenuButton;

    // Panels
    public GameObject startPanel;
    public GameObject freeRoamPanel;
    public GameObject collectionMenuPanel;


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
        collectionMenuPanel.SetActive(false);
        //constellationsPanel.canvasManager = this;

        // Event listeners
        startGameButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GoToFreeRoam();
        });
        openConsellationMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GoToCollectionLog();
        });
    }


    public void ShowFreeRoam()
    {
        // Deactivate other panels
        collectionMenuPanel.SetActive(false);
        startPanel.SetActive(false);

        // Show active panel
        freeRoamPanel.SetActive(true);
    }

    public void ShowCollectionLog()
    {
        // Deactivate other panels
        startPanel.SetActive(false);

        // Show active panel
        collectionMenuPanel.SetActive(true);
        collectionMenuPanel.GetComponent<ConstellationsMenuManager>().RefreshDisplay();

        // Stuff Im trying to figure out still
        constellationMatchDisplayInFocus.SetActive(false);
    }

    public void ShowMatchMode(int i_matchId)
    {
        // Deactivate other panels
        collectionMenuPanel.SetActive(false);

        if (constellationMatchItemId >= 0)
        {
            MusicManager.Instance.ChangeChannel("find");
            constellationMatchDisplayInFocus.SetActive(true);
        }
    }


    public void setConstellationMatch(int idInCostellationItemList)
    {

        constellationMatchDisplayInFocus.SetActive(true);
        MusicManager.Instance.ChangeChannel("find");
        Debug.Log("Enter set image! id:" + idInCostellationItemList);
        constellationMatchItemId = idInCostellationItemList;
        constellationMatchItem = constellationManager.constellationItemList[idInCostellationItemList];
        constellationMatchDisplayInFocus.transform.GetComponentInChildren<Renderer>().material = constellationMatchItem.matchMaterial;
        constellationMatchDisplayInFocus.transform.localScale = constellationMatchItem.scale;
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
        MusicManager.Instance.ChangeChannel("background");
        constellationMatchItemId = -1;


    }

    private void Update()
    {
        if (constellationMatchItemId >= 0 && constellationMatchDisplayInFocus.activeInHierarchy)
        {
            Vector3 newRotation = new Vector3(playerFocus.transform.eulerAngles.x, playerFocus.transform.eulerAngles.y, playerFocus.transform.eulerAngles.z);
            constellationMatchDisplayInFocus.transform.rotation = Quaternion.Euler(newRotation);
            MusicManager.Instance.UpdateFindingDistanceMusic(constellationMatchItem, constellationMatchDisplayInFocus);
            if (ConstellationManager.IsMatchConstellation(constellationMatchItem, constellationMatchDisplayInFocus))
                matchConstellation();
        }
    }

}
