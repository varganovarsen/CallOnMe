using System.Collections;
using UnityEngine;
using Assets.Scripts.Tasks;

namespace Assets.Scripts.Deals
{


    public class Enemy : TaskGiver
    {
        public override void CompleteTaskGiver(Task endedTask)
        {
            if (endedTask == task)
            {
                //TODO: implement animation
                _gfx.enabled = false;
                TaskController.instance.OnTaskStarted -= ToggleInteractivity;
                DealController.instance.EnemyCount--;
            }
        }

        //protected override void ToggleInteractivity(Task task)
        //{
        //    base.ToggleInteractivity(task);
        //}

    }


}
