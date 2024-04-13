using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class TaskController : MonoBehaviour
    {
        public static TaskController instance;

        [SerializeField]
        GameObject taskPointPrefab;

        TaskPointFactory taskPointFactory;
        TaskPoint currentPoint;
        Task currentTask;

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
            taskPointFactory = new TaskPointFactory(currentTask, taskPointPrefab);
            currentPoint = taskPointFactory.SpawnTaskPoint();
            currentPoint.OnDestroyed += OnTaskPointDesroyed;

        }

        public void OnTaskPointDesroyed()
        {
            currentPoint.OnDestroyed -= OnTaskPointDesroyed;

            if(currentTask.TaskPointPositions.Count > 0)
            {
                currentPoint = taskPointFactory.SpawnTaskPoint();
                currentPoint.OnDestroyed += OnTaskPointDesroyed;
            }
        }
    }
}