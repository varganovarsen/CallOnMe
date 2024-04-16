using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TaskGiver : MonoBehaviour
{
    [SerializeField]
    protected Task task;
    const string testTaskPath = "Tasks/testTask";




    bool _interactable = true;
    public bool Interactable { get => _interactable;
        set
        {
            _interactable = value;

            _collider.enabled = value;
        }
    }
    Collider2D _collider;

    protected SpriteRenderer _gfx;

    [SerializeField]
    AnimationCurve _animationCurve;
    private Color _basicColor;

    [SerializeField]
    GameObject changeObjectTo;

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

    private void OnValidate()
    {
        if (!task) {
            task =  Resources.Load(testTaskPath) as Task;
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
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        TaskController.instance.OnTaskStarted += ToggleInteractivity;
        TaskController.instance.OnTaskEnded += ToggleInteractivity;
        LevelLoader.Instance.OnUpwordUnloaded += SetInteractivity;
    }

    private void OnDisable()
    {
        TaskController.instance.OnTaskStarted -= ToggleInteractivity;
        TaskController.instance.OnTaskEnded -= ToggleInteractivity;
        LevelLoader.Instance.OnUpwordUnloaded -= SetInteractivity;
    }

    private void OnMouseEnter()
    {
        OnHoverTaskGiver?.Invoke(task);
    }

    private void OnMouseDown()
    {
        if(Interactable)
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
        if (Interactable)
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

        if (Interactable)
        {
            Color newColor = _gfx.color;
            newColor.a = 0.9f;
            _gfx.color = newColor;
        }
        else
        {
            Color newColor = _gfx.color;
            newColor.a = 1f;
            _gfx.color = newColor;
        }

        Interactable = !Interactable;

    }

    private void SetInteractivity()
    {
        Interactable = true;

        //Color newColor = _gfx.color;
        //newColor.a = 1f;
        //_gfx.color = newColor;
    }

    public virtual void CompleteTaskGiver(Task endedTask)
    {
        if (endedTask == task)
        {
            //TODO: implement animation
            TaskController.instance.OnTaskStarted -= ToggleInteractivity;

            Interactable = false;
            if (changeObjectTo)
            {
                changeObjectTo.SetActive(true);
                gameObject.SetActive(false);
            }
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
        Interactable=false;
        OnNotEnoughMana?.Invoke();
        LeanTween.color(_gfx.gameObject, Color.red, 0.15f).setLoopPingPong(1).setOnComplete(() => Interactable = true);

    }

    private void OnDrawGizmosSelected()
    {
        if (task is not null)
        {

            if (task.TasksPointsPositionsList.Count < 0)
                return;

            foreach (var taskPointPosition in task.TasksPointsPositionsList)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(taskPointPosition, 0.2f);
            }
        }
    }
}

