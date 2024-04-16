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

    bool nextHour;

    private void Awake()
    {
    }

    void Update()
    {
        if (!Timer.Started)
        {
            return;
        }

        float hours = Mathf.Floor((Timer.CurrentTime) / 60f);
        float minutes = Mathf.Ceil(Timer.CurrentTime - (hours * 60)) - 1;
        if(hours >= 25)
        {
            hours -= 25;
        }
        
        string s_hours = hours.ToString();
        string s_minutes = minutes.ToString();

        if(minutes < 10)
            s_minutes = "0" + s_minutes;
        if(hours < 10)
            s_hours = "0" + s_hours;

        _timerText.text = $"{s_hours}:{s_minutes}";



    }

}
