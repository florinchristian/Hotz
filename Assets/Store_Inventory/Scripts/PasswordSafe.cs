using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordSafe : SafeType
{
    public GameObject panel;
    public GameObject WinningObject;
    public Item potion;
    
    private string _assignedPassword = "3625";
    private bool _isOpen;
    private bool _opened;
    
    [SerializeField]
    private Animator _anim;

    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _opened = false;
    }
    public  override void OnMouseDown()
    {
        if (!_isOpen && !_opened)
        {
            if (HavePotion())
                Open();
            else
            {
                panel.SetActive(true);
                PasswordPanel.instance.SetSafe(this);
                Cursor.lockState = CursorLockMode.None;
                
            }
        }
        else
            Close();
    }

    private void Close()
    {
        _isOpen = false;
        _anim.SetTrigger("CloseDoor");
        if(WinningObject!=null) WinningObject.SetActive(false);
    }

    private bool HavePotion()
    { 
        return InventoryManager.instace.HasItem(potion);

    }

    public bool CheckPassword(string inputTextField)
    {
        Debug.Log(_assignedPassword);
        return (inputTextField == _assignedPassword);
    }

    public void Open()
    {
        _isOpen = true;
        _anim.SetTrigger("OpenDoor");
        if (WinningObject != null && !_opened)
        {
            WinningObject.SetActive(true);
            _opened = true;
        }
    }
}
