using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class AttackPoint : TaskPoint
    {
        [SerializeField]
        int manaDamage = 1;

        private void OnEnable()
        {
            OnWrongTaskPointClick += ApplyManaDamage;
        }

        private void OnDisable()
        {
            OnWrongTaskPointClick -= ApplyManaDamage;
        }

        void ApplyManaDamage()
        {
            ManaBank.RemoveMana(manaDamage);
        }
    }
}