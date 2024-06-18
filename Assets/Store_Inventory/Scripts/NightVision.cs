using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightVision : MonoBehaviour
{
    public static NightVision instance;
    public Color defaultColor;

    public Color nightVision;
    public GameObject light;
    private PostProcessVolume volume;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = defaultColor;
        instance = this;
    }

    public void GoToDarkMode()
    {
        RenderSettings.ambientLight = nightVision;
        light.SetActive(false);
    }

    public void GoToLightMode()
    {
        RenderSettings.ambientLight = defaultColor;
        light.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
