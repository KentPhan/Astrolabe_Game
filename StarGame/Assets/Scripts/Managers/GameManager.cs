using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }


    public enum GameState
    {
        Start,
        FreeRoam,
        CollectionLog,
        MatchStarsMode
    }
    private GameState _currentState = GameState.Start;
    private ObjectPool _currentPool;
    public GameObject poolObject;
    private PlayerEntity player;

    private GameManager()
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
        _currentPool = new ObjectPool(poolObject);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GoToStart()
    {
        MusicManager.Instance.ChangeChannel("start");
        CanvasManager.Instance.ShowStart();
        _currentState = GameState.Start;
    }

    /// <summary>
    /// Goes to free roam.
    /// </summary>
    public void GoToFreeRoam()
    {
        MusicManager.Instance.ChangeChannel("playing_default");
        CanvasManager.Instance.ShowFreeRoam();
        _currentState = GameState.FreeRoam;

    }

    /// <summary>
    /// Goes to collection log.
    /// </summary>
    public void GoToCollectionLog()
    {
        CanvasManager.Instance.ShowCollectionLog();
        MusicManager.Instance.ChangeChannel("playing_menu");
        _currentState = GameState.CollectionLog;
    }


    public void GoToMatchStarsMode(int idInCostellationItemList)
    {
        MusicManager.Instance.ChangeChannel("playing_find");
        CanvasManager.Instance.ShowMatchMode(idInCostellationItemList);
        _currentState = GameState.MatchStarsMode;
    }

    public GameState GetCurrentState()
    {
        return _currentState;
    }

    public PlayerEntity GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerEntity>();
        }

        return player;
    }


    public ObjectPool GetPool()
    {
        return _currentPool;
    }
}



