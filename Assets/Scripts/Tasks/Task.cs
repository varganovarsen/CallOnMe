using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    [Serializable]
    public class Task
    {
        [SerializeField]
        List<Vector3> tasksPointsPositionsList;

        Queue<Vector3> _taskPointPositions = new Queue<Vector3>();

        public Queue<Vector3> TaskPointPositions => _taskPointPositions;
        
        public void Initialize()
        {
            _taskPointPositions = new Queue<Vector3>(tasksPointsPositionsList);
        }
        
    }
}