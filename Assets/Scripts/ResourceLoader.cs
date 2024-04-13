using Assets.Scripts.Deals;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ResourceLoader
{

    const string _dealsDirectory = "Deals";


    static public Queue<Deal> LoadDeals()
    {
        Deal[] _deals = Resources.LoadAll<Deal>(_dealsDirectory);
        Queue<Deal> _dealQueue = new Queue<Deal>(_deals);

        string message = "Following deals are loaded:\n";
        foreach (Deal deal in _dealQueue) { message += "\n" + deal.name; }
        Debug.Log(message);

        return _dealQueue;
    }
}
