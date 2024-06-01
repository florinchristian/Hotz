using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform Player;
    public GameObject InventoryStore;
    public GameObject MiniMap;

    void Update()
    {
        if (InventoryStore.activeSelf)
            MiniMap.SetActive(false);
        else
            MiniMap.SetActive(true);
    }
    void LateUpdate()
    {
        Vector3 newPosition = Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
