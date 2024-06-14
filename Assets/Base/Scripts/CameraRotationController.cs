using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    public static CameraRotationController instance;
    public GameObject InventoryStore;
    public GameObject SafeUiPassword;
    public GameObject SafeUiLock;
    private bool _canFollowMouse;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (InventoryStore.activeSelf || SafeUiPassword.activeSelf || SafeUiLock.activeSelf)
        {
            _canFollowMouse = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            _canFollowMouse = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public bool CanFollowMouse()
    {
        return _canFollowMouse;
    }
}
