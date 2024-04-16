using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Assets.Scripts.Tasks
{
    [CreateAssetMenu(fileName ="NewTask", menuName = "Create new task")]
    public class Task : ScriptableObject
    {
        [SerializeField]
        public List<Vector3> tasksPointsPositionsList;

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

        [ContextMenu("GenerateTaskPointsPosition")]
        void GenerateTaskPointsPosition()
        {
            for (int i = 0; i < tasksPointsPositionsList.Count; i++)
            {
                tasksPointsPositionsList[i] = new Vector3(Random.Range(-7f, 7f), Random.Range(-3.5f, 3.5f));
            } 
        }
        
    }
}