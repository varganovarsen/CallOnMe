using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using utils;
using TMPro;
using UnityEngine.UI;

public class TaskPoint : MonoBehaviour
{
    public virtual event Action OnDestroyed;

    bool _holding;

    [SerializeField]
    float maxHoldTime = 1.3f;
    float _currentHoldTime = 0f;

    [SerializeField]
    AnimationCurve _curve;


    SpriteRenderer _gfx;

    public event Action OnWrongTaskPointClick;


    [SerializeField]
    ButtonToDestroy _buttonToDestroy;

    [SerializeField]
    Sprite[] buttonToDestroySprites;

    Image _buttonToDestroyImage;
    enum ButtonToDestroy
    {
        Left = 0,
        Right = 1
    }


    private void Awake()
    {
        _gfx = GetComponentInChildren<SpriteRenderer>();
        _buttonToDestroy = Utils.GetRandomEnum<ButtonToDestroy>();
        _buttonToDestroyImage = GetComponentInChildren<Image>();
        _buttonToDestroyImage.sprite = buttonToDestroySprites[(int)_buttonToDestroy];
    }

    public void SetSprite(Sprite sprite)
    {
        _gfx.sprite = sprite;
    }


    private void Update()
    {
        if (!_holding && _currentHoldTime > 0)
        {
            _currentHoldTime -= Time.deltaTime;
            _currentHoldTime = Mathf.Clamp(_currentHoldTime, 0f, maxHoldTime);
            UpdateSize();
        }
        else if (_holding)
        {
            UpdateSize();
        }
    }

    protected virtual void OnMouseOver()
    {

        switch (_buttonToDestroy)
        {
            case ButtonToDestroy.Left:
                if (Input.GetMouseButtonDown((int)ButtonToDestroy.Right))
                    OnWrongTaskPointClick?.Invoke();
                if (Input.GetMouseButton((int)ButtonToDestroy.Right)) return;
                break;
            case ButtonToDestroy.Right:
                if (Input.GetMouseButtonDown((int)ButtonToDestroy.Left))
                    OnWrongTaskPointClick?.Invoke();
                if (Input.GetMouseButton((int)ButtonToDestroy.Left)) return;
                break;
        }

        if(!Input.GetMouseButton((int)_buttonToDestroy)) return;


        _holding = true;
            OnHold();

    }

    private void OnMouseExit()
    {
        _holding = false;
    }

    private void OnMouseUp()
    {
        _holding = false;
    }

    //public virtual void OnClick()
    //{
    //    OnDestroyed.Invoke();
    //    Destroy(gameObject, 0.1f);

    //}

    void OnHold()
    {
        _currentHoldTime += Time.deltaTime;
        _currentHoldTime = Mathf.Clamp(_currentHoldTime, 0f, maxHoldTime);



        if (_currentHoldTime >= maxHoldTime)
        {
            _holding = false;
            _currentHoldTime = 0;
            OnDestroyed.Invoke();
            Destroy(gameObject);
        }
    }


    void UpdateSize()
    {
        Vector2 newSize;
        newSize.x = 1 - _curve.Evaluate(_currentHoldTime / maxHoldTime);
        newSize.y = 1 - _curve.Evaluate(_currentHoldTime / maxHoldTime);
        _gfx.transform.localScale = newSize;
    }

}
