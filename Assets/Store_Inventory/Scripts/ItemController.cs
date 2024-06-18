using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void TakeInventoryItem()
    { 
      
        InventoryManager.instace.Add(item);
        Destroy(gameObject);
      
    }
    private void OnMouseDown()
    {
        if (item.isMoney()){Debug.Log("isMoney"); CollectMoney();}
        else TakeInventoryItem();
    }

    private void CollectMoney()
    {
        MoneyController.instance.CollectMoney(item.count);
        AudioMAnager.instance.collectMoney.Play();
        Destroy(gameObject);
    }
}
