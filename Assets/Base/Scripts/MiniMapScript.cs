using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform Player;
    public GameObject InventoryStore;
    public GameObject MiniMap;

    private float distance = 10;
    void Update()
    {
        /*if (InventoryStore.activeSelf)
            MiniMap.SetActive(false);
        else
            MiniMap.SetActive(true);*/
    }
    void LateUpdate()
    {
        // Calculate the difference in height between the player and the camera
        float heightDifference =  transform.position.y-Player.position.y;

        // Determine the target Y position for the camera based on the player's height
        // If the player is above the camera, move the camera up; otherwise, move it down
        float targetYPosition =(heightDifference == distance)? transform.position.y : Player.position.y+distance;

        // Set the camera's new position, keeping it 10 units away from the player on the Y axis
        Vector3 newPosition = new Vector3(Player.position.x, targetYPosition, Player.position.z);
        transform.position = newPosition;
    }
}
