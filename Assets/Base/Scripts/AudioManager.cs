using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioMAnager : MonoBehaviour
{
    public static AudioMAnager instance;
    
    [Header("Audio Sources")] 
    public AudioSource backgroundMusic;
    public AudioSource buttonClick;
    public AudioSource cabinetDoor;

    void Start()
    {
        instance = this;
        
    }
}
