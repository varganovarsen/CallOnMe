using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using utils;
using System.Linq;
using Assets.Scripts.Deals;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _upworld;
    [SerializeField]
    CinemachineVirtualCamera _underworld;

    public static bool InUnderworld = true;

    public static float BlendTime;
    static List<float> TransitionDelays = new List<float>();

    public static event Action CameraTransitionStarted;
    public static event Action CameraTransitionEnded;
    public static float TransitionDelay
    {
        set
        {
            float v = Mathf.Clamp(value, 0, Mathf.Infinity);
            TransitionDelays.Add(v);
        }

        get
        {
            if (TransitionDelays.Count > 0)
                return TransitionDelays.Max();
            else
                return 0;
        }
    }
    private void OnEnable()
    {
        LevelLoader.Instance.OnUpworldLoaded += ToggleCameras;
        DealController.instance.OnReturnFromDeal += ToggleCameras;
        CameraTransitionEnded += ToggleInUnderworld;

        BlendTime = GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time;

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(0).parent = null;
        }
    }

    private void OnDisable()
    {
        LevelLoader.Instance.OnUpworldLoaded -= ToggleCameras;
        DealController.instance.OnReturnFromDeal -= ToggleCameras;
        CameraTransitionEnded -= ToggleInUnderworld;
    }

    public void ToggleCameras()
    {
        Utils.Invoke(this, ToggleCameraPriority, TransitionDelay);
    }
    public void ToggleCameras(Deal deal)
    {
        Utils.Invoke(this, ToggleCameraPriority, TransitionDelay);
    }

    private void ToggleCameraPriority()
    {
        CameraTransitionStarted?.Invoke();

        if (DealController.IsOnDeal)
        {
            _underworld.Priority = 0;
            _upworld.Priority = 1;
        }
        else
        {
            _underworld.Priority = 1;
            _upworld.Priority = 0;
        }

        Utils.Invoke(this, () => CameraTransitionEnded?.Invoke(), TransitionDelay + BlendTime);
    }

    private void ToggleInUnderworld() => InUnderworld = !InUnderworld;

    

}
