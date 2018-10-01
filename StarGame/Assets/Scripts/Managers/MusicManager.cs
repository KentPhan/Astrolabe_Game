using System.Collections;
using System.Collections.Generic;
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


    public AudioMixerSnapshot menu;
    public AudioMixerSnapshot find;
    public AudioMixerSnapshot background;
    public AudioMixerSnapshot match;
    public GameObject findMusicCollection;
    public float bpm = 128;

    private float m_TransitionIn; // fade in
    private float m_QuarterNote;

    private MusicManager()
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

    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        find.TransitionTo(m_TransitionIn);
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
            case "find": find.TransitionTo(m_TransitionIn); break;
            case "menu": menu.TransitionTo(m_TransitionIn); break;
            case "background": background.TransitionTo(m_TransitionIn); break;
            case "match": match.TransitionTo(m_TransitionIn); break;
        }

    }

    bool lessThanDistance(ConstellationItem displayItem, GameObject currentFocus, float distance)
    {
        if (Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.x) - constrainEulerAngle(currentFocus.transform.eulerAngles.x)) < distance &&
            Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.y) - constrainEulerAngle(currentFocus.transform.eulerAngles.y)) < distance &&
            Mathf.Abs(constrainEulerAngle(displayItem.eulerAngles.z) - constrainEulerAngle(currentFocus.transform.eulerAngles.z)) < distance)
            return true;
        return false;
    }


    public void UpdateFindingDistanceMusic(ConstellationItem displayItem, GameObject currentFocus)
    {
        float distanceA = 30f;
        float distanceB = 15f;
        float distanceC = 5f;
        AudioSource beat = findMusicCollection.GetComponent<AudioSource>();
        if (lessThanDistance(displayItem, currentFocus, distanceA))
        {
            beat.mute = false;
            if (lessThanDistance(displayItem, currentFocus, distanceC))
                beat.pitch = 2f;
            else if (lessThanDistance(displayItem, currentFocus, distanceB))
                beat.pitch = 1.5f;
            else
                beat.pitch = 1f;
        }
        else
        {
            beat.mute = true;
        }

    }
}
