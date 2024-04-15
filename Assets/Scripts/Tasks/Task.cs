using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    [Serializable, AllowsNull]
    public class Task
    {
        [SerializeField]
        private List<Vector3> tasksPointsPositionsList;

        [SerializeField]
        public int manaCost;

        [SerializeField]
        private Sprite[] spritesForTaskPoints;
        public Sprite[] SpritesForTaskPoints => spritesForTaskPoints;

        Queue<Vector3> _taskPointPositions = new Queue<Vector3>();

        public Queue<Vector3> TaskPointPositions => _taskPointPositions;

        public List<Vector3> TasksPointsPositionsList { get => tasksPointsPositionsList; }

        public void Initialize()
        {
            _taskPointPositions = new Queue<Vector3>(tasksPointsPositionsList);
        }
        
    }
}