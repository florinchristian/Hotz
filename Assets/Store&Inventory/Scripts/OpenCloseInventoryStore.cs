using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseInventoryStore : MonoBehaviour
{
    public GameObject instance;
    public Button inventoryButton;
    public Button storeButton;
    public GameObject inventory;
    public GameObject store;
    private bool _isOpen;
    private float _transparencyLevel = 0.5f;
    void Start()
    {
        
        OpenInventory();
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                instance.SetActive(true);
            }
            else
            {
                instance.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && _isOpen)
            OpenInventory();
        if (Input.GetKeyDown(KeyCode.RightArrow) && _isOpen)
            OpenStore();
    }

    private void OpenStore()
    {
        inventory.SetActive(false);
        store.SetActive(true);
        StoreManager.instance.ListItems();
        Image image=inventoryButton.GetComponent<Image>();
        image.color=new Color(image.color.r, image.color.g, image.color.b, 1);
        Image storeImage=storeButton.GetComponent<Image>();
        storeImage.color=new Color(storeImage.color.r, storeImage.color.g, storeImage.color.b, _transparencyLevel);
    }

    private void OpenInventory()
    { 
        inventory.SetActive(true);
        store.SetActive(false);
        Image image=inventoryButton.GetComponent<Image>();
        image.color=new Color(image.color.r, image.color.g, image.color.b, _transparencyLevel);
        Image storeImage=storeButton.GetComponent<Image>();
        storeImage.color=new Color(storeImage.color.r, storeImage.color.g, storeImage.color.b, 1);
    }
}
