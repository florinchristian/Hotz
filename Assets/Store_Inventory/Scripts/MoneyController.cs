using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI MoneyText;
    public static MoneyController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        value = 100;
    }

    public bool BuyItem(int value)
    {
        bool ok = false;
        if (this.value >= value)
        {
            this.value -= value;
            MoneyText.text = $"{this.value}";
            Debug.Log("Item bought successfully");
            ok = true;
        }

        return ok;
    }

    public void CollectMoney(int itemCount)
    {   
        value += itemCount;
        MoneyText.text = $"{this.value}";
    }
}
