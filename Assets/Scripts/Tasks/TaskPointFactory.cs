using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class TaskPointFactory
    {
        Task task;

        GameObject taskPointPrefab;

        public TaskPoint SpawnTaskPoint()
        {
            return GameObject.Instantiate(taskPointPrefab, task.TaskPointPositions.Peek(), Quaternion.identity).GetComponent<TaskPoint>();
        }

        public TaskPointFactory(Task _task, GameObject _taskPointPrefab)
        {
            task = _task;
            taskPointPrefab = _taskPointPrefab;
        }
       
    }
}