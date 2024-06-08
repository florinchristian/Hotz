using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySafe : MonoBehaviour
{
    public Item key;
    public Item potion;
    
    [SerializeField]
    private Animator _anim;

    private bool _isOpen;
   
    void Start()
    {
        _anim = GetComponent<Animator>();
        
    }

    public void OnMouseDown()
    {
        if (!_isOpen)
        {
            if (CanOpen())
                Open();
            else if (HavePotion()) Open();
        }
        else
        {
            _isOpen = false;
            _anim.SetTrigger("CloseDoor");
        }
    }

    private void Open()
    {
        _isOpen = true;
         _anim.SetTrigger("OpenSafe");
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
