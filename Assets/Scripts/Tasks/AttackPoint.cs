using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class AttackPoint : TaskPoint
    {
        public override event Action OnDestroyed;
        public override void OnMouseDown()
        {
            OnClick();
        }

        public override void OnClick()
        {
            OnDestroyed.Invoke();
            Destroy(gameObject);
        }


    }
}