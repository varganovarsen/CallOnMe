using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using utils;
using TMPro;

public class TaskPoint : MonoBehaviour
{
    public virtual event Action OnDestroyed;
 
    bool _holding;

    [SerializeField]
    float maxHoldTime = 1.3f;
    float _currentHoldTime = 0f;

    [SerializeField]
    AnimationCurve _curve;


    GameObject _gfx;
    

    [SerializeField]
    ButtonToDestroy _buttonToDestroy;

    TMP_Text _buttonToDestroyText;
    enum ButtonToDestroy
    {
        Left = 0,
        Right = 1
    }


    private void Awake()
    {
        _gfx = GetComponentInChildren<SpriteRenderer>().gameObject;
        _buttonToDestroy = Utils.GetRandomEnum<ButtonToDestroy>();
        _buttonToDestroyText = GetComponentInChildren<TMP_Text>();
        _buttonToDestroyText.text = _buttonToDestroy.ToString().ToCharArray()[0].ToString();
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(((int)_buttonToDestroy)))
        {
            _holding = true;
            OnHold();

        }


    }

    private void OnMouseExit()
    {
        _holding = false;
    }

    private void OnMouseUp()
    {
        _holding = false;
    }

    public virtual void OnClick()
    {
        OnDestroyed.Invoke();
        Destroy(gameObject, 0.1f);

    }

    void OnHold()
    {
        _currentHoldTime += Time.deltaTime;
        _currentHoldTime = Mathf.Clamp(_currentHoldTime, 0f, maxHoldTime);



        if (_currentHoldTime >= maxHoldTime)
        {
            _holding = false;
            _currentHoldTime = 0;
            OnDestroyed.Invoke();
            Destroy(gameObject );
        }
    }


    void UpdateSize()
    {
        Vector2 newSize;
        newSize.x = 1 -  _curve.Evaluate(_currentHoldTime / maxHoldTime);
        newSize.y = 1 - _curve.Evaluate(_currentHoldTime / maxHoldTime);
        _gfx.transform.localScale = newSize;
    }

}
