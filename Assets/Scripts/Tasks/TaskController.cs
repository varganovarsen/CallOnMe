using System;
using System.Collections;
using UnityEngine;

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

            if(currentTask.TaskPointPositions.Count > 0)
            {
                currentPoint = taskPointFactory.SpawnTaskPoint();
                currentPoint.OnDestroyed += OnTaskPointDesroyed;
            } else
            {
                EndTask();
            }
        }

        public void EndTask()
        {
            OnTaskEnded.Invoke(currentTask);
            currentTask = null;
            currentPoint = null;

        }
    }
}