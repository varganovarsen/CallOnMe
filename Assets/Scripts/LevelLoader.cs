using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using utils;
using Invoke = utils.Utils;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [SerializeField]
    GameObject CameraPrefab;

    public event Action OnUpworldLoaded;
    public event Action OnUpwordUnloaded;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void LoadUpworldScene(string _sceneToLoad)
    {
        SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Additive);

    }

    public void UnloadUpworldScene(string sceneToUnload)
    {
        Utils.Invoke(this, () => SceneManager.UnloadSceneAsync(sceneToUnload), CameraController.BlendTime);
        Utils.Invoke(this, () => OnUpwordUnloaded.Invoke(), CameraController.BlendTime);
    }


    private void OnLevelLoaded(Scene loadedScene, LoadSceneMode loadedSceneMode)
    {
        if (!Camera.main)
        {
            Instantiate(CameraPrefab);
        }


        if (loadedScene.name.ToLower().Contains("upworld"))
        {
            OnUpworldLoaded?.Invoke();


#if UNITY_EDITOR
            int sceneCount = SceneManager.sceneCount;
            bool _hasUnderworld = false;
            for (int i = 0; i < sceneCount; i++)
            {
                _hasUnderworld = SceneManager.GetSceneAt(i).name.ToLower().Contains("underworld");
                if (_hasUnderworld)
                    return;
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
