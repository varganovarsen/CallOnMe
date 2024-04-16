using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryPoints
{
    [CreateAssetMenu(fileName = "New story point", menuName = "Create story point")]
    public class StoryPoint : ScriptableObject
    {

        //public StoryPointCondition condition;

        [SerializeField]
        public PhraseList phrases;

        [SerializeField]
        public StoryActionType[] actionType;

    }

    public enum StoryActionType
    {
        StartDialogue,
        EnableObject,
        DisableObject
    }

}

