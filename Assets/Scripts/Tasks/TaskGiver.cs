using Assets.Scripts.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskGiver : MonoBehaviour 
{
    [SerializeField]
    Task task;

    bool _interactable = true;
    SpriteRenderer _gfx;

    private void Awake()
    {
        task.Initialize();
        _gfx = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        TaskController.instance.OnTaskStarted += ToggleInteractivity;
        TaskController.instance.OnTaskEnded += ToggleInteractivity;
    }

    private void OnMouseDown()
    {
        if (_interactable)
        {
            OnClick();
        }
    }

    void OnClick()
    {
        TaskController.instance.StartTask(task);
        TaskController.instance.OnTaskEnded += CompleteTaskGiver;
    }

    void ToggleInteractivity(Task task)
    {

        if (_interactable)
        {
            Color newColor = _gfx.color;
            newColor.a = 0.5f;
            _gfx.color = newColor;
        } else
        {
            Color newColor = _gfx.color;
            newColor.a = 1f;
            _gfx.color = newColor;
        }

        _interactable = !_interactable;
        

    }

    void CompleteTaskGiver(Task endedTask)
    {
        if(endedTask == task)
        {
            //TODO: implement animation
            _gfx.enabled = false;
            TaskController.instance.OnTaskStarted -= ToggleInteractivity;
        }
    }
}
