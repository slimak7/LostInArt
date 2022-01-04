using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{

    [System.Serializable]
    public class Clip
    {
        public enum rolloff
        {
            logarithmic, linear, custom
        };



        public string name;

        public AudioClip audioClip;

        [Range(0, 1f)]
        public float volume;

        [Range(0, 256)]
        public int priority;

        public bool loop;

        public bool playOnAwake;

        public GameObject gObject;

        public AudioMixerGroup mixerGroup;

        [Header("3d sound doesn't play on awake!")]
        
        public bool sound3D;
        

        [Header("Default - 1")]
        [Range(0,1f)]
        public float spatialBlend;

        public float minDist;
        public float maxDist;

        [Header("Default - 1")]
        [Range(0, 1f)]
        public float pitch;

        [Header("Default - 1")]
        [Range(0, 1f)]
        public float reverbZoneMix;

        public rolloff volumeRolloff;

        [HideInInspector]
        public AudioSource AS;
    }
}
