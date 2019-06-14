using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFeedback : MonoBehaviour
{
    Text feedbackText;
    string purchaseInfo;

    public string PurchaseInfo { set { purchaseInfo = value; }}

    void Start()
    {
        feedbackText = GetComponent<Text>();
    }

    void Update()
    {
        feedbackText.text = purchaseInfo;
    }    
}
