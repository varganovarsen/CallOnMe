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
        LoadUpworldScene();
    }

    private void Start()
    {
        
    }

    // Debug!!!!
    [ContextMenu("Toggle deal state")]
    void DEBUG_ChangeDealState()
    {
        _isOnDeal = !_isOnDeal;
    }

    //Debug!!!

    public void LoadUpworldScene()
    {
        _isOnDeal = true;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Camera.main.transform.Translate(0, 10, 0);
    }
}
