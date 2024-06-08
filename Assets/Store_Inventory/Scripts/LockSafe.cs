using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LockSafe : SafeType
{
    public GameObject panel;
    public GameObject WinningObject;
    public Item potion;
    private List<int> _assignedPassword = new List<int>();
    private List<bool> _verifiablePassword = new List<bool>(4);
    private bool _isOpen;
    private bool _opened;
    
    [SerializeField]
    private Animator _anim;

    public void InitializeList()
    {
        for (int i = 0; i < 4; i++)
            _verifiablePassword[i] = false;
    }

    void AssignValues()
    {
        _assignedPassword.Add(45);
                _assignedPassword.Add(180);
                _assignedPassword.Add(90);
                _assignedPassword.Add(270);
                _verifiablePassword.Add(false);
                _verifiablePassword.Add(false);
                _verifiablePassword.Add(false);
                _verifiablePassword.Add(false);
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
        AssignValues();
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
                Debug.Log(panel.activeSelf);
                LockPanel.instance.SetSafe(this);
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

    public bool CheckPassword(int inputTextField)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_verifiablePassword[i] == false)
            {   Debug.Log(inputTextField);
                if (_assignedPassword[i] == inputTextField)
                {
                    _verifiablePassword[i] = true;
                    return true;
                }
                else
                {
                    InitializeList();
                    return false;
                }
            }
        }

        return false;
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

    public bool PasswordEnd()
    {
        return _verifiablePassword[3];
    }
}
