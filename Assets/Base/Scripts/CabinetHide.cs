using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetHide : MonoBehaviour
{
    public GameObject player;

    public AudioSource sunet;
    //public AudioSource heartbeatSound;
    public GameObject hidingCamera;
    public GameObject mainCamera;
    private bool _canHide;
    private bool _notHiding;
    public Transform door;
    
    private float interactionRange = 10f;

    private void Start()
    {
        _notHiding = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {      if(_notHiding)
                 _canHide = CanHide();
            if (_canHide && _notHiding)
            {
                player.GetComponent<PlayerMovement>().SetCabinet(this);
            }
            else if (_notHiding == false)
            {
                
                 player.GetComponent<PlayerMovement>().SetCabinet(null); 
                 Unhide();
            }
            
        }
       
        
    }
 
    private bool CanHide()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange))
        {
            if (hit.collider.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
    private IEnumerator SmoothDoorTransition(float rotation)
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(door.rotation.eulerAngles.x, door.rotation.eulerAngles.y + rotation, door.rotation.eulerAngles.z));

        // Calculate the duration of the transition
        float transitionDuration = 1f; // Adjust this value to control the speed of the transition

        // Interpolate the door's position and rotation towards the target values over time
        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            float progress = t / transitionDuration;
            door.rotation = Quaternion.Slerp(door.rotation, targetRotation, progress);
            yield return null; // Wait for the next frame
        }

        
        door.rotation = targetRotation;
    }

    public void Hide()
    {
       
        sunet.Play();
        if(_notHiding)
            door.Rotate(0, 45, 0, Space.World);//open door
        
       

        //hide the camera
        player.SetActive(false);
        mainCamera.SetActive(false);
        hidingCamera.SetActive(true);
        
        if(_notHiding)
            StartCoroutine(SmoothDoorTransition(-45f)); //close door
       
        _notHiding = false;
    }


    public void Unhide()
    {   
        player.SetActive(true);
        hidingCamera.SetActive(false);
        mainCamera.SetActive(true);
        _notHiding = true;
        door.Rotate(0, 45, 0, Space.World);
        StartCoroutine(SmoothDoorTransition(-45f));
        sunet.Play();

    }
}
