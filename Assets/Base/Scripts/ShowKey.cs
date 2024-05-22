using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    public GameObject objectToInteractWith; // Assign in Inspector
    public float interactionRange = 10f; // Set the interaction range
    public GameObject key;
    public string tag;
    private void Update()
    {
        RaycastHit hit;
        if (Camera.main != null)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,
                    interactionRange))
            {
                if (hit.collider.gameObject.CompareTag(tag))
                {
                    ShowButton();

                }
                else
                {
                    HideButton();

                }
            }
            else
            {
                HideButton();

            }
        }

    }

    private void ShowButton()
    {
       key.SetActive(true);
     
    }

    private void HideButton()
    {
        key.SetActive(false);
       
    }
    
}
