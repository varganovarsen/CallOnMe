using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class TaskPointFactory
    {
        Task task;

        GameObject taskPointPrefab;

       public Queue<Vector3> TaskPointPositions = new Queue<Vector3>();

        public TaskPoint SpawnTaskPoint()
        {
            TaskPoint taskPoint = GameObject.Instantiate(taskPointPrefab, TaskPointPositions.Peek(), Quaternion.identity).GetComponent<TaskPoint>();

            if (task.SpritesForTaskPoints.Length > 0 && task.TasksPointsPositionsList.Count >0)
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

            TaskPointPositions = new Queue<Vector3>(task.tasksPointsPositionsList);
        }
       
    }
}