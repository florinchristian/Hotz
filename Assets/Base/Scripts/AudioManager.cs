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
    public AudioSource buyItem;
    public AudioSource gameOver;
    public AudioSource wrongCode;
    public AudioSource collectMoney;

    void Start()
    {
        instance = this;
        
    }
}
