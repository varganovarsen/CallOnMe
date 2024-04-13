using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DealController : MonoBehaviour
{
    public static DealController instance;

    static bool _isOnDeal = false;



    static public bool IsOnDeal => _isOnDeal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

   public void AcceptDeal()
    {
        _isOnDeal = true;
        LevelLoader.Instance.LoadUpworldScene();
    }

    private void Start()
    {
        
    }

    // Debug!!!!
    [ContextMenu("Toggle deal state")]
    public void DEBUG_ChangeDealState()
    {
        _isOnDeal = !_isOnDeal;
        FindObjectOfType<CameraController>().ToggleCameras();
    }

    //Debug!!!

    
}
