using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBank : MonoBehaviour
{
    public static ManaBank instance;

    static int manaCount;
    public event Action<int> OnManaChanged;

    static public int ManaCount => manaCount;

    [SerializeField]
    int startMana;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        manaCount = startMana;
    }

    static public void AddMana(int amount)
    {
            manaCount += amount;
            instance.OnManaChanged?.Invoke(amount);
        

    }

    static public void RemoveMana(int amount)
    {

            manaCount -= amount;
            instance.OnManaChanged?.Invoke(amount);
        
    }
}
