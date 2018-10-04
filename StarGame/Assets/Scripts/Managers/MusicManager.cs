using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    private static MusicManager _instance = null;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MusicManager();
            }
            return _instance;
        }
    }


    public AudioMixerSnapshot Playing_Default;
    public AudioMixerSnapshot Playing_Match;
    public AudioMixerSnapshot Start_Default;

    public GameObject buttonClickSound;
    public GameObject successSound;

    public GameObject findMusicCollection;
    public float bpm = 128;

    private float m_TransitionIn; // fade in
    private float m_QuarterNote;

    private MusicManager()
    {

    }
    void PlaySound(GameObject playingSound)
    {
        playingSound.GetComponent<AudioSource>().Play();
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

    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        Start_Default.TransitionTo(m_TransitionIn);
    }

    // Use this for initialization
    public static float constrainEulerAngle(float x)
    {
        x %= 360;
        if (x > 180)
            x -= 360;
        return x;
    }

    public void ChangeChannel(string channel)
    {
        switch (channel)
        {
            case "start": Start_Default.TransitionTo(m_TransitionIn); break;
            case "playing_find": Playing_Match.TransitionTo(m_TransitionIn); break;
            case "playing_menu": Playing_Default.TransitionTo(m_TransitionIn); break;
            case "playing_background": Playing_Default.TransitionTo(m_TransitionIn); break;
            case "playing_default": Playing_Default.TransitionTo(m_TransitionIn); break;
        }
    }


    public void PlaySoundEffect(string soundName)
    {
        Debug.Log(soundName);
        switch (soundName)
        {
            case "button_click": PlaySound(buttonClickSound); break;
            case "success": PlaySound(successSound); break;
        }
    } 

    bool lessThanDistance(Constellation display, GameObject currentFocus, float distance)
    {
        if (Mathf.Abs(constrainEulerAngle(display.eulerAngles.x) - constrainEulerAngle(currentFocus.transform.eulerAngles.x)) < distance &&
            Mathf.Abs(constrainEulerAngle(display.eulerAngles.y) - constrainEulerAngle(currentFocus.transform.eulerAngles.y)) < distance &&
            Mathf.Abs(constrainEulerAngle(display.eulerAngles.z) - constrainEulerAngle(currentFocus.transform.eulerAngles.z)) < distance)
            return true;
        return false;
    }


    public void UpdateFindingDistanceMusic(Constellation display, GameObject currentFocus)
    {
        float distanceA = 30f;
        float distanceB = 15f;
        float distanceC = 5f;
        AudioSource beat = findMusicCollection.GetComponent<AudioSource>();
        if (lessThanDistance(display, currentFocus, distanceA))
        {
            beat.mute = false;
            if (lessThanDistance(display, currentFocus, distanceC))
                beat.pitch = 2.5f;
            else if (lessThanDistance(display, currentFocus, distanceB))
                beat.pitch = 2f;
            else
                beat.pitch = 1.5f;
        }
        else
        {
            beat.mute = true;
        }

    }
}
