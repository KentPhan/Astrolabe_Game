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


    enum GameState
    {
        Start,
        FreeRoam,
        CollectionLog,
        MatchStarsMode
    }
    private GameState _currentState = GameState.Start;



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



    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GoToFreeRoam()
    {
        CanvasManager.Instance.ShowFreeRoam();
        _currentState = GameState.FreeRoam;

    }

    public void GoToCollectionLog()
    {
        CanvasManager.Instance.ShowCollectionLog();
        MusicManager.Instance.ChangeChannel("menu");
        _currentState = GameState.CollectionLog;
    }


    public void GoToMatchStarsMode()
    {
        CanvasManager.Instance.ShowMatchMode();
        _currentState = GameState.MatchStarsMode;
    }
}



