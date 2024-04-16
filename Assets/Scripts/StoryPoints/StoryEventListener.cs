using StoryPoints;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryEventListener : MonoBehaviour
{
    [SerializeField]
    public StoryPoint storyPoint;

    [SerializeField]
    StoryActionType[] listenFor;

    private void OnEnable()
    {
        StoryPointInvoker.OnStoryPointReached += ApplyAction;
    }


    private void ApplyAction(StoryPoint invokedStoryPoint)
    {
        if (invokedStoryPoint != storyPoint)
            return;

        foreach (var action in invokedStoryPoint.actionType)
        {
            if (!listenFor.Contains(action))
            {
                continue;
            }

            switch (action)
            {
                
                case StoryActionType.EnableObject:
                    ChengeObjectState(true);
                    break;
                case StoryActionType.DisableObject:
                    ChengeObjectState(false);
                    break;
            }

        }

        
    }

    void ChengeObjectState(bool setActibeTo)
    {
        gameObject.SetActive(setActibeTo);
    }
}
