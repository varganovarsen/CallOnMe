using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Deals;
namespace Assets.Scripts.Tasks
{
    public class TaskController : MonoBehaviour
    {
        public static TaskController instance;

        [SerializeField]
        GameObject taskPointPrefab;
        [SerializeField]
        GameObject attackPointPrefab;

        TaskPointFactory taskPointFactory;
        TaskPoint currentPoint;
        Task currentTask;

        public event Action<Task> OnTaskEnded;
        public event Action<Task> OnTaskStarted;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            } else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            DealController.instance.OnAcceptDeal += InterruptTask;
        }


        public void StartTask(Task taskToStart)
        {
            currentTask = taskToStart;
            taskPointFactory = new TaskPointFactory(currentTask, DealController.IsOnDeal ? attackPointPrefab : taskPointPrefab);
            currentPoint = taskPointFactory.SpawnTaskPoint();
            currentPoint.OnDestroyed += OnTaskPointDesroyed;

            OnTaskStarted.Invoke(currentTask);
        }

        public void OnTaskPointDesroyed()
        {
            currentPoint.OnDestroyed -= OnTaskPointDesroyed;
            currentTask.TaskPointPositions.Dequeue();

            if(currentTask.TaskPointPositions.Count > 0)
            {
                currentPoint = taskPointFactory.SpawnTaskPoint();
                currentPoint.OnDestroyed += OnTaskPointDesroyed;
            } else
            {
                EndTask();
            }
        }

        void InterruptTask(Deal _deal)
        {
            if (currentTask == null)
                return;

            currentTask = null;

            if (currentPoint == null)
                return;

            currentPoint.OnDestroyed -= OnTaskPointDesroyed;
            Destroy(currentPoint.gameObject);
            currentPoint = null;
        }

        public void EndTask()
        {
            OnTaskEnded.Invoke(currentTask);
            currentTask = null;
            currentPoint = null;

        }
    }
}