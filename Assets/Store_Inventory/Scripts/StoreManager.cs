using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    public List<Item> items = new List<Item>();
    public Item openingPotion;
    public Item darkeningPotion;
    public Item invisibilityPotion;
    public Transform ItemContent;
    public GameObject StoreItem;
    private void Awake()
    {
        instance = this;
        AddItems();
    }

    private void AddItems()
    {
        items.Add(invisibilityPotion);
        items.Add(darkeningPotion);
        items.Add(openingPotion);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject obj = Instantiate(StoreItem, ItemContent);
            
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("ItemImage").GetComponent<Image> ();
            var itemCount = obj.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            
            itemName.text = item.itemName;
            itemImage.sprite = item.image;
            itemCount.text = item.cost.ToString();
            
            obj.GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
        }
    }

    private void BuyItem(Item item)
    {
        if(MoneyController.instance.BuyItem(item.cost))
            InventoryManager.instace.Add(item);
    }
}
