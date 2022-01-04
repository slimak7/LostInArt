using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Audio;

public class AudioManager : MonoBehaviour {

    public bool playRandomOnAwake = true;

    public bool setAudioManAsAChildOfPlayer = true;
    public bool dontDestroyOnLoad = true;

    public AudioSection[] audioSections;

    public static AudioManager instance;

    private AudioSource currentAS;

    private bool changeMusic;

    private void Awake()
    {
        
        if (dontDestroyOnLoad)
        DontDestroyOnLoad(gameObject);

        instance = this;

        changeMusic = true;

        for (int i = 0; i < audioSections.Length; i++)
        {

            for (int j = 0; j < audioSections[i].audioClips.Length; j++)
            {

                AudioSource AS = (audioSections[i].audioClips[j].gObject == null) ? gameObject.AddComponent<AudioSource>() :
                    audioSections[i].audioClips[j].gObject.AddComponent<AudioSource>();
                
                AS.clip = audioSections[i].audioClips[j].audioClip;
                AS.volume = audioSections[i].audioClips[j].volume;
                AS.loop = audioSections[i].audioClips[j].loop;
                AS.priority = audioSections[i].audioClips[j].priority;

                AS.outputAudioMixerGroup = audioSections[i].audioClips[j].mixerGroup;

                if ((audioSections[i].audioClips[j].sound3D))
                {
                    AS.spatialBlend = audioSections[i].audioClips[j].spatialBlend;
                    
                    AS.minDistance = audioSections[i].audioClips[j].minDist;
                    AS.maxDistance = audioSections[i].audioClips[j].maxDist;
                    AS.pitch = audioSections[i].audioClips[j].pitch;
                    AS.reverbZoneMix = audioSections[i].audioClips[j].reverbZoneMix;

                    if (audioSections[i].audioClips[j].volumeRolloff == Clip.rolloff.logarithmic)
                    AS.rolloffMode = AudioRolloffMode.Logarithmic;

                    if (audioSections[i].audioClips[j].volumeRolloff == Clip.rolloff.linear)
                        AS.rolloffMode = AudioRolloffMode.Linear;

                    if (audioSections[i].audioClips[j].volumeRolloff == Clip.rolloff.custom)
                        AS.rolloffMode = AudioRolloffMode.Custom;
                    // AS.Play();
                }

                AS.playOnAwake = audioSections[i].audioClips[j].playOnAwake;
                

                audioSections[i].audioClips[j].AS = AS;
            }
        }

        if (playRandomOnAwake)
        {
           currentAS =  playRandomSound("Music");
        }


    }

    private void OnApplicationPause(bool pause)
    {
        changeMusic = !pause;
    }

    

    private void Update()
    {//!pauseSettings.PauseManager.isPaused
        if (currentAS != null)
        if (!currentAS.isPlaying && changeMusic)
        {
              currentAS =  playRandomSound("Music");
        }
    }

    public void playSound (string sectionName, string clipName)
    {

        AudioSource AS = getSource(sectionName, clipName);

        if (AS != null)
        {
            AS.Play();
        }
        
            
        }

    public void stopPlaying ()
    {



        currentAS.Stop();
    }


    private AudioSource getSource (string sectionName, string clipName)
    {
        AudioSection audioS = Array.Find<AudioSection>(audioSections, asec => asec.name == sectionName);

        if (audioS == null)
            return null;

        Clip[] c = audioS.audioClips;

        Clip clip = Array.Find<Clip>(c, cl => cl.name == clipName);

        if (clip == null)
            return null;

        return clip.AS;

        
    }

    public AudioSource playRandomSound (string sectionName)
    {
        AudioSection audioS = Array.Find<AudioSection>(audioSections, asec => asec.name == sectionName);

        if (audioS == null)
            return null;

        Clip[] c = audioS.audioClips;

        int r = UnityEngine.Random.Range(0, c.Length);

        c[r].AS.Play();

        currentAS = c[r].AS;

        return c[r].AS;

        
    }

    public void turnDownCurrentSound ()
    {
        StartCoroutine(turnDown());
    }
    
    private IEnumerator turnDown ()
    {
        if (currentAS == null)
            yield break;

        while (currentAS.volume > 0)
        {
            currentAS.volume -= 0.05f;
            yield return null;
        }
    }
    
}


