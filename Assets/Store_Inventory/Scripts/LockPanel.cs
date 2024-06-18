using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LockPanel : MonoBehaviour
{
    private int degrees;
    public static LockPanel instance;
    public GameObject panel;
    public Transform wheal;
    public LockSafe safe;
    public TextMeshProUGUI Textpanel;
    void Start()
    {
        instance = this;
        panel.SetActive(false);
    }

    void RestartPanel()
    {
        wheal.transform.rotation = Quaternion.identity;
        safe.InitializeList();
        degrees = 0;
        Textpanel.text = null;
    }
    void Update()
    {
        switch (degrees)
        {
            case 0:
                Degrees(45,315);
                break;
            case 45:
                Degrees(90,0);
                break;
            case 90:
                Degrees(135,45);
                break;
            case 135:
                Degrees(180,90);
                break;
            case 180:
                Degrees(225,135);
                break;
            case 225:
                Degrees(270,180);
                break;
            case 270:
                Degrees(315,225);
                break;
            case 315:
                Degrees(0,270);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (!CheckPassword())
            {
                RestartPanel();
            }
            else
            {
                Textpanel.text += degrees.ToString();
                Textpanel.text += " ";
            }
            

            if (safe.PasswordEnd())
            {
                panel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                RestartPanel();
                safe.Open();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Degrees(int right, int left)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            wheal.Rotate(0,0,-45f);
            degrees = right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            wheal.Rotate(0,0,45f);
            degrees = left;
        }
    }

    public void SetSafe(LockSafe lockSafe)
    {
        safe = lockSafe;
    }
    private bool CheckPassword()
    {
        return safe.CheckPassword(degrees);
    }

   

    
}
