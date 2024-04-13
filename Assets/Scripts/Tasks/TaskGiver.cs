using Assets.Scripts.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskGiver : MonoBehaviour 
{
    [SerializeField]
    Task task;

    private void Awake()
    {
        task.Initialize();
    }

    private void OnMouseDown()
    {
        OnClick();
    }

    void OnClick()
    {
        TaskController.instance.StartTask(task);
    }
}
