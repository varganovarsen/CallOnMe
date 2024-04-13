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

    private void OnMouseDown()
    {
        OnClick();
    }

    void OnClick()
    {

        ManaBank.RemoveMana(manaCost);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        OnDestroyed.Invoke();
    }
}
