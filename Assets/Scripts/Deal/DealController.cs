using Assets.Scripts.Deals;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DealController : MonoBehaviour
{


    public static DealController instance;

    static bool _isOnDeal = false;


    public Queue<Deal> _dealQueue = new Queue<Deal>();

    Deal _currentDeal;
    static public bool IsOnDeal => _isOnDeal;


    public event Action<Deal> OnOfferDeal;
    public event Action<Deal> OnAcceptDeal;
    public event Action OnCompleteDeal;

    int enemyCount = 0;
    public int EnemyCount
    {
        get => enemyCount;

        set { 
            
            if (value <= 0)
            {
                CompleteDeal();
                return;
            }
            enemyCount = value;

        }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }    


        _dealQueue = ResourceLoader.LoadDeals();
        
    }

    private void OnEnable()
    {
        LevelLoader.Instance.OnUpworldLoaded += PrepareDealGoal;
    }

    private void OnDisable()
    {
        LevelLoader.Instance.OnUpworldLoaded -= PrepareDealGoal;
    }




    public void OfferDeal()
    {
        Deal _dealToOffer;
        _dealQueue.TryPeek(out _dealToOffer);

        _currentDeal = _dealToOffer;
        if (_dealToOffer != null)
            OnOfferDeal?.Invoke(_dealToOffer);
    }

    public void OverdueDeal()
    {
        _currentDeal = null;


    }

    public void AcceptDeal()
    {
        _isOnDeal = true;
        OnAcceptDeal?.Invoke(_currentDeal);
        LevelLoader.Instance.LoadUpworldScene();
    }

    void PrepareDealGoal()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        enemyCount = enemies.Length;
    }


    public void CompleteDeal()
    {
        _isOnDeal = false;
        _currentDeal = null;
       Deal _completedDeal =  _dealQueue?.Dequeue();
        OnCompleteDeal.Invoke();

        LevelLoader.Instance.UnloadUpworldScene(_completedDeal.sceneReference.SceneName);

    }

    private void Start()
    {
        Invoke(nameof(OfferDeal), 3f);
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
