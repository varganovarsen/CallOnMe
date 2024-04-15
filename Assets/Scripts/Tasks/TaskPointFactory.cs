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
            TaskPoint taskPoint = GameObject.Instantiate(taskPointPrefab, task.TaskPointPositions.Peek(), Quaternion.identity).GetComponent<TaskPoint>();

            if (task.TasksPointsPositionsList.Count >0)
                taskPoint.SetSprite(task.SpritesForTaskPoints[Random.Range(0, task.SpritesForTaskPoints.Length)]);
            else
            {
                Debug.Log("There is no sprite on this task giver");
            }
            
            return taskPoint;
        }

        public TaskPointFactory(Task _task, GameObject _taskPointPrefab)
        {
            task = _task;
            taskPointPrefab = _taskPointPrefab;
        }
       
    }
}