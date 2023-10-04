using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource sound;
    public float soundVolume;
    public static SoundController Instance;

    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(sound == null)
        {
            sound = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(sound != null)
        {
            soundVolume = sound.volume;

        }
    }
}
