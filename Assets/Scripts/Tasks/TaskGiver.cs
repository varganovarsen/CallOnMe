using Assets.Scripts.Tasks;
using System;
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

    [SerializeField]
    AnimationCurve _animationCurve;
    private Color _basicColor;

    [SerializeField]
    Color buyForManaColor;

    private bool _holding;
    private float _currentHoldTime;
    [SerializeField]
    private float maxHoldTime;

    public event Action OnNotEnoughMana;

    public static event Action<Task> OnHoverTaskGiver;
    public static event Action OnExitHoverTaskGiver;

    float holdResetSpeedModifier
    {
        get
        {
            if (_holding)
                return 1f;
            else return 5f;
        }
    }

    private void Awake()
    {
        task.Initialize();
        _gfx = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _basicColor = _gfx.color;
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

    private void OnMouseEnter()
    {
        OnHoverTaskGiver?.Invoke(task);
    }

    private void OnMouseDown()
    {
        if(_interactable)
        {

            if (Input.GetMouseButton(0) && !_holding)
            {
                StartTask();
                return;
            }
        }
    }

    protected virtual void OnMouseOver()
    {
        if (_interactable)
        {

            if (Input.GetMouseButton(1))
            {
                if(ManaBank.ManaCount < task.manaCost)
                {
                    _holding = false;
                    NotEnoughManaAnimation();
                    return;
                }
                    

                _holding = true;
            }
            else
            {
                _holding = false;
            }

        }

        if (!_holding)
            return;

        OnHold();
    }
    private void OnMouseExit()
    {
        OnExitHoverTaskGiver.Invoke();
         _holding = false;
        
    }


    private void Update()
    {
        if (!_holding && _currentHoldTime > 0)
        {
            _currentHoldTime -= Time.deltaTime * holdResetSpeedModifier;
            _currentHoldTime = Mathf.Clamp(_currentHoldTime, 0f, maxHoldTime);
            UpdateComplitionVisuals();
        }
        else if (_holding)
        {
            UpdateComplitionVisuals();
        }
    }

    private void SolveTaskForMana()
    {
        ManaBank.RemoveMana(task.manaCost);
        CompleteTaskGiver(task);
    }

    void OnHold()
    {
        _currentHoldTime += Time.deltaTime;
        _currentHoldTime = Mathf.Clamp(_currentHoldTime, 0f, maxHoldTime);



        if (_currentHoldTime >= maxHoldTime)
        {
            _holding = false;
            _currentHoldTime = 0;
            SolveTaskForMana();
        }
    }

    public virtual void StartTask()
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
        }
        else
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
        if (endedTask == task)
        {
            //TODO: implement animation
            TaskController.instance.OnTaskStarted -= ToggleInteractivity;

            Destroy(gameObject);
        }
    }

    public void UpdateComplitionVisuals()
    {
        Color newColor;
        newColor = Color.Lerp(_basicColor, buyForManaColor, _animationCurve.Evaluate(_currentHoldTime / maxHoldTime));
        _gfx.color = newColor;
    }

    public void NotEnoughManaAnimation()
    {
        _interactable=false;
        OnNotEnoughMana?.Invoke();
        LeanTween.color(_gfx.gameObject, Color.red, 0.15f).setLoopPingPong(1).setOnComplete(() => _interactable = true);

    }

    private void OnDrawGizmosSelected()
    {
        if (task is not null)
        {
            foreach (var taskPointPosition in task.TasksPointsPositionsList)
            {
                Gizmos.DrawWireSphere(taskPointPosition, 0.2f);
            }
        }
    }
}
