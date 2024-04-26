using StoryPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPointController : MonoBehaviour
{

    private void OnEnable()
    {
        StoryPointInvoker.OnStoryPointReached += StartDialogue;
    }

    private void OnDisable()
    {
        StoryPointInvoker.OnStoryPointReached -= StartDialogue;
    }

    private void StartDialogue(StoryPoint storyPoint)
    {
        if (storyPoint.phrases is not null)
        {
            DialogueManager.instance.StartDialogue(storyPoint.phrases);
        }
    }


}
