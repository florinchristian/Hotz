using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordPanel : MonoBehaviour
{
    public static PasswordPanel instance;
    public GameObject panel;
    private TMP_InputField _inputTextField;
    private PasswordSafe safe;
    void Start()
    {
       instance = this;
        _inputTextField = GetComponent<TMP_InputField>();
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(_inputTextField.text);
            if (CheckPassword())
            {
                panel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                safe.Open();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private bool CheckPassword()
    {
       return safe.CheckPassword(_inputTextField.text);
    }


    public void SetSafe(PasswordSafe passwordSafe)
    {
        safe = passwordSafe;
    }
}
