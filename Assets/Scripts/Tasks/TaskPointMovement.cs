using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Tasks
{
    public class TaskPointMovement : MonoBehaviour
    {
        [SerializeField]
        float maxSpeed;
        [SerializeField]
        float maxForce;

        [Header("Wander")]
        [SerializeField]
        float wanderPower;
        [SerializeField]
        float wanderDistance;
        [SerializeField, Range(0, 1)]
        float wanderWeight;


        [Header("Seek")]
        [SerializeField]
        float seekPower;
        [SerializeField, Range(0, 1)]
        float seekWeight;


        Vector2 _velocity;
        Vector2 _steeringVelocity;
        Vector2 _startPosition;
        


        private void Start()
        {
            _velocity = new Vector2(Random.value, Random.value) * maxSpeed;
            _startPosition = transform.position;
        }

        private void Update()
        {
            _steeringVelocity = Vector2.zero;
            

            ApplyWandering(wanderWeight);
            ApplySeeking(seekWeight);

            _velocity += _steeringVelocity;
            _velocity = Vector2.ClampMagnitude( _velocity, maxSpeed );
            transform.position += (Vector3)_velocity ;
        }

        private void ApplyWandering(float _weight)
        {
            Vector2 _wanderVelocity;
            Vector2 wanderDirection = _velocity.normalized * wanderDistance;
            Vector2 wanderAngle = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            _wanderVelocity = wanderDirection + (wanderAngle * wanderPower);
            
            _steeringVelocity += _wanderVelocity * _weight;
        }

        private void ApplySeeking(float _weight)
        {
            Vector2 _seekVelocity;
            Vector2 direction = _startPosition - (Vector2)transform.position;
            _seekVelocity = direction.normalized * seekPower;
            _steeringVelocity += _seekVelocity * _weight;


        }
    }
}