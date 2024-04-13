using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSubscriber : MonoBehaviour
{
    [SerializeField]
    Button AcceptCallButton;


    private void Start()
    {
        AcceptCallButton.onClick.AddListener(DealController.instance.AcceptDeal);
    }
}
