using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerVisualizer : MonoBehaviour
{
    [SerializeField]
    TMP_Text _timerText;

    [SerializeField]
    float timeBeforeDate;

    private void Awake()
    {
    }
    void Update()
    {
        _timerText.text = (timeBeforeDate - Mathf.Ceil(Time.time)).ToString();
    }
}
