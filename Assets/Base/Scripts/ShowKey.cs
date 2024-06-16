using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    
    public GameObject UiPanel;
    public TextMeshProUGUI UiText;
    public bool IsDisplayed;
    public static ShowKey instance;
    
    private Camera _mainCamera;
    
    void Start()
    {
        instance = this;
        _mainCamera = Camera.main;
        IsDisplayed = false;
        UiPanel.SetActive(false);
    }
    private void Update()
    {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position+rotation*Vector3.forward,rotation*Vector3.up);

        if (IsDisplayed)
        {
            UiPanel.SetActive(true);
        }
        else
        {
            ClosePanel();
        }
    }

    public void CanOpenPanel(string newKey)
    {
        UiText.text = newKey;
        IsDisplayed = true;
    }

    public void CanNotOpenPanel()
    {
        IsDisplayed = false;
    }
    
    public void ClosePanel()
    {
        UiPanel.SetActive(false);
    }
    
}
