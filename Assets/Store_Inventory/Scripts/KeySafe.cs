using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySafe : SafeType
{
    public Item key;
    public Item potion;
    public GameObject WinningObject;
    
    [SerializeField]
    private Animator _anim;

    private bool _isOpen;
    private bool _opened;
   
    void Start()
    {
        _anim = GetComponent<Animator>();
        _opened = false;
    }

    public override void OnMouseDown()
    {
        if (!_isOpen && !_opened)
        {
            if (CanOpen())
                Open();
            else if (HavePotion()) Open();
            else 
                _anim.SetTrigger("Shake");
        }
        else
        {
            _isOpen = false;
            _anim.SetTrigger("CloseDoor");
            if(WinningObject!=null) WinningObject.SetActive(false);
        }
    }

    private void Open()
    {
        _isOpen = true;
        _opened = true;
        
         _anim.SetTrigger("OpenSafe");
         if(WinningObject!=null) WinningObject.SetActive(true);
    }

    private bool HavePotion()
    { 
        return InventoryManager.instace.HasItem(potion);

    }
    public  bool CanOpen()
    {   
       return InventoryManager.instace.HasItem(key);
    }
}
