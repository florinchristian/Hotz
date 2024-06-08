using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncollectableItems : MonoBehaviour
{
    public GameObject panel;

    private void OnMouseDown()
    {
        panel.SetActive(true);
    }
}
