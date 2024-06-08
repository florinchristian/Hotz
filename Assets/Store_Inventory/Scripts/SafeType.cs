using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SafeType : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    public Item potion;
    void Start()
    {
        _anim = GetComponent<Animator>();
        
    }

    public virtual void OnMouseDown()
    { 
        if(CanOpen())
            _anim.SetTrigger("OpenSafe");
        else  if(HavePotion())  _anim.SetTrigger("OpenSafe");
    }

    private bool HavePotion()
    { 
        return InventoryManager.instace.HasItem(potion);

    }

    public virtual bool CanOpen()
    {
        return false;
    }
}
