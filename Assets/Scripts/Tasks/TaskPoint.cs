using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskPoint : MonoBehaviour
{
    public event Action OnDestroyed;
    [SerializeField]
    int manaCost;

    bool _holding;

    [SerializeField]
    float maxHoldTime = 1.3f;
    float _currentHoldTime = 0f;


    GameObject _gfx;


    private void Awake()
    {
        _gfx = GetComponentInChildren<SpriteRenderer>().gameObject;
    }


    private void OnMouseDown()
    {
        if (ManaBank.ManaCount > 0)
            OnClick();
        else
            _holding = true;

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
        if (!_holding)
            return;

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

    void OnClick()
    {

        ManaBank.RemoveMana(manaCost);
        OnDestroyed.Invoke();
        Destroy(gameObject);

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
        newSize.x = Mathf.SmoothStep(1f, 0, Mathf.Pow((_currentHoldTime / maxHoldTime), 2.3f));
        newSize.y = Mathf.SmoothStep(1f, 0, Mathf.Pow((_currentHoldTime / maxHoldTime), 2.3f));
        _gfx.transform.localScale = newSize;
    }

}
