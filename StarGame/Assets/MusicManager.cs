using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {
    public AudioMixerSnapshot menu;
    public AudioMixerSnapshot find;
    public AudioMixerSnapshot background;
    public AudioMixerSnapshot match;
    public float bpm = 128;

    private float m_TransitionIn; // fade in
    private float m_QuarterNote;
    // Use this for initialization

    public void ChangeChannel(string channel)
    {
        switch (channel)
        {
            case "find": find.TransitionTo(m_TransitionIn);break;
            case "menu": menu.TransitionTo(m_TransitionIn); break;
            case "background": background.TransitionTo(m_TransitionIn); break;
            case "match": match.TransitionTo(m_TransitionIn); break;
        }
        
    }

    void Start () {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        find.TransitionTo(m_TransitionIn);
    }
}
