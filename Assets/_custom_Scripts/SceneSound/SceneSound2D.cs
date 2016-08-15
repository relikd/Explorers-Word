using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*************************************************************
 * Ereugt AudioSources für jedes übergebene Audio-file.
 * Der Spatial Blend aller Sources ist immer 0.
 * Das Skript soll daher nur für die Soundeffekte Verwendet werden,
 * die unabhängig von Objekten global zu hören sind
 * (z.B. Musik, Erzähler, Regen, Donner). 
 * Per Index kann man einzelne AudioSources ansteuern.
 * Man kann auch alle AudioSources die so erstellt werden gleichzeitig manipulieren.
 *************************************************************/

public class SceneSound2D : MonoBehaviour {

    [SerializeField] private AudioClip[] m_Sounds;           
    private AudioSource[] m_Audiosources;
    private AudioSource m_Audiosource;

    

    void Awake()
    {
        for (int i = 0; i < m_Sounds.Length; i++) 
        {
            m_Audiosource = gameObject.AddComponent<AudioSource>();
            m_Audiosource.clip = m_Sounds[i];
            m_Audiosource.loop = false;
            m_Audiosource.playOnAwake = false;
        }
    }
    public void startSound(int index )
    //Spielt SoundEffect ab, der an der bestimmten Stelle im Array liegt
    //Bricht dafür das Spielen des Aktuellen Sounds aus dieser Soundquelle ab.
    {
            m_Audiosources = GetComponents<AudioSource>();
            m_Audiosources[index].Play();    
    }

    public void pauseSound(int index)
    //Unterbricht Abspielen des Sounds, er kann später weiter abgespielt werden.
    {
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].Pause();
    }
    public void unpauseSound(int index)
    //Spielt Sound weiter ab.
    {
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].UnPause();
    }
    public void stopSound(int index)
    //Bricht abspielen des Sounds ab.
    {
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].Stop();
    }
    public void toggleLoop(int index)
    //schaltet Loop Eigenschaft der Soundquelle an bzw. aus
    {
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].loop = !(m_Audiosources[index].loop);
    }

    public void setVolume(int index,float volume) {
    //Setzt Lautstärke der Soundquelle
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].volume = volume;
    }
    public void setPriority(int index, int Prio) {
    //setzt Priorität der Soundquelle (0 = höchste)
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].priority = Prio;
    }
    public void setPitch(int index, float pitch) {
    //setzt Pitch der Soundquelle
        m_Audiosources = GetComponents<AudioSource>();
        m_Audiosources[index].pitch = pitch;
    }


    public void startSound()
    //Spielt alle Soundeffekte ab
    //Bricht dafür das Spielen der Aktuellen Sounds aus allen hier verwalteten Soundquelle ab.
    {
        m_Audiosources = GetComponents<AudioSource>();
        foreach (AudioSource cur in m_Audiosources) cur.Play();
    }

    public void pauseSound()
    //Unterbricht Abspielen aller Sounds, sie können später weiter abgespielt werden.
    {
        m_Audiosources = GetComponents<AudioSource>();
        foreach (AudioSource cur in m_Audiosources)
        {
            cur.Pause();
        }
    }
    public void unpauseSound()
    //Spielt alle Sound weiter ab.
    {
        m_Audiosources = GetComponents<AudioSource>();
        foreach (AudioSource cur in m_Audiosources)
        {
            cur.UnPause();
        }
    }
    public void stopSound()
    //Bricht abspielen aller Sounds ab.
    {
        foreach (AudioSource cur in m_Audiosources) cur.Stop();
    }
    public void toggleLoop()
    //schaltet Loop Eigenschaft aller Soundquellen an bzw. aus
    {
        foreach (AudioSource cur in m_Audiosources) cur.loop = !(cur.loop);
    }

    public void setVolume(float volume)
    {
        //Setzt Lautstärke aller Soundquelle
        foreach (AudioSource cur in m_Audiosources) cur.volume = volume;
    }
    public void setPriority( int Prio)
    {
        //setzt Priorität aller Soundquellen (0 = höchste) 
        foreach (AudioSource cur in m_Audiosources) cur.priority = Prio;
    }
    public void setPitch(float pitch)
    {
        //setzt Pitch aller Soundquelle
        foreach (AudioSource cur in m_Audiosources) cur.pitch = pitch;
    }
}
