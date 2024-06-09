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

    // Update is called once per frame
    void Update()
    {
        if (InventoryStore.activeSelf || SafeUiPassword.activeSelf || SafeUiLock.activeSelf)
            _canFollowMouse = false;
        else _canFollowMouse = true;
    }

    public bool CanFollowMouse()
    {
        return _canFollowMouse;
    }
}
