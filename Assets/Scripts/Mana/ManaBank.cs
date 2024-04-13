using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManaBank
{
    static int manaCount;

    static public int ManaCount => manaCount;

    static public void AddMana(int amount)
    {
        manaCount += Mathf.Clamp(amount, 0, int.MaxValue);
    }

    static public void RemoveMana(int amount)
    {
        manaCount -= Mathf.Clamp(amount, int.MinValue, 0);
    }
}
