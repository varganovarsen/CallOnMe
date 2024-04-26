using StoryPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;


    [SerializeField]
    float runOutTime;

    [SerializeField]
    float timeKoefficent;
    [SerializeField]
    float startTime;

    private StoryPointInvoker storyPointInvoker;

    [SerializeField]
    StoryPoint[] timeStoryPoints;
    [SerializeField]
    float[] timeStamps;

    static float _currentTime;
    static bool _started;
    public static bool TimeRunOut;

    public static float CurrentTime => _currentTime;
    public static bool Started { get => _started; set => _started = value; }
    public float NextTimeStamp;
    int _currentTimeStampConter = 0;



    private void OnValidate()
    {
        storyPointInvoker = GetComponent<StoryPointInvoker>();
        Array.Resize(ref timeStamps, timeStoryPoints.Length);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _started = true;

        UpdateNextTimeStemp();
        _currentTime += startTime;
    }

    private void UpdateNextTimeStemp() => NextTimeStamp = startTime + timeStamps[_currentTimeStampConter] / timeKoefficent;


    private void Update()
    {
        if (_started)

            _currentTime += Time.deltaTime / timeKoefficent;

        if (CurrentTime > NextTimeStamp && !TimeRunOut)
        {
            StoryPointInvoker.InvokeTimerStoryPoint(timeStoryPoints[_currentTimeStampConter]);
            _currentTimeStampConter++;

            if (timeStamps.Length >= _currentTimeStampConter)
            {
                TimeRunOut = true;
                return;
            }

            UpdateNextTimeStemp();
        }

    }




}
