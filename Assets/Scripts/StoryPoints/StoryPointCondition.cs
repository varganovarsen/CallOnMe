using Assets.Scripts.Deals;
using Assets.Scripts.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alchemy;
using Alchemy.Inspector;

namespace StoryPoints
{
    [Serializable]
    public class StoryPointCondition
    {
        public StoryPointConditionEnum conditionEnum;

        bool _needsDealReference() => conditionEnum == StoryPointConditionEnum.EndDeal || conditionEnum == StoryPointConditionEnum.AcceptDeal || conditionEnum == StoryPointConditionEnum.OfferDeal;
        [ShowIf("_needsDealReference")]
        public Deal deal;

        public bool _needsTaskReference() => conditionEnum == StoryPointConditionEnum.EndTask || conditionEnum == StoryPointConditionEnum.StartTask;
        [ShowIf("_needsTaskReference")]
        public Task task;

        bool _needsTimeReference() => conditionEnum == StoryPointConditionEnum.SpecificTime;
        [ShowIf("_needsTimeReference")]
        public float invokeTime;

        bool _needsDialogueReference() => conditionEnum == StoryPointConditionEnum.DialogueComplete;
        [ShowIf("_needsDialogueReference")]
        public PhraseList dialogue;

    }

    public enum StoryPointConditionEnum
    {
        AcceptDeal,
        OfferDeal,
        EndDeal,
        StartTask,
        EndTask,
        TimeRunOut,
        SpecificTime,
        DialogueComplete

    }
}
