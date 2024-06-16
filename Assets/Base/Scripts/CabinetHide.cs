using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class CabinetHide : MonoBehaviour
{
    public GameObject player;
    
    public GameObject hidingCamera;
    public GameObject mainCamera;
    public GameObject sneakPeekPanel;
    private bool _canHide;
    private bool _notHiding;
    private bool _sneakPeek;
    public Transform door;
    public LayerMask isCabinet;

    private float interactionRange = 5f;
    private string keyboardKey = "Press F";

    private void Start()
    {
        _notHiding = true;
        _sneakPeek = false;
    }

    void Update()
    {
        if (_sneakPeek)
        {
            HandleState();
            return;
        }

        if(_notHiding) _canHide = CanHide();
        if(Input.GetKeyDown(KeyCode.F))
        {     
            if (_canHide && _notHiding)
            {
                player.GetComponent<PlayerMovement>().SetCabinet(this);
                
            }
            else if (_notHiding == false)
            {
                StartCoroutine(SmoothDoorTransition(45f,5f));
                sneakPeekPanel.SetActive(true);
                _sneakPeek = true;
            }

        }


    }

    private void HandleState()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            sneakPeekPanel.SetActive(false);
            _sneakPeek = false;
            Unhide();
        }

        if (Input.GetKeyDown(KeyCode.D))
      {
          StartCoroutine(SmoothDoorTransition(0, 5f));
          _sneakPeek = false;
          sneakPeekPanel.SetActive(false);
      }
    }

    private bool CanHide()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange,isCabinet))
        {
                ShowKey.instance.CanOpenPanel(keyboardKey);
                return true;
        }
        else
        {
            ShowKey.instance.CanNotOpenPanel();
             return false;
        }
       
    }
    private IEnumerator SmoothDoorTransition(float rotation,float time)
    {
        Quaternion startRotation = door.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(door.rotation.eulerAngles.x, rotation, door.rotation.eulerAngles.z));

        float transitionDuration = time;

        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            float progress = t / transitionDuration;
            door.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null; // Wait for the next frame
        }
        if(IsDoorClosing(rotation)) AudioMAnager.instance.cabinetDoor.Play();
        door.rotation = targetRotation;
    }

    private bool IsDoorClosing(float value)
    {
        return value == 0;
    }
    public void Hide()
    {

       // sunet.Play();
        if(_notHiding)
            door.Rotate(0, 45, 0, Space.World);//open door
        
        //hide the camera
        player.SetActive(false);
        mainCamera.SetActive(false);
        hidingCamera.SetActive(true);

        if(_notHiding)
            StartCoroutine(SmoothDoorTransition(0,1.5f)); //close door

        _notHiding = false;
    }


    public void Unhide()
    {   
        player.SetActive(true);
        hidingCamera.SetActive(false);
        mainCamera.SetActive(true);
        _notHiding = true;
       // door.Rotate(0, 45, 0, Space.World);
        StartCoroutine(SmoothDoorTransition(0,1.5f));
        player.GetComponent<PlayerMovement>().SetCabinet(null);

    }
}
