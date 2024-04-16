using Assets.Scripts.Deals;
using Assets.Scripts.Tasks;
using StoryPoints;
using System;
using UnityEngine;

public class StoryPointInvoker : MonoBehaviour
{

    public StoryPointCondition condition;

    public StoryPoint storyPoint;

    public static event Action<StoryPoint> OnStoryPointReached;

    private  void OnEnable()
    {
        SubscribeOnEvents(GetCondition());
    }

    public StoryPointCondition GetCondition()
    {
        return condition;
    }

    public void SubscribeOnEvents(StoryPointCondition condition)
    {
        switch (condition.conditionEnum)
        {
            case StoryPointConditionEnum.AcceptDeal:
                DealController.instance.OnAcceptDeal += InvokeStoryPoint;
                break;
            case StoryPointConditionEnum.EndDeal:
                DealController.instance.OnCompleteDeal += InvokeStoryPoint;
                break;
            case StoryPointConditionEnum.StartTask:
                TaskController.instance.OnTaskStarted += InvokeStoryPoint;
                break;
            case StoryPointConditionEnum.EndTask:
                TaskController.instance.OnTaskEnded += InvokeStoryPoint;
                break;
            case StoryPointConditionEnum.TimeRunOut:
                Timer.OnTimerRunOut += InvokeTimerStoryPoint;
                break;
            default:
                break;
        }

        
    }

    void InvokeStoryPoint(Task task)
    {
        if (condition.task == task)
            OnStoryPointReached?.Invoke(storyPoint);
    }

    void InvokeStoryPoint(Deal deal)
    {
        if (condition.deal == deal)
            OnStoryPointReached?.Invoke(storyPoint);
    }

    void InvokeTimerStoryPoint()
    {
        OnStoryPointReached?.Invoke(storyPoint);
    }


    [ContextMenu("Test story point activation")]
    public void TestStoryPointActivation()
    {
        OnStoryPointReached.Invoke(storyPoint);
    }

    public interface IInvokig
    {
        void InvokeStoryPoint();
    }
}
