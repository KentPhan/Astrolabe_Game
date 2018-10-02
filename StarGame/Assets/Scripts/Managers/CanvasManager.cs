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



    private Vector3 offset;

    // Buttons
    public Button startGameButton;
    public Button openConsellationMenuButton;
    public Button closeConstellationMenuButton;

    // Panels
    public GameObject startPanel;
    public GameObject freeRoamPanel;
    public GameObject collectionMenuPanel;
    public GameObject constellationMatchScreenPanel;


    // matching constellation
    public Constellation ConstellationMatch;
    private int constellationMatchItemId = -1;
    private float matchTime = 0.5f;

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
        closeConstellationMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GoToFreeRoam();
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
        freeRoamPanel.SetActive(false);

        // Show active panel
        collectionMenuPanel.SetActive(true);
        collectionMenuPanel.GetComponent<ConstellationMenu>().RefreshDisplay();

        // Stuff Im trying to figure out still
        constellationMatchScreenPanel.SetActive(false);
    }

    public void ShowMatchMode(int i_matchId)
    {
        // Deactivate other panels
        collectionMenuPanel.SetActive(false);
        freeRoamPanel.SetActive(true);

        if (constellationMatchItemId >= 0)
        {
            MusicManager.Instance.ChangeChannel("find");

            constellationMatchScreenPanel.SetActive(true);
        }
    }


    public void setConstellationMatch(int idInCostellationItemList)
    {

        constellationMatchScreenPanel.SetActive(true);
        MusicManager.Instance.ChangeChannel("find");
        Debug.Log("Enter set image! id:" + idInCostellationItemList);
        constellationMatchItemId = idInCostellationItemList;
        ConstellationMatch = ConstellationManager.Instance.constellationItemList[idInCostellationItemList];
        constellationMatchScreenPanel.transform.GetComponentInChildren<Renderer>().material = ConstellationMatch.matchMaterial;
        constellationMatchScreenPanel.transform.localScale = ConstellationMatch.scale;
    }


    void matchConstellation()
    {
        Debug.Log("Matching!");
        // TODO: animation display

        // change status of constellation
        // set collectable to 0
        ConstellationManager.Instance.constellationItemList[constellationMatchItemId].collectable = 0;

        // disable matching constellation in camera and set status to unavailable
        constellationMatchScreenPanel.SetActive(false);
        MusicManager.Instance.ChangeChannel("background");
        constellationMatchItemId = -1;


    }

    private void Update()
    {
        if (constellationMatchItemId >= 0 && constellationMatchScreenPanel.activeInHierarchy)
        {
            Vector3 newRotation = new Vector3(GameManager.Instance.player.transform.eulerAngles.x, GameManager.Instance.player.transform.eulerAngles.y, GameManager.Instance.player.transform.eulerAngles.z);
            constellationMatchScreenPanel.transform.rotation = Quaternion.Euler(newRotation);
            MusicManager.Instance.UpdateFindingDistanceMusic(ConstellationMatch, constellationMatchScreenPanel);
            if (ConstellationManager.IsMatchConstellation(ConstellationMatch, constellationMatchScreenPanel))
            {
                matchTime += Time.deltaTime;
                if (matchTime > 0.5)
                    matchConstellation();
            }
            else
            {
                matchTime = 0;
            }
        }
    }

}
