using System.Collections.Generic;
using AI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instace;
    public List<Item> items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject CloseButtonPrefab;
    public GameObject light;

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
                CheckForWin(item);
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

    private void CheckForWin(Item item)
    {
        if (item.itemName == "" && item.count == 3)
            SceneManager.LoadScene("Winner");
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
            
            obj.GetComponent<Button>().onClick.AddListener(() =>MagicalPotions(itemName.text,item));
        }
    }

    private void MagicalPotions(string itemName,Item item)
    {
          Debug.Log($"Used {itemName} potion!");
                        
          var playerGameObject = GameObject.Find("Player");
        
          if (itemName.Equals("Invisibility Potion"))
          { 
              playerGameObject.GetComponent<Player>().SetInvisible(3);
              UseItem(item);
          }

          if (itemName.Equals("Darkening Potion"))
          {
              NightVision.instance.GoToDarkMode();
              playerGameObject.GetComponent<Player>().CloseLights(3);
              UseItem(item);
          }

    }
    private void UseItem(Item item)
    {
        if (item.REemovable())
        {
            Remove(item);
        }

        ListItems();
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

    private void CloseInstance(GameObject obj, Item item)
    {
        ThrowItem(item);
        items.Remove(item);
        Destroy(obj);
    }

    public bool HasItem(Item item)
    {
        foreach (var inventoryItem in items)
        {
            if (item.itemName == inventoryItem.name)
            {
                UseItem(item);
                return true;
            }
        }

        return false;
    }
}