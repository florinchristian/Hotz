using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioMAnager : MonoBehaviour
{
    public static AudioMAnager instance;
    
    [Header("Audio Sources")] [SerializeField]
    private AudioSource backgroundMusic;
     public AudioSource buttonClick;

    void Start()
    {
        instance = this;
        backgroundMusic.Play();
        
    }
}
