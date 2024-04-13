using Assets.Scripts.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskGiver : MonoBehaviour 
{
    [SerializeField]
    protected Task task;

    bool _interactable = true;
    protected SpriteRenderer _gfx;

    private void Awake()
    {
        task.Initialize();
        _gfx = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        TaskController.instance.OnTaskStarted += ToggleInteractivity;
        TaskController.instance.OnTaskEnded += ToggleInteractivity;
        LevelLoader.Instance.OnUpwordUloaded += SetInteractivity;
    }

    private void OnDisable()
    {
        TaskController.instance.OnTaskStarted -= ToggleInteractivity;
        TaskController.instance.OnTaskEnded -= ToggleInteractivity;
        LevelLoader.Instance.OnUpwordUloaded -= SetInteractivity;
    }

    private void OnMouseDown()
    {
        if (_interactable)
        {
            OnClick();
        }
    }

    public virtual void OnClick()
    {
        TaskController.instance.StartTask(task);
        TaskController.instance.OnTaskEnded += CompleteTaskGiver;
    }

    protected virtual void ToggleInteractivity(Task task)
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

    private void SetInteractivity()
    {
        _interactable = true;

        Color newColor = _gfx.color;
        newColor.a = 1f;
        _gfx.color = newColor;
    }

    public virtual void CompleteTaskGiver(Task endedTask)
    {
        if(endedTask == task)
        {
            //TODO: implement animation
            _gfx.enabled = false;
            TaskController.instance.OnTaskStarted -= ToggleInteractivity;
        }
    }
}
