using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [SerializeField]
    GameObject CameraPrefab;

    public event Action OnUpworldLoaded;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (!Camera.main)
        {
            Instantiate(CameraPrefab);
        }

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void LoadUpworldScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);


    }

    private void OnLevelLoaded(Scene loadedScene, LoadSceneMode loadedSceneMode)
    {

        if (loadedScene.name.ToLower().Contains("upworld"))
        {
            OnUpworldLoaded?.Invoke();


#if UNITY_EDITOR
            int sceneCount = SceneManager.sceneCount;
            bool _hasUnderworld = false;
            for (int i = 0; i < sceneCount; i++)
            {
                _hasUnderworld = SceneManager.GetSceneAt(i).name.ToLower().Contains("underworld");
            }

            if (!_hasUnderworld)
            {
                DealController.instance.DEBUG_ChangeDealState();

            }

#endif
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
}
