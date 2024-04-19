using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour 
{
    public static Timer Instance;

    public static event Action OnTimerRunOut;

    [SerializeField]
    float runOutTime;

    [SerializeField]
    float timeKoefficent;
    [SerializeField]
    float startTime;

    static float _currentTime;
    static bool _started;
    public static bool TimeRunOut;

    public static float CurrentTime  => _currentTime;
    public static bool Started { get => _started; set => _started = value; }
    public static float RunOutTime;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _started = true;
        RunOutTime = runOutTime + startTime;
        _currentTime += startTime;
    }

    private void Update()
    {
        if (_started)

        _currentTime += Time.deltaTime / timeKoefficent;

        if (CurrentTime > RunOutTime && !TimeRunOut)
        {
            OnTimerRunOut.Invoke();
            TimeRunOut = true;
        }

    }



}
