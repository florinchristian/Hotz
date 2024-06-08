using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Assign the panel GameObject in the Inspector

    void Start()
    {
        Invoke("DisablePanel", 2f);
    }

    private void Update()
    {
        Invoke("DisablePanel", 2f);
    }

    void DisablePanel()
    {
        panel.SetActive(false); // Disables the panel
    }
}
