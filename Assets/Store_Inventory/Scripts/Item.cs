using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item",menuName = "Item/Create new item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int count;
    public Sprite image;
    public ItemController prefab;
    public int cost;

    public bool REemovable()
    {
        count--;
        if (count <= 0)
            return true;
        return false;
    }


    public bool isMoney()
    {   Debug.Log(itemName);
        return itemName == "Coin";
    }
}