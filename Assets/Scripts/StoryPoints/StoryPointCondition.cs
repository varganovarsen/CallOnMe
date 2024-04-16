using Assets.Scripts.Deals;
using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryPoints
{
    [Serializable]
    public class StoryPointCondition
    {
        public StoryPointConditionEnum conditionEnum;

        public Deal deal;
        public Task task;

    }

    public enum StoryPointConditionEnum
    {
        AcceptDeal,
        OfferDeal,
        EndDeal,
        StartTask,
        EndTask,
        TimeRunOut
        
    }
}
