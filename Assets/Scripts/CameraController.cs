using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _upworld;
    [SerializeField]
    CinemachineVirtualCamera _underworld;

    public static float BlendTime;
    private void OnEnable()
    {
        LevelLoader.Instance.OnUpworldLoaded += ToggleCameras;
        DealController.instance.OnCompleteDeal += ToggleCameras;

        BlendTime = GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time;

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(0).parent = null;
        }
    }

    public void ToggleCameras()
    {
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
    }

    private void OnDisable()
    {
        LevelLoader.Instance.OnUpworldLoaded -= ToggleCameras;
    }

}
