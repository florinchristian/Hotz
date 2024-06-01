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
    private ItemType itemType;

    private void selectType()
    {  
        switch (itemName)
        { 
            case "Darkening Potion":
                itemType = new DarkeningPotion();
                break;
            case "Invisibility Potion":
                itemType = new InvisibilityPotion();
                break;
            case "Opening Potion":
                itemType = new OpeningPotion();
                break;
        }
    }

    public void UseItem()
    {
        selectType();
        itemType.UseItem();
        count--;
    }


    public bool isMoney()
    {   Debug.Log(itemName);
        return itemName == "Coin";
    }
}