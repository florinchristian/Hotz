using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
     public static InventoryManager instace;
    public List<Item> items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject CloseButtonPrefab;
    private void Awake()
    {
        instace = this;
    }

    public void Add(Item item)
    { 
        
            bool done = false;
            foreach (var inventoryItem in items)
            {
                if (item.name == inventoryItem.name)
                {
                    inventoryItem.count++;
                    done = true;
                    break;
                }
            }

            if (done == false)
            {
                item.count = 1;
                items.Add(item);
            }
            ListItems();
        
    }

    public void Remove(Item item)
    {
        
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            
                GameObject obj = Instantiate(InventoryItem, ItemContent);

                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemImage = obj.transform.Find("ItemImage").GetComponent<Image>();
                var itemCount = obj.transform.Find("Count").GetComponent<TextMeshProUGUI>();

                itemName.text = item.itemName;
                itemImage.sprite = item.image;
                itemCount.text = item.count.ToString();

                GameObject closeButton = Instantiate(CloseButtonPrefab, obj.transform);
                closeButton.GetComponent<Button>().onClick.AddListener(() => CloseInstance(obj, item));

                obj.GetComponent<Button>().onClick.AddListener(() => UseItem(item, obj));
            
        }
    }

    private void UseItem(Item item,GameObject obj)
    {
       item.UseItem();
       if (item.count <= 0)
       {
           Destroy(obj);
           Remove(item);
       }

       var itemCount = obj.transform.Find("Count").GetComponent<TextMeshProUGUI>();
       itemCount.text = item.count.ToString();
    }

    private void ThrowItem(Item item)
    {
        while (item.count > 0)
        {
            Vector3 cameraPosition = Camera.main.transform.position;

            // Define the maximum distance from the camera
            float maxDistanceFromCamera = 10f;
            float minDistanceFromCamera = 2f;

            // Generate random X and Z positions within the adjusted range
            float x = cameraPosition.x + Random.Range(minDistanceFromCamera, maxDistanceFromCamera);
            float z = cameraPosition.z + Random.Range(minDistanceFromCamera, maxDistanceFromCamera);

            Instantiate(item.prefab, new Vector3(x, 1, z), Quaternion.identity);
            item.count--;
        }
    }
    private void CloseInstance(GameObject obj,Item item)
    {
        ThrowItem(item);
        items.Remove(item);
        Destroy(obj);
    }
}
